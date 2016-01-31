using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuneMenuController : MonoBehaviour {

	public bool open = false;
	private float openLocation = -141;
	private float closeLocation = 141;

	public Image[] runes;
	public Text title;
	public Text description;
	private int currentIndex = 0;
	public int playerID;

	int cooldown = 7;

	void Start(){
		openLocation = transform.position.x;
		closeLocation = openLocation + 273;

		transform.position = new Vector3(closeLocation, transform.position.y, 0);
	}

	void FixedUpdate(){
		if(cooldown > 0)
			cooldown--;

		float location = closeLocation;
		if (open)
			location = openLocation;

		transform.position = transform.position * 0.8f + new Vector3 (location, transform.position.y, 0) * 0.2f;

		populateRunes();

		if(open){
			float axis = GlobalPlayerManager.GetAxis (playerID, "Horizontal");

			if(axis != 0 && cooldown == 0){
				int offset = -1;

				if (axis > 0)
					offset = 1;	

				offsetIndex (offset);
				
				cooldown = 7;
			}

			for(int i = 0; i<4; i++){
				if (i == currentIndex) {
					runes [i].color = Color.white;
					if (runes [i].enabled) {
						RuneData rune = GlobalPlayerManager.playerPowerups [playerID, i].GetComponent<RuneData>();
						title.text = rune.runeName + " rune (Tier " + rune.tier + ")";
						description.text = "" + rune.description;
					} else {
						title.text = "";
						description.text = "No rune currently selected";
					}

				} else {
					runes [i].color = Color.grey;
				}
			}
		}
	}

	void offsetIndex(int offset){
		int runeCount = 0;
		foreach(Image rune in runes){
			if (rune.enabled)
				runeCount++;
		}

		if(runeCount == 0){
			currentIndex = -1;
			return;
		}

		currentIndex += offset;
		if (currentIndex > 3)
			currentIndex = 0;
		if (currentIndex < 0)
			currentIndex = 3;

		while(!runes[currentIndex].enabled){
			currentIndex += offset;
			if (currentIndex > 3)
				currentIndex = 0;
			if (currentIndex < 0)
				currentIndex = 3;

		}
	}

	void populateRunes(){
		if(playerID != -1){
			for(int i = 0; i<4; i++){
				GameObject rune = GlobalPlayerManager.playerPowerups [playerID,i];
				if (rune == null) {
					runes[i].enabled = false;
				} else {
					runes [i].enabled = true;
					runes [i].sprite = rune.GetComponent<SpriteRenderer> ().sprite;
				}
			}
		}
	}
}
