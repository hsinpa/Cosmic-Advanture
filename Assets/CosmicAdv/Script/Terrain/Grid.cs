using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  CA_Terrain
{
    [SerializeField]
	public class CA_Grid {
		public Vector2 position;
		//public GameObject p_grid;
		//public TerrainBuilder terrainRow;
		public bool isWalkable {
            get {
                return (this.mapComponent == null && position != Vector2.zero);
            }
        } 

        public MapComponent mapComponent;

        public CA_Grid(Vector2 position)
        {
            this.position = position;
            this.mapComponent = null;
        }
    }
}