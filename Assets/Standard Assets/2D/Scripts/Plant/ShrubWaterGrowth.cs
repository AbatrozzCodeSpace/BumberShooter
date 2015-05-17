using UnityEngine;
using System.Collections;

public class ShrubWaterGrowth : MonoBehaviour {

	PlantHealth waterScript;

	Vector3 fullSizePosition;
    Vector3 fullSizeLocalscale;
	float shrinkSize = 0.7f;

	bool isLastGrowth = false;

	public Color normalColor = new Color (0, 1f, 0);
	public Color growthColor = new Color (60f/255f, 160f/255f, 60f/255f);

	// Use this for initialization
	void Start () {
		fullSizePosition = transform.position;
        fullSizeLocalscale = transform.localScale;
		waterScript = GetComponent<PlantHealth> ();
		Shrink ();
		isLastGrowth = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (waterScript.isGrowth && !isLastGrowth) {
			Growth ();
		} else {
			if(!waterScript.isGrowth && isLastGrowth){
				Shrink ();
			}
		}

		isLastGrowth = waterScript.isGrowth;
	}

	void Growth(){
		transform.position = fullSizePosition;
		transform.localScale = fullSizeLocalscale;
		Component[] components = GetComponentsInChildren<BoxCollider2D>();

		foreach (Component component in components){
			((BoxCollider2D) component).enabled = true;
		}

		transform.Find ("Body").GetComponent<SpriteRenderer> ().color = growthColor;

	}

	void Shrink(){
		Transform body = transform.Find ("Body");
		Vector3 oldSize =  body.GetComponent<Renderer> ().bounds.size;
		transform.localScale = new Vector3 (shrinkSize, shrinkSize, 1f);
		Vector3 newSize = body.GetComponent<Renderer> ().bounds.size;
		transform.position += new Vector3 ((oldSize.x - newSize.x) / 2, (oldSize.y - newSize.y) / 2, 0);
		body.GetComponent<SpriteRenderer> ().color = normalColor;

		BoxCollider2D[] components = GetComponentsInChildren<BoxCollider2D>();
		foreach (BoxCollider2D component in components){
			if(!component.isTrigger)
				component.enabled = false;
		}

	}


}
