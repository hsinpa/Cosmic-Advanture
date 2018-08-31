using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA_Terrain
{
	public class HighwayBuilder : TerrainBuilder {
		private List<GameObject> vehicles = new List<GameObject>();
		private int maxVehicleNUm = 5;
		private float timePeriod, timeRecord; 

		public override void GenerateObstacle(bool p_isPreBuild = false) {
			
		}

		private void Update() {

			if (Time.time > timeRecord) {
				EnableVehicle();
			}

		}

		private void EnableVehicle() {
			timeRecord = Time.time + timePeriod;


		}


		public void GenerateVehicle() {
			Highway_STP hightway_stp = (Highway_STP) terrain_stp;
			AnimatedObject_STP animatedObject = hightway_stp.vehicles[Random.Range(0, hightway_stp.vehicles.Count)];

			timePeriod = total_size / (float)maxVehicleNUm;

			for(int i = 0; i < maxVehicleNUm; i++) {
				vehicles.Add(PoolManager.instance.ReuseObject(animatedObject._id));

			}
		}	

	}
}

