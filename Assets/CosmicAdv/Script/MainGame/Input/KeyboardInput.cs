using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  InputWrapper {
	public class KeyboardInput : BaseInput {
		private bool isHolding = false;

		public override bool IsRightClick() { 
			isHolding = true;
			return (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D));
		}
		
		public override bool IsLeftClick() { 
			isHolding = true;
			return (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A));
		}

		public override bool IsFrontClick() {
			isHolding = true;
			return (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W));
		}

		public override bool IsDownClick() { 
			isHolding = true;
			return  (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S));
		}

		public override bool IsTap() {
			isHolding = true;
			return  (Input.GetKeyDown(KeyCode.Space));
		}

		public override bool IsRelease() { 
			if (Input.anyKey && isHolding) {
				isHolding = false;
				return true;
			} 
			return false;
		}

	}
}