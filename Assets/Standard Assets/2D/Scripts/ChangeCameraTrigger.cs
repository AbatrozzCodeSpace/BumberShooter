using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class ChangeCameraTrigger : MonoBehaviour {
	public Transform target;
	public float cameraSize;
	public bool setDefault;

	void OnTriggerEnter2D( Collider2D other ) {
		if( other.tag == "Player" ) {
			Camera2DFollow c = GameObject.FindGameObjectWithTag("MainCamera" ).GetComponent<Camera2DFollow>();
			if( !setDefault ) {
				c.setNewTarget( target, cameraSize );
			} else {
				c.backToDefault();
			}
		}
	}

}
