using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityDictionary : MonoBehaviour {

	Dictionary<string, Ability> storedAbilities = new Dictionary<string, Ability>();


	// Use this for initialization
	void Start () {
		float[] tempFloat = new float[8];

		for (int i = 0; i < tempFloat.Length; i++) {
			tempFloat [i] = 0;
		}
		tempFloat [0] = 0.25f;
		tempFloat [7] = 1f;
		Ability tempAbil = new Ability (0,0,0,"bluntSkill",1,9,tempFloat,"useWeapon","TempAssets/testingBluntSkill");
		storedAbilities.Add (tempAbil.getName(), tempAbil);

		for (int i = 0; i < tempFloat.Length; i++) {
			tempFloat [i] = 0;
		}
		tempFloat [0] = 0.125f;
		tempFloat [7] = 1f;
		tempAbil = new Ability (0,0,0,"slashSkill",1,9,tempFloat,"useWeapon","TempAssets/testingSlashSkill");
		storedAbilities.Add (tempAbil.getName(), tempAbil);

		for (int i = 0; i < tempFloat.Length; i++) {
			tempFloat [i] = 0;
		}
		tempFloat [7] = 1f;
		tempAbil = new Ability (0,0,0,"pierceSkill",1,9,tempFloat,"useWeapon","TempAssets/testingPierceSkill");
		storedAbilities.Add (tempAbil.getName(), tempAbil);
	}

	public Ability getAbility(string name){
		return storedAbilities [name];
	}

	// Update is called once per frame
	void Update () {
	
	}
}
