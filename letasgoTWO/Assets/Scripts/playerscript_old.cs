/*
using UnityEngine;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	// tileX and tileY represent the correct map-tile position
	// for this piece.  Note that this doesn't necessarily mean
	// the world-space coordinates, because our map might be scaled
	// or offset or something of that nature.  Also, during movement
	// animations, we are going to be somewhere in between tiles.
	public int tileX;
	public int tileY;

	public TileMap_Old map;
	public UnitHandler units;

	// Our pathfinding info.  Null if we have no destination ordered.
	public List<Node> currentPath = null;

	// How far this unit can move in one turn. Note that some tiles cost extra.
	int moveSpeed = 99;
	//rn its preeeeety far
	float remainingMovement=0;
	//lets not have it automatically move upon getting the path; that could lead to some interesting shenanagins

	void Update() {
		if(currentPath != null) {
			int currNode = 0;

			while( currNode < currentPath.Count-1 ) {
				map.ChangePathColor (currentPath[currNode].x, currentPath[currNode].y,true);
				map.ChangePathColor (currentPath[currNode+1].x, currentPath[currNode+1].y,true);

				currNode++;
			}
		}

		// Have we moved our visible piece close enough to the target tile that we can
		// advance to the next step in our pathfinding?
	if(Vector3.Distance(transform.position, map.TileCoordToWorldCoord( tileX, tileY )) < 0.1f)
			AdvancePathing();

		// Smoothly animate towards the correct map tile.
		transform.position = Vector3.Lerp(transform.position, map.TileCoordToWorldCoord( tileX, tileY ), 5f * Time.deltaTime);
	}

	// Advances our pathfinding progress by one tile.
	void AdvancePathing() {
		if(currentPath==null)
			return;

		if(remainingMovement <= 0)
			return;

		//OI TEHRE BE A VILLAN STOP THE PRESSES AND THE MOVEMENTS
		//like, totally stop everything and just DONT FUCKING MOVE BIATHC
		if (!units.IsValidMove (currentPath [1].x, currentPath [1].y)) {

			currentPath = null;
			map.ResetPathColors ();
			remainingMovement = 0;
			print ("Can't move! Object in the way!");
			return;
		}


		// Teleport us to our correct "current" position, in case we
		// haven't finished the animation yet.
		transform.position = map.TileCoordToWorldCoord( tileX, tileY );

		// Get cost from current tile to next tile
		remainingMovement -= map.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y );

		//change the color of the current tile back to normal
		map.ChangePathColor(currentPath[0].x,currentPath[0].y,false);

		// Move us to the next tile in the sequence
		tileX = currentPath[1].x;
		tileY = currentPath[1].y;
		
		// Remove the old "current" tile from the pathfinding list
		currentPath.RemoveAt(0);



		if(currentPath.Count == 1) {
			// We only have one tile left in the path, and that tile MUST be our ultimate
			// destination -- and we are standing on it!
			// So let's just clear our pathfinding info.
			currentPath = null;

			//also make sure that the pesky color isnt like ugly lol
			map.ChangePathColor(tileX,tileY,false);

			//make sure that when you get to the target you finish up your movement points; this isoptional
			remainingMovement=0;
		}
	}

	// The "Next Turn" button calls this.
	public void NextTurn() {
		// Make sure to wrap-up any outstanding movement left over.
		while(currentPath!=null && remainingMovement > 0) {
			AdvancePathing();
		}

		// Reset our available movement points.
		remainingMovement = moveSpeed;
	}
}
*/