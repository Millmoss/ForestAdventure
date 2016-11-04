using UnityEngine;
using System.Collections;

public class Consumable : Item {

	//TODO : talk about what a consumable does (uses skills, or not)

	string type;
	//should all be skills tbh, idk 
	int range;
	int healAmount;

	public Consumable(){
		type = "heal";
		name = "Dual Groin";
		descript = "ahhhhhhh";
	}

	public Consumable(string givenName, string givenDescript, string givenType, int givenRange, int healAmount){
		name = givenName;

		descript = givenDescript;
	}
}
