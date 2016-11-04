using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class TileMapMouse : MonoBehaviour {

	TileMap tilemap;
	Vector3 currentTileCoord;
	public GameObject selectionCube;
	string curAbility;
	public GameObject player;
	PlayerScript givenScript;
	int[] givenStats;
	Unit playerUnit;

	public GameObject eventSystem;
	SystemHandler systemScript;

	void Start(){
		curAbility="NOT FOUND PLEASE SEND HELP HOW DID THIS HAPPNE OH GOD MY EYES ARE BURNING WHERE IS THE LIGHT";
		givenScript = player.GetComponent<PlayerScript> ();
		systemScript = eventSystem.GetComponent<SystemHandler> ();
		playerUnit = player.GetComponent<Unit> ();

		tilemap = GetComponent<TileMap> ();
	}

	public void setCurAbility(string ajfdskljmf){
		curAbility = ajfdskljmf;
	}

	public string getCurAbility(){
		return curAbility;
	}

	public void setGivenStats(int[] gib){
		givenStats = gib;
	}

	//NOTE you CANT LIKE DYE THE MATERIAL COLOR FOR RASIN?
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;

		if (GetComponent<Collider> ().Raycast (ray, out hitInfo, Mathf.Infinity)) {
			int x = Mathf.FloorToInt (hitInfo.point.x / tilemap.tileSize);
			int z = Mathf.FloorToInt (hitInfo.point.z / tilemap.tileSize);

			currentTileCoord.x = x+0.5f;
			currentTileCoord.z = z+0.5f;
			//this is the offset thats needed to stay within the tiles

			selectionCube.transform.transform.position = currentTileCoord;
			//its 5 because the cube is a 5x5

		} else {
			//give it to me daddy
		}
		//thats for colliisons, now for mosuedown

		if (Input.GetMouseButtonDown (0)&&selectionCube.activeSelf==true) {
			systemScript.activateAbility(givenStats,curAbility,currentTileCoord.x - 0.5f,currentTileCoord.z - 0.5f,playerUnit.tileX, playerUnit.tileZ);
			selectionCube.SetActive (false);
			systemScript.beginToEndTurn ();
		}


			
	}
}
