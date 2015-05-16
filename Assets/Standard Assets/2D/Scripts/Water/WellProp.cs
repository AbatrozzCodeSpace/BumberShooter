using UnityEngine;
using System.Collections;

public class WellProp : MonoBehaviour {
    public float waterRate = 0.1f;
    public float damageRate = 0.05f;
    public float TIME_INTERVAL = 1f;
    public DynamicParticle.STATES wellType = DynamicParticle.STATES.WATER;
    float lastHitTime = float.MinValue;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    void OnTriggerStay2D(Collider2D other) {
        if (lastHitTime + TIME_INTERVAL < Time.time) {
            if (other.gameObject.tag == "Umbrella") {
                UmbrellaUtility umbrella = other.GetComponent<UmbrellaUtility>();
                umbrella.IncreaseWaterLevel(waterRate);
                umbrella.waterGunType = wellType;
                lastHitTime = Time.time;
            }
            else if (other.gameObject.tag == "Player") {
                other.GetComponent<CharacterHealth>().hitBehavior(this.gameObject);
                lastHitTime = Time.time;
            }
            else if (other.gameObject.tag == "Plant") {
                other.GetComponent<WaterReceiver>().IncreaseWater(waterRate);
                lastHitTime = Time.time;
            }
        }
    }
}
