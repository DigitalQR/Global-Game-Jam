using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinningSceneScript : MonoBehaviour {

	public Text text;
	public Image image;
	public Sprite[] playerSprites;

	void Start () {
		int winnerID = -1;

		for(int i = 0; i<4; i++){
			for(int n = 0; n<4; n++){
				GameObject runeObject = GlobalPlayerManager.playerPowerups [i, n];
				if(runeObject != null){
					RuneData rune = runeObject.GetComponent<RuneData> ();
					if(rune.tier == 4){
						winnerID = i;
						break;
					}
				}
			}
		}

		if(winnerID != -1){
			image.sprite = playerSprites [winnerID];
			text.text = "Player " + (winnerID + 1) + " has won!";
		}
	}

}
