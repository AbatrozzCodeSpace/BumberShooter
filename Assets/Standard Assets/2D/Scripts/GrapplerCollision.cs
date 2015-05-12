using UnityEngine;
using System.Collections;

public class GrapplerCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D( Collision2D coll ){
		GetComponent<Rigidbody2D>().isKinematic = true;
		GameObject go = GameObject.Find ("CharacterRobotBoy");
		DistanceJoint2D joint = go.AddComponent<DistanceJoint2D>();
		joint.enabled = false;
		joint.anchor = Vector2.zero;
		joint.connectedAnchor = transform.position;
		Vector2 a = new Vector2(  go.transform.position.x,  go.transform.position.y );
		Vector2 b = new Vector2(  transform.position.x,  transform.position.y );
		//Debug.Log (a + "," + b);
		joint.distance = Vector2.Distance( a,b );
		joint.maxDistanceOnly = true;
		joint.enableCollision = true;
		joint.enabled = true;
		GameObject umbrellaGo = GameObject.Find ("Umbrella");
		umbrellaGo.GetComponent<Grappler>().state = Grappler.GrappleState.HOOK;
		int sign = (int) Mathf.Sign (b.x-a.x);
		go.GetComponent<Rigidbody2D>().AddForce(new Vector2( sign * 1000 ,0 ));
	}
}
