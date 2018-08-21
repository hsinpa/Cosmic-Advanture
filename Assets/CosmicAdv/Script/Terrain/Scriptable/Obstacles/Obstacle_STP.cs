using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA_Terrain
{
	public abstract class Obstacle_STP : ScriptableObject {
		public int _id;
		public string _tag;

		public GameObject ObstaclePrefab;
		public ScriptableObject theme;
	}
}