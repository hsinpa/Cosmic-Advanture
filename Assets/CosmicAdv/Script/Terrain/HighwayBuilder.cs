using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace CA_Terrain
{
	public class HighwayBuilder : TerrainBuilder {
		private List<GameObject> vehicles = new List<GameObject>();
		private int direction;
		private float vehicleSpeed, timePeriod, timeRecord; 

		public override void GenerateObstacle(bool p_isPreBuild = false) {
			if (p_isPreBuild) return;

			Highway_STP hightway_stp = (Highway_STP) terrain_stp;
			AnimatedObject_STP animatedObject = hightway_stp.vehicles[Random.Range(0, hightway_stp.vehicles.Count)];

			direction = (UtilityMethod.RollDice() == 0 ) ? -1 : 1;
			timePeriod = animatedObject._displayPeriod;
			vehicleSpeed = animatedObject.randomSpeed;

			for(int i = 0; i < animatedObject.maxVehicleNUm; i++) {
				GameObject generatedVehicle = PoolManager.instance.ReuseObject(animatedObject._id);
				generatedVehicle.transform.SetParent(AnimatedObject.transform);
				vehicles.Add(generatedVehicle);
			}
		}

		private void Update() {

			if (Time.time > timeRecord) {
				EnableVehicle();
			}

			foreach (GameObject vehicle in vehicles) {
				if (!vehicle.activeSelf) continue;
				vehicle.transform.position += Vector3.right * direction * vehicleSpeed * Time.deltaTime;

				if (vehicle.transform.localPosition.x > stored_prefabs[stored_prefabs.Length-1].transform.position.x ||
					vehicle.transform.localPosition.x < stored_prefabs[0].transform.position.x)
					vehicle.SetActive(false);
			}

		}

		private void EnableVehicle() {
			timeRecord = Time.time + timePeriod;

			foreach (GameObject vehicle in vehicles) {
				if (!vehicle.activeSelf) {

					Vector3 landingPosition = (direction == -1) ? stored_prefabs[stored_prefabs.Length-1].transform.position : stored_prefabs[0].transform.position;
					landingPosition += Vector3.up;
					vehicle.transform.position = landingPosition;
					vehicle.SetActive(true);
					return;
				}
			}

		}

		public override void OnTerrainDestroy() {
			
		}


	}
}

