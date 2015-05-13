using UnityEngine;
using System.Collections;

public class BumperScript : MonoBehaviour {

	Collider2D collider;
	void Start(){
		collider = GetComponent<Collider2D> ();
	}

	void Update(){
		if (!Physics2D.IsTouchingLayers (collider)) {
			transform.parent.GetComponent<Walker>().ChangeDirection();
		}
	}
}
