using UnityEngine;
using System.Collections;

public class VineWaterGrowth : MonoBehaviour {

	PlantHealth waterScript;
	float yRelative;
	float speed = 1f;

	// Use this for initialization
	void Start () {
		yRelative =  transform.Find("Body").localScale.y / transform.Find ("Body").GetComponent<Renderer>().bounds.size.y ;
		waterScript = GetComponent<PlantHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (waterScript.isGrowth && !waterScript.isFullGrowth) {
			waterScript.isDecreasing = false;
			Growth (1);
		} else {
			waterScript.isDecreasing = true;
			if(!waterScript.isGrowth && !waterScript.isFullShrink){
				Growth(-1);
			}
		}
	}

	void Growth(int direction){
		float updateTime = Time.deltaTime;
		//int direction = 1;
		Transform bodyTransform = transform.FindChild ("Body").transform;
		bodyTransform.localScale += new Vector3 (0f, speed * updateTime * direction * yRelative, 0f);
		bodyTransform.position -= new Vector3 (0f, speed * updateTime * direction, 0f);
	}

	void Shrink(){

	}


}
