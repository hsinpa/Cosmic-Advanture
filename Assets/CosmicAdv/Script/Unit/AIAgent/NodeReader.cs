using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace _AIAgent
{
	public class NodeReader {
		public static AgentBaseNode Parse(AIAgentChart p_agent_chart) {
			if (p_agent_chart.nodes.Count <= 0) {
				return null;
			}

			AgentBaseNode baseNode = (AgentBaseNode) p_agent_chart.nodes.Find((x) => x.GetType() == typeof(AgentBaseNode));
			if (baseNode == null) 
				return null;

			int strategyCount = baseNode.GetInputPort("strategies").ConnectionCount;
			baseNode.strategies = new List<StrategyNode>();

			for (int i = 0; i < strategyCount; i++) {
				StrategyNode strategy = baseNode.GetInputPort("strategies").GetConnection(i).node as StrategyNode;
				strategy.tacticsNode = GetTacticsNodes(strategy);
				strategy.eventNodes = GetEventNodes(strategy);
				baseNode.strategies.Add(strategy);
			}

			return baseNode;
		}

		private static TacticsNode GetTacticsNodes(StrategyNode p_strategyNode) {
			if (p_strategyNode.GetInputPort("tacticsNode").IsConnected) {
				return (TacticsNode) p_strategyNode.GetInputPort("tacticsNode").Connection.node;
			}
			return null;
		}

		private static List<EventNode> GetEventNodes(StrategyNode p_strategyNode) {
			List<EventNode> events = new List<EventNode>();
			if (p_strategyNode.GetInputPort("eventNodes").IsConnected) {
				int eventCount = p_strategyNode.GetInputPort("eventNodes").ConnectionCount;
				for (int i = 0; i < eventCount; i++)
					events.Add(p_strategyNode.GetInputPort("eventNodes").GetConnection(i).node as EventNode );
			}
			return events;
		}
	}
}