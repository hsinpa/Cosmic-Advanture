using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObserverPattern;
using Utility;

public class MapController : Observer {
    MapGenerator _mapGeneration;
    ThemeGenerator _themeGeneration;
    InputController _inputController;
    AIDirector _aiDirector;

    CameraHandler _camera;

    public BaseUnit baseUnit;

    public override void OnNotify(string p_event, params object[] p_objects) {
        base.OnNotify(p_event, p_objects);

        switch (p_event) {
            case EventFlag.Game.SetUp: {
                    Debug.Log("Game Start");
                    _mapGeneration = MainApp.Instance.FindObject<MapGenerator>("model/map_generator");
                    _themeGeneration = MainApp.Instance.FindObject<ThemeGenerator>("model/map_generator");
                    _inputController = MainApp.Instance.GetObserver<InputController>();
                    _aiDirector = MainApp.Instance.GetObserver<AIDirector>();
                    _camera = MainApp.Instance.FindObject<CameraHandler>("view/camera");

                    Init();
                }
                break;

            case EventFlag.Game.PlayerMove: {
                    CA_Terrain.TerrainBuilder terrainBuilder = _mapGeneration.AssignSRandomTerrain();
                    _aiDirector.AssignAgentsInSingleRow(terrainBuilder);
                };
                break;


            case EventFlag.Game.UnitAttack: {
                    if (p_objects.Length == 2) {
                        OnUnitAttack((BaseUnit)p_objects[0], (BaseUnit)p_objects[1]);
                    }
                };
                break;

            case EventFlag.Game.UnitDestroy: {
                    OnUnitDestroy((BaseUnit)p_objects[0]);
                }
                break;

        }
    }

    private void Init() {
        GameObject obstacleHolderObject = MainApp.Instance.FindObject<Transform>("view/terrain_holder").gameObject;
        GameObject unitHolderObject = MainApp.Instance.FindObject<Transform>("view/unit_holder").gameObject;

        baseUnit.transform.position = new Vector3(5, baseUnit.transform.position.y, 2);
        baseUnit.SetUp();
        _mapGeneration.SetUp(obstacleHolderObject);

        List<CA_Terrain.Unit_STP> unitsSTP = _themeGeneration.GetObjectByType<CA_Terrain.Unit_STP>();
        _aiDirector.SetUp(unitsSTP, unitHolderObject, _mapGeneration);
        _inputController.SetUp(baseUnit, _mapGeneration);

        _camera.SetUp(baseUnit);
    }

    private void OnUnitAttack(BaseUnit p_attacker, BaseUnit p_defenser) {
        p_defenser.OnAttack(p_attacker);
    }

    private void OnUnitDestroy(BaseUnit p_unit) {
        if (p_unit.baseStat.controllable)
        {
            Debug.Log("Game over you lose");
        }
        else {
            _aiDirector.RemoveAgent( p_unit.GetComponent<AIAgent>());
        }
    }
}
