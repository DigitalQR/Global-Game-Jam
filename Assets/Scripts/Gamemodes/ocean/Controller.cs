using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	int[] Scores = new int[4];

	void Start ()
	{
		Invoke ("checkScores", 20);
	}


	public void BadTouch(int hitPlayeNumber) 
	{
		int playerNumber = hitPlayeNumber;
		int current = (Scores[playerNumber]);
		Scores.SetValue (current + 1, playerNumber);
		Debug.Log (hitPlayeNumber);
	}

	void checkScores()
	{
		int winningID = -1;
		int highScore = -1;

		for (int i = 0; i < 4; i++)
			{
			if (Scores [i] > highScore) {
				highScore = Scores [i];
				winningID = i;
			} 	
		}

		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
			RawPlayerController pc = player.GetComponent<RawPlayerController> ();

			if(pc.playerNumber != winningID){
				pc.kill ();
			}
		}
	}
}
