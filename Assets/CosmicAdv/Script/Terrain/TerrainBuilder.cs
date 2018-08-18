using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace  CA_Terrain
{
	public abstract class TerrainBuilder : MonoBehaviour {
		public Terrain_STP terrain_stp;
		public int total_size;
		public int activate_size;

		public GameObject[] stored_prefabs;

		public void BuildTerrain() {
			if (total_size < activate_size) {
				Debug.LogError("Total Size too small");
				return;
			}

			if (terrain_stp == null) {
				Debug.LogError("Terrain ScritableObject not exist");
				return;
			}

			Clear();

			stored_prefabs = new GameObject[total_size];
			int activateStartXPos = Mathf.RoundToInt(transform.position.x - (activate_size * 0.5f));
			int totalStartXPos = Mathf.RoundToInt(transform.position.x - (total_size * 0.5f));

			for (int i = 0; i < total_size; i++) {
				GameObject selectedPrefab = terrain_stp.DisableTerrainPrefab;
				if (UtilityMethod.IsWithinRange(activateStartXPos, -activateStartXPos, totalStartXPos + i )) {
					GameObject terrainPrefab = (terrain_stp.TerrainPrefab.Length > 1) ? terrain_stp.TerrainPrefab[i%2] : terrain_stp.TerrainPrefab[0];
					selectedPrefab = terrainPrefab;
				}
				
				GameObject instantiateObject = Instantiate(
					selectedPrefab,new Vector3(activateStartXPos + i, 0, transform.position.z),
					Quaternion.identity);

				instantiateObject.transform.SetParent(this.transform);
				stored_prefabs[i] = instantiateObject;
			}
		}

		private void Clear() {
			UtilityMethod.ClearChildObject(transform);
		}

	}
}

