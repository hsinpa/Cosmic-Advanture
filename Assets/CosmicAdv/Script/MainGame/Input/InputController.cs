using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using Utility;
using InputWrapper;

public class InputController : Observer {
	BaseUnit _playerUnit;
    MapGenerator _map;
	InputManager _input;
    Vector3 moveDir {
        get {
            return _moveDir;
        } set {

            _playerUnit.OnClick();
            _moveDir = value;
        }
    }
    Vector3 _moveDir ;

	// InputManager _inputManager;

    public override void OnNotify(string p_event, params object[] p_objects) {
        base.OnNotify(p_event, p_objects);
    }

	public void SetUp(BaseUnit p_baseUnit, MapGenerator p_map) {
		_playerUnit = p_baseUnit;
        _map = p_map;

		if (_input == null)
			_input = new InputManager(gameObject);
		// _inputManager = new InputManager();
	}

	public void Update() {
		if (_input == null) return;

		if (_input.IsDownClick()) {
            moveDir = -Vector3.forward;
        }
        if (_input.IsFrontClick()) {
            moveDir = Vector3.forward;
        }

        if (_input.IsLeftClick()) {
            moveDir = Vector3.left;
        }

        if (_input.IsRightClick()) {
            moveDir = Vector3.right;
        }

        if (_input.IsTap()) {
            moveDir = Vector3.forward;
        }

		if (_input.IsRelease()) {

            bool isMove = _map.IsPosAvailable(_playerUnit.transform.position, _moveDir);
            BaseUnit.MoveDir moveDir = new BaseUnit.MoveDir(_moveDir, isMove);
            _playerUnit.Move(moveDir);

        }
    }


}
