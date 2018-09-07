using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CA_Terrain;
using Utility;

public class MapGenerator : MonoBehaviour {
    public float slopeRate;
    public int maxTerrainCapacity = 20;
    public static Vector2 gridSize;

    public List<GameObject> terrainPrefab = new List<GameObject>();
    public List<Obstacle_STP> _obstacleHolder = new List<Obstacle_STP>();
    public List<AnimatedObject_STP> _animatedHolder = new List<AnimatedObject_STP>();

    private int perlin_offsetX, perlin_offsetY, _line_index = 0;
    private int offsetX;
    private Queue<TerrainBuilder> _terrainsHolder = new Queue<TerrainBuilder>();
    private GameObject _terainholder;

    [SerializeField]
    private List<CA_Grid[]> gridMap = new List<CA_Grid[]>();

    public void SetUp(GameObject p_terrain_holder) {
        _terainholder = p_terrain_holder;

        perlin_offsetX = Random.Range(0, 20000);
        perlin_offsetY = Random.Range(0, 20000);
        CalculateGridSize();

        PreparePooling();
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

    public void AssignSRandomTerrain() {
        float noiseValue = Mathf.PerlinNoise(_line_index * slopeRate + perlin_offsetX, perlin_offsetY);
        IdentifyTerrainPiece(noiseValue);

        //If terrainholder reach its maximum capacity
        if (_terrainsHolder.Count > maxTerrainCapacity) {
            TerrainBuilder eraseTerrain = _terrainsHolder.Dequeue();
            eraseTerrain.OnTerrainDestroy();
            gridMap.RemoveAt(0);
            PoolManager.instance.Destroy(eraseTerrain.gameObject);
        }
    }


    private void PreparePooling() {
        //All type of terrain;
        int terrainSize = 15;
        foreach (GameObject t_prefab in terrainPrefab) {
            TerrainBuilder tBuilder = t_prefab.GetComponent<TerrainBuilder>();
            PoolManager.instance.CreatePool(t_prefab, tBuilder.terrain_stp._id, terrainSize);
        }

        int obstacleSize = 20;
        //All type of Obstacle;
        foreach (Obstacle_STP t_object in _obstacleHolder) {
            PoolManager.instance.CreatePool(t_object.ObstaclePrefab, t_object._id, obstacleSize);
        }

        //All type of Coin;
        int animatedSize = 15;
        foreach (AnimatedObject_STP t_object in _animatedHolder) {
            PoolManager.instance.CreatePool(t_object.ObstaclePrefab, t_object._id, obstacleSize);
        }

    }

    private void PrebuildMap() {
        //prebuild the first three tiles as plain
        int prebuildNum = 3;
        for (int p = 0; p < prebuildNum; p++) {
            InstantiateTerrain(PoolingID.TerrainPlain);
        }

        for (int i = 0; i < 14 - prebuildNum; i++) {
            AssignSRandomTerrain();
        }
    }

    private void IdentifyTerrainPiece(float p_noiseValue) {
        //Hard code now
        // Debug.Log("Noise Value " + p_noiseValue);

        //Plain
        if (p_noiseValue > 0.45f && p_noiseValue < 0.5f) {
            InstantiateTerrain(PoolingID.TerrainPlain);
            return;
        }

        //Terrain
        if (p_noiseValue >= 0.5f) {
            InstantiateTerrain(PoolingID.TerrainRoad);
        }

        //Train
        if (p_noiseValue <= 0.45f) {
            InstantiateTerrain(PoolingID.TerrainTrain);
        }
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
        // Debug.Log("p_position.z " + p_position.z );
        // Debug.Log("_line_index " + (_line_index - p_position.z) );

        // float percentage = ((float)gridMap.Count /  _line_index);
        // Debug.Log("Y " +  (gridMap.Count - (_line_index - p_position.z)));


        //Find X
        int X = Mathf.RoundToInt(p_position.x);

        if (X < 0 || Y < 0 || Y >= gridMap.Count || X >= gridMap[Y].Length)
            //Return unwalkable if pos not even exist in array
            return new CA_Grid(Vector2.zero, false);
        else {
            //Debug.Log("Grid Pos " + gridMap[Y][X].position);
            //Debug.Log("Grid Index Y:" + Y + ", X:" + X);

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
