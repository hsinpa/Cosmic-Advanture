using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _AIAgent;

[RequireComponent(typeof(BaseUnit))]
public class AIAgent : MonoBehaviour {
	public 	BaseUnit baseUnit {
		get {
			return _baseUnit;
		}
	}
	BaseUnit _baseUnit;

	private AgentBaseNode agentBaseNode;
	private TacticsHandler tacticsHandler;
	private EventHandler eventHandler;
	private StrategyNode currentStrategy;

	public AIAgentChart AIAgentChart;

	public bool SetUp() {
		_baseUnit = GetComponent<BaseUnit>();
		_baseUnit.SetUp();

		eventHandler = new EventHandler(this);

		bool validAIAgent = (AIAgentChart != null);
		
		if (validAIAgent) {
			agentBaseNode = AIAgentChart.agentNode;
			SetStrategy(agentBaseNode.GetDefaultStrategy());
		}

		return validAIAgent;
	}

	//Notify child event about coming event
	public void Notify(string p_event) {
		eventHandler.ReceiveEvents(p_event);
	}

	public void SetStrategy(StrategyNode p_strategy) {
		if (p_strategy == null) {
			tacticsHandler = null;
			eventHandler.SetUp(null);
			return;
		}

		currentStrategy = p_strategy;
		if (currentStrategy.tacticsNode.GetType() == typeof(TacticsRepeatNode)) {
			tacticsHandler = new TacticsRepeat(this, currentStrategy.tacticsNode);
		} else if (currentStrategy.tacticsNode.GetType() == typeof(TacticsPursueNode)) {
			// tacticsHandler = new TacticsRepeat(this, currentStrategy.tacticsNode);
		}

		eventHandler.SetUp(currentStrategy.eventNodes);
	}

	public void Execute() {
		if (tacticsHandler == null) return;

		Vector3 moveDirection = tacticsHandler.Planning();
		bool moveFeasibility = (moveDirection != Vector3.zero);

		BaseUnit.MoveDir dir = new BaseUnit.MoveDir(moveDirection, moveFeasibility);
		if (!_baseUnit.Move(dir)) {
			Debug.Log("No Movement is make");
			// Notify(EventFlag.AIAgent.MeetInline);
		}
	}
	
}
