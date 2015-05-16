using UnityEngine;
using System.Collections;

public class WaterReceiver : MonoBehaviour {

    PlantHealth waterScript;
    // Use this for initialization
    void Start() {
        waterScript = transform.parent.GetComponent<PlantHealth>();
        if (waterScript == null) {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        print("collide with water");
        if (other.tag == "Water" || other.tag == "Water_u" || other.tag == "Rain") {
            DynamicParticle particle = other.gameObject.GetComponent<DynamicParticle>();
            WaterProp waterProp = other.gameObject.GetComponent<WaterProp>();
            if (waterProp == null)
                return;
            if (particle != null && particle.currentState == DynamicParticle.STATES.ACID) {
                this.transform.parent.GetComponent<PlantHealth>().DecreaseHP(waterProp.damage);
                particle.SetState(DynamicParticle.STATES.WATER_EFFECT);
            } else {
                waterScript.IncreaseWater(waterProp.damage);
                Destroy(other.gameObject);
            }
        }
    }

}
