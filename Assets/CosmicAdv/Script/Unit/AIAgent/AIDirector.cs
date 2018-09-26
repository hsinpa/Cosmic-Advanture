using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _AIAgent;
using ObserverPattern;
using Utility;
using CA_Terrain;

public class AIDirector : Observer
{
    private List<Unit_STP> unitSTPlist = new List<Unit_STP>();
    private List<AIAgent> agentlist = new List<AIAgent>();
    private MapGenerator _map;
    private NodeReader nodeReader;
    private GameObject unitHolder;

    public override void OnNotify(string p_event, params object[] p_objects)
    {
        base.OnNotify(p_event, p_objects);

        switch (p_event)
        {
            case EventFlag.Game.PlayerMove : {
            }
            break;
        }
    }

    public void SetUp(List<Unit_STP> p_unitStp, GameObject p_unitHolder, MapGenerator p_map) {
        unitSTPlist = p_unitStp;
        unitHolder = p_unitHolder;
        _map = p_map;
    }

	public void AddAgent(AIAgent p_unit) {
		bool isValid = p_unit.SetUp(_map);

		if (isValid)
			agentlist.Add(p_unit);
	}

	public void RemoveAgent(AIAgent p_unit, int p_index = -1) {
        if (p_index == -1) p_index = agentlist.IndexOf(p_unit);

        if (p_index >= 0 && p_index < agentlist.Count) {
            PoolManager.instance.Destroy(agentlist[p_index].gameObject);
            agentlist.RemoveAt(p_index);
        }
	}
    
    //TODO : Hard Code, should make it data-driven 
    public void AssignAgentsInSingleRow(CA_Terrain.TerrainBuilder p_terrain) {
        if (p_terrain.GetType() != typeof(PlainBuilder) || unitSTPlist.Count <= 0) return;

        for (int i = 0; i < p_terrain.activate_size; i++)
        {
            int offsetIndex = i + p_terrain.activateStartXPos;

            Transform g_prefab = p_terrain.stored_prefabs[offsetIndex].transform;

            if (g_prefab.name.IndexOf("On") > 0 && p_terrain.grids[i].isWalkable)
            {
                if (UtilityMethod.PercentageGame(0.05f))
                {
                    Unit_STP unitSTP = unitSTPlist[Random.Range(0, unitSTPlist.Count)];

                    GameObject generateUnit = PoolManager.instance.ReuseObject(unitSTP._id);
                    AIAgent aIAgent = generateUnit.GetComponent<AIAgent>();
                    AddAgent(aIAgent);

                    generateUnit.transform.SetParent(unitHolder.transform);
                    generateUnit.transform.position = g_prefab.transform.position + (Vector3.up * 0.5f);

                    aIAgent.SetUp(_map);
                    generateUnit.SetActive(true);
                }
            }
        }
    }

    private void Update() {
        ExecuteAgentPlanning();
    }

    private void ExecuteAgentPlanning() {
		try {
			for (int i = agentlist.Count -1; i >= 0; i--) {
				if (agentlist[i] != null) {
                    agentlist[i].Execute();
                    ClearUpInvalidUnit(agentlist[i], i);
                }
			}
		} catch(System.Exception e) {
			Debug.Log("ExecuteAgentPlanning Exception " + e.ToString());
		}
	}

    private void ClearUpInvalidUnit(AIAgent p_agent, int p_index) {
        CA_Grid grid = _map.IsPosAvailable(p_agent.transform.position, Vector3.zero);
        //If out of grid index, should be eradicate
        if (grid.position == Vector2.zero && !grid.isWalkable) {
            RemoveAgent(p_agent, p_index);
        }
    }

}
