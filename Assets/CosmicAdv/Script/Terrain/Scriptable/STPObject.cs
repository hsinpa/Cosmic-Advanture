using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA_Terrain
{
	public abstract class STPObject : ScriptableObject {
		public int _id;
		public string _tag;
		public int poolingNum = 1;
        public GameObject prefab;
	}
}
