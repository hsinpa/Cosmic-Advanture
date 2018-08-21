using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CA_Terrain
{
	[CreateAssetMenu(fileName = "[STP]Plain", menuName = "STP/Terrain/Plain", order = 1)]
	public  class Plain_STP : Terrain_STP {
		public List<Obstacle_STP> obstables;
		
		public Obstacle_STP FindObstacleByTag(string tag) {
			List<Obstacle_STP> findObjects = obstables.FindAll(x=>x._tag == tag);
			if (findObjects.Count <= 0) return null;

			return findObjects[Random.Range(0, findObjects.Count)];
		}
	}
}