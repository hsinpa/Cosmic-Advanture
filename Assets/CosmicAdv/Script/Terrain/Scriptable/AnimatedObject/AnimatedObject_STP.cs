using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA_Terrain
{
	[CreateAssetMenu(fileName = "[STP]AnimatedObject", menuName = "STP/AnimatedObject/Vehicle", order = 2)]
	public class AnimatedObject_STP : STPObject {
		public Material MaterialPrefab;
		public Color[] colorSet;

		public float[] speedRange;
		public float randomSpeed {
			get {
				if (speedRange.Length <= 0) return 0;
				if (speedRange.Length < 2) return speedRange[0];

				return Random.Range(speedRange[0], speedRange[1]);
			}
		}

		public float displayPeriod;
		public float displayErrorRange;
		public float _displayPeriod {
			get {
				float randomPeriod = Random.Range(displayPeriod - displayErrorRange, displayPeriod + displayErrorRange);
				return Mathf.Clamp(randomPeriod, 0, displayPeriod + displayErrorRange);
			}
		}
		
		public ScriptableObject theme;
	}
}