using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CA_Terrain;
using Utility;

public class MapGenerator : MonoBehaviour {
	public float slopeRate;
	public int maxTerrainCapacity = 15;

	public List<GameObject> terrainPrefab = new List<GameObject>();
	public List<Obstacle_STP> _obstacleHolder = new List<Obstacle_STP>();

	private int _offsetX, _offsetY, _line_index = 0;
	private Queue<TerrainBuilder> _terrainsHolder = new Queue<TerrainBuilder>();
	private GameObject _terainholder;

	public void SetUp(GameObject p_terrain_holder ) {
		_terainholder = p_terrain_holder;

		_offsetX = Random.Range(0, 20000);
		_offsetY = Random.Range(0, 20000);

		PreparePooling();
		PrebuildMap();
	}

	public void AssignSRandomTerrain() {
			float noiseValue =  Mathf.PerlinNoise(_line_index * slopeRate + _offsetX, _offsetY);
			IdentifyTerrainPiece(noiseValue);


			//If terrainholder reach its maximum capacity
			if (_terrainsHolder.Count > maxTerrainCapacity) {
				TerrainBuilder eraseTerrain = _terrainsHolder.Dequeue();
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
    }

	private void PrebuildMap() {
		//prebuild the first three tiles as plain
		int prebuildNum = 3;
		for(int p = 0; p < prebuildNum; p++) {
			InstantiateTerrain(PoolingID.TerrainPlain);
		}
		 
		for(int i = 0; i < maxTerrainCapacity; i++) {
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
		GameObject createdObject = PoolManager.instance.ReuseObject(object_id );
		createdObject.transform.SetParent( _terainholder.transform );
		createdObject.transform.localPosition = new Vector3(0,0, _line_index);
		createdObject.SetActive(true);

		TerrainBuilder terrainBuilder = createdObject.GetComponent<TerrainBuilder>();
		terrainBuilder.GenerateObstacle();

		_terrainsHolder.Enqueue( terrainBuilder );

		_line_index++;
		return createdObject.GetComponent<TerrainBuilder>();
	}

}
