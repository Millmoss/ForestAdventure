using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour {
    public char character; //a for adventurer, t for theif and so on

    //base stats //getStat returns stat value //addStat adds whatever value is given, negatives matter and please give it integers in float form
    private float strength;
    private float speed;
    private float dexterity;
    private float constitution;
    private float intelligence;
    private float willpower;
    private float luck;

    //resultant stats //getStat rules apply for all resultant stats, not addStat //later will have modStat, a float that is factored into calculations, used for items bonuses and whatnot
    private float health;
    private float mana; //regenMana method should be called at the end of every turn by whatever handles turns, same with regenStamina method
    private float stamina;
    private float moveSpeed; //amount of free moves in a turn
    private float sprintCost; //additive cost when moving beyond number of free moves (first extra move would cost 1*sprintCost, next 2*sprintCost, next 3*sprintCost and so on)
    private float bluntAccuracy; //accuracy is percent chance to hit, added or subtracted from according to enemy dodge and status effects and so on
    private float slashAccuracy;
    private float pierceAccuracy;
    private float dodge; //dodge is subtracted directly from the enemy accuracy percentage, if enemy has 90 accuracy and player has 10 dodge, enemy will have 80 percent chance to hit
    private float bluntDamage; //this is the amount of damage the player does atm, if needs buff, change weaponDamage in Start method
    private float slashDamage; //at the moment, damage is flat for debugging purposes, once game works and stuff, damage will be something like (damage + random(.1f * damage, .4f * damage))
    private float pierceDamage;

    //pool stats //getStat and addStat rules apply for these three //use addStat to change currentStat value, do not add a subtractStat method since addStat accepts negatives for subtracting from currentStat
    private float currentHealth;
    private float currentMana;
    private float currentStamina;

    //skills //start will eventually set all skill values //getStat and addStat must be given a string representing the skill desired, otherwise functions normally
    Dictionary<string, float> skills = new Dictionary<string, float>();

    //items //later this will have a lot more affectors under it
    private float weaponDamage;

    //statuses
    private bool dead; //tells whether or not the player is dead

	void Start ()
    {
	    //when there are more characters we'll put most of this code into a switch statement
        strength=24f;
        speed=20f;
        dexterity=26f;
        constitution=24f;
        intelligence=22f;
        willpower=18f;
        luck=16f;
        skills.Add("bluntSkill", 0);
        skills.Add("slashSkill", 1);
        skills.Add("pierceSkill", 0);
        weaponDamage = 20;
        //calculations
        health = Mathf.Floor(100 * (constitution/20));
        mana = Mathf.Floor(25 * (((1.3f * willpower) + (.7f * intelligence)) / 40));
        stamina = Mathf.Floor(50 * ((strength + dexterity) / 40));
        currentHealth = health;
        currentMana = mana;
        currentStamina = stamina;
        moveSpeed = Mathf.Floor(1.5f * (speed / 10));
        sprintCost = 3 * (1 / ((strength + speed) / 40));
        bluntAccuracy = 90;
        slashAccuracy = 90 + (dexterity / 2);
        pierceAccuracy = 90 + dexterity;
        dodge = Mathf.Floor(((1.3f * dexterity) + (.7f * speed)) / 4);
        bluntDamage = weaponDamage * ((9 + skills["bluntSkill"] + (strength / 4)) / 10);
        slashDamage = weaponDamage * ((9 + skills["slashSkill"] + (strength / 8)) / 10);
        pierceDamage = weaponDamage * ((9 + skills["pierceSkill"]) / 10);
        dead = false;
    }

	void Update ()
    {
	    
	}
    //getStat and addStat statements
    #region
    public float getStrength()
    {
        return strength;
    }

    public void addStrength(float add)
    {
        strength += add;
        recalculate();
    }

    public float getSpeed()
    {
        return speed;
    }

    public void addSpeed(float add)
    {
        speed += add;
        recalculate();
    }

    public float getDexterity()
    {
        return dexterity;
    }

    public void addDexterity(float add)
    {
        dexterity += add;
        recalculate();
    }

    public float getConstitution()
    {
        return constitution;
    }

    public void addConstitution(float add)
    {
        constitution += add;
        recalculate();
    }

    public float getIntelligence()
    {
        return intelligence;
    }

    public void addIntelligence(float add)
    {
        intelligence += add;
        recalculate();
    }

    public float getWillpower()
    {
        return willpower;
    }

    public void addWillpower(float add)
    {
        willpower += add;
        recalculate();
    }

    public float getLuck()
    {
        return luck;
    }

    public void addLuck(float add)
    {
        luck += add;
        recalculate();
    }

    public float getHealth()
    {
        return health;
    }

    public float getMana()
    {
        return mana;
    }

    public float getStamina()
    {
        return stamina;
    }

    public float getManaRegen()
    {
        return (10 * (intelligence / 20));
    }

    public float getStaminaRegen()
    {
        return (10 * ((stamina - currentStamina) / stamina));
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    public float getSprintCost()
    {
        return sprintCost;
    }

    public float getBluntAccuracy()
    {
        return bluntAccuracy;
    }

    public float getSlashAccuracy()
    {
        return slashAccuracy;
    }

    public float getPierceAccuracy()
    {
        return pierceAccuracy;
    }

    public float getDodge()
    {
        return dodge;
    }

    public float getBluntDamage()
    {
        return bluntDamage;
    }

    public float getSlashDamage()
    {
        return slashDamage;
    }

    public float getPierceDamage()
    {
        return pierceDamage;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public void addCurrentHealth(float add)
    {
        currentHealth += add;
        if (currentHealth > health)
            currentHealth = health;
        if (currentHealth <= 0)
            dead = true;

    }

    public float getCurrentMana()
    {
        return currentMana;
    }

    public void addCurrentMana(float add)
    {
        currentMana += add;
        if (currentMana > mana)
            currentMana = mana;
        if (currentMana < 0)
            currentMana = 0;
    }

    public float getCurrentStamina()
    {
        return currentStamina;
    }

    public void addCurrentStamina(float add)
    {
        currentStamina += add;
        if (currentStamina > stamina)
            currentStamina = stamina;
        if (currentStamina < 0)
            currentStamina = 0;
    }

    public float getSkill(string stat)
    {
        return skills[stat];
    }

    public void addSkill(string stat, float add)
    {
        skills.Add(stat, skills[stat] + add);
        recalculate();
    }

    public bool getDead()
    {
        return dead;
    }
    #endregion

    public void regenMana()
    {
        currentMana += 10 * (intelligence / 20);
        if (currentMana > mana)
            currentMana = mana;
    }

    public void regenStamina()
    {
        currentStamina += 10 * ((stamina - currentStamina) / stamina);
    }

    //total recalculation method
    private void recalculate()
    {
        health = Mathf.Floor(100 * (constitution / 20));
        mana = Mathf.Floor(25 * (((1.3f * willpower) + (.7f * intelligence)) / 40));
        stamina = Mathf.Floor(50 * ((strength + dexterity) / 40));
        moveSpeed = Mathf.Floor(1.5f * (speed / 10));
        sprintCost = 3 * (1 / ((strength + speed) / 40));
        bluntAccuracy = 90;
        slashAccuracy = 90 + (dexterity / 2);
        pierceAccuracy = 90 + dexterity;
        dodge = Mathf.Floor(((1.3f * dexterity) + (.7f * speed)) / 4);
        bluntDamage = weaponDamage * ((9 + skills["bluntSkill"] + (strength / 4)) / 10);
        slashDamage = weaponDamage * ((9 + skills["slashSkill"] + (strength / 8)) / 10);
        pierceDamage = weaponDamage * ((9 + skills["pierceSkill"]) / 10); ;
    }
}
