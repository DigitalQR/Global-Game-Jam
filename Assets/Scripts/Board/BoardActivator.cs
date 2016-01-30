using UnityEngine;
using System.Collections;

public class BoardActivator : MonoBehaviour {

	public GameObject board;

	void Start () {
		GameObject controller = GameObject.FindGameObjectWithTag ("GameController");
		if (controller == null) {
			Instantiate (board);
		} else {
			controller.SetActive (true);
		}
		Destroy(gameObject);
	}

}
