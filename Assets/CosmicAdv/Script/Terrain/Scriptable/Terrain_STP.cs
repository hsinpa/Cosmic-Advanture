using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA_Terrain
{
	public abstract class Terrain_STP : ScriptableObject {
		public GameObject[] TerrainPrefab;
		public GameObject DisableTerrainPrefab;
	}
}