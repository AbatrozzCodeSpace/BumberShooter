using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class Ladder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D ( Collider2D other ) {
		if ( other.tag == "Player" ) {
			other.GetComponent<PlatformerCharacter2D>().onLadder = true;
		}
	}

	void OnTriggerExit2D ( Collider2D other ) {
		if ( other.tag == "Player" ) {
			other.GetComponent<PlatformerCharacter2D>().onLadder = false;
		}
	}
}
