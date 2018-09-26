using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace CA_Terrain
{
	public class PlainBuilder : TerrainBuilder {
		private List<GameObject> runtimeObstacle = new List<GameObject>();
		
		public override void GenerateObstacle(bool p_isPreBuild = false) {
			Plain_STP plain_stp = (Plain_STP) terrain_stp;
			if (p_isPreBuild) {
				
				for (int i = 0; i < stored_prefabs.Length; i++) {
					Transform g_prefab = stored_prefabs[i].transform;

					if (g_prefab.name.IndexOf("Off") > 0) {
						GameObject randomObstacle = plain_stp.FindObstacleByTag("Tree").prefab;
						GameObject generate_obstacle = Instantiate( randomObstacle);
						generate_obstacle.transform.SetParent(Obstacle.transform);
						generate_obstacle.transform.position = g_prefab.transform.position + (Vector3.up*0.5f);
					}
				}
			} else {
				GenerateRandomObstacle();
			}
		}

		private void GenerateRandomObstacle() {
			Plain_STP plain_stp = (Plain_STP) terrain_stp;

			for (int i = 0; i < activate_size; i++) {
                int offsetIndex = i + activateStartXPos;

                Transform g_prefab = stored_prefabs[offsetIndex].transform;

					if (g_prefab.name.IndexOf("On") > 0 && UtilityMethod.PercentageGame(obstacleDistribution) ) {
						Obstacle_STP randomObstacle = plain_stp.obstables[Random.Range(0, plain_stp.obstables.Count)];
						GameObject generate_obstacle = PoolManager.instance.ReuseObject(randomObstacle._id);
						generate_obstacle.transform.SetParent(Obstacle.transform);
						generate_obstacle.transform.position = g_prefab.transform.position + (Vector3.up*0.5f);
						generate_obstacle.SetActive(true);
						
						runtimeObstacle.Add(generate_obstacle);
						grids[i].isWalkable = false;
                }
            }
		}

		public override void OnTerrainDestroy() {
			base.OnTerrainDestroy();
			int obstacleNum = runtimeObstacle.Count;
			for (int i = 0; i < obstacleNum; i++)
				PoolManager.instance.Destroy(runtimeObstacle[i]);
			
			runtimeObstacle.Clear();
		}
	}
}

