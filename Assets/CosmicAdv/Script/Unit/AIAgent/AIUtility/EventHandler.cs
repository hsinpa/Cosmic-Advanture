using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _AIAgent
{
	public class EventHandler {
		AIAgent agent;
		List<EventNode> event_nodes; 

		public EventHandler(AIAgent p_agent) {
			agent = p_agent;
		}

		public void SetUp(List<EventNode> p_event_nodes) {
			event_nodes = p_event_nodes;
		}
		
		public void ReceiveEvents(string p_event) {
			foreach (var eventnode in event_nodes)
			{
				if (eventnode.event_id == p_event) {
					EventParser(eventnode);
				}
			}
		}

		private void EventParser(EventNode p_node) {
			switch (p_node.TITLE)
			{
				case EventFlag.AIEvent.LINK : {
					StrategyNode s_node = agent.AIAgentChart.agentNode.GetStrategyByID(p_node.MainValue);
					Debug.Log("Strategy " + s_node.id);
					if(s_node != null)
						agent.SetStrategy(s_node);
				};
				break;
			}
		}

	}
}