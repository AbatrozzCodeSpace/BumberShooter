using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class UmbrellaController : MonoBehaviour {

	public Sprite spriteClosed;
	public Sprite spriteOpen;

	private PolygonCollider2D umbrellaCollider;

	private bool m_skillPressed;
	public bool m_Attacking;


	public void setOpen(){
		if( !m_Attacking ) {
			gameObject.GetComponent<SpriteRenderer>().sprite = spriteOpen;
			umbrellaCollider.enabled = true;
		}
	}

	public void setClosed() {
		gameObject.GetComponent<SpriteRenderer>().sprite = spriteClosed;
		umbrellaCollider.enabled = false;
	}

	public void setUmbrellaRotation( Vector2 axes ) {

		if( !m_Attacking ) {
			float angle = Mathf.Atan( -axes.y / axes.x ) * Mathf.Rad2Deg;
			if (angle != angle)
				return;
			if ( axes.x < 0 ) {
				angle = - ( ( -Mathf.Atan( -axes.y / axes.x ) * Mathf.Rad2Deg ) + 90 ) - 90;
			}
			if ( transform.root.localScale.x < 0 ) { // to the left
				angle = -(angle + 90) - 90;
			}
			transform.rotation = Quaternion.Euler( new Vector3( 0,0, angle ) );
		}
	}

	public void setUmbrellaRotation( float angle ) {
		if ( !m_Attacking ) {
			transform.rotation = Quaternion.Euler( new Vector3( 0, 0, angle ) );
		}
	}

	// Use this for initialization
	void Awake() {
		umbrellaCollider = GetComponent<PolygonCollider2D>();
		m_skillPressed = false;
		m_Attacking = false;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ( CrossPlatformInputManager.GetButtonDown( "ToggleSkills" ) ) {
			GetComponent<UmbrellaUtility>().toggleSkills();
		}
		GetComponent<UmbrellaUtility>().setSkillPressed( m_skillPressed );
	}

	void FixedUpdate() {

	}

	public void useSkill() {
		GetComponent<UmbrellaUtility>().useSkill();
	}

	public void setSkillPressed( bool skill ) {
		m_skillPressed = skill;
	}
}
