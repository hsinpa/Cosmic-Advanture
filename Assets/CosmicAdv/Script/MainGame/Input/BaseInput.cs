using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  InputWrapper {
	public abstract class BaseInput : MonoBehaviour {
		abstract public bool IsRightClick();
		abstract public bool IsLeftClick();
		abstract public bool IsFrontClick();
		abstract public bool IsDownClick();
		abstract public bool IsTap();
		abstract public bool IsRelease();

		// abstract public bool IsRightClickRelease();
		// abstract public bool IsLeftClickRelease();
		// abstract public bool IsFrontClickRelease();
		// abstract public bool IsDownClickRelease();
		// abstract public bool IsTapRelease();
	}
}