using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class CharacterHealth : MonoBehaviour {
    public float health = 1.0f;					// The player's health.
    public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
    public float rainDamagePeriod = 0.1f;
    public float loseControlPeriod = 0.2f;		// How frequently the player can be damaged.
    //public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
    public float hurtForce = 100f;				// The force with which the player is pushed when hurt.
    public float damageAmount = 0.3f;			// The amount of damage to take when enemies touch the player

    //private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
    private float lastHitTime;					// The time at which the player was last hit.
    //private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
    private PlatformerCharacter2D character;		// Reference to the PlatformerCharacter2D script.
    private Platformer2DUserControl platformControl;
    private Animator anim;						// Reference to the Animator on the player


    void Awake() {
        // Setting up references.
        character = GetComponent<PlatformerCharacter2D>();
        platformControl = GetComponent<Platformer2DUserControl>();
        //healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        lastHitTime = -repeatDamagePeriod;
        // Getting the intial scale of the healthbar (whilst the player has full health).
        //healthScale = healthBar.transform.localScale;
    }

    public void hitBehavior(GameObject other) {
        // If the colliding gameobject is an Enemy...
        if (other.tag == "Water" || other.tag == "Obstacles" || other.tag == "Water_u") {
            Debug.Log("damage by something--------------");
            // ... and if the time exceeds the time of the last hit plus the time between hits...
            if (Time.time > lastHitTime + repeatDamagePeriod) {
                if (health > 0f) {
                    WaterProp waterDamage = other.GetComponent<WaterProp>();
                    if (waterDamage != null) {
                        damageAmount = waterDamage.damage;
                    }
                    if (damageAmount > 0f) {
                        TakeDamage(other.transform);
                        lastHitTime = Time.time;
                    }
                }
                else {
                    Collider2D[] cols = GetComponents<Collider2D>();
                    foreach (Collider2D c in cols) {
                        //c.isTrigger = true;
                    }
                    GetComponent<PlatformerCharacter2D>().enabled = false;
                    anim.SetTrigger("Die");
                    platformControl.canControl = false;
					Application.LoadLevel(Application.loadedLevelName);
				}
            }
        }
        else if (other.tag == "Enemy") {
            if (Time.time > lastHitTime + repeatDamagePeriod) {
                if (health > 0f) {
                    EnemyDamage enemyDamage = other.GetComponent<EnemyDamage>();
                    if (enemyDamage != null) {
                        damageAmount = enemyDamage.damage;
                    }
                    if (damageAmount > 0f) {
                        TakeDamage(other.transform);
                        lastHitTime = Time.time;
                    }
                }
                else {
                    Collider2D[] cols = GetComponents<Collider2D>();
                    foreach (Collider2D c in cols) {
                        //c.isTrigger = true;
                    }
                    GetComponent<PlatformerCharacter2D>().enabled = false;
                    anim.SetTrigger("Die");
                    platformControl.canControl = false;
					Application.LoadLevel(Application.loadedLevelName);
                }
            }
        }
        else if (other.tag == "Well") {
            if (Time.time > lastHitTime + repeatDamagePeriod) {
                if (health > 0f) {
                    WellProp wellProp = other.GetComponent<WellProp>();
                    if (wellProp != null) {
                        damageAmount = wellProp.damageRate;
                    }
                    if (damageAmount > 0f) {
                        TakeDamage(other.transform);
                        lastHitTime = Time.time;
                    }
                }
                else {
                    Collider2D[] cols = GetComponents<Collider2D>();
                    foreach (Collider2D c in cols) {
                        //c.isTrigger = true;
                    }
                    GetComponent<PlatformerCharacter2D>().enabled = false;
                    anim.SetTrigger("Die");
                    platformControl.canControl = false;
					Application.LoadLevel(Application.loadedLevelName);
				}
            }
        }
        else if (other.tag == "Rain") {

            // ... and if the time exceeds the time of the last hit plus the time between hits...
            // ... and if the player still has health...
            if (health > 0f) {
                // ... take damage and reset the lastHitTime.
                WaterProp rainDamage = other.GetComponent<WaterProp>();
                if (rainDamage != null) {
                    damageAmount = rainDamage.damage;
                }
                if (damageAmount > 0f) {
                    TakeDamage(other.transform);
                }
            }
            else {
                Collider2D[] cols = GetComponents<Collider2D>();
                foreach (Collider2D c in cols)
                    GetComponent<PlatformerCharacter2D>().enabled = false;
                anim.SetTrigger("Die");
                platformControl.canControl = false;
				Application.LoadLevel(Application.loadedLevelName);
			}
			
            if (other.tag == "Obstacles") {
                return;
            }
            Destroy(other);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        OnCollisionStay2D(col);
    }

    void OnCollisionStay2D(Collision2D col) {
        hitBehavior(col.gameObject);
        //		// If the colliding gameobject is an Enemy...
        //		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Water" || col.gameObject.tag == "Obstacles" || col.gameObject.tag == "Water_u") {
        //			Debug.Log ("damage by something--------------");
        //			// ... and if the time exceeds the time of the last hit plus the time between hits...
        //			if (Time.time > lastHitTime + repeatDamagePeriod) {
        //				// ... and if the player still has health...
        //				if (health > 0f) {
        //					// ... take damage and reset the lastHitTime.
        //					WaterProp damage = col.gameObject.GetComponent<WaterProp> ();
        //					if (damage != null) {
        //						damageAmount = damage.damage;
        //					}
        //					if (damageAmount > 0f) {
        //						TakeDamage (col.transform);
        //						lastHitTime = Time.time;
        //					}
        //				}
        //				// If the player doesn't have health, do some stuff, let him fall into the river to reload the level.
        //				else {
        //					// Find all of the colliders on the gameobject and set them all to be triggers.
        //					Collider2D[] cols = GetComponents<Collider2D> ();
        //					foreach (Collider2D c in cols) {
        //						//c.isTrigger = true;
        //					}
        //					
        //					// Move all sprite parts of the player to the front
        //					//SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
        //					//foreach(SpriteRenderer s in spr)
        //					//{
        //					//	s.sortingLayerName = "UI";
        //					//}
        //					
        //					// ... disable user Player Control script
        //					GetComponent<PlatformerCharacter2D> ().enabled = false;
        //					
        //					// ... disable the Gun script to stop a dead guy shooting a nonexistant bazooka
        ////					GetComponentInChildren<Gun>().enabled = false;
        //					
        //					// ... Trigger the 'Die' animation state
        //					anim.SetTrigger ("Die");
        //					platformControl.canControl = false;
        //				}
        //			}
        //			if (col.gameObject.tag == "Obstacles") {
        //				return;
        //			}
        //			Destroy (col.gameObject);
        //		} else if (col.gameObject.tag == "Rain") {
        //			Debug.Log ("damage by rain--------------");
        //			// ... and if the time exceeds the time of the last hit plus the time between hits...
        //			// ... and if the player still has health...
        //			if (health > 0f) {
        //				// ... take damage and reset the lastHitTime.
        //				WaterProp damage = col.gameObject.GetComponent<WaterProp> ();
        //				if (damage != null) {
        //					damageAmount = damage.damage;
        //				}
        //				if (damageAmount > 0f) {
        //					TakeDamage (col.transform);
        //					lastHitTime = Time.time;
        //				}
        //			} else {
        //				Collider2D[] cols = GetComponents<Collider2D> ();
        //				foreach (Collider2D c in cols)
        //					GetComponent<PlatformerCharacter2D> ().enabled = false;
        //				anim.SetTrigger ("Die");
        //				platformControl.canControl = false;
        //			}
        //
        //			if (col.gameObject.tag == "Obstacles") {
        //				return;
        //			}
        //			Destroy (col.gameObject);
        //		}
    }

    void TakeDamage(Transform enemy) {
        // Make sure the player can't jump.
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        if (enemy.tag != "Rain")
            platformControl.canControl = false;

        // Create a vector that's from the enemy to the player with an upwards boost.
        Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

        // Add a force to the player in the direction of the vector and multiply by the hurtForce.
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        if (enemy.tag != "Rain") {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(hurtVector * hurtForce);
        }
        // Reduce the player's health by 10.
        EnemyDamage enemyDamage = enemy.GetComponent<EnemyDamage>();
        if (enemyDamage != null) {
            damageAmount = enemyDamage.damage;
        }
        health -= damageAmount;


        if (health <= 0) {
            anim.SetTrigger("Die");
            platformControl.canControl = false;
        }
        else {
            anim.SetFloat("Speed", 0.0f);
            anim.SetBool("Ground", true);
            if (enemy.tag != "Rain")
                anim.SetTrigger("Hurt");
        }
        // Update what the health bar looks like.
        //UpdateHealthBar();

        // Play a random clip of the player getting hurt.
        //int i = Random.Range (0, ouchClips.Length);
        //AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
    }

    void Update() {
        if (health > 0) {
            if (Time.time > lastHitTime + repeatDamagePeriod) {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            }
            if (Time.time > lastHitTime + loseControlPeriod) {
                platformControl.canControl = true;
            }
        }
    }

    public void UpdateHealthBar() {
        // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        //		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

        // Set the scale of the health bar to be proportional to the player's health.
        //		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
    }

    public void heal(float amount) {
        health += amount;
        if (health >= 1.0f) {
            health = 1.0f;
        }
    }
}
