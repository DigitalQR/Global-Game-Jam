using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PieceController : MonoBehaviour {

	public int playerID;
	private int visualRoll = 0;

	public Text popupText;
	public Image diceBackground;

	public GameObject currentTile;
	public GameObject lastTile;

	private bool branch;
	private GameObject upOption;
	private GameObject downOption;
	private GameObject leftOption;
	private GameObject rightOption;

	int cooldown = 0;
	int roll = 0;
	float moveFactor = 0;

	void Start(){
		transform.position = currentTile.transform.position;
	}

	void FixedUpdate(){
		if(playerID == BoardController.getCurrentPlayerID()){
			if(branch){
				float xaxis = GlobalPlayerManager.GetAxis (playerID, "Horizontal");
				float yaxis = GlobalPlayerManager.GetAxis (playerID, "Vertical");

				if(upOption != null && yaxis > 0){
					currentTile = upOption;
					branch = false;
					return;
				}
				if(downOption != null && yaxis < 0){
					currentTile = downOption;
					branch = false;
					return;
				}
				if(leftOption != null && xaxis < 0){
					currentTile = leftOption;
					branch = false;
					return;
				}
				if(rightOption != null && xaxis > 0){
					currentTile = rightOption;
					branch = false;
					return;
				}
				return;
			}

			if(cooldown != 0){
				cooldown--;
			}

			if(cooldown == 0 && GlobalPlayerManager.GetButton(playerID, "A")){
				Roll();
				visualRoll = roll;
				cooldown = 7;
			}else if(roll == 0){
				popupText.text = "Press A to roll the dice";
				diceBackground.enabled = false;
			}

			if(roll != 0){
				popupText.text = "" + visualRoll;
				diceBackground.enabled = true;

				if (moveFactor <= 0) {
					moveFactor = 1;
					roll--;

					if (roll != 0) {
						getNextTiles ();
						visualRoll = roll;
					} else {
						BoardController.playerDone ();
					}

				} else {
					moveFactor -= 0.07f;
					transform.position = lastTile.transform.position * (moveFactor) + currentTile.transform.position * (1f - moveFactor);
				}
			}
		}
	}

	void Roll(){
		roll = Random.Range (1, 7);
		moveFactor = 1;
		Debug.Log (roll);

		getNextTiles ();
	}

	void getNextTiles(){
		if(currentTile.GetComponent<TileController> ().howManyAdjacentTiles () <= 2){
			GameObject actualLastTile = currentTile;
			currentTile = currentTile.GetComponent<TileController> ().getTileThatIsnt (lastTile);
			lastTile = actualLastTile;

		}else{

			branch = true;
			upOption = null;
			downOption = null;
			leftOption = null;
			rightOption = null;

			foreach(GameObject tile in currentTile.GetComponent<TileController>().getAdjacentTiles()){
				if(tile != lastTile){
					Vector2 direction = tile.transform.position - currentTile.transform.position;

					if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {
						if (direction.x < 0) {
							leftOption = tile;
						} else {
							rightOption = tile;
						}
					} else {
						if (direction.y < 0) {
							downOption = tile;
						} else {
							upOption = tile;
						}
					}

				}
			}
			lastTile = currentTile;
		}
	}

}
