using UnityEngine;
using System.Collections;

public class WalkerBody : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Enemy" || other.gameObject.layer == 8) {
			transform.parent.GetComponent<Walker>().ChangeDirection();
		}
		if ( other.gameObject.tag == "Player" ) {
			other.gameObject.GetComponent<CharacterHealth>().hitBehavior( gameObject );
		}
	}
}
