using UnityEngine;
using System.Collections;

public class Rubbish : MonoBehaviour {

	public GameObject minicontroller;
	public float speed = 0.5f;
	static int numberFallen;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals ("Player")) {
			int hitPlayerNumber = other.gameObject.GetComponent<RawPlayerController> ().playerNumber;			
			minicontroller.GetComponent<Controller> ().BadTouch (hitPlayerNumber);
			Destroy (gameObject);
		}
	}

	void OnBecameInvisible()
	{
		numberFallen++;
		if (numberFallen > 1)
			Destroy (gameObject);
	}
}
