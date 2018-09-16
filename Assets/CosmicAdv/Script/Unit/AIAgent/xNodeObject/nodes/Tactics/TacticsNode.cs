using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace _AIAgent
{
    public abstract class TacticsNode : Node {
        public int responseTime = 2;
        [Output(connectionType = ConnectionType.Multiple)] public TacticsNode node;

        protected override void Init()
        {
            base.Init();
        }

        public override object GetValue(NodePort port) {
            if (port.fieldName == "node") {
                return this;
            }

            return null;
        }

    }
}