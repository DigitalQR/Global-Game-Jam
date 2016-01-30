using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {

	bool firstLaunch = true;

	// Use this for initialization
	void OnEnable () {
		if(firstLaunch){
			firstLaunch = false;
			return;
		}
		Debug.Log ("7");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
