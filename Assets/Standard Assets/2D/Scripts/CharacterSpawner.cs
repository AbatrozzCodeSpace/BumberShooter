using UnityEngine;
using System.Collections;

public class CharacterSpawner : MonoBehaviour {

	//public GameObject spawnPosition;

	void Awake() {
		//DontDestroyOnLoad( spawnPosition );
	}

	// Use this for initialization
	void Start () {
		if( PlayerPrefs.HasKey( "SpawnX" ) ) {
			transform.position = new Vector3( PlayerPrefs.GetFloat( "SpawnX" ), PlayerPrefs.GetFloat( "SpawnY" ) );

		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnApplicationQuit() {
		PlayerPrefs.DeleteKey("SpawnX");
		PlayerPrefs.DeleteKey("SpawnY");
	}
}
