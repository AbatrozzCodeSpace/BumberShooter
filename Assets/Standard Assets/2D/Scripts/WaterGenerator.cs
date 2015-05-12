using UnityEngine;
using System.Collections;

public class WaterGenerator : MonoBehaviour {
	float SPAWN_INTERVAL=0.1f; // How much time until the next particle spawns
	float lastSpawnTime=float.MinValue; //The last spawn time
	public int PARTICLE_LIFETIME=3; //How much time will each particle live
	public Vector3 particleForce; //Is there a initial force particles should have?
	public DynamicParticle.STATES particlesState=DynamicParticle.STATES.WATER; // The state of the particles spawned
	public Transform particlesParent; // Where will the spawned particles will be parented (To avoid covering the whole inspector with them)
	public GameObject spawningWater;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//		if( lastSpawnTime+SPAWN_INTERVAL<Time.time ){ // Is it time already for spawning a new particle?
		//			GameObject newLiquidParticle=(GameObject)Instantiate(Resources.Load("DynamicParticle")); //Spawn a particle
		//			newLiquidParticle.GetComponent<Rigidbody2D>().AddForce( particleForce); //Add our custom force
		//			DynamicParticle particleScript=newLiquidParticle.GetComponent<DynamicParticle>(); // Get the particle script
		//			particleScript.SetLifeTime(PARTICLE_LIFETIME); //Set each particle lifetime
		//			particleScript.SetState(particlesState); //Set the particle State
		//			newLiquidParticle.transform.position=transform.position;// Relocate to the spawner position
		//			newLiquidParticle.transform.parent=particlesParent;// Add the particle to the parent container			
		//			lastSpawnTime=Time.time; // Register the last spawnTime			
		//		}
		Debug.Log("update water generator");
		if (lastSpawnTime+SPAWN_INTERVAL<Time.time ) {
			GameObject spawnedWater = (GameObject) (GameObject.Instantiate( spawningWater ) );
			spawnedWater.tag = "Water_u";
			Rigidbody2D rigid2d = spawnedWater.GetComponent<Rigidbody2D>();
			rigid2d.AddForce( particleForce );

			DynamicParticle particleScript=spawnedWater.GetComponent<DynamicParticle>(); // Get the particle script
			particleScript.SetLifeTime(PARTICLE_LIFETIME); //Set each particle lifetime
			particleScript.SetState(particlesState); //Set the particle State
			spawnedWater.transform.position = transform.position;// Relocate to the spawner position
			spawnedWater.transform.rotation = transform.rotation;
			spawnedWater.transform.parent = particlesParent;// Add the particle to the parent container	

			Debug.Log( "spawn from water generator " ); 
			lastSpawnTime=Time.time;
		}
	}
}
