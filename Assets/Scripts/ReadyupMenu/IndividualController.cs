using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IndividualController : MonoBehaviour {

	public int playerID;
	public bool ready = false;
	public Text readyText;

	int cooldown = 7;

	void OnEnable(){
		ready = false;
		cooldown = 7;
	}

	void FixedUpdate () {
		if(cooldown > 0){
			cooldown--;
		}

		if ((GlobalPlayerManager.GetButton (playerID, "A") || GlobalPlayerManager.GetButton (playerID, "Start")) && cooldown == 0 && !ready) {
			ready = true;
			readyText.text = "READY";
			readyText.color = Color.green;
			cooldown = 7;
		}
		

		if (GlobalPlayerManager.GetButton (playerID, "B") && cooldown == 0 && ready) {
			ready = false;
			readyText.text = "UNREADY";
			readyText.color = Color.red;
			cooldown = 7;
		}

		if(GlobalPlayerManager.GetButton(playerID, "Select") && GlobalPlayerManager.GetButton(playerID, "B") && cooldown == 0){
			GlobalPlayerManager.playerGamepadID [playerID] = -1;
			gameObject.SetActive (false);
			cooldown = 7;
		}

		
	}
}
