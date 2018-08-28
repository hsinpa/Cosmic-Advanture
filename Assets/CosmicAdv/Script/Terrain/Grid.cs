using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  CA_Terrain
{
	public class CA_Grid {
		public Vector2 position;
		public GameObject p_grid;
		public TerrainBuilder terrainRow;
		public bool isWalkable;

		public CA_Grid(Vector2 p_position, bool p_isWalkable) {
			position = p_position;
			isWalkable = p_isWalkable;
		}

	}
}