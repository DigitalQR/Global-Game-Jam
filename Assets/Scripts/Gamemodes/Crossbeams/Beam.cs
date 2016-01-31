using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {

	BoxCollider2D boxCollider;

	void Start () {
		boxCollider = GetComponent<BoxCollider2D>();	
		boxCollider.enabled = false;
		transform.localScale = new Vector3(transform.localScale.x, 0.05f, 1);
		Invoke ("StartBeam", 1);
	}
	

	void StartBeam(){
		transform.localScale = new Vector3(transform.localScale.x, 1, 1);
		boxCollider.enabled = true;
		Invoke ("DestroyBeam", 1);
	}

	void DestroyBeam(){
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag.Equals("Player")){
			collider.gameObject.GetComponent<RawPlayerController> ().kill ();
		}
	}
}
