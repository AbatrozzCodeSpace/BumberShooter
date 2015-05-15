using UnityEngine;
using System.Collections;

public class GunTurretAttack : MonoBehaviour {

	public float attackTime = 3f;
	public float cooldownTime = 2f;
	public float rotateAngleStart = 0;
	public float rotateAngleEnd = 0;
	public bool dualMode;
	public bool lockTarget;
	float cooldownTimer = 0;
	float attackTimer = 0;
	int direction = 1;
	bool isActive;
	GameObject player;

	// Use this for initialization
	void Start () {
		RotateGun (direction, 0);
		isActive = false;
		EnableGun (false);
		if (lockTarget) {
			player = GameObject.Find("CharacterRobotBoy");
		}
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
				if(!lockTarget)
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
				if(lockTarget){
					float axesX = transform.position.x - player.transform.position.x;
					float axesY = transform.position.y - player.transform.position.y;
					float angle = Mathf.Atan( axesY / axesX ) * Mathf.Rad2Deg + 90;
					if (axesX < 0){
						angle -= 180;
					}
					if (angle<rotateAngleStart){
						angle = rotateAngleStart;
					}
					else{
						if(angle> rotateAngleEnd){
							angle = rotateAngleEnd;
						}
					}

					RotateGun(angle);


//					if ( axes.x < 0 ) {
//						angle = - ( ( -Mathf.Atan( -axes.y / axes.x ) * Mathf.Rad2Deg ) + 90 ) - 90;
//					}
				
				}
				else{
					if(!dualMode)
						RotateGun(direction, cooldownTimer/cooldownTime);
				}
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
		AdjustWaterForce ();
	}

	void RotateGun(float angle){
		transform.localRotation = Quaternion.Euler(0,0,angle);
		AdjustWaterForce ();
	}

	void EnableGun(bool enable){
		GetComponentInChildren<WaterGenerator> ().enabled = enable;
	}

	void AdjustWaterForce(){
		WaterGenerator waterScript = GetComponentInChildren<WaterGenerator> ();
		float speed = waterScript.particleForce.magnitude;
		//print ("speed " + speed);
		
		float nowAngle = transform.localRotation.eulerAngles.z;
		//print ("ss " + nowAngle+" " + (nowAngle / 180 * Mathf.PI) + " " +Mathf.Sin(nowAngle/180*Mathf.PI)+" "+Mathf.Cos(nowAngle/180*Mathf.PI));
		GetComponentInChildren<WaterGenerator> ().particleForce = new Vector3(-speed*Mathf.Sin(nowAngle/180*Mathf.PI), speed*Mathf.Cos(nowAngle/180*Mathf.PI),0);
	}






}
