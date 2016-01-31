using UnityEngine;
using System.Collections;

public class ShopTile : TileAction {

	public ShopMenu shopMenu;

	public override void runTileAction (PieceController piece){
		shopMenu.playerID = piece.playerID;
		shopMenu.ResetShop ();
	}

	public override void updateTileAction (){
		finished = !shopMenu.open;
		if(finished){
			shopMenu.selectedIndices[0] = -1;
			shopMenu.selectedIndices[1] = -1;
		}
	}

}
