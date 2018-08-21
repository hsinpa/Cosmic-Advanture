using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using Utility;

public class MapController : Observer {
	MapGenerator _mapGeneration;
    InputController _inputController;


    public override void OnNotify(string p_event, params object[] p_objects) {
        base.OnNotify(p_event, p_objects);

        switch (p_event) {
            case EventFlag.Game.SetUp : {
                Debug.Log("Game Start");
				_mapGeneration = MainApp.Instance.FindObject<MapGenerator>("model/map_generator");
                _inputController = MainApp.Instance.GetObserver<InputController>();

				Init();
            }
            break;
        }
    }

	private void Init() {
		GameObject holderObject = MainApp.Instance.FindObject<Transform>("view/terrain_holder").gameObject;
		_mapGeneration.SetUp(holderObject);
        _inputController.SetUp(null);
	}

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Space) && _mapGeneration != null) {
            _mapGeneration.AssignSRandomTerrain();
        }
        
    }


}
