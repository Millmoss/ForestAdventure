using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {
	//the unit's tile coords should be taken from, well its unit script. ezed.
	//that being said, we need to get that, so...

	//this stuff might get refactored ventually so this only has the movement script and the data is on eventsystem....

	//TODO: Rings. like rings everywhere.


	//get the map function and the unithandler
	public TileMap map;
	public GameObject eventSystem;
	AbilityDictionary abilityDic;
	ItemDictionary itemDic;

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
	SystemHandler systemHandlr;
	SkillDictionary skillDic;

	Item[] equips;

	Item[] ringEquip;
	//standard; ringEqip will eventaully have 10 or something like that

	Item[] inventory;
	int maxInventorySize=16;
	//rn we have 16 cubes, yay!

	bool isSelected=false;
	int firstItemNumber;
	int isEquip;
	//if -1, its in inventory. else, corresponds to the equip.

	void Start () {
		unitScript = unitModel.GetComponent<Unit> ();
		abilityDic = eventSystem.GetComponent<AbilityDictionary> ();
		itemDic = eventSystem.GetComponent<ItemDictionary> ();
		systemHandlr = eventSystem.GetComponent<SystemHandler> ();
		skillDic = eventSystem.GetComponent<SkillDictionary> ();

		inventory = new Item[maxInventorySize];
		for (int i = 0; i < maxInventorySize; i++) {
			inventory [i] = new Item ();
		}

		equips = new Item[3];

		equips[0] = itemDic.getItem("testingWeaponOne");
		equips[1]= itemDic.getItem("testingHatOne");
		equips [2] = itemDic.getItem ("testingArmorOne");
		//set equipts

		inventory [0] = itemDic.getItem ("testingWeaponTwo");
		inventory [1] = itemDic.getItem ("testingWeaponThree");
		inventory [2] = itemDic.getItem ("testingWeaponFour");
		inventory [3] = itemDic.getItem ("testingWeaponFive");

		inventory [4] = itemDic.getItem ("testingHatTwo");

		inventory [5] = itemDic.getItem ("testingArmorTwo");
		//set the inventory items

		curMoves = new List<int>();

		startingAbilities = new string[3];
		startingAbilities [0] = "bluntSkill";
		startingAbilities [1] = "slashSkill";
		startingAbilities [2] = "pierceSkill";
		//set the base abilities

		InstantiateAbilities ();
		InstantiateSkills ();

		unitScript.addToSkill ("pierceSkill", 100);
		//testing; set pierceskill way high

		systemHandlr.resetPlayerInventoryPanels ();
		updateUnitStats (false);
	}

	void InstantiateSkills(){
		unitScript.setSkillList(skillDic.getAllSkills ());
	}

	void updateUnitStats(bool reset){
		if (!reset) {
			for (int i = 1; i < 3; i++) {
				unitScript.addMaxHealth (equips [i].getAddHealth ());
			}
			//rn just goes throguh the hp stuff and adds the heatlh
			unitScript.setWeaponDamage(((Weapon)equips[0]).getBaseDamage());
		} else {
			for (int i = 1; i < 3; i++) {
				unitScript.addMaxHealth (-(equips [i].getAddHealth ()));
				//same as above, but removes it so we dont get INFINITE HEALTH
			}
			unitScript.setWeaponDamage(-((Weapon)equips[0]).getBaseDamage());
		}
		print (unitScript.getMaxHealth ());
	}

	public Armor getChestEquip(){
		return (Armor)equips[2];
	}

	public Item getWeaponEquip(){
		return equips[0];
	}

	public Item getHatEquip(){
		return equips[1];
	}

	public Item[] getRingEquip(){
		return ringEquip;
	}

	public Item[] getPlayerInventory(){
		return inventory;
	}

	public Item[] getEquips(){
		return equips;
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

	public void equipArmor(int number)
	{
		//TODO: condense code
		if (isSelected) {
			switch (number) {
			case 0:
				if (inventory [firstItemNumber].GetType () == typeof(Weapon)) {
					updateUnitStats (true);
					Weapon tempArmor = (Weapon)equips [number];
					equips [number] = (Weapon)inventory [firstItemNumber];
					inventory [firstItemNumber] = tempArmor;
					systemHandlr.setInventoryPanelColor (firstItemNumber, false, false);
					isSelected = false;
					systemHandlr.resetPlayerInventoryPanels ();
					systemHandlr.setInventoryTargetText (-1, false);
					updateUnitStats (false);
				} else {
					systemHandlr.setInventoryInvalidTargetText ();
					isSelected = false;
					systemHandlr.resetPlayerInventoryPanels ();
					systemHandlr.setInventoryPanelColor (firstItemNumber, false, false);
				}				
				break;
			case 1:
				if (inventory [firstItemNumber].GetType () == typeof(Hat)) {
					updateUnitStats (true);
					Hat tempArmor = (Hat)equips [number];
					equips [number] = (Hat)inventory [firstItemNumber];
					inventory [firstItemNumber] = tempArmor;
					systemHandlr.setInventoryPanelColor (firstItemNumber, false, false);
					isSelected = false;
					systemHandlr.resetPlayerInventoryPanels ();
					systemHandlr.setInventoryTargetText (-1, false);
					updateUnitStats (false);
				} else {
					systemHandlr.setInventoryInvalidTargetText ();
					isSelected = false;
					systemHandlr.resetPlayerInventoryPanels ();
					systemHandlr.setInventoryPanelColor (firstItemNumber, false, false);
				}						
				break;
			case 2:
				if (inventory [firstItemNumber].GetType () == typeof(Armor)) {
					updateUnitStats (true);
					Armor tempArmor = (Armor)equips [number];
					equips [number] = (Armor)inventory [firstItemNumber];
					inventory [firstItemNumber] = tempArmor;
					systemHandlr.setInventoryPanelColor (firstItemNumber, false, false);
					isSelected = false;
					systemHandlr.resetPlayerInventoryPanels ();
					systemHandlr.setInventoryTargetText (-1, false);
					updateUnitStats (false);
				} else {
					systemHandlr.setInventoryInvalidTargetText ();
					isSelected = false;
					systemHandlr.resetPlayerInventoryPanels ();
					systemHandlr.setInventoryPanelColor (firstItemNumber, false, false);
				}
				break;
			case 3:
				break;

			}
		} else {
			systemHandlr.setInventoryPanelColor (number, true, true);

			firstItemNumber = number;

			systemHandlr.setInventoryTargetText (number, true);

			isSelected = true;
			isEquip = number;
		}
	}

	public void selectItemInInventory(int number){
		if (isSelected == false) {
			systemHandlr.setInventoryPanelColor (number, true, false);
			firstItemNumber = number;
			systemHandlr.setInventoryTargetText (number,false);
			isSelected = true;
			isEquip = -1;
		} else {
			Item tempItem = inventory [number];

			if (isEquip == -1) {
				inventory [number] = inventory [firstItemNumber];
				inventory [firstItemNumber] = tempItem;
			} else if(isEquip!=3){
				if(inventory[number].GetType() == equips[isEquip].GetType())
				{
					inventory [number] = equips[isEquip];

					switch (isEquip) {
					case 0:
						equips[0] = tempItem;
						break;
					case 1:
						equips[1] = tempItem;
						break;
					case 2:
						equips[2] = (Armor)tempItem;
						break;
					case 3:
						break;
					}
				}

			}
			systemHandlr.setInventoryPanelColor (firstItemNumber, false,false);
			isSelected = false;
			systemHandlr.resetPlayerInventoryPanels ();
			systemHandlr.setInventoryTargetText (-1,false);
		}


	}

	public void clearNumberofMoves(){
		curNumberOfMoves = 0;
	}

	public Ability[] getAbilities(){
		return AbilityList;
	}

	public int getRemainingMoves(){
		return curNumberOfMoves;
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
