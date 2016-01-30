using UnityEngine;
using System.Collections;

public class PieceController : MonoBehaviour {

	public int playerID;

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
		if(branch){
			float xaxis = GlobalPlayerManager.GetAxis (playerID, "Horizontal");
			float yaxis = GlobalPlayerManager.GetAxis (playerID, "Vertical");

			if(upOption != null && yaxis > 0){
				lastTile = currentTile;
				currentTile = upOption;
				branch = false;
				Debug.Log ("Up");
				return;
			}
			if(downOption != null && yaxis < 0){
				lastTile = currentTile;
				currentTile = downOption;
				branch = false;
				Debug.Log ("D");
				return;
			}
			if(leftOption != null && xaxis < 0){
				lastTile = currentTile;
				currentTile = leftOption;
				branch = false;
				Debug.Log ("L");
				return;
			}
			if(rightOption != null && xaxis > 0){
				lastTile = currentTile;
				currentTile = rightOption;
				branch = false;
				Debug.Log ("R");
				return;
			}
			return;
		}

		if(cooldown != 0){
			cooldown--;
		}

		if(cooldown == 0 && GlobalPlayerManager.GetButton(playerID, "A")){
			Roll();
			cooldown = 7;
		}

		if(roll != 0){
			if (moveFactor <= 0) {
				moveFactor = 1;
				roll--;

				if(roll != 0){
					getNextTiles();
				}

			} else {
				moveFactor -= 0.07f;
				transform.position = lastTile.transform.position * (moveFactor) + currentTile.transform.position * (1f - moveFactor);
			}
		}
	}

	void Roll(){
		roll = Random.Range (1, 7);
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
							Debug.Log ("L--");
						} else {
							rightOption = tile;
							Debug.Log ("R--");
						}
					} else {
						if (direction.y < 0) {
							downOption = tile;
							Debug.Log ("D--");
						} else {
							upOption = tile;
							Debug.Log ("U--");
						}
					}

				}
			}
		}
	}

}
