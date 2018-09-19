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
    private MapController mapController;
    private NodeReader nodeReader;
    private GameObject unitHolder;

    //public void Start() {
    //	AIAgent[] agents = GameObject.FindObjectsOfType<AIAgent>();
    //	foreach(AIAgent agent in agents) {
    //		AddAgent(agent);
    //	}
    //}

    public override void OnNotify(string p_event, params object[] p_objects)
    {
        base.OnNotify(p_event, p_objects);

        switch (p_event)
        {

        }
    }

    public void SetUp(List<Unit_STP> p_unitStp, GameObject p_unitHolder) {
        unitSTPlist = p_unitStp;
        unitHolder = p_unitHolder;
    }

    public void ExecuteAgentPlanning() {
		try {
			for (int i = agentlist.Count -1; i >= 0; i--) {
				if (agentlist[i] != null)
					agentlist[i].Execute();
			}
		} catch(System.Exception e) {
			Debug.Log("ExecuteAgentPlanning Exception " + e.ToString());
		}
	}

	public void AddAgent(AIAgent p_unit) {
		bool isValid = p_unit.SetUp();

		if (isValid)
			agentlist.Add(p_unit);
	}

	public void RemoveAgent(BaseUnit p_unit) {

	}

    //Not final version
    public void AssignAgentsInSingleRow(CA_Terrain.TerrainBuilder p_terrain) {

        for (int i = 0; i < p_terrain.activate_size; i++)
        {
            int offsetIndex = i + p_terrain.activateStartXPos;

            Transform g_prefab = p_terrain.stored_prefabs[offsetIndex].transform;

            if (g_prefab.name.IndexOf("On") > 0 && p_terrain.grids[i].isWalkable)
            {
                if (UtilityMethod.PercentageGame(0.1f) && unitSTPlist.Count > 0)
                {
                    Unit_STP unitSTP = unitSTPlist[Random.Range(0, unitSTPlist.Count)];

                    GameObject generateUnit = PoolManager.instance.ReuseObject(unitSTP._id);
                    generateUnit.transform.SetParent(unitHolder.transform);
                    
                    generateUnit.transform.position = g_prefab.transform.position + (Vector3.up * 0.5f);
                    generateUnit.SetActive(true);
                }
            }
        }
    }

	//void Update() {
	//	if (Input.GetKeyDown(KeyCode.Space)) {
	//		ExecuteAgentPlanning();
	//	}
	//}
}
