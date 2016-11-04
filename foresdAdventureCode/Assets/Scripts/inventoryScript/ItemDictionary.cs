using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDictionary : MonoBehaviour {

	Dictionary<string,Item> itemDictionary = new Dictionary<string,Item>();

	// Use this for initialization
	void Start () {
		itemDictionary = new Dictionary<string,Item> ();

		//lets CRAFT SOME ITEMS

		string ImageAssetName = "TempAssets/testingArmorOne";
		string itemName = "testingArmorOne";
		Armor armor = new Armor (itemName, "Dragon type cards HP x1.35. ATK x2 when simultaneously clearing 3 connected Heart orbs. ATK x1.5 for each additional orb, up to ATK x6.5 at ", 10,ImageAssetName);
		//max description size is 1 tweet
		itemDictionary.Add (itemName, armor);


		ImageAssetName = "TempAssets/testingArmorTwo";
		itemName = "testingArmorTwo";
		armor = new Armor (itemName, "Dragon type cards HP x1.35. All attribute cards ATK x3 when reaching 1 set of Heart combo. ATK x3.5 for each additional combo, up to ATK x6.", 15,ImageAssetName);
		itemDictionary.Add (itemName, armor);

		//armors

		ImageAssetName = "TempAssets/testingHatOne";
		itemName = "testingHatOne";
		Hat hat = new Hat (itemName, "All attribute cards ATK x3.5 when HP is full. ATK x2.5, RCV x2.5 in cooperation mode.", 3,ImageAssetName);
		itemDictionary.Add (itemName, hat);

		ImageAssetName = "TempAssets/testingHatTwo";
		itemName = "testingHatTwo";
		hat = new Hat (itemName, "All attribute cards ATK x3 when HP greater than 80%. ATK x2.5, RCV x2.5 in cooperation mode.", 6,ImageAssetName);
		itemDictionary.Add (itemName, hat);

		ImageAssetName = "TempAssets/testingWeaponOne";
		itemName = "testingWeaponOne";
		Weapon weapon = new Weapon (itemName, "ATK x4 when attacking with 3 of following orb types: Fire, Wood, Light & Heart. 30% Fire, Wood & Light damage reduction.", 4,ImageAssetName);
		itemDictionary.Add (itemName, weapon);

		ImageAssetName = "TempAssets/testingWeaponTwo";
		itemName = "testingWeaponTwo";
		weapon = new Weapon (itemName, "ATK x4 when attacking with 3 of following orb types: Water, Wood, Dark & Heart. 30% Water, Wood & Dark damage reduction.", 99,ImageAssetName);
		itemDictionary.Add (itemName, weapon);

		ImageAssetName = "TempAssets/testingWeaponThree";
		itemName = "testingWeaponThree";
		weapon = new Weapon (itemName, "ATK x4 when attacking with 3 of following orb types: Wood, Light, Dark & Heart. 30% Wood, Light & Dark damage reduction.", 123445,ImageAssetName);
		itemDictionary.Add (itemName, weapon);

		ImageAssetName = "TempAssets/testingWeaponFour";
		itemName = "testingWeaponFour";
		weapon = new Weapon (itemName, "ATK x5 when attacking with Fire, Water, Wood & Light orb types at the same time. ATK x1.2 at 6 combos. ATK x0.2 for each additional combo, u", 76,ImageAssetName);
		itemDictionary.Add (itemName, weapon);

		ImageAssetName = "TempAssets/testingWeaponFive";
		itemName = "testingWeaponFive";
		weapon = new Weapon (itemName, "ATK x4 when attacking with 3 of following orb types: Fire, Water, Dark & Heart. 30% Fire, Water & Dark damage reduction.", 42,ImageAssetName);
		itemDictionary.Add (itemName, weapon);
	}
	
	// Update is called once per frame
	public Item getItem(string name){
		return itemDictionary [name];
	}
}