using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CA_Terrain;
using Utility;

public class MapGenerator : MonoBehaviour {
	public float slopeRate;
	public int maxMapHeightPerTime = 10;
	public List<GameObject> terrainPrefab = new List<GameObject>();

	private int _line_index = 0;
	private List<TerrainBuilder> _terrainsHolder = new List<TerrainBuilder>();
	private GameObject _terainholder;

	public void SetUp(GameObject p_terrain_holder ) {
		_terainholder = p_terrain_holder;
		GenerateMap();
	}

	public void GenerateMap() {

		//prebuild the first three tiles as plain
		int prebuildNum = 3;
		for(int p = 0; p < prebuildNum; p++) {
			InstantiateTerrain(terrainPrefab[0]);
		}
		int offSetX = Random.Range(0, 20000),
			offSetY = Random.Range(0, 20000);
		 

		for(int i = 0; i < maxMapHeightPerTime; i++) {
			float noiseValue =  Mathf.PerlinNoise(_line_index * slopeRate + offSetX, offSetY);
			IdentifyTerrainPiece(noiseValue);
		}

	}

	public void IdentifyTerrainPiece(float p_noiseValue) {
		//Hard code now
		Debug.Log("Noise Value " + p_noiseValue);

		//Plain
		if (p_noiseValue > 0.45f && p_noiseValue < 0.5f) {
			InstantiateTerrain(terrainPrefab[0]);
			return;
		}

		//Terrain
		if (p_noiseValue >= 0.5f) {
			InstantiateTerrain(terrainPrefab[1]);
		}

		//Train
		if (p_noiseValue <= 0.45f) {
			InstantiateTerrain(terrainPrefab[2]);
		}

	}

	public TerrainBuilder InstantiateTerrain(GameObject p_prefab) {
		GameObject createdObject = UtilityMethod.CreateObjectToParent(_terainholder.transform, p_prefab);
		createdObject.transform.localPosition = new Vector3(0,0, _line_index);

		_line_index++;
		return createdObject.GetComponent<TerrainBuilder>();
	}

}
