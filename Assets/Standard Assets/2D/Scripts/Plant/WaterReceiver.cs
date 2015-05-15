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
		if (other.tag == "Water" || other.tag=="Water_u" || other.tag == "Rain") {
			WaterProp waterProp = other.gameObject.GetComponent<WaterProp>();
			if(waterProp!=null){
				waterScript.IncreaseWater(waterProp.damage);
			}
			Destroy( other.gameObject);
		}
	}

}
