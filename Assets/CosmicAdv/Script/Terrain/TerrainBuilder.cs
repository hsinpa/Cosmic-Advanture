using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace  CA_Terrain
{
	public abstract class TerrainBuilder : MonoBehaviour {
		#region Inspector Parameters
			public Terrain_STP terrain_stp;
			public int total_size;
			public int activate_size;

			[Range(0, 0.8f)]
			public float obstacleDistribution;

			public GameObject[] stored_prefabs;
			public GameObject Obstacle;
			public GameObject Terrains;
			public GameObject AnimatedObject;
		#endregion

		public void PreCheck() {
			if (Obstacle == null)
				Obstacle = transform.Find("Obstacle").gameObject;
			if (Terrains == null)
				Terrains = transform.Find("Terrains").gameObject;
			if (AnimatedObject == null)
				AnimatedObject = transform.Find("AnimatedObject").gameObject;
		}

		public void BuildTerrain() {
			if (total_size < activate_size) {
				Debug.LogError("Total Size too small");
				return;
			}

			if (terrain_stp == null) {
				Debug.LogError("Terrain ScritableObject not exist");
				return;
			}

			PreCheck();
			Clear();

			stored_prefabs = new GameObject[total_size];
			int activateStartXPos = -Mathf.RoundToInt((activate_size * 0.5f));
			int totalStartXPos = -Mathf.RoundToInt((total_size * 0.5f));

			for (int i = 0; i < total_size; i++) {
				GameObject selectedPrefab = terrain_stp.DisableTerrainPrefab;
				if (UtilityMethod.IsWithinRange(activateStartXPos, -activateStartXPos, totalStartXPos + i )) {
					GameObject terrainPrefab = (terrain_stp.TerrainPrefab.Length > 1) ? terrain_stp.TerrainPrefab[i%2] : terrain_stp.TerrainPrefab[0];
					selectedPrefab = terrainPrefab;
				}
				
				GameObject instantiateObject = Instantiate(
					selectedPrefab,new Vector3( transform.position.x + activateStartXPos + i, 0, transform.position.z),
					Quaternion.identity);

				instantiateObject.transform.SetParent(Terrains.transform);
				stored_prefabs[i] = instantiateObject;
			}

			GenerateObstacle(true);
		}

		public abstract void GenerateObstacle(bool p_isPreBuild = false);

		private void Clear() {
			UtilityMethod.ClearChildObject(Obstacle.transform);
			UtilityMethod.ClearChildObject(Terrains.transform);
			UtilityMethod.ClearChildObject(AnimatedObject.transform);
		}

	}
}

