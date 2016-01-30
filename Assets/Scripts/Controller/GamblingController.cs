using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamblingController : MonoBehaviour {

	public Image[] displayRune;
	private GameObject[] runeObject = new GameObject[4];

	void Start(){
		Object[] allRuneObject = Resources.LoadAll("Object/Rune");
		Debug.Log (allRuneObject.Length);

		for (int i = 0; i < 4; i++) {
			int ID = Random.Range (0, allRuneObject.Length - 1);
			runeObject [i] = (GameObject) allRuneObject [ID];
			displayRune [i].sprite = runeObject [i].GetComponent<SpriteRenderer> ().sprite;
		}


	}

}
