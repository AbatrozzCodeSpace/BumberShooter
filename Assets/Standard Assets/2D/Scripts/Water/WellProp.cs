using UnityEngine;
using System.Collections;

public class WellProp : MonoBehaviour {
    public float waterRate = 0.1f;
    public float damageRate = 0.05f;
    public float SPAWN_INTERVAL = 0.5f;
    float lastSpawnTime = float.MinValue;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    void OnTriggerStay2D(Collider2D other) {
        if (lastSpawnTime + SPAWN_INTERVAL < Time.time) {
            if (other.gameObject.tag == "Umbrella") {
                other.GetComponent<UmbrellaUtility>().IncreaseWaterLevel(waterRate);
            }
        }
    }
}
