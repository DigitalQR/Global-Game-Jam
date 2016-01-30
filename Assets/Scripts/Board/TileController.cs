using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {

	public GameObject[] nearbyTiles;

	public GameObject getTileThatIsnt(GameObject tile){
		for(int i = 0; i<nearbyTiles.Length; i++){
			if (tile != nearbyTiles [i])
				return (GameObject) nearbyTiles [i];
		}
		return tile;
	}

}
