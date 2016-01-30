using UnityEngine;
using System.Collections;

public class RawPlayerController : MonoBehaviour {
	//Checks for input of a specific player

	public int playerNumber = -1;

	public void kill(){
		Destroy (gameObject);
	}

	public float getHorizontalState(){
		return GlobalPlayerManager.GetAxis (playerNumber, "Horizontal");
	}

	public float getVerticalState(){
		return GlobalPlayerManager.GetAxis (playerNumber, "Vertical");
	}

	public bool isPressingUp(){
		float value = GlobalPlayerManager.GetAxis(playerNumber, "Vertical");
		return value == 1;
	}

	public bool isPressingDown(){
		float value = GlobalPlayerManager.GetAxis(playerNumber, "Vertical");
		return value == -1;
	}

	public bool isPressingRight(){
		float value = GlobalPlayerManager.GetAxis(playerNumber, "Horizontal");
		return value == 1;
	}

	public bool isPressingLeft(){
		float value = GlobalPlayerManager.GetAxis(playerNumber, "Horizontal");
		return value == -1;
	}

	public bool isPressingA(){
		return GlobalPlayerManager.GetButton (playerNumber, "A");
	}

	public bool isPressingB(){
		return GlobalPlayerManager.GetButton (playerNumber, "B");
	}
		
	public bool isPressingStart(){
		return GlobalPlayerManager.GetButton (playerNumber, "Start");
	}

	public bool isPressingSelect(){
		return GlobalPlayerManager.GetButton (playerNumber, "Select");
	}
}
