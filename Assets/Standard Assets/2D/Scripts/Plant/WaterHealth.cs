using UnityEngine;
using System.Collections;

public class WaterHealth : MonoBehaviour {

	public float waterLevel = 0f;
	public float waterMax = 1.0f;
	public float waterAmplifier = 2f;
	public bool isGrowth = false;
	public bool isFullGrowth = false;
	public bool isFullShrink = false;
	public bool isDecreasing = true;
	public float waterDecreaseSpeed = 0.3f;

	void Update(){
		if (isDecreasing) {
			waterLevel -= waterDecreaseSpeed * Time.deltaTime;
			if(waterLevel < 0){
				if(isGrowth){
					isGrowth = false;
				}
				waterLevel = 0;
			}
		}
	}

	public void IncreaseWater(float waterSensitivity){
		waterLevel += waterSensitivity * waterAmplifier;
		if (waterLevel > waterMax) {
			waterLevel = waterMax;
			if(!isGrowth){
				isGrowth = true;
			}
		}
	}
	
}
