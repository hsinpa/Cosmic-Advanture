using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA_Terrain
{
	[CreateAssetMenu(fileName = "[STP]Plain", menuName = "STP/Terrain/Plain", order = 1)]
	public  class Plain_STP : Terrain_STP {
		public List<GameObject> obstables;
		
	}
}