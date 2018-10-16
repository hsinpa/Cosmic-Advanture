using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace _AIAgent
{
    public abstract class TacticsNode : Node {
        public float responseTime = 2;
        public enum PositionType  {
            WorldPosition,
            LocalPosition
        }

        public PositionType positionType;
        public Vector3[] customPattern;
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