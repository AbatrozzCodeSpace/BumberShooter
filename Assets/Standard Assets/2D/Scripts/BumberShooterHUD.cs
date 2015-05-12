using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class BumberShooterHUD : MonoBehaviour {

	public GameObject textObject;
	public GameObject player;
	public GameObject umbrellaObject;

	private CharacterHealth character;
	private UmbrellaUtility umbrella;

	// Use this for initialization
	void Awake() {
		character = player.GetComponent<CharacterHealth>();
		umbrella = umbrellaObject.GetComponent<UmbrellaUtility>();
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Text text = textObject.GetComponent<Text>();
		text.text = "Health\t: "+ character.health * 100 +"\nWater\t: " + umbrella.waterLevel * 100 + "%\nSkill\t: " + umbrella.currentMode;
	}
	
}
