using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public float hp = 1.0f;
	private float redTime;

	// Use this for initialization
	void Start () {
		redTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		redTime -= Time.smoothDeltaTime;
		if( redTime > 0.0f ) {
			SpriteRenderer[] spriteRenderers = transform.root.gameObject.GetComponentsInChildren<SpriteRenderer>();
			foreach ( SpriteRenderer sr in spriteRenderers ) {
				sr.material.color = Color.red;
			}
		} else {
			SpriteRenderer[] spriteRenderers = transform.root.gameObject.GetComponentsInChildren<SpriteRenderer>();
			foreach ( SpriteRenderer sr in spriteRenderers ) {
				sr.material.color = Color.white;
			}
		}
	}

	public void applyDamage( float damage ) {
		redTime = 0.5f;
		hp -= damage;
		if( hp <= 0.0f ) {
			hp = 0.0f;
			Destroy ( transform.root.gameObject );
		}
	}

}
