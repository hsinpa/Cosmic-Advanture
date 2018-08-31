using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA_Terrain
{
	[CreateAssetMenu(fileName = "[STP]AnimatedObject", menuName = "STP/AnimatedObject/Vehicle", order = 2)]
	public class AnimatedObject_STP : ScriptableObject {
		public int _id;
		public string _tag;

		public GameObject ObstaclePrefab;
		public Material MaterialPrefab;

		public float[] speedRange;
		public float randomSpeed {
			get {
				if (speedRange.Length <= 0) return 0;
				if (speedRange.Length < 2) return speedRange[0];

				return Random.Range(speedRange[0], speedRange[1]);
			}
		}

		public ScriptableObject theme;
	}
}