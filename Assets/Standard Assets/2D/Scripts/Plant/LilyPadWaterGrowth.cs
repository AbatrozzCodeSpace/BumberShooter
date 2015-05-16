using UnityEngine;
using System.Collections;

public class LilyPadWaterGrowth : MonoBehaviour {

	PlantHealth waterScript;
	float yRelative;
	public float speed = 1f;
	
	// Use this for initialization
	void Start () {
		yRelative =  transform.Find("Stem").localScale.y / transform.Find ("Stem").GetComponent<Renderer>().bounds.size.y ;
		waterScript = GetComponent<PlantHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (waterScript.isGrowth && !waterScript.isFullGrowth) {
			waterScript.isDecreasing = false;
			Lift (1);
		} else {
			waterScript.isDecreasing = true;
			if(!waterScript.isGrowth && !waterScript.isFullShrink){
				Lift(-1);
			}
		}
	}

	void Lift(int direction){
		float updateTime = Time.deltaTime;
		foreach (Transform child in transform) {
			if (child.name == "EndPoint") {
				continue;
			} 
			else if(child.name == "StartPoint"){
				continue;
			} else if (child.name == "Stem") {
				child.transform.localScale += new Vector3 (0f, speed * updateTime * direction * yRelative, 0f);
			} else {
				child.transform.position += new Vector3 (0f, speed * updateTime * direction, 0f);
			}
		}
	}


}
