using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using Utility;

public class MapController : Observer {
	MapGenerator _mapGeneration;
    InputController _inputController;
    CameraHandler _camera;

    public BaseUnit baseUnit;

    public override void OnNotify(string p_event, params object[] p_objects) {
        base.OnNotify(p_event, p_objects);

        switch (p_event) {
            case EventFlag.Game.SetUp : {
                Debug.Log("Game Start");
				_mapGeneration = MainApp.Instance.FindObject<MapGenerator>("model/map_generator");
                _inputController = MainApp.Instance.GetObserver<InputController>();
                _camera = MainApp.Instance.FindObject<CameraHandler>("view/camera");

				Init();
            }
            break;
        }
    }

	private void Init() {
		GameObject holderObject = MainApp.Instance.FindObject<Transform>("view/terrain_holder").gameObject;
        
        baseUnit.transform.position = new Vector3(5, baseUnit.transform.position.y, 1);
        baseUnit.SetUp();
		_mapGeneration.SetUp(holderObject);
        _inputController.SetUp(baseUnit);

        _camera.SetUp(baseUnit);
	}

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Space) && _mapGeneration != null) {
            _mapGeneration.AssignSRandomTerrain();
        }
        
    }


}
