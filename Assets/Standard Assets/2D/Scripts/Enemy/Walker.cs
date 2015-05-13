using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour {

	public int direction;
	public float speed;
	float defaultHeadLocalPosX;
	float defaultBumperLocalPosX;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find ("CharacterRobotBoy");
		foreach(Collider2D collider in player.GetComponents<Collider2D>()){
			foreach(Collider2D enemyCollider in GetComponents<CircleCollider2D>()){
				Physics2D.IgnoreCollision(collider, enemyCollider);

			}
		}
		Transform head = transform.FindChild ("Head");
		defaultHeadLocalPosX = Mathf.Abs (head.localPosition.x);
		Transform bumper = transform.FindChild ("Bumper");
		defaultBumperLocalPosX = Mathf.Abs (bumper.localPosition.x);
		setObjectFromDirection ();
	}
	
	// Update is called once per frame
	void Update () {
		Walk ();
	}

	void Walk(){
		transform.position += new Vector3 (speed * direction, 0f, 0f);
	}

	public void ChangeDirection(){
		direction *= -1;
		setObjectFromDirection ();
	}
	void setObjectFromDirection(){
		Transform head = transform.FindChild ("Head");
		head.localPosition = new Vector3 (defaultHeadLocalPosX * direction, head.localPosition.y, head.localPosition.z);
		Transform bumper = transform.FindChild ("Bumper");
		bumper.localPosition = new Vector3 (defaultBumperLocalPosX * direction, bumper.localPosition.y, bumper.localPosition.z);
	}


}
