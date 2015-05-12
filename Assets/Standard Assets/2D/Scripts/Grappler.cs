using UnityEngine;
using System.Collections;

public class Grappler : MonoBehaviour {

	private LineRenderer lineRenderer;
	private Transform originHookPosition;
	public float grapplerSpeed = 1.0f;
	public float swingForce = 1.0f;
	public LayerMask grapplable;
	public GameObject hookPrefab;
	public float lineWidth;
	public Material lineRendererMaterial;

	private bool isGrappling;
	private GameObject grappleHookInstance;
	private BoxCollider2D grappleHookCollider;

	public enum GrappleState {
		IDLE = 0,
		SHOOT,
		HOOK,
		RELEASE,
		SIZE
	}

	public GrappleState state;

	void Awake() {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.SetWidth(lineWidth, lineWidth);
		lineRenderer.SetColors( Color.black, Color.black );
		lineRenderer.SetVertexCount(2);
		lineRenderer.material.color = Color.black;
		lineRenderer.enabled = false;
		lineRenderer.material = lineRendererMaterial;

		state = GrappleState.IDLE;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		draw ();
	}

	void FixedUpdate() {
		Debug.Log (state);
		if( isGrappling ) {
			if( state == GrappleState.IDLE ) {
				state = GrappleState.SHOOT;
				float angle = this.gameObject.transform.localEulerAngles.z;
				int sign = (int)Mathf.Sign ( transform.root.localScale.x );
				grappleHookInstance = (GameObject) GameObject.Instantiate( hookPrefab ,GameObject.Find ( "Tip" ).transform.position, Quaternion.Euler( 0,0, sign * (angle + 90) - 90 ) );

				Rigidbody2D rigid = grappleHookInstance.GetComponent<Rigidbody2D>();

				Vector2 force = new Vector2( sign * Mathf.Cos(angle * Mathf.Deg2Rad) , Mathf.Sin (angle * Mathf.Deg2Rad) ) * 1000;
				rigid.AddForce( force );
				//grappleHookInstance.transform.parent = gameObject.transform;

			}

		} else {
			state = GrappleState.IDLE;
			if( grappleHookInstance != null ) {
				Destroy( grappleHookInstance );
				// also delete the joint in the character

			}
			DestroyImmediate( transform.root.gameObject.GetComponent<DistanceJoint2D>() );
		}
		grapple();
		//Debug.Log (transform.root.gameObject.GetComponent<Rigidbody2D>().a)

	}

	public void grapple() {
		if( state == GrappleState.HOOK ) {
			// add force to character


		}

	}

	void draw() {
		// draw the line
		if( isGrappling ) {
			lineRenderer.enabled = true;
			lineRenderer.SetPosition(0, GameObject.Find ("Tip").transform.position);
			lineRenderer.SetPosition(1, grappleHookInstance.transform.position);
		} else {
			lineRenderer.enabled = false;
		}
	}

	public void setGrappling( bool grap ) {
		isGrappling = grap;
	}
}
//
//using UnityEngine;
//using System.Collections;
//
//public class grapplingHook: movement 
//{
//	// Line start width
//	public float startWidth = 0.05f;
//	// Line end width
//	public float endWidth = 0.05f;
//	
//	LineRenderer line;
//	
//	//game objects
//	private GameObject player;
//	private GameObject hook;
//	private GameObject arm;
//	private GameObject hand;
//	private GameObject springJoint;
//	
//	//Booleans
//	public bool isGrappled;
//	private bool isCollide;
//	
//	//Floats
//	private float distance;
//	
//	// Use this for initialization
//	void Start () 
//	{
//		//Sets up the line drawing settings
//		line = gameObject.AddComponent<LineRenderer>();
//		line.SetWidth(startWidth, endWidth);
//		line.SetVertexCount(2);
//		line.material.color = Color.red;
//		line.enabled = false;
//		
//		//sets the game objects
//		player = GameObject.FindWithTag("Player");
//		arm = GameObject.FindWithTag("arm");
//		hand = GameObject.FindWithTag("hand");
//		hook = GameObject.FindWithTag("hook");
//	}
//	
//	// Update is called once per frame
//	void Update () 
//	{
//	}
//	
//	void FixedUpdate()
//	{
//		controllerUpdate();
//		grapple();
//		distanceFromHook();
//	}
//	
//	void grapple()
//	{
//		//if the trigger is pressed set the joint and draw a line
//		if(trigger_Value > 0.01 && isGrounded == false && distance <= 20)
//		{
//			setJoint();
//			isGrappled = true;	
//		}
//		
//		if(isGrappled == true)
//		{
//			drawLine();
//		}
//		
//		//If the A button is pressed during the swing cancel the joint
//		if(aButton == 1 && isGrappled == true)
//		{
//			isGrappled = false;
//			cancelJoint();
//		}
//	}
//	
//	void drawLine()
//	{
//		//sets the line positions start and end points
//		//and enables the line to be drawn
//		line.enabled = true;
//		line.SetPosition(0, arm.transform.position);
//		line.SetPosition(1, hook.transform.position);
//	}
//	
//	void setJoint()
//	{
//		Vector3 gravityChange = new Vector3 (0, -15, 0);
//		//sets the target objects hinge joint to be connected to the players
//		hook.hingeJoint.connectedBody = transform.rigidbody;
//		line.enabled = true;
//		rigidbody.constraints = RigidbodyConstraints.None;
//		rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationZ;		
//	}
//	
//	void cancelJoint()
//	{
//		//sets the targets hinge joint connected body to be null
//		hook.hingeJoint.connectedBody = null;
//		line.enabled = false;
//		Vector3 lastVel = rigidbody.velocity;
//		lastVel.z = 30;
//		rigidbody.AddForce(lastVel, ForceMode.VelocityChange);
//	}
//	
//	void handPhysics()
//	{
//		
//		float speed = 30;
//		
//		//creates a vector between the arm and the grapple point
//		Vector3 hingeVector = (hook.transform.position - transform.position).normalized;
//		Rigidbody handsRigidBody = hand.AddComponent<Rigidbody>();
//		hand.rigidbody.velocity = hingeVector * speed;
//	}
//	
//	void distanceFromHook()
//	{
//		distance = Vector3.Distance(transform.position, hook.transform.position);
//	}
//	
//	void OnCollisionEnter(Collision collision)
//	{
//		if(collision.gameObject.name == "hook")
//		{
//			hand.rigidbody.isKinematic = true;
//			isCollide = true;
//		}
//	}
//	
//	void OnCollisionExit(Collision collisio)
//	{
//		isCollide = false;
//	}
//}
