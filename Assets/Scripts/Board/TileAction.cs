using UnityEngine;
using System.Collections;

public abstract class TileAction : MonoBehaviour {

	public bool finished = false;

	public abstract void runTileAction (PieceController piece);

	public abstract void updateTileAction ();
}
