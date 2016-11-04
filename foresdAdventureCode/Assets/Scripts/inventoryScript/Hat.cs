using UnityEngine;
using System.Collections;

public class Hat : Item {

	public Hat(){
		addHealth = 10;
		name = "testHat";
		descript = "quert";
	}

	public Hat(string givenName, string givenDescript, int givenHealth, string givenImagePath){
		name = givenName;
		addHealth = givenHealth;
		descript = givenDescript;
		imageRepresentation = Resources.Load<Sprite>(givenImagePath) as Sprite;
	}
}
