using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {
	BaseUnit _baseUnit;
    float smoothSpeed = 1.5f;
	int maxBackStepToFollow = 4;
	int recordBackStepToFollow  = 0;
	int highestStepZ = 0;

	public void SetUp(BaseUnit p_baseUnit) {
		_baseUnit = p_baseUnit;


        transform.position = new Vector3(_baseUnit.transform.position.x, transform.position.y, _baseUnit.transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate () {
		if (_baseUnit == null) return;

		Vector3 targetPosition = new Vector3(_baseUnit.transform.position.x, 0, _baseUnit.transform.position.z-1),
				cameraPosition = new Vector3(transform.position.x, 0, transform.position.z);
		float dist = Vector3.Distance(targetPosition, cameraPosition);
		
		if (dist > 0.1f) {
			// Vector3 lerpPosition = Vector3.Slerp(targetPosition, cameraPosition, 0.01f);
			Vector3 lerpPosition = Vector3.Lerp(cameraPosition, targetPosition, smoothSpeed * Time.deltaTime);

            // if (lerpPosition.z > cameraPosition.z) {				
				if (highestStepZ < targetPosition.z) 
					highestStepZ = Mathf.RoundToInt(targetPosition.z);

				if (highestStepZ - targetPosition.z > maxBackStepToFollow) {
					return;
				}

                lerpPosition.y = transform.position.y;
                transform.position = lerpPosition;
            // }
        }



	}
}
