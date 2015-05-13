using UnityEngine;
using System.Collections;

public class AttackBlow : MonoBehaviour {

	public float blowTimeout;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		blowTimeout -= Time.smoothDeltaTime;
		if( blowTimeout <= 0.0f ){
			Destroy( gameObject );
		}
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if( other.tag == "" ) {

		}
	}
}
