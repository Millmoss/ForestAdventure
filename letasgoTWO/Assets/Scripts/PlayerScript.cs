using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {
	//the unit's tile coords should be taken from, well its unit script. ezed.
	//that being said, we need to get that, so...


	//get the map function and the unithandler
	public TileMap map;
	public GameObject eventSystem;
	AbilityDictionary abilityDic;

	//we do have code for pathfinding, but that shouldn't be used in the playerscript seeing how we're going to move via arrow keys
	//that bein said however, there needs to be limits of movement due to the whole thing.
	List<int> curMoves;
	int maxNumberOfMoves=5;
	//hardcorded for npow
	int curNumberOfMoves=5;

	public GameObject unitModel;

	//this unit's 'unit' script
	Unit unitScript;

	Ability[] AbilityList;
	string[] startingAbilities;
	Unit playerStats;

	void Start () {
		unitScript = unitModel.GetComponent<Unit> ();
		abilityDic = eventSystem.GetComponent<AbilityDictionary> ();
		playerStats = GetComponent<Unit> ();

		startingAbilities = new string[3];
		startingAbilities [0] = "baseAttack";
		startingAbilities [1] = "baseSpell";
		startingAbilities [2] = "baseHustle";

		curMoves = new List<int>();

		InstantiateAbilities ();
	}

	void InstantiateAbilities(){
		AbilityList = new Ability[startingAbilities.Length];
		for (int i = 0; i < startingAbilities.Length; i++) {
			AbilityList [i] = abilityDic.getAbility (startingAbilities [i]);
		}
	}

	public void resetNumberofMoves(){
		curNumberOfMoves = maxNumberOfMoves;
	}

	public Ability[] getAbilities(){
		return AbilityList;
	}

	void Update(){
		//note that rn this does not support backtracing; think would be simple to do (add each movements coords into a list; if wanted coords in list, remove it and add one to curNumberofMoves

		//idk if you can do this in a switch statement; if yes it would be rather nice tbh
	
		if (Input.GetKeyDown (KeyCode.W)) {
			if (map.CheckIfCanPass (unitScript.tileX, unitScript.tileZ, "up")&&curNumberOfMoves>0) {
				unitScript.tileZ++;
				curNumberOfMoves--;
			}
		} else if (Input.GetKeyDown (KeyCode.A)) {
			if (map.CheckIfCanPass (unitScript.tileX, unitScript.tileZ, "left")&&curNumberOfMoves>0) {
				unitScript.tileX--;
				curNumberOfMoves--;
			}
		} else if (Input.GetKeyDown (KeyCode.S)) {
			if (map.CheckIfCanPass (unitScript.tileX, unitScript.tileZ, "down")&&curNumberOfMoves>0) {
				unitScript.tileZ--;
				curNumberOfMoves--;
			}
		} else if (Input.GetKeyDown (KeyCode.D)) {
			if (map.CheckIfCanPass (unitScript.tileX, unitScript.tileZ, "right")&&curNumberOfMoves>0) {
				unitScript.tileX++;
				curNumberOfMoves--;
			}
		}
		
	}
}
