using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChooseControl : MonoBehaviour {

	private GameObject[] newRunes;
	public Image[] player1;
	public Image[] player2;
	public Image[] player3;
	public Image[] player4;

	public float animationHeight;

	void Start(){
		animationHeight = transform.position.y;
		transform.position += new Vector3 (0, 600, 0);
	}

	public void go(GameObject[] runes){
		newRunes = runes;

		bool[] placed = { false, false, false, false };

		for(int i = 0; i<4; i++){
			for(int playerID = 0; playerID<4; playerID++){
				if (GlobalPlayerManager.playerPowerups [playerID, i] == null && !placed[playerID]) {
					GlobalPlayerManager.playerPowerups [playerID, i] = newRunes [playerID];
					placed [playerID] = true;
				}
			}
		}

		if (GlobalPlayerManager.playerGamepadID [0] == -1 || placed [0]) {
			player1 [0].transform.parent.gameObject.SetActive (false);
		} else {
			for(int i = 0; i<4; i++){
				player1[i].sprite = GlobalPlayerManager.playerPowerups [0, i].GetComponent<SpriteRenderer> ().sprite;
			}
			player1 [4].sprite = newRunes [0].GetComponent<SpriteRenderer> ().sprite;
			player1 [0].transform.parent.GetComponent<RuneReplacer> ().newRune = newRunes [0];
		}

		if(GlobalPlayerManager.playerGamepadID[1] == -1 || placed[1]){
			player2[0].transform.parent.gameObject.SetActive (false);
		} else {
			for(int i = 0; i<4; i++){
				player2[i].sprite = GlobalPlayerManager.playerPowerups [1, i].GetComponent<SpriteRenderer> ().sprite;
			}
			player2 [4].sprite = newRunes [1].GetComponent<SpriteRenderer> ().sprite;
			player2 [0].transform.parent.GetComponent<RuneReplacer> ().newRune = newRunes [1];
		}

		if(GlobalPlayerManager.playerGamepadID[2] == -1 || placed[2]){
			player3[0].transform.parent.gameObject.SetActive (false);
		} else {
			for(int i = 0; i<4; i++){
				player3[i].sprite = GlobalPlayerManager.playerPowerups [2, i].GetComponent<SpriteRenderer> ().sprite;
			}
			player3 [4].sprite = newRunes [2].GetComponent<SpriteRenderer> ().sprite;
			player3 [0].transform.parent.GetComponent<RuneReplacer> ().newRune = newRunes [2];
		}

		if(GlobalPlayerManager.playerGamepadID[3] == -1 || placed[3]){
			player4[0].transform.parent.gameObject.SetActive (false);
		} else {
			for(int i = 0; i<4; i++){
				player4[i].sprite = GlobalPlayerManager.playerPowerups [3, i].GetComponent<SpriteRenderer> ().sprite;
			}
			player4 [4].sprite = newRunes [3].GetComponent<SpriteRenderer> ().sprite;
			player4 [0].transform.parent.GetComponent<RuneReplacer> ().newRune = newRunes [3];
		}

		gameObject.SetActive (true);

		if(!player1[0].transform.parent.gameObject.activeSelf &&
			!player2[0].transform.parent.gameObject.activeSelf &&
			!player3[0].transform.parent.gameObject.activeSelf &&
			!player4[0].transform.parent.gameObject.activeSelf){
			stop();
		}
	}

	void FixedUpdate (){
		transform.position = new Vector3(transform.position.x, transform.position.y * 0.8f + animationHeight * 0.2f, 0);

		if(!player1[0].transform.parent.gameObject.activeSelf &&
			!player2[0].transform.parent.gameObject.activeSelf &&
			!player3[0].transform.parent.gameObject.activeSelf &&
			!player4[0].transform.parent.gameObject.activeSelf){
			stop();
		}
	}

	void stop(){
		gameObject.SetActive (false);
		SceneManager.LoadScene ("gameboard");
	}
}
