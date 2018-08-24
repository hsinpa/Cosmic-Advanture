using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
	BaseUnit _baseUnit;
    float smoothSpeed = 1.5f;

	public void SetUp(BaseUnit p_baseUnit) {
		_baseUnit = p_baseUnit;


        transform.position = new Vector3(_baseUnit.transform.position.x, transform.position.y, _baseUnit.transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate () {
		if (_baseUnit == null) return;

		Vector3 targetPosition = new Vector3(_baseUnit.transform.position.x, 0, _baseUnit.transform.position.z),
				cameraPosition = new Vector3(transform.position.x, 0, transform.position.z);
		float dist = Vector3.Distance(targetPosition, cameraPosition);
		
		if (dist > 0.1f) {
			// Vector3 lerpPosition = Vector3.Slerp(targetPosition, cameraPosition, 0.01f);
			Vector3 lerpPosition = Vector3.Lerp(cameraPosition, targetPosition, smoothSpeed * Time.deltaTime);

            if (lerpPosition.z > cameraPosition.z) {
                lerpPosition.y = transform.position.y;
                transform.position = lerpPosition;
            }

        }



	}
}
