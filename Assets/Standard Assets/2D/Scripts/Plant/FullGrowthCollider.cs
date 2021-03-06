﻿using UnityEngine;
using System.Collections;

public class FullGrowthCollider : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag != "GrowthPointer")
			return;
		if (transform.parent != other.transform.parent) {
			return;
		}
		PlantHealth waterScript = other.gameObject.GetComponentInParent<PlantHealth> ();
		if (waterScript != null) {
			waterScript.isFullGrowth = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag != "GrowthPointer")
			return;
		if (transform.parent != other.transform.parent) {
			return;
		}
		PlantHealth waterScript = other.gameObject.GetComponentInParent<PlantHealth> ();
		if (waterScript != null) {
			waterScript.isFullGrowth = false;
		}
	}
}
