using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using XNode;

namespace _AIAgent
{
	[ NodeTint("#e19df9")]
    public class AgentBaseNode : Node {
        [Input(connectionType = ConnectionType.Multiple)] public List<StrategyNode> strategies = new List<StrategyNode>();
        public string default_strategy_id;

        public StrategyNode GetDefaultStrategy() {
            if (strategies.Count <= 0) return null;
            if (string.IsNullOrEmpty(default_strategy_id)) return strategies[0];

            StrategyNode s = strategies.Find(x => x.id == default_strategy_id);
            if (s == null) return strategies[0];
            
            return s;
        }

        public StrategyNode GetStrategyByID(string p_id) {
            if (strategies.Count <= 0) return null;
            StrategyNode s = strategies.Find(x => x.id == p_id);            
            return s;
        }

        protected override void Init()
        {
            base.Init();
            name = "AgentBaseNode";
        }

    }
}