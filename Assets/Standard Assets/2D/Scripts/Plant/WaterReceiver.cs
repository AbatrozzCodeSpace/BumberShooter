using UnityEngine;
using System.Collections;

public class WaterReceiver : MonoBehaviour {
    public float wellReceiverRate = 0.1f;
    PlantHealth waterScript;
    // Use this for initialization
    void Start() {
        waterScript = transform.parent.GetComponent<PlantHealth>();
        if (waterScript == null) {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    public void IncreaseWater(float waterAmount) {
        waterScript.IncreaseWater(waterAmount);
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Water" || other.tag == "Water_u" || other.tag == "Rain") {
            DynamicParticle particle = other.gameObject.GetComponent<DynamicParticle>();
            WaterProp waterProp = other.gameObject.GetComponent<WaterProp>();
            if (particle != null && particle.currentState == DynamicParticle.STATES.ACID) {
                if (waterScript.resistAcid) {
                    particle.SetState(DynamicParticle.STATES.WATER_EFFECT);
                    return;
                }
                this.transform.parent.GetComponent<PlantHealth>().DecreaseHP(waterProp.damage);
                particle.SetState(DynamicParticle.STATES.WATER_EFFECT);
            }
            waterScript.IncreaseWater(waterProp.damage);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Well") {
            print("collide with well");
            waterScript.IncreaseWater(other.gameObject.GetComponent<WellProp>().waterRate * wellReceiverRate);
        }
    }

}
