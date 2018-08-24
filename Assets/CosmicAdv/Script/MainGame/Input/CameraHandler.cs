﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
	BaseUnit _baseUnit;

	public void SetUp(BaseUnit p_baseUnit) {
		_baseUnit = p_baseUnit;
	}
	
	// Update is called once per frame
	void Update () {
		if (_baseUnit == null) return;

		Vector3 targetPosition = new Vector3(_baseUnit.transform.position.x, 0, _baseUnit.transform.position.z),
				cameraPosition = new Vector3(transform.position.x, 0, transform.position.z);
		float dist = Vector3.Distance(targetPosition, cameraPosition);
		
		if (dist > 0.1f) {
			// Vector3 lerpPosition = Vector3.Slerp(targetPosition, cameraPosition, 0.01f);
						Vector3 lerpPosition = Vector3.Lerp(targetPosition, cameraPosition, 0.001f);

			lerpPosition.y = transform.position.y;
			transform.position = lerpPosition;
		}

	}
}