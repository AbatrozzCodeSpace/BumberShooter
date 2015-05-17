using UnityEngine;
using System.Collections;

public class PlantHealth : MonoBehaviour {

    public float waterLevel = 0f;
    public float waterMax = 1.0f;
    public float waterAmplifier = 2f;
    public float plantHP = 1.0f;
    public bool resistAcid = false;
    public bool isGrowth = false;
    public bool isFullGrowth = false;
    public bool isFullShrink = false;
    public bool isDecreasing = true;
    public float waterDecreaseSpeed = 0.1f;

	public GameObject waterLevelBar;

    void Update() {
        if (isDecreasing) {
            waterLevel -= waterDecreaseSpeed * Time.deltaTime;
            if (waterLevel < 0) {
                if (isGrowth) {
                    isGrowth = false;
                }
                waterLevel = 0;
            }
        }
		Vector3 currentScale = waterLevelBar.transform.localScale;
		currentScale.y = waterLevel/waterMax;
		waterLevelBar.transform.localScale = currentScale;
    }

    public void IncreaseWater(float waterSensitivity) {
        waterLevel += waterSensitivity * waterAmplifier;
        if (waterLevel > waterMax) {
            waterLevel = waterMax;
            if (!isGrowth) {
                isGrowth = true;
            }
        }
    }
    public void DecreaseHP(float decreasingHP) {
        plantHP -= decreasingHP;
        if (plantHP <= 0)
            Destroy(gameObject);
    }
}
