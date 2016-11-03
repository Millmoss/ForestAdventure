using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ability{

	int manaCost;
	int staminaCost;
	int healthCost;
	string name;
	public Image skillImage;

	int range;
	int basePower;
	float[] damageFormula;
	string abilityForm;
	//not enums becuase lazy; is damage/status/heal


	public Ability(){
		manaCost = 0;
		staminaCost = 0;
		healthCost = 0;
		name = "IM A DISPLAY NAME";
		range = 1;
		damageFormula = new float[7];

		for (int i = 0; i < damageFormula.Length; i++) {
			damageFormula[i] = 0f;
		}
		damageFormula [0] = 1f;

		abilityForm = "damage";
	}

	public Ability(int mana, int stamina, int health, string givenName, int givenRange, int givenBasePower, float[] givenFormula, string typeOfPower){
		manaCost = mana;
		staminaCost = stamina;
		healthCost = health;
		name = givenName;
		range = givenRange;
		damageFormula = givenFormula;
		basePower = givenBasePower;
		abilityForm = typeOfPower;
	}

	/*
	private float strength;
    private float speed;
    private float dexterity;
    private float constitution;
    private float intelligence;
    private float willpower;
    private float luck;
    */

	public string getType(){
		return abilityForm;
	}

	public int getValue(int[] givenStats, int skillLevel)
	{
		float resultValue=(basePower+skillLevel);
		for (int i = 0; i < 7; i++) {
			resultValue += damageFormula [i] * givenStats[i];
		}
			
		return (int)resultValue;
		//right now we are assuming that the formula for damage is just (base+skilllevel)*stat modifiers.
		//note that i ahve no idea what casting it does
	}

	public int getManaCost(){
		return manaCost;
	}

	public int getStaminaCost(){
		return staminaCost;
	}

	public int getRange(){
		return range;
	}

	public int getHealthCost(){
		return healthCost;
	}

	public string getName(){
		return name;
	}
}
