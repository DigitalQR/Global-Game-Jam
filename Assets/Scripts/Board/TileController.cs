using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {

	private GameObject[] nearbyTiles;
	private float relationDistance = 1.5f;

	void Start(){
		autoCreateRelations();
	}

	public GameObject getTileThatIsnt(GameObject tile){
		for(int i = 0; i<nearbyTiles.Length; i++){
			if (tile != nearbyTiles [i])
				return (GameObject) nearbyTiles [i];
		}
		return tile;
	}

	public int howManyAdjacentTiles(){
		return nearbyTiles.Length;
	}

	public GameObject[] getAdjacentTiles(){
		return nearbyTiles;
	}

	void Update(){
		foreach(GameObject tile in nearbyTiles){
			Debug.DrawLine (transform.position, tile.transform.position);
		}
	}

	public void autoCreateRelations(){
		ArrayList list = new ArrayList ();

		foreach (GameObject tile in GameObject.FindGameObjectsWithTag("Tile")) {
			if(tile != gameObject && (transform.position - tile.transform.position).sqrMagnitude <= relationDistance * relationDistance){
				list.Add (tile);
			}
		}

		nearbyTiles = new GameObject[list.Count];
		for(int i = 0; i<list.Count; i++){
			nearbyTiles [i] = (GameObject)list [i];
		}
	}
}
