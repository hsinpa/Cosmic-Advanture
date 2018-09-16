using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _AIAgent;

public class AIDirector : MonoBehaviour {
	private List<AIAgent> agentlist = new List<AIAgent>();
	private MapController mapController;
	private NodeReader nodeReader;

	public void Start() {
		AIAgent[] agents = GameObject.FindObjectsOfType<AIAgent>();
		foreach(AIAgent agent in agents) {
			AddAgent(agent);
		}		
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

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			ExecuteAgentPlanning();
		}
	}
}
