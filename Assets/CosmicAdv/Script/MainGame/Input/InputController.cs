using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using Utility;
using InputWrapper;

public class InputController : Observer {
	BaseUnit _playerUnit;
	InputManager _input;
	// InputManager _inputManager;

    public override void OnNotify(string p_event, params object[] p_objects) {
        base.OnNotify(p_event, p_objects);

        // switch (p_event) {
        // }
    }

	public void SetUp(BaseUnit p_baseUnit) {
		_playerUnit = p_baseUnit;

		if (_input == null)
			_input = new InputManager(gameObject);
		// _inputManager = new InputManager();
	}

	public void Update() {
		if (_input == null) return;

		if (_input.IsDownClick()) {
			Debug.Log("DownClick");
		}
		if (_input.IsFrontClick()) {
			Debug.Log("FrontClick");
		}

		if (_input.IsLeftClick()) {
			Debug.Log("LeftClick");
		}

		if (_input.IsRightClick()) {
			Debug.Log("RightClick");
		}

		if (_input.IsTap()) {
			Debug.Log("TapClick");

		}
		if (_input.IsRelease()) {
			Debug.Log("IsRelease");
		}
	}


}
