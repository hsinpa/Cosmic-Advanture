using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  InputWrapper {
	public class SwipeInput : BaseInput {

		private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
		private Vector2 startTouch, swipeDelta;
		private bool isDragging = false;

		public Vector2 SwipeDelta { get { return swipeDelta; } }
		public bool SwipeLeft { get { return swipeLeft; } }
		public bool SwipeRight { get { return swipeRight; } }
		public bool SwipeDown { get { return swipeDown; } }
		public bool SwipeUp { get { return swipeUp; } }
		public bool Tap { get { return tap; } }

		public override bool IsRightClick() { return swipeRight;}
		public override bool IsLeftClick() { return swipeLeft;}
		public override bool IsFrontClick() { return swipeUp;}
		public override bool IsDownClick() { return swipeDown;}
		public override bool IsTap() { return tap;}

		public override bool IsRelease() {
			#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
				return (Input.GetMouseButtonUp(0));

			#elif UNITY_IOS || UNITY_ANDROID
				return ((Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled));
			#endif

		}

		private void Update() {
			tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

			#region Standalone Inputs
			if (Input.GetMouseButtonDown(0)) {
				tap = true;
				isDragging = true;
				startTouch = Input.mousePosition;
			} else if (Input.GetMouseButtonUp(0)) {
				isDragging = false;
				Reset();
			}

			#endregion

			#region Mobile Inputs
			if (Input.touches.Length > 0) {
				if (Input.touches[0].phase == TouchPhase.Began) {
					tap = true;
					isDragging = true;
					startTouch = Input.touches[0].position;
				} 
				else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) {
					isDragging = false;
					Reset();
				}
			}
			#endregion

			swipeDelta = Vector2.zero;
			if (isDragging) {
				if (Input.touches.Length > 0)
					swipeDelta = Input.touches[0].position - startTouch;
				else if (Input.GetMouseButton(0)) 
					swipeDelta = (Vector2)Input.mousePosition - startTouch;
			}
			
			int swipeRangeActivation = 60;
			if (swipeDelta.magnitude > swipeRangeActivation) {
				//Which direction
				float x = swipeDelta.x;
				float y = swipeDelta.y;

				if (Mathf.Abs(x) > Mathf.Abs(y)) {
					// Left or Right
					if (x < 0) 
						swipeLeft = true;
					else 
						swipeRight = true;
				} else {
					//Up or down
					if (y < 0) 
						swipeDown = true;
					else 
						swipeUp = true;
				}
				Reset(false);
			}

		}

		private void Reset(bool isDeepClean = true) {
				isDragging = false;

			if (isDeepClean) {
				startTouch = swipeDelta = Vector2.zero;
			}
		}

	}
}