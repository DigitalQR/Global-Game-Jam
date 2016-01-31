using UnityEngine;
using System.Collections;

public class Spawning : MonoBehaviour {

	int numToSpawn;
	public GameObject rubbish;
	public Sprite tentacle;
	public Sprite skull;
	public Sprite drum;

	void FixedUpdate ()
	{
		Invoke ("RandomSpawn",2);
	}

	void RandomSpawn () 
	{			
		numToSpawn = Random.Range (0, 5);
		numToSpawn = Mathf.RoundToInt (numToSpawn);
		for (int x = 0; x < numToSpawn; x++)
		{
			int spriteNum = Random.Range(0,3);
			if (spriteNum == 0)
				rubbish.GetComponent<SpriteRenderer> ().sprite = tentacle;
			else if (spriteNum == 1)
				rubbish.GetComponent<SpriteRenderer> ().sprite = skull;
			else
				rubbish.GetComponent<SpriteRenderer> ().sprite = drum;	
			float randomX = Random.Range(-9,9);
			Instantiate (rubbish, new Vector3 (randomX, 8, 0), Quaternion.identity);
		}	
	}
}
