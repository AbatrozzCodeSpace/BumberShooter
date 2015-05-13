using UnityEngine;
using System.Collections;

public class UmbrellaUtility : MonoBehaviour {

	public float waterLevel = 0.0f;
	public float waterLevelForEachHit = 0.1f;

	public GameObject spawningWater;
	public float delaySpawningWaterTime = 0.5f;
	private float delaySpawningWaterTimeLeft;
	private bool m_skillPressed;

	public enum Mode {
		GRAPPLER = 0,
		WATERGUN = 1,
		NumberOfTypes
	};

	public Mode currentMode;

	public void toggleSkills() {
		currentMode = (Mode) ((int)(currentMode + 1) % (int)Mode.NumberOfTypes);
	}

	// Use this for initialization
	void Awake() {
		waterLevel = 0.0f;
		delaySpawningWaterTimeLeft = 0.0f;
		m_skillPressed = false;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		if( m_skillPressed ) {
			useSkill();
		} else {
			GetComponent<Grappler>().setGrappling( m_skillPressed );
		}
		delaySpawningWaterTimeLeft -= Time.smoothDeltaTime;
	}

	void OnTriggerEnter2D( Collider2D other ) {
		if ( other.tag == "Water" || other.tag == "Rain") {
			WaterProp waterProp = other.gameObject.GetComponent<WaterProp>();
			if(waterProp != null)
			waterLevel += waterProp.volume;
			if ( waterLevel > 1.0f ) {
				waterLevel = 1.0f;
			}
			//Destroy( other.gameObject );
			DynamicParticle particleScript = other.GetComponent<DynamicParticle>(); // Get the particle script
			particleScript.SetState(DynamicParticle.STATES.WATER_O);
		} else if ( other.tag == "Enemy" ) {
			// disable body colliders!
			//other.enabled = false;
		}
	}

	void OnTriggerExit2D( Collider2D other ) {

	}

	public void useSkill() {

		switch( currentMode ) {
		case Mode.GRAPPLER :
			GetComponent<Grappler>().setGrappling( m_skillPressed );
			break;
		case Mode.WATERGUN:
			if (waterLevel >= waterLevelForEachHit && delaySpawningWaterTimeLeft <= 0.0f ) {
				delaySpawningWaterTimeLeft = 0.0f;
				delaySpawningWaterTimeLeft += delaySpawningWaterTime;
				GameObject spawnedWater = (GameObject) (GameObject.Instantiate( spawningWater, GameObject.Find( "Tip" ).transform.position, Quaternion.identity ) );
				spawnedWater.tag = "Water_u";
				Rigidbody2D rigid2d = spawnedWater.GetComponent<Rigidbody2D>();
				float angle = this.gameObject.transform.localEulerAngles.z;
				int sign = (int)( Mathf.Sign(this.gameObject.transform.parent.root.localScale.x ) );
				Vector2 vel = new Vector2( sign * Mathf.Cos( angle * Mathf.Deg2Rad ) * 500, Mathf.Sin( angle *  Mathf.Deg2Rad ) * 500 );
				rigid2d.AddForce( vel );
				Debug.Log( angle + "," + sign + "," + vel );

			}
			break;
		}
	}

	public void setSkillPressed( bool skill ) {
		m_skillPressed = skill;
	}

}
