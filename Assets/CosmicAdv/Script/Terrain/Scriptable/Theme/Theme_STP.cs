using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA_Terrain
{
	[CreateAssetMenu(fileName = "[STP]ThemeObject", menuName = "STP/ThemeObject", order = 1)]
	public class Theme_STP : STPObject
    {
        public List<GameObject> terrainPrefab = new List<GameObject>();
        public List<STPObject>  stpObjectHolder = new List<STPObject>();



	}
}