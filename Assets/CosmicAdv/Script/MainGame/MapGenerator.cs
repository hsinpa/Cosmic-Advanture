using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CA_Terrain;
using Utility;

public class MapGenerator : MonoBehaviour {
    public float slopeRate;
    public int maxTerrainCapacity = 8;
    public static Vector2 gridSize;

    public List<GameObject> terrainPrefab = new List<GameObject>();

    private int perlin_offsetX, perlin_offsetY, _line_index = 0;
    private int offsetX;
    private Queue<TerrainBuilder> _terrainsHolder = new Queue<TerrainBuilder>();
    private GameObject _terainholder;
    private ThemeGenerator _themeGenerator;

    [SerializeField]
    private List<CA_Grid[]> gridMap = new List<CA_Grid[]>();

    public void SetUp(GameObject p_terrain_holder) {
        _terainholder = p_terrain_holder;
        _themeGenerator = GetComponent<ThemeGenerator>();

        perlin_offsetX = Random.Range(0, 20000);
        perlin_offsetY = Random.Range(0, 20000);
        CalculateGridSize();

        _themeGenerator.GeneratePoolingObject();
        //PreparePooling();
        PrebuildMap();
    }

    private void CalculateGridSize() {
        if (terrainPrefab.Count > 0) {
            TerrainBuilder terrainBuilder = terrainPrefab[0].GetComponent<TerrainBuilder>();
            MeshRenderer terrainMesh = terrainBuilder.transform.Find("Terrains").GetChild(0).GetComponent<MeshRenderer>();
            gridSize = new Vector2(terrainMesh.bounds.size.x, terrainMesh.bounds.size.z);
            //offsetX = Mathf.RoundToInt(terrainBuilder.index_offset - (terrainBuilder.activate_size * 0.5f) );
            offsetX = terrainBuilder.index_offset;
        }
    }

    public TerrainBuilder AssignSRandomTerrain() {
        float noiseValue = Mathf.PerlinNoise(_line_index * slopeRate + perlin_offsetX, perlin_offsetY);
        TerrainBuilder newTerrain = IdentifyTerrainPiece(noiseValue);

        //If terrainholder reach its maximum capacity
        if (_terrainsHolder.Count > maxTerrainCapacity) {
            TerrainBuilder eraseTerrain = _terrainsHolder.Dequeue();
            eraseTerrain.OnTerrainDestroy();
            gridMap.RemoveAt(0);
            PoolManager.instance.Destroy(eraseTerrain.gameObject);
        }
        
        return newTerrain;
    }

    private void PrebuildMap() {
        //prebuild the first three tiles as plain
        int prebuildNum = 5;
        for (int p = 0; p < prebuildNum; p++) {
            InstantiateTerrain(PoolingID.TerrainPlain);
        }

        for (int i = 0; i < 14 - prebuildNum; i++) {
            AssignSRandomTerrain();
        }
    }

    private TerrainBuilder IdentifyTerrainPiece(float p_noiseValue) {
        //Hard code now
        // Debug.Log("Noise Value " + p_noiseValue);

        //Plain
        if (p_noiseValue > 0.35f && p_noiseValue < 0.6f) {
            return InstantiateTerrain(PoolingID.TerrainPlain);
        }

        //Terrain
        if (p_noiseValue >= 0.6f) {
            return InstantiateTerrain(PoolingID.TerrainRoad);
        }

        //Train
        if (p_noiseValue <= 0.35f) {
            return InstantiateTerrain(PoolingID.TerrainTrain);
        }

        return null;
    }

    private TerrainBuilder InstantiateTerrain(int object_id) {
        GameObject createdObject = PoolManager.instance.ReuseObject(object_id);

        createdObject.SetActive(true);
        createdObject.transform.SetParent(_terainholder.transform);
        createdObject.transform.localPosition = new Vector3(0, 0, _line_index);
        createdObject.SetActive(true);

        TerrainBuilder terrainBuilder = createdObject.GetComponent<TerrainBuilder>();
        terrainBuilder.SetUp();
        terrainBuilder.GenerateObstacle();

        _terrainsHolder.Enqueue(terrainBuilder);
        gridMap.Add(terrainBuilder.grids);


        _line_index++;
        return createdObject.GetComponent<TerrainBuilder>();
    }

    #region Grid Panel
    private CA_Grid FindGridByWorldPosition(Vector3 p_position) {
        //Find Y
        int Y =  Mathf.RoundToInt(gridMap.Count - (_line_index - p_position.z));

        //Find X
        int X = Mathf.RoundToInt(p_position.x);

        if (X < 0 || Y < 0 || Y >= gridMap.Count || X >= gridMap[Y].Length)
            //Return unwalkable if pos not even exist in array
            return new CA_Grid(Vector2.zero, false);
        else {
            return gridMap[Y][X];
        }
    }

    public bool IsPosAvailable(Vector3 p_position, Vector3 p_direction) {
        Vector3 aheadPosition = p_position + p_direction;
        CA_Grid caGrid = FindGridByWorldPosition(aheadPosition);
        return caGrid.isWalkable;
    }

	#endregion
}
