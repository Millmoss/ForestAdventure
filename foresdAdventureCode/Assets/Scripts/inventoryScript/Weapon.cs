using UnityEngine;
using System.Collections;

public class Weapon : Item {

	int baseDamage;

	public Weapon(){
		baseDamage = 10;
		name = "testSword";
		descript = "desu";
	}

	public Weapon(string givenName, string givenDescript, int givenDamage, string givenImagePath){
		name = givenName;
		baseDamage = givenDamage;
		descript = givenDescript;
		imageRepresentation = Resources.Load<Sprite>(givenImagePath) as Sprite;
	}
}
