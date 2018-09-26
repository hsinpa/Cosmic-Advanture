using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace _AIAgent
{
    [CreateAssetMenu(fileName = "AgentChart", menuName = "STP/AIAgent", order = 11)]
    public class AIAgentChart : NodeGraph {
        public AgentBaseNode agentNode;

        void Awake() {
            agentNode = AddNode<AgentBaseNode>();
        }

        [ContextMenu("Save")]
        private void SaveRecord() {
            Debug.Log(this.name);
            agentNode = NodeReader.Parse(this);
        }

    }
}