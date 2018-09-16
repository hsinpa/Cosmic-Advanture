using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _AIAgent
{
	public abstract class TacticsHandler {
		private TacticsNode tacticsNode;
		private AIAgent aIAgent;

		private int period_left_to_act;
		
		public TacticsHandler(AIAgent p_aIAgent, TacticsNode p_tacticsNode) {
			aIAgent = p_aIAgent;
			tacticsNode = p_tacticsNode;
			period_left_to_act = p_tacticsNode.responseTime;
		}

		public abstract Vector3 Planning();

		protected bool IsAvilableToTakeAction() {
			bool takeAction = false;
			if (period_left_to_act - 1 <= 0) {
				//React
				takeAction = true;
				period_left_to_act = tacticsNode.responseTime;
			} else {
				period_left_to_act--;
			}
			return takeAction;
		}

	}
}