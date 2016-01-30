using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class MainController : MonoBehaviour {

	public bool TopDownMode = true;
	public GameObject playerObject;
	public Sprite[] playerSprites;
	private ArrayList players = new ArrayList();
	public static int winnerID = -1;

	public Text winnerText;
	public Text nameText;
	public Image playerImage;

	public GameObject gamblingGroup;

	void Start(){
		animationPosition = 250;
		gamblingGroup.SetActive (false);

		for(int i = 0; i < 4; i++) {
			if(GlobalPlayerManager.playerGamepadID[i] != -1){
				GameObject player = Instantiate(playerObject);
				player.GetComponent<SpriteRenderer>().sprite = playerSprites[i];
				player.GetComponent<RawPlayerController> ().playerNumber = i;
				player.GetComponent<RawPlayerController> ().gameController = this;

				if(TopDownMode){
					player.GetComponent<TopDownMovement> ().enabled = true;
				}else{
					player.GetComponent<SideOnMovement> ().enabled = true;
				}

				players.Add(player);
			}
		}
	}

	float animationPosition;

	void FixedUpdate(){
		if(winnerID != -1){
			Camera.main.GetComponent<Blur>().enabled = true;

			if(gamblingGroup.activeSelf){
				gamblingGroup.transform.position = new Vector3(gamblingGroup.transform.position.x, gamblingGroup.transform.position.y * 0.8f + animationPosition * 0.2f, 0);

			}else{
				nameText.text = "Player " + (winnerID + 1);

				if(animationPosition != - 150) 
					winnerText.transform.position = new Vector3(winnerText.transform.position.x, winnerText.transform.position.y * 0.8f + animationPosition * 0.2f, 0);
				else
					winnerText.transform.position = new Vector3(winnerText.transform.position.x, winnerText.transform.position.y * 0.9f + animationPosition * 0.1f, 0);
				
			}
		}
	}

	public void killPlayer(GameObject player){
		if(players.Count == 1){
			GameObject winningPlayer = (GameObject)players [0];
			playerImage.sprite = winningPlayer.GetComponent<SpriteRenderer> ().sprite;
			winnerID = winningPlayer.GetComponent<RawPlayerController>().playerNumber;
			nameText.text = "Player " + (winnerID + 1);

			Invoke ("MoveWinningTextOffTheScreen", 5);
		}

		players.Remove (player);
		Destroy(player);
	}

	void MoveWinningTextOffTheScreen(){
		animationPosition = -150;
		Invoke ("ActivateGamblingSection", 1.5f);
	}

	void ActivateGamblingSection(){
		animationPosition = gamblingGroup.transform.position.y;
		gamblingGroup.transform.position += new Vector3 (0, 600, 0);
		gamblingGroup.SetActive (true);
	}
}
