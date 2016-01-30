using UnityEngine;
using System.Collections;

public class killPlayerOnEnter : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag.Equals("Player")){
			collider.gameObject.GetComponent<RawPlayerController> ().kill ();
		}
	}
}
