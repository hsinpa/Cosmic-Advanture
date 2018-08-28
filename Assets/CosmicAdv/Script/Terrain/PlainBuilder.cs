using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace CA_Terrain
{
	public class PlainBuilder : TerrainBuilder {

		public override void GenerateObstacle(bool p_isPreBuild = false) {
			Plain_STP plain_stp = (Plain_STP) terrain_stp;
			if (p_isPreBuild) {
				
				for (int i = 0; i < stored_prefabs.Length; i++) {
					Transform g_prefab = stored_prefabs[i].transform;

					if (g_prefab.name.IndexOf("Off") > 0) {
						GameObject randomObstacle = plain_stp.FindObstacleByTag("Tree").ObstaclePrefab;
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
					Transform g_prefab = stored_prefabs[i + index_offset].transform;

					if (g_prefab.name.IndexOf("On") > 0 && UtilityMethod.PercentageGame(obstacleDistribution) ) {
						Obstacle_STP randomObstacle = plain_stp.obstables[Random.Range(0, plain_stp.obstables.Count)];
						GameObject generate_obstacle = PoolManager.instance.ReuseObject(randomObstacle._id);
						generate_obstacle.transform.SetParent(Obstacle.transform);
						generate_obstacle.transform.position = g_prefab.transform.position + (Vector3.up*0.5f);
						generate_obstacle.SetActive(true);

						grids[i].isWalkable = false;
					}
			}	
		}
	}
}

