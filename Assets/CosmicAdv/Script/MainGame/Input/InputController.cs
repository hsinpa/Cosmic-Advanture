using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using Utility;
using InputWrapper;

public class InputController : Observer {
	BaseUnit _playerUnit;
	Swipe swipe;
	// InputManager _inputManager;

    public override void OnNotify(string p_event, params object[] p_objects) {
        base.OnNotify(p_event, p_objects);

        // switch (p_event) {
        // }
    }

	public void SetUp(BaseUnit p_baseUnit) {
		_playerUnit = p_baseUnit;
		swipe = gameObject.AddComponent<Swipe>();
		// _inputManager = new InputManager();

	}

	public void Update() {
		if (swipe == null) return;

		if (swipe.IsDownClick()) {
			Debug.Log("DownClick");
		}
		if (swipe.IsFrontClick()) {
			Debug.Log("FrontClick");
		}

		if (swipe.IsLeftClick()) {
			Debug.Log("LeftClick");
		}

		if (swipe.IsRightClick()) {
			Debug.Log("RightClick");
		}

		if (swipe.IsTap()) {
			Debug.Log("TapClick");
		}
		if (swipe.IsRelease()) {
			Debug.Log("IsRelease");
		}
	}


}
