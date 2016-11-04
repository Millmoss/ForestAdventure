using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityDictionary : MonoBehaviour {

	Dictionary<string, Ability> storedAbilities = new Dictionary<string, Ability>();


	// Use this for initialization
	void Start () {
		float[] tempFloat = new float[7];
		for (int i = 0; i < tempFloat.Length; i++) {
			tempFloat [i] = 0;
		}
		tempFloat [0] = 1;
		Ability tempAbil = new Ability (0,0,0,"baseAttack",1,100,tempFloat,"damage");
		storedAbilities.Add (tempAbil.getName(), tempAbil);

		for (int i = 0; i < tempFloat.Length; i++) {
			tempFloat [i] = 0;
		}
		tempFloat [4] = 1;
		tempAbil = new Ability (3,0,0,"baseSpell",5,125,tempFloat,"damage");
		storedAbilities.Add (tempAbil.getName(), tempAbil);

		for (int i = 0; i < tempFloat.Length; i++) {
			tempFloat [i] = 0;
		}
		tempAbil = new Ability (0,3,0,"baseHustle",3,0,tempFloat,"status");
		storedAbilities.Add (tempAbil.getName(), tempAbil);
	}

	public Ability getAbility(string name){
		return storedAbilities [name];
	}

	// Update is called once per frame
	void Update () {
	
	}
}
