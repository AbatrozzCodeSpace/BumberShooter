using UnityEngine;
using System.Collections;

public class Medkit : MonoBehaviour {

	public float healing = 0.25f;
	public bool pickable;

	// Use this for initialization
	void Start () {
		if ( pickable ) {
			GetComponent<BoxCollider2D>().isTrigger = true;
		} else {
			GetComponent<BoxCollider2D>().isTrigger = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if ( pickable ) {
			if ( other.gameObject.tag == "Player" ) {
				other.GetComponent<CharacterHealth>().heal( healing );
				// TODO spawn picking effect
				Destroy ( gameObject );

			}
		} else {
			if ( other.gameObject.tag == "Blow" ) {
				FindObjectOfType<CharacterHealth>().heal( healing );
				// TODO spawn picking effect
				Destroy ( gameObject );
				
			}
		}
	}

	void OnCollisionEnter2D( Collision2D other ) {
		if ( other.gameObject.tag == "Blow" ) {
			FindObjectOfType<CharacterHealth>().heal( healing );
			Destroy ( gameObject );
		}
	}
}
