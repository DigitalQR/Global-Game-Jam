using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	public bool TopDownMode = true;
	public GameObject playerObject;
	public Sprite[] playerSprites;
	private ArrayList players = new ArrayList();
	public GameObject winner = null;

	void Start(){
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

	void FixedUpdate(){
		if(winner != null){
			//Do a winning thing
		}
	}

	public void killPlayer(GameObject player){
		players.Remove (player);
		Destroy(player);

		if(players.Count == 1){
			winner = (GameObject) players[0];
		}
	}
}
