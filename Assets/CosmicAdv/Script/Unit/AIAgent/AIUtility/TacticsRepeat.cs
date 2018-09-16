using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _AIAgent
{
	public class TacticsRepeat : TacticsHandler {
		private TacticsRepeatNode repeatNode;


		private Vector3[] testMovePattern = new Vector3[] {
			Vector3.right,
			Vector3.left,
		};
		private int movePatternIndex = 0;

		public TacticsRepeat(AIAgent p_aIAgent, TacticsNode p_tacticsNode) : base(p_aIAgent, p_tacticsNode) {
			repeatNode = (TacticsRepeatNode)p_tacticsNode;
		}

		public override Vector3 Planning() {
			//If no pattern or can't move yet
			if (repeatNode.customPattern.Length <= 0 || !IsAvilableToTakeAction()) 
				return Vector3.zero;
			
			if (repeatNode.random) {
				return GetRandomDir();
			} else {
				int patternIndex = (movePatternIndex + 1) % repeatNode.customPattern.Length;
				// bool moveFeasibility = true;
				// BaseUnit.MoveDir dir = new BaseUnit.MoveDir(testMovePattern[patternIndex], moveFeasibility);
				//if (_baseUnit.Move(dir)) {
					movePatternIndex = patternIndex;
					return testMovePattern[patternIndex];
				//}
			}
		}

		private Vector3 GetRandomDir() {
			int randomIndex = UnityEngine.Random.Range(0, repeatNode.customPattern.Length);
			return repeatNode.customPattern[randomIndex];
		}

	}
}