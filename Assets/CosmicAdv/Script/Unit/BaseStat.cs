using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : MonoBehaviour{
	public float hp {
		get {
			return _hp;
		} set {
            _hp = value;
		}
	}
	[SerializeField]
	private float _hp;

    public float defense {
		get {
			return _defense;
		} set {
			_defense = value;
		}
	}
	[SerializeField]
	private float _defense;


    public float attack
    {
        get
        {
            return _attack;
        }
    }
    [SerializeField]
    private float _attack;


    public TeamLabel team_label;
	public bool controllable;

}
