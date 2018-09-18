using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _AIAgent;

namespace CA_Terrain
{
	[CreateAssetMenu(fileName = "[STP]Unit", menuName = "STP/Unit/Basic", order = 1)]
	public class Unit_STP : STPObject
    {
        public AIAgentChart agentBehavior;

	}
}