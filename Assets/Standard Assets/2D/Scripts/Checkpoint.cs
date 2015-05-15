using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public GameObject spawnPoint;

	void Awake() {
		DontDestroyOnLoad( spawnPoint );
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if( other.tag == "Player" ) {
//			other.GetComponent<CharacterSpawner>().spawnPosition = gameObject;
			PlayerPrefs.SetFloat( "SpawnX", gameObject.transform.position.x );
			PlayerPrefs.SetFloat( "SpawnY", gameObject.transform.position.y );
			Destroy ( gameObject.GetComponent<ParticleSystem>() );

		}
	}

}
