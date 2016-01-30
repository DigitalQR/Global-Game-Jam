using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamblingController : MonoBehaviour {

	public Image[] displayRune;
	public Image[] displayPlayer;
	public Sprite[] playerSprites;

	public GameObject chooseMenu;

	public Text runeTitle;
	public Text runeDescription;
	private GameObject[] runeObject = new GameObject[4];
	private int currentIndex = 0;

	private int currentPlayerIndex = 0;
	private int[] playerRunes = {-1,-1,-1,-1};
	private int[] playerOrder;

	public float animationHeight;

	void Start(){
		animationHeight = transform.position.y;
		transform.position += new Vector3 (0, 600, 0);

		Object[] allRuneObject = Resources.LoadAll("Object/Rune");

		for (int i = 0; i < 4; i++) {
			int ID = Random.Range (0, allRuneObject.Length - 1);
			runeObject [i] = (GameObject) allRuneObject [ID];
			displayRune [i].sprite = runeObject [i].GetComponent<SpriteRenderer> ().sprite;
		
			displayPlayer[i].enabled = false;
		}

		int playerCount = 0;
		for(int i = 0; i<4; i++){
			if (GlobalPlayerManager.playerGamepadID [i] != -1)
				playerCount++;
		}

		playerOrder = new int[playerCount];

		int track = 1;
		playerOrder[0] = MainController.winnerID;

		for(int i = 0; i<4; i++){
			if(i != MainController.winnerID && GlobalPlayerManager.playerGamepadID[i] != -1){
				playerOrder [track] = i;
				track++;
				if (track == playerCount)
					break;
			}
		}


	}

	int cooldown = 0;

	void FixedUpdate(){
		transform.position = new Vector3(transform.position.x, transform.position.y * 0.8f + animationHeight * 0.2f, 0);


		if(currentPlayerIndex == playerOrder.Length){
			transform.position = transform.position * 0.8f + new Vector3 (transform.position.x, -700, 0) * 0.2f;
			Invoke ("goToChoosing", 1);
			return;
		}

		if(cooldown > 0){
			cooldown--;
		}

		RuneData rune = runeObject [currentIndex].GetComponent<RuneData>();

		runeTitle.text = rune.runeName + " rune (Tier " + rune.tier + ")";
		runeDescription.text = rune.description;

		for(int i = 0; i<4; i++){
			if (i == currentIndex) {
				displayRune [i].color = Color.white;
				displayPlayer [i].sprite = playerSprites[playerOrder[currentPlayerIndex]];
				displayPlayer [i].enabled = true;
			} else {
				displayRune [i].color = Color.grey;

				int playerID = getPlayerWhoOwnsRune (i);
				if (playerID == -1)
					displayPlayer [i].enabled = false;
				else {
					displayPlayer [i].enabled = true;
					displayPlayer [i].sprite = playerSprites [playerID];
				}
			}
		}

		float axis = GlobalPlayerManager.GetAxis(MainController.winnerID, "Horizontal");

		if (axis != 0 && cooldown == 0) {
			int offset = 1;
			if(axis < 0){
				offset = -1;
			}
			adjustIndex (offset);

			cooldown = 7;
		}

		if(GlobalPlayerManager.GetButton(MainController.winnerID, "A") &&cooldown == 0){
			playerRunes [playerOrder[currentPlayerIndex]] = currentIndex;

			currentPlayerIndex++;
			adjustIndex (1);
			cooldown = 7;
		}

		if(GlobalPlayerManager.GetButton(MainController.winnerID, "B") && cooldown == 0 && currentPlayerIndex != 0){
			currentPlayerIndex--;
			playerRunes [playerOrder[currentPlayerIndex]] = -1;

			cooldown = 7;
		}
	}

	void goToChoosing(){
		GameObject[] runes = new GameObject[4];
		for(int i = 0; i<playerOrder.Length; i++){
			runes [playerOrder[i]] = runeObject [playerRunes [playerOrder[i]]];
		}

		chooseMenu.GetComponent<ChooseControl> ().go(runes);
		gameObject.SetActive (false);
	}

	void adjustIndex(int offset){
		if(currentPlayerIndex == playerOrder.Length){
			return;
		}
		currentIndex += offset;

		if(currentIndex < 0){
			currentIndex = 3;
		}
		if(currentIndex > 3){
			currentIndex = 0;
		}

		while(hasPlayerTakenRune(currentIndex)){
			currentIndex += offset;
			if(currentIndex < 0){
				currentIndex = 3;
			}
			if(currentIndex > 3){
				currentIndex = 0;
			}
		}
	}

	bool hasPlayerTakenRune(int runeID){
		for(int i = 0; i<playerRunes.Length; i++){
			if (playerRunes [i] == runeID)
				return true;
		}
		return false;
	}

	int getPlayerWhoOwnsRune(int runeID){
		for(int i = 0; i<playerRunes.Length; i++){
			if (playerRunes [i] == runeID)
				return i;
		}
		return -1;
	}
}
