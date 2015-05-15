using UnityEngine;
using System.Collections;

public class GunTurretAttack : MonoBehaviour {

	public float attackTime;
	public float cooldownTime;
	public float rotateAngleStart;
	public float rotateAngleEnd;
	public bool dualMode;
	float cooldownTimer = 0;
	float attackTimer = 0;
	int direction = 1;
	bool isActive;

	// Use this for initialization
	void Start () {
		RotateGun (direction, 0);
		isActive = false;
		EnableGun (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isActive) {
			attackTimer += Time.deltaTime;
			if (attackTimer > attackTime) {
				attackTimer = 0;
				direction *= -1;
				isActive = false;
				EnableGun(false);
			}
			else{
				RotateGun(direction,attackTimer/attackTime);
			}
		} else {
			cooldownTimer += Time.deltaTime;
			if (cooldownTimer > cooldownTime){
				cooldownTimer = 0;
				isActive = true;
				EnableGun(true);
				if(!dualMode){
					direction *= -1;
				}
			}
			else{
				if(!dualMode)
					RotateGun(direction, cooldownTimer/cooldownTime);
			}
		}
	}

	void RotateGun(int direction, float progress){
		if (direction > 0) {
			transform.localRotation = Quaternion.Slerp (Quaternion.Euler (new Vector3 (0, 0, rotateAngleStart)), Quaternion.Euler (new Vector3 (0, 0, rotateAngleEnd)), progress);
		} else {
			if (direction < 0){
				transform.localRotation = Quaternion.Slerp (Quaternion.Euler (new Vector3 (0, 0, rotateAngleEnd)), Quaternion.Euler (new Vector3 (0, 0, rotateAngleStart)), progress);
			}
		}
		WaterGenerator waterScript = GetComponentInChildren<WaterGenerator> ();
		float speed = waterScript.particleForce.magnitude;
		//print ("speed " + speed);

		float nowAngle = transform.localRotation.eulerAngles.z;
		//print ("ss " + nowAngle+" " + (nowAngle / 180 * Mathf.PI) + " " +Mathf.Sin(nowAngle/180*Mathf.PI)+" "+Mathf.Cos(nowAngle/180*Mathf.PI));
		GetComponentInChildren<WaterGenerator> ().particleForce = new Vector3(-speed*Mathf.Sin(nowAngle/180*Mathf.PI), speed*Mathf.Cos(nowAngle/180*Mathf.PI),0);
	}

	void EnableGun(bool enable){
		GetComponentInChildren<WaterGenerator> ().enabled = enable;
	}






}
