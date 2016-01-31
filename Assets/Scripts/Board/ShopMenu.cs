using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopMenu : MonoBehaviour {

	public bool open = false;
	public bool canExit = true;
	private float openLocation = -141;
	private float closeLocation = 141;

	public Image[] runes;
	public Text title;
	public Text description;
	public Text instructionText;
	public Image newRuneImage;
	private int currentIndex = 0;
	public int playerID;
	int cooldown = 7;

	public int[] selectedIndices = {-1,-1};

	public void ResetShop(){
		open = true;
		canExit = true;
		newRuneImage.enabled = false;
		currentIndex = 0;
	}

	void Start (){
		openLocation = transform.position.y;
		closeLocation = openLocation + 1000;

		transform.position = new Vector3(transform.position.x, closeLocation, 0);
	}

	void FixedUpdate(){
		if (cooldown > 0)
			cooldown--;

		if (!has2RunesInSameLevel ())
			open = false;

		float location = closeLocation;
		if (open)
			location = openLocation;

		transform.position = transform.position * 0.8f + new Vector3 (transform.position.x, location, 0) * 0.2f;
		if(canExit) populateRunes ();

		if(open && canExit){
			if (selectedIndices [0] == -1 || selectedIndices [1] == -1) {
				float axis = GlobalPlayerManager.GetAxis (playerID, "Horizontal");
				instructionText.text = "Select 2 runes of the same tier to trade";

				if (axis != 0 && cooldown == 0) {
					int offset = -1;

					if (axis > 0)
						offset = 1;	

					offsetIndex (offset);

					cooldown = 7;
				}

				for (int i = 0; i < 4; i++) {
					if (i == currentIndex) {
						runes [i].color = Color.white;
						if (runes [i].enabled) {
							RuneData rune = GlobalPlayerManager.playerPowerups [playerID, i].GetComponent<RuneData> ();
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

				if (GlobalPlayerManager.GetButton (playerID, "A") && cooldown == 0) {
					if (selectedIndices [0] == -1)
						selectedIndices [0] = currentIndex;
					else
						selectedIndices [1] = currentIndex;

					offsetIndex (1);
					cooldown = 7;
				}

				if (GlobalPlayerManager.GetButton (playerID, "B") && cooldown == 0 && selectedIndices [0] != -1) {
					if (selectedIndices [1] != -1)
						selectedIndices [1] = -1;
					else
						selectedIndices [0] = -1;

					cooldown = 7;
				}

			} else {


				if(GlobalPlayerManager.GetButton (playerID, "B") && cooldown == 0 && canExit) {
					selectedIndices [1] = -1;
					cooldown = 7;
				}else{
					int i0 = selectedIndices [0];
					int i1 = selectedIndices [1];

					if (GlobalPlayerManager.playerPowerups [playerID, i0].GetComponent<RuneData> ().tier !=
					   GlobalPlayerManager.playerPowerups [playerID, i1].GetComponent<RuneData> ().tier) {
						instructionText.text = "THE RUNES MUST BE IN THE SAME TIER";
					} else if(canExit){
						instructionText.text = "I'll have those and you can have a...";
						canExit = false;
						Invoke ("DisplayNewRune", 2);
					}
				}
			}

			if(GlobalPlayerManager.GetButton(playerID, "B") && cooldown == 0 && canExit){
				open = false;
				cooldown = 7;
			}

			for(int i = 0; i<4; i++){
				if(i == selectedIndices[0] || i == selectedIndices[1]){
					runes [i].color = Color.green;
				}
			}
		}
	}

	void DisplayNewRune(){
		int newRuneTier = GlobalPlayerManager.playerPowerups [playerID, selectedIndices [0]].GetComponent<RuneData> ().tier + 1;

		Object[] level = Resources.LoadAll("Object/Rune");
		ArrayList desiredTier = new ArrayList();

		foreach (Object obj in level) {
			GameObject rune = (GameObject)obj;
			if(rune.GetComponent<RuneData>().tier == newRuneTier)
				desiredTier.Add (rune);	
		}

		newRune = (GameObject)desiredTier [Random.Range (0, desiredTier.Count)];

		instructionText.text = newRune.GetComponent<RuneData>().runeName + " rune!";
		newRuneImage.sprite = newRune.GetComponent<SpriteRenderer>().sprite;
		newRuneImage.enabled = true;

		Invoke("ExitShop", 3);
	}

	private GameObject newRune;

	void ExitShop(){
		int i0 = selectedIndices [0];
		int i1 = selectedIndices [1];
		GlobalPlayerManager.playerPowerups [playerID, i0] = newRune;
		GlobalPlayerManager.playerPowerups [playerID, i1] = null;

		if(newRune.GetComponent<RuneData>().tier == 4){
			//Win
		}
		open = false;
	}

	bool has2RunesInSameLevel(){
		int[] count = new int[4];

		for(int i = 0; i<4; i++) {
			GameObject rune = GlobalPlayerManager.playerPowerups [playerID, i];
			if(rune != null)
				count [rune.GetComponent<RuneData>().tier]++;	
		}

		for(int i = 0; i<4; i++){
			if(count[i] >= 2){
				return true;
			}
		}
		return false;
	}

	void offsetIndex(int offset){
		int runeCount = 0;
		foreach(Image rune in runes){
			if (rune.enabled)
				runeCount++;
		}
		if (selectedIndices [0] != -1)
			runeCount--;
		if (selectedIndices [1] != -1)
			runeCount--;

		if(runeCount == 0){
			currentIndex = -1;
			return;
		}

		currentIndex += offset;
		if (currentIndex > 3)
			currentIndex = 0;
		if (currentIndex < 0)
			currentIndex = 3;

		while(!runes[currentIndex].enabled  || currentIndex == selectedIndices[0] || currentIndex == selectedIndices[1]){
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
