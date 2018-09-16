using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace _AIAgent
{
	[CreateNodeMenu("StrategyNode"), NodeTint("#f2c58a")]
    public class StrategyNode : Node {
        public string id;

        [Input(connectionType = ConnectionType.Override)] public TacticsNode tacticsNode;
        [Input(connectionType = ConnectionType.Multiple)] public List<EventNode> eventNodes;

        [Output(connectionType = ConnectionType.Override)] public StrategyNode node;
		
        protected override void Init()
        {
            base.Init();
        }

        public override object GetValue(NodePort port) {
            if (port.fieldName == "node") {
                return node;
            }

            return null;
        }


    }
}