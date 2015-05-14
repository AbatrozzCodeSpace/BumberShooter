using UnityEngine;
using System.Collections;

public class GunTurretAttack : MonoBehaviour {

	public float attackTime;
	public float cooldownTime;
	public float rotateAngleStart;
	public float rotateAngleEnd;
	public bool dualSide;
	float cooldownTimer = 0;
	float attackTimer = 0;
	int direction = 1;
	bool isActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		attackTimer += Time.deltaTime;
		if (attackTimer > attackTime) {
			attackTimer = 0;
			direction *= -1;
		}
		RotateGun (direction);
	}

	void RotateGun(int direction){
		if (direction > 0) {
			transform.localRotation = Quaternion.Slerp (Quaternion.Euler (new Vector3 (0, 0, rotateAngleStart)), Quaternion.Euler (new Vector3 (0, 0, rotateAngleEnd)), attackTimer / attackTime);
		} else {
			if (direction < 0){
				transform.localRotation = Quaternion.Slerp (Quaternion.Euler (new Vector3 (0, 0, rotateAngleEnd)), Quaternion.Euler (new Vector3 (0, 0, rotateAngleStart)), attackTimer / attackTime);
			}
		}
		WaterGenerator waterScript = GetComponentInChildren<WaterGenerator> ();
		float speed = waterScript.particleForce.magnitude;
		//print ("speed " + speed);

		float nowAngle = transform.localRotation.eulerAngles.z;
		print ("ss " + nowAngle+" " + (nowAngle / 180 * Mathf.PI) + " " +Mathf.Sin(nowAngle/180*Mathf.PI)+" "+Mathf.Cos(nowAngle/180*Mathf.PI));
		GetComponentInChildren<WaterGenerator> ().particleForce = new Vector3(-speed*Mathf.Sin(nowAngle/180*Mathf.PI), speed*Mathf.Cos(nowAngle/180*Mathf.PI),0);
	}






}
