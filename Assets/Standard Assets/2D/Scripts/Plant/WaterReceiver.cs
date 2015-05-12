using UnityEngine;
using System.Collections;

public class WaterReceiver : MonoBehaviour {

	WaterHealth waterScript;
	// Use this for initialization
	void Start () {
		waterScript = transform.parent.GetComponent<WaterHealth> ();
		if (waterScript == null) {
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		print ("collide with water");
		if (other.tag == "Water" || other.tag=="Water_u") {
			waterScript.IncreaseWater ();
			Destroy( other.gameObject);
		}
	}

}
