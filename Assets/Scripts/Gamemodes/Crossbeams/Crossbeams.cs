using UnityEngine;
using System.Collections;

public class Crossbeams : MonoBehaviour {

	public GameObject beam;

	bool firstLaunch = true;

	// Use this for initialization
	void OnEnable () {
		if(firstLaunch){
			firstLaunch = false;
			return;
		}

		Invoke ("MakeBeam", frequency);
	}

	float frequency = 2;

	void MakeBeam(){
		if (Random.Range (0, 2) == 0) {
			Instantiate (beam, new Vector3 (0, Random.Range (-6, 6), 0), Quaternion.identity);
		} else {
			Instantiate (beam, new Vector3 (Random.Range (-9, 9), 0, 0), Quaternion.AngleAxis(90, Vector3.forward));
		}

		Invoke ("MakeBeam", frequency *= 0.95f);
	}
}
