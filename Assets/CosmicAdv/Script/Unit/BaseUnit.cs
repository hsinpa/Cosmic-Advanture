using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseUnit : MonoBehaviour {

	public GameObject modelPrefab;
	public BaseStat baseStat;

	public virtual void Move(Vector2 p_direction) {

	}
	
}
