using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyAIOne : MonoBehaviour {

	Ability[] AbilityList;
	string[] startingAbilities;
	Unit unitStats;

	public GameObject playerObject;
	Unit player;

	AbilityDictionary abilityDic;
	public GameObject systemHandlerObject;
	SystemHandler sysHandl;
	public List<Node> currentPath = new List<Node> ();

	TileMap currentTileMap;
	public GameObject givenTileMap;

	int moveSpeed = 100;
	int remainingMovement = 100;

	float animationDelay=0.5f;
	float nextUsage=0f;

	public bool isMoving=false;

	// Use this for initialization
	void Start () {
		abilityDic = systemHandlerObject.GetComponent<AbilityDictionary> ();
		sysHandl = systemHandlerObject.GetComponent<SystemHandler> (); 
		unitStats = GetComponent<Unit> ();
		player = playerObject.GetComponent<Unit> ();
		currentTileMap = givenTileMap.GetComponent<TileMap> ();

		currentPath.Add(new Node());

		loadAbilities ();
		InstantiateAbilities ();
	}

	void InstantiateAbilities(){
		AbilityList = new Ability[startingAbilities.Length];
		for (int i = 0; i < startingAbilities.Length; i++) {
			AbilityList [i] = abilityDic.getAbility (startingAbilities [i]);
		}
	}

	void loadAbilities(){
		startingAbilities = new string[1];
		//startingAbilities [0] = "baseSpell";
		startingAbilities [0] = "baseAttack";
	}


	int getMaxMoveRange(){
		//this retursn if the player is within the move's range.
		for (int i = 0; i < AbilityList.Length; i++) {
			int testRange = AbilityList [i].getRange ();
			int comparedRange = 0;
			comparedRange += Mathf.Abs(player.tileX - unitStats.tileX);
			comparedRange += Mathf.Abs(player.tileZ - unitStats.tileZ);
			if (testRange <= comparedRange)
				return i;
		}
		return -1;
	}


	// Update is called once per frame
	void Update () {
		if (currentPath != null && remainingMovement > 0 &&isMoving==true && Time.time>nextUsage) {
			progressPath();
			nextUsage = Time.time + animationDelay;
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			sysHandl.activateAbility (unitStats.getUnitStatS (), startingAbilities [0], player.tileX, player.tileZ, unitStats.tileX, unitStats.tileZ);
		}
	}

	public void activate(){
		GeneratePathTo (player.tileX, player.tileZ);
		int checkNum = getMaxMoveRange ();
		if (checkNum != -1) {
			startMovement();
			sysHandl.activateAbility (unitStats.getUnitStatS (), startingAbilities [checkNum], player.tileX, player.tileZ, unitStats.tileX, unitStats.tileZ);
		} 
	}

	void startMovement(){
		nextUsage = Time.time + animationDelay;
		isMoving = true;
		remainingMovement = moveSpeed;
	}


	void progressPath() {
		if (currentPath == null||remainingMovement <= 0||currentPath.Count==1) {
			isMoving = false;
			sysHandl.nextEnemyMove ();
			if (currentPath.Count == 1) {
				currentPath = null;
				sysHandl.nextEnemyMove ();
			}

			return;
		}
		if (remainingMovement <= 0) {
			isMoving = false;
			sysHandl.nextEnemyMove ();
			return;
		}
		remainingMovement -= 1;
		//right now we have a static 1 for each movement; can be updated later
		unitStats.tileX = currentPath [1].x;
		unitStats.tileZ = currentPath [1].z;
		currentPath.RemoveAt (0);
		if (currentPath.Count == 1) {
			currentPath = null;
			sysHandl.nextEnemyMove ();
		}
	}


	public void GeneratePathTo(int x, int y) {
		//des des kowai des kil me
		// Clear out our unit's old path.
		currentPath = null;

		Dictionary<Node, float> dist = new Dictionary<Node, float>();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

		// Setup the "Q" -- the list of nodes we haven't checked yet.
		List<Node> unvisited = new List<Node>();

		Node source = currentTileMap.getGraphNodeAtPosition (unitStats.tileX, unitStats.tileZ);

		Node target = currentTileMap.graph[
			x, 
			y
		];

		dist[source] = 0;
		prev[source] = null;

		// Initialize everything to have INFINITY distance, since
		// we don't know any better right now. Also, it's possible
		// that some nodes CAN'T be reached from the source,
		// which would make INFINITY a reasonable value
		foreach(Node v in currentTileMap.graph) {
			if(v != source) {
				dist[v] = Mathf.Infinity;
				prev[v] = null;
			}

			unvisited.Add(v);
		}

		while(unvisited.Count > 0) {
			// "u" is going to be the unvisited node with the smallest distance.
			Node u = null;

			foreach(Node possibleU in unvisited) {
				if(u == null || dist[possibleU] < dist[u]) {
					u = possibleU;
				}
			}

			if(u == target) {
				break;	// Exit the while loop!
			}

			unvisited.Remove(u);

			foreach(Node v in u.neighbours) {
				//float alt = dist[u] + u.DistanceTo(v);
				int canPass=0;
				if (currentTileMap.CheckIfAlreadyInhabited (v.x,v.z)) {
					canPass = 9999;
				}
				float alt = dist [u] + canPass;
				if( alt < dist[v] ) {
					dist[v] = alt;
					prev[v] = u;
				}
			}
		}

		// If we get there, the either we found the shortest route
		// to our target, or there is no route at ALL to our target.

		if(prev[target] == null) {
			// No route between our target and the source
			return;
		}

		currentPath = new List<Node>();

		Node curr = target;

		// Step through the "prev" chain and add it to our path
		while(curr != null) {
			currentPath.Add(curr);
			curr = prev[curr];
		}

		currentPath.RemoveAt(0);
		//delet this

		// Right now, currentPath describes a route from out target to our source
		// So we need to invert it!

		currentPath.Reverse();


		//this should change the color of the path afterwards

	}

}
