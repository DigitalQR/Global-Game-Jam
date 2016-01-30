using UnityEngine;
using System.Collections;

public class SideOnMovement : MonoBehaviour {

	private Rigidbody2D body;
	private RawPlayerController gamepad;
	public float speed;
	public float jumpHeight;
	private bool onGround = false;

	void Start () {
		gamepad = GetComponent<RawPlayerController>();
		body = GetComponent<Rigidbody2D> ();
		body.gravityScale = 1;
	}

	void FixedUpdate () {
		float y = body.velocity.y;

		if(onGround && gamepad.isPressingA())
		{
			y = jumpHeight;
			onGround = false;
		}


		Vector2 velocity = new Vector2 (gamepad.getHorizontalState() * speed, y);
		body.velocity = velocity;
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.transform.position.y < transform.position.y){
			onGround = true;
		}
	}
}
