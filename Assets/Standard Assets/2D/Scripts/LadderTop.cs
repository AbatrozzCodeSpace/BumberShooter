using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class LadderTop : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D ( Collider2D other ) {
		if ( other.tag == "Player" ) {
			other.GetComponent<PlatformerCharacter2D>().onTopLadder = true;
		}
	}

	void OnTriggerExit2D ( Collider2D other ) {
		if ( other.tag == "Player" ) {
			other.GetComponent<PlatformerCharacter2D>().onTopLadder = false;
		}
	}
}
