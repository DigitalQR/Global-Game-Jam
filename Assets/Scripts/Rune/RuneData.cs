using UnityEngine;
using System.Collections;

public class RuneData : MonoBehaviour {

	public bool overworldEffect = false;
	public string runeName = "Rune";
	[Range(1,4)]
	public int tier = 1;
	[TextAreaAttribute(3,3)]
	public string description = "-=Rune description=-";
	GameObject player = null;

	public void runOverworldEffect(){
	}

	public void runMinigameEffect(){
	}

	public GameObject getOwner(){
		return player;
	}
}
