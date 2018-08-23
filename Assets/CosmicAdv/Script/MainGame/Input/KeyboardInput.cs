using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  InputWrapper {
	public class KeyboardInput : BaseInput {
		private bool isHolding = false;

		public override bool IsRightClick() {
			return IsButtonDown(KeyCode.RightArrow, KeyCode.D);
		}
		
		public override bool IsLeftClick() { 
			return IsButtonDown(KeyCode.LeftArrow, KeyCode.A);
		}

		public override bool IsFrontClick() {
			return IsButtonDown(KeyCode.UpArrow, KeyCode.W);
		}

		public override bool IsDownClick() { 
			return IsButtonDown(KeyCode.DownArrow, KeyCode.S);
		}

		public override bool IsTap() {
			return IsButtonDown(KeyCode.Space);
		}

		private bool IsButtonDown(KeyCode p_mandatoryKeycode, KeyCode p_secondaryKeycode = KeyCode.Joystick6Button19) {
			if (Input.GetKeyDown(p_mandatoryKeycode) || Input.GetKeyDown(p_secondaryKeycode)) {
				isHolding = true;
				return true;
			}
			return false;
		}

		public override bool IsRelease() { 
			if (!Input.anyKey && isHolding) {
				isHolding = false;
				return true;
			}
			return false;
		}

	}
}