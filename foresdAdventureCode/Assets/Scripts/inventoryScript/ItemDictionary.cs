using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDictionary : MonoBehaviour {

	Dictionary<string,Item> itemDictionary = new Dictionary<string,Item>();

	// Use this for initialization
	void Start () {
		itemDictionary = new Dictionary<string,Item> ();

		//lets CRAFT SOME ITEMS

		string tempArmorImageAsset = "TempAssets/testingArmor";
		string itemName = "testingArmor";
		Armor AnkhCuts = new Armor (itemName, "Dragon type cards HP x1.35. ATK x2 when simultaneously clearing 3 connected Heart orbs. ATK x1.5 for each additional orb, up to ATK x6.5 at ", 10,tempArmorImageAsset);
		//max description size is 1 tweet

		itemDictionary.Add (itemName, AnkhCuts);


	}
	
	// Update is called once per frame
	public Item getItem(string name){
		return itemDictionary [name];
	}
}