using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace _AIAgent
{
    [CreateNodeMenu("Tactics/PursueNode"), NodeTint("#CCFFCC")]
    public class TacticsPursueNode : TacticsNode {
        public string walkPattern;

        protected override void Init()
        {
            base.Init();
        }

        public System.Type GetPursueType() {
            return typeof(BaseUnit);
        }

    }
}