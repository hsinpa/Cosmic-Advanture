﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _AIAgent;

[RequireComponent(typeof(BaseUnit), typeof(BaseStat))]
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
	private MapGenerator _map;

	public AIAgentChart AIAgentChart;

	private float _time_record;

	public bool SetUp(MapGenerator p_map) {
		_map = p_map;
		_baseUnit = GetComponent<BaseUnit>();
		_baseUnit.SetUp();

		eventHandler = new EventHandler(this);

		bool validAIAgent = (AIAgentChart != null);
		
		if (validAIAgent) {
			agentBaseNode = AIAgentChart.agentNode;
			SetStrategy(agentBaseNode.GetDefaultStrategy());

			_time_record = Time.time + tacticsHandler.period_to_act;
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


		if (tacticsHandler == null || Time.time < _time_record) return;

		Vector3 moveDirection = tacticsHandler.Planning();
		// CA_Terrain.CA_Grid grid = _map.IsPosAvailable(transform.position, moveDirection);
		// bool isMove = grid.isWalkable;
		bool moveFeasibility = (moveDirection != Vector3.zero);
		BaseUnit.MoveDir dir = new BaseUnit.MoveDir(moveDirection, moveFeasibility);
		if (!_baseUnit.Move(dir)) {
			//Debug.Log("No Movement is make");
			// Notify(EventFlag.AIAgent.MeetInline);
		}

		_time_record = Time.time + tacticsHandler.period_to_act;

	}
	
}
