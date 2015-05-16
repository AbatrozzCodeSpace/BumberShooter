using UnityEngine;
using System.Collections;

public class WaterGenerator : MonoBehaviour {
    public float SPAWN_INTERVAL = 0.1f; // How much time until the next particle spawns
    float lastSpawnTime = float.MinValue; //The last spawn time
    public int PARTICLE_LIFETIME = 1; //How much time will each particle live
    public Vector3 particleForce; //Is there a initial force particles should have?
    public DynamicParticle.STATES particlesState = DynamicParticle.STATES.WATER; // The state of the particles spawned
    public GameObject waterSource;
    public float damage = 0.05f;
    public float volume = 0.01f;
    public float gravity = 1.0f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (lastSpawnTime + SPAWN_INTERVAL < Time.time) {
            GameObject spawnedWater = (GameObject)(GameObject.Instantiate(waterSource));
            spawnedWater.tag = "Water";
            spawnedWater.layer = 4;
            //spawnedWater.transform.parent = this.gameObject.transform;
            Rigidbody2D rigid2d = spawnedWater.GetComponent<Rigidbody2D>();
            rigid2d.AddForce(particleForce);
            rigid2d.gravityScale = gravity;
            DynamicParticle particleScript = spawnedWater.GetComponent<DynamicParticle>(); // Get the particle script
            particleScript.SetLifeTime(PARTICLE_LIFETIME); //Set each particle lifetime
            particleScript.SetState(particlesState); //Set the particle State
            spawnedWater.transform.position = transform.position;// Relocate to the spawner position
            spawnedWater.transform.rotation = transform.rotation;

            WaterProp waterProp = spawnedWater.GetComponent<WaterProp>();
            if (waterProp != null) {
                waterProp.damage = damage;
                waterProp.volume = volume;
            }

            lastSpawnTime = Time.time;
        }
    }
}
