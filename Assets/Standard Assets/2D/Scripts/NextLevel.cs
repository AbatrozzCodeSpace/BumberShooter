using System;
using UnityEngine;


public class NextLevel : MonoBehaviour {
	public string levelName;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Application.LoadLevel(levelName);
		}
	}
}

