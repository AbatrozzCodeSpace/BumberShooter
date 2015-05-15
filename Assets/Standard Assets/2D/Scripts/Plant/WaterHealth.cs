using UnityEngine;
using System.Collections;

public class WaterHealth : MonoBehaviour {

	public float waterLevel = 0f;
	public float waterMax = 100f;
	public float waterSensitity = 20f;
	public bool isGrowth = false;
	public bool isFullGrowth = false;
	public bool isFullShrink = false;
	public bool isDecreasing = true;
	public float waterDecreaseSpeed = 30f;

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

	public void IncreaseWater(){
		waterLevel += waterSensitity;
		if (waterLevel > waterMax) {
			waterLevel = waterMax;
			if(!isGrowth){
				isGrowth = true;
			}
		}
	}
	
}
