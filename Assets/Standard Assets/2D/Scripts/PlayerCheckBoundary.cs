using UnityEngine;
using System.Collections;

public class PlayerCheckBoundary : MonoBehaviour {

	bool isActive;

	// Use this for initialization
	void Start () {
		isActive = false;
		EnableObject (false);
	}


	void OnTriggerEnter2D(Collider2D other){
		if (!isActive) {
			isActive = true;
			EnableObject(true);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (isActive) {
			isActive = false;
			EnableObject(false);
		}
	}

	void EnableObject(bool enable){
		print ("set object " + enable);
		foreach (Transform child in transform.parent.transform) {
			if(child != transform)
				child.gameObject.SetActive(enable);
		}
	}

}
