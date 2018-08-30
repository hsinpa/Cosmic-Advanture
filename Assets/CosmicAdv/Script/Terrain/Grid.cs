using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  CA_Terrain
{
    [SerializeField]
	public struct CA_Grid {
		public Vector2 position;
		//public GameObject p_grid;
		//public TerrainBuilder terrainRow;
		public bool isWalkable;

        public CA_Grid(Vector2 position, bool isWalkable)
        {
            this.position = position;
            this.isWalkable = isWalkable;
        }
    }
}