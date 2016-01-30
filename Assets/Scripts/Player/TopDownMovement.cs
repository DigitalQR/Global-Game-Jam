using UnityEngine;
using System.Collections;

public class TopDownMovement : MonoBehaviour {

	private Rigidbody2D body;
	private RawPlayerController gamepad;
	public float speed;

	void Start () {
		gamepad = GetComponent<RawPlayerController>();
		body = GetComponent<Rigidbody2D> ();
		body.gravityScale = 0;
		body.drag = 10;
	}
	
	void FixedUpdate () {
		Vector2 velocity = new Vector2 (gamepad.getHorizontalState (), gamepad.getVerticalState ());
		if(velocity.sqrMagnitude != 0){
			body.velocity = velocity.normalized * speed;
		}
	}
}
