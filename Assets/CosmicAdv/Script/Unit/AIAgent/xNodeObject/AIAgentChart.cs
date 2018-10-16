using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace _AIAgent
{
    [CreateAssetMenu(fileName = "AgentChart", menuName = "STP/AIAgent", order = 11)]
    public class AIAgentChart : NodeGraph {
        public AgentBaseNode agentNode {
            get {

                if (_agentNode == null)
                    _agentNode = (AgentBaseNode)nodes.Find((x) => x.GetType() == typeof(AgentBaseNode));
                    
                return _agentNode;
            }
        }
        private AgentBaseNode _agentNode;

        public delegate void SaveChartRecord();
        public event SaveChartRecord SaveEvent; 

        [ContextMenu("Save Record")]
        public void SaveRecord() {
            NodeReader.Parse(this);

            if (SaveEvent != null)
                SaveEvent();
        }

    }
}