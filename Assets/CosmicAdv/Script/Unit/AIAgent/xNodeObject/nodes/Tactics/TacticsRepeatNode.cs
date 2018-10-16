using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace _AIAgent
{
    [CreateNodeMenu("Tactics/RepeatNode"), NodeTint("#CCFFCC")]
    public class TacticsRepeatNode : TacticsNode {	
        public bool random;

        protected override void Init()
        {
            base.Init();
        }
    }
}