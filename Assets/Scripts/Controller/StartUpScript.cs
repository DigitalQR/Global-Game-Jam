using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class StartUpScript : MonoBehaviour {

	[SerializeField]
	private Text headerText;
	[SerializeField]
	private Text popupText;
	public string GameModeHeader = "GAMEMODE NAME";
	[TextAreaAttribute(3,3)]
	public string GameModeDescription = " GAMEMODE DESCRIPTION";

	public int displayTime = 2;

	void Start () {
		Camera.main.GetComponent<Blur> ().enabled = true;
		GetComponent<MainController> ().enabled = false;

		headerText.text = GameModeHeader;
		popupText.text = GameModeDescription;

		countDownNumber = 3;
		Invoke ("CountDown", displayTime);
	}

	int countDownNumber = 0;

	void CountDown(){
		Camera.main.GetComponent<Blur> ().enabled = false;

		if(countDownNumber == -1){
			headerText.text = "";
			popupText.text = "";
			
		}else if (countDownNumber == 0) {
			popupText.text = "";
			headerText.text = "GO!";

			GetComponent<MainController> ().enabled = true;

			countDownNumber--;
			Invoke ("CountDown", 1);
			
		} else {

			popupText.text = "";
			headerText.text = "" + countDownNumber;
			countDownNumber--;
			Invoke ("CountDown", 1);
		}
	}

}
