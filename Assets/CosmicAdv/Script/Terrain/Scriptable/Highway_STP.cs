using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA_Terrain
{
	[CreateAssetMenu(fileName = "[STP]Highway", menuName = "STP/Terrain/Highway", order = 2)]
	public class Highway_STP : Terrain_STP {
		public List<AnimatedObject_STP> vehicles;
		public int maxEnableNum = 2;
		
		public bool staticObstacle = false;
	}
}