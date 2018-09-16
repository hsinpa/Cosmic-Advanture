using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace _AIAgent
{
	[CreateNodeMenu("EventNode"), NodeTint("#9dc8f2")]
    public class EventNode : Node {
        [Output(connectionType = ConnectionType.Override)] public EventNode node;
        public string description;
        public string event_id;
        public string TITLE;
        public string MainValue;

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