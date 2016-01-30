using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour {

	private int currentPlayer = -1;
	private Vector3 cameraLocation = new Vector3(0,0,0);

	public GameObject piece; 
	public Sprite[] playerSprites;

	void Start () {
		DontDestroyOnLoad (this);

		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("Tile");
		GameObject[] spawnTiles = { 
			tiles[Random.Range(0,tiles.Length)],
			tiles[Random.Range(0,tiles.Length)],
			tiles[Random.Range(0,tiles.Length)],
			tiles[Random.Range(0,tiles.Length)]
		};

		ArrayList playerList = new ArrayList ();

		for(int i = 0; i<4; i++){
			if(GlobalPlayerManager.playerGamepadID[i] != -1){
				GameObject player = (GameObject) Instantiate(piece, spawnTiles[i].transform.position, Quaternion.identity);
				player.transform.SetParent(transform);
				playerList.Add (player);

				player.GetComponent<PieceController> ().playerID = i;
				player.GetComponent<PieceController> ().currentTile = spawnTiles[i];
				player.GetComponent<SpriteRenderer> ().sprite = playerSprites [i];
			}
		}


	}

}
