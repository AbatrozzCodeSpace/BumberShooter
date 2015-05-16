using UnityEngine;
using System.Collections;

public class WalkerBody : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Water_u") {
			this.GetComponent<EnemyHealth>().applyDamage(other.gameObject.GetComponent<WaterProp>().damage);
			other.gameObject.GetComponent<DynamicParticle>().SetState(DynamicParticle.STATES.WATER_EFFECT);
		}
		else if (other.gameObject.tag == "Enemy" || other.gameObject.layer == 8) {
			transform.parent.GetComponent<Walker>().ChangeDirection();
		}
		else if ( other.gameObject.tag == "Player" ) {
			other.gameObject.GetComponent<CharacterHealth>().hitBehavior( gameObject );
		}

	}
}
