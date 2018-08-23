using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  InputWrapper
{
	public class InputManager  {

		BaseInput _input;


		public InputManager(GameObject gameObject) {
			#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
				_input = gameObject.AddComponent<KeyboardInput>();

			#elif UNITY_IOS || UNITY_ANDROID
				_input = gameObject.AddComponent<Swipe>();
			#endif
		}

		public bool IsRightClick() {
			return _input.IsRightClick();
		}

		public bool IsLeftClick() {
			return _input.IsLeftClick();
		}

		public bool IsFrontClick() {
			return _input.IsFrontClick();
		}

        public bool IsDownClick() {
			return _input.IsDownClick();
		}

        public bool IsRelease() {
        	return _input.IsRelease();
        }

        public bool IsTap() {
        	return _input.IsTap();
        }

	}

}

