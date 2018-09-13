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
        public int index_offset {
            get {
                return total_size - activate_size;
            }
        }

        public int activateStartXPos {
                get {
                    return Mathf.RoundToInt((index_offset* 0.5f));
            }
        }

        [Range(0, 0.8f)]
			public float obstacleDistribution;

			public GameObject[] stored_prefabs;
			public CA_Grid[] grids;
			protected GameObject Obstacle;
			protected GameObject Terrains;
			protected GameObject AnimatedObject;

		#endregion

		public void SetUp() {
            PreCheck();

            grids =	ReCalculateGrid();
		}

		private CA_Grid[] ReCalculateGrid() {
			CA_Grid[] grids = new CA_Grid[index_offset];
			for (int i = 0 ; i < index_offset; i++) {
                int offsetIndex = i + activateStartXPos;
				Vector2 gridPos = new Vector2(stored_prefabs[offsetIndex].transform.position.x, stored_prefabs[offsetIndex].transform.position.z);
				grids[i] = new CA_Grid(gridPos, true);
            }
            return grids;
		}

		private void PreCheck() {
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

		private void Clear() {
			UtilityMethod.ClearChildObject(Obstacle.transform);
			UtilityMethod.ClearChildObject(Terrains.transform);
			UtilityMethod.ClearChildObject(AnimatedObject.transform);
		}

		public abstract void GenerateObstacle(bool p_isPreBuild = false);

		public virtual void OnTerrainDestroy() {
			int animatedCount = AnimatedObject.transform.childCount;
			for (int i = animatedCount-1; i >= 0; i--)
				PoolManager.instance.Destroy(AnimatedObject.transform.GetChild(i).gameObject);
		}

	}
}

