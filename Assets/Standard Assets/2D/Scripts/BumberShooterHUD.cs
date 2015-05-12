using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class BumberShooterHUD : MonoBehaviour {

	public GameObject textObject;
	public GameObject player;
	public GameObject umbrellaObject;

	public bool debugText;

	private CharacterHealth character;
	private UmbrellaUtility umbrella;

	public GameObject hpBar;
	public GameObject waterBar;
	public GameObject badge;

	private float lerpFactor;
	private float lerpFactorWater;
	private float currentHealth;
	private float oldHealth;
	private float currentWater;
	private float oldWater;
	private bool startLerp;
	private bool startLerpWater;
	public float lerpSpeed = 0.1f;

	public Sprite[] skillBadges;
	// Use this for initialization
	void Awake() {
		character = player.GetComponent<CharacterHealth>();
		umbrella = umbrellaObject.GetComponent<UmbrellaUtility>();
		lerpFactor = 0.0f;
		lerpFactorWater = 0.0f;
		currentHealth = character.health;
		oldHealth = character.health;
		currentWater = umbrella.waterLevel;
		oldWater = umbrella.waterLevel;
		startLerp = false;
		startLerpWater = false;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( debugText ) {
			Text text = textObject.GetComponent<Text>();
			text.enabled = true;
			text.text = "Health\t: "+ character.health * 100 +"\nWater\t: " + umbrella.waterLevel * 100 + "%\nSkill\t: " + umbrella.currentMode;
		} else {
			Text text = textObject.GetComponent<Text>();
			text.enabled = false;
		}

		// health bar
		if( currentHealth != character.health ) {
			// got a hit!
			currentHealth = character.health;
			startLerp = true;
		}

		if( startLerp ) {
			lerpFactor += lerpSpeed;
			hpBar.transform.localScale = Vector3.Lerp(new Vector3(oldHealth,1,1), new Vector3(currentHealth,1,1), lerpFactor );
			if( lerpFactor >= 1.0f ){
				lerpFactor = 0.0f;
				startLerp = false;
				currentHealth = character.health;
				oldHealth = currentHealth;
			}
		} else {
			hpBar.transform.localScale = new Vector3( character.health, 1,1 );
		}

		// water bar
		if( currentWater != umbrella.waterLevel ) {
			// got a hit!
			currentWater = umbrella.waterLevel;
			startLerpWater = true;
		}
		
		if( startLerpWater ) {
			lerpFactorWater += lerpSpeed;
			waterBar.transform.localScale = Vector3.Lerp(new Vector3(oldWater,1,1), new Vector3(currentWater,1,1), lerpFactorWater );
			if( lerpFactorWater >= 1.0f ){
				lerpFactorWater = 0.0f;
				startLerpWater = false;
				currentWater = umbrella.waterLevel;
				oldWater = currentWater;
			}
		} else {
			waterBar.transform.localScale = new Vector3( umbrella.waterLevel, 1,1 );
		}

		//Badge
		badge.GetComponent<Image>().sprite = skillBadges[(int) umbrella.currentMode];

	}

}
