using UnityEngine;
using System.Collections;

public class GlobalPlayerManager{

	//Holds the gamepad IDs for each player (-1 implies there is no player)
	public static int[] playerGamepadID = {3, 2, 0,-1};

	//Returns true or false as to whether a player is pressing a certain number
	public static bool GetButton(int playerNumber, string buttonName){
		return Input.GetButton ("Gamepad " + playerGamepadID [playerNumber] + " " + buttonName);
	}

	//Returns the state of a specific button(axis) of a player
	public static float GetAxis(int playerNumber, string buttonName){
		return Input.GetAxis ("Gamepad " + playerGamepadID [playerNumber] + " " + buttonName);
	}
}
