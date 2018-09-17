using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA_Terrain
{
	[CreateAssetMenu(fileName = "[STP]ThemeObject", menuName = "STP/ThemeObject", order = 1)]
	public class Theme_STP : ScriptableObject {
		public int _id;
		public string _tag;
		
		public List<Obstacle_STP> _obstacleHolder = new List<Obstacle_STP>();
		public List<AnimatedObject_STP> _animatedHolder = new List<AnimatedObject_STP>();

		[SerializeField]
		public List<PoolingObject> _unitPrefabs = new List<PoolingObject>();

	}
}