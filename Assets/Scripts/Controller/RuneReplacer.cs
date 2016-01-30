using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuneReplacer : MonoBehaviour {

	public int playerID = -1;
	public GameObject newRune;
	public Image[] runes;
	private int currentIndex = 0;

	int cooldown = 0;

	void FixedUpdate(){
		if(cooldown > 0)
			cooldown--;

		float axis = GlobalPlayerManager.GetAxis(playerID, "Horizontal");

		if(cooldown == 0 && axis != 0){
			if (axis > 0)
				currentIndex++;
			else
				currentIndex--;

			if (currentIndex < 0)
				currentIndex = 4;
			if (currentIndex > 4)
				currentIndex = 0;

			cooldown = 7;
		}

		if(cooldown == 0 && GlobalPlayerManager.GetButton(playerID, "A")){
			if(currentIndex < 4){
				GlobalPlayerManager.playerPowerups [playerID, currentIndex] = newRune;
			}
			gameObject.SetActive (false);
		}

		for(int i = 0; i<5; i++){
			if (i == currentIndex)
				runes [i].color = Color.white;
			else
				runes [i].color = Color.grey;
		}
		
	}
}
