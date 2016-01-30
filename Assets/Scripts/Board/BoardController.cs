using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BoardController : MonoBehaviour {

	private Vector3 cameraLocation = new Vector3(0,0,0);
	public static string[] minigames = { "Lasers 1", "Lasers Sideways" };
	private static int lastMinigameIndex = -1;

	public Text currentPlayerText;
	public Image[] playerPictures;

	public GameObject piece; 
	public Sprite[] playerSprites;

	static private ArrayList playerList;
	static private int playerIndex;

	public static void playerDone(){
		playerIndex++;
		if(playerIndex == playerList.Count){
			playerIndex = 0;
			lastMinigameIndex = Random.Range (0, minigames.Length);
			SceneManager.LoadScene (minigames [lastMinigameIndex]);
		}
	}


	void Start () {
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("Tile");
		GameObject[] spawnTiles = { 
			tiles[Random.Range(0,tiles.Length)],
			tiles[Random.Range(0,tiles.Length)],
			tiles[Random.Range(0,tiles.Length)],
			tiles[Random.Range(0,tiles.Length)]
		};

		playerList = new ArrayList ();

		for(int i = 0; i<4; i++){
			if (GlobalPlayerManager.playerGamepadID [i] != -1) {
				GameObject player = (GameObject)Instantiate (piece, spawnTiles [i].transform.position, Quaternion.identity);
				player.transform.SetParent (transform);
				playerList.Add (player);

				player.GetComponent<PieceController> ().playerID = i;
				player.GetComponent<PieceController> ().currentTile = spawnTiles [i];
				player.GetComponent<SpriteRenderer> ().sprite = playerSprites [i];
				playerPictures [i].enabled = true;
			} else {
				playerPictures [i].enabled = false;
			}
		}


	}

	static GameObject getPlayer(){
		return (GameObject)(playerList [playerIndex]);
	}

	void FixedUpdate(){
		GameObject player = getPlayer ();
		cameraLocation = player.transform.position;
		cameraLocation.z = -10;

		currentPlayerText.text = "Player " + (getCurrentPlayerID () + 1);
		for(int i = 0; i<4; i++){
			if (i == getCurrentPlayerID ()) {
				playerPictures [i].color = Color.white;
			} else {
				playerPictures [i].color = new Color(0.1f,0.1f,0.1f);
			}
		}
	}

	void Update(){
		cameraLocation.z = -10;
		Camera.main.transform.position = Camera.main.transform.position * 0.8f + cameraLocation * 0.2f;
	}

	void OnLevelWasLoaded(int level){
		SceneManager.UnloadScene (minigames [lastMinigameIndex]);
	}

	public static int getCurrentPlayerID(){
		return getPlayer().GetComponent<PieceController>().playerID;
	}

}
