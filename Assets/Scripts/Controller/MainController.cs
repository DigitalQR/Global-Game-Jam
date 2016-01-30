using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	public bool TopDownMode = true;
	public GameObject playerObject;
	public Sprite[] playerSprites;

	void Start(){
		for(int i = 0; i < 4; i++) {
			if(GlobalPlayerManager.playerGamepadID[i] != -1){
				GameObject player = Instantiate(playerObject);
				player.GetComponent<SpriteRenderer>().sprite = playerSprites[i];
				player.GetComponent<RawPlayerController> ().playerNumber = i;

				if(TopDownMode){
					player.GetComponent<TopDownMovement> ().enabled = true;
				}else{
					player.GetComponent<SideOnMovement> ().enabled = true;
				}
			}
		}
	}
}
