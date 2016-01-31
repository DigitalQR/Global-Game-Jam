using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainConnectionController : MonoBehaviour {

	public GameObject[] players;
	public Text countDownText;
	private bool gameStarting = false;
	private int countDown = 0;

	void Start(){
		for(int i = 0; i<4; i++){
			players [i].SetActive (false);
		}
	}

	void FixedUpdate(){
		for(int i = 0; i<5; i++){
			if(!isGamepadTaken(i)){
				if(GetAnyInput(i)){
					int ID = getFirstFreePlayerID();

					if(ID != -1){
						GlobalPlayerManager.playerGamepadID [ID] = i;
						players [ID].SetActive (true);
					}
				}
			}
		}

		if (GlobalPlayerManager.GetActivePlayerCount () > 1 && isEveryoneReady ()) {
			if(countDown == -1){
				gameStarting = true;
				countDown = 3;
				countDownText.text = "All players are ready";
				Invoke("CountDownAndStart", 1);
			}
		} else {
			gameStarting = false;
			countDown = -1;
			countDownText.text = "There are players still not ready";
		}
	}

	void CountDownAndStart(){
		if(gameStarting){
			countDownText.text = "" + countDown;

			if(countDown == 0){
				SceneManager.LoadScene("gameboard");
			}
			countDown--;
			Invoke("CountDownAndStart", 1);
		}
	}

	bool isEveryoneReady(){
		bool ready = true;
		for(int i = 0; i<4; i++){
			if(GlobalPlayerManager.playerGamepadID[i] != -1){
				if (!players [i].GetComponent<IndividualController> ().ready)
					ready = false;
			}
		}
		return ready;
	}

	bool isGamepadTaken(int gamepad){
		for(int i = 0; i<4; i++){
			if (GlobalPlayerManager.playerGamepadID [i] == gamepad)
				return true;
		}
		return false;
	}

	int getFirstFreePlayerID(){
		for (int i = 0; i < 4; i++) {
			if(GlobalPlayerManager.playerGamepadID[i] == -1){
				return i;
			}
		}
		return -1;
	}

	//Only input not allowed is Select+B
	bool GetAnyInput(int controlNumber){
		bool anyButton = Input.GetAxis ("Gamepad " + controlNumber + " Horizontal") != 0 ||
		                 Input.GetAxis ("Gamepad " + controlNumber + " Vertical") != 0 ||
		                 Input.GetButton ("Gamepad " + controlNumber + " A") ||
		                 Input.GetButton ("Gamepad " + controlNumber + " B") ||
		                 Input.GetButton ("Gamepad " + controlNumber + " Start") ||
		                 Input.GetButton ("Gamepad " + controlNumber + " Select");

		bool exiting = Input.GetButton ("Gamepad " + controlNumber + " B") && Input.GetButton ("Gamepad " + controlNumber + " Select");

		return anyButton && !exiting;
	}
}
