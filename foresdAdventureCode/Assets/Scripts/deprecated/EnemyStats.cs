using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
    //enemy stats are set directly, probably will be pulled from a .txt later or have a switch statement like playerStats will, once we have more than one enemy type of course
    private float health;
    private float mana; //regenMana method should be called at the end of every turn by whatever handles turns, same with regenStamina method
    private float stamina;
    private float moveSpeed; //amount of free moves in a turn
    private float sprintCost; //additive cost when moving beyond number of free moves (first extra move would cost 1*sprintCost, next 2*sprintCost, next 3*sprintCost and so on)
    private float accuracy; //accuracy is percent chance to hit, added or subtracted from according to player dodge and status effects and so on
    private float dodge; //dodge is subtracted directly from the player accuracy percentage, if player has 90 accuracy and enemy has 10 dodge, player will have 80 percent chance to hit
    private float damage; //this is the amount of damage the enemy does //will be flat for now, but later will function similar to player damage
    private float manaRegen;
    private float staminaRegen;
    private float currentHealth;
    private float currentMana;
    private float currentStamina;
    private bool dead;

    void Start ()
    {
        health = 50;
        mana = 0;
        stamina = 0; //at the moment, we will not be using stamina as it complicates enemy ai, once we have the game working we'll start messing with enemy mana and stamina
        moveSpeed = 2;
        sprintCost = 999;
        accuracy = 95;
        dodge = 5;
        damage = 10;
        manaRegen = 0;
        staminaRegen = 0;
        currentHealth = health;
        currentMana = mana;
        currentStamina = stamina;
        dead = false;
	}
	
	void Update () {
	    
	}
	//cant you just do unit stats, and then have playerstats inherit the methods from it????

    #region
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
        return manaRegen;
    }

    public float getStaminaRegen()
    {
        return staminaRegen;
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    public float getSprintCost()
    {
        return sprintCost;
    }

    public float getAccuracy()
    {
        return accuracy;
    }

    public float getDodge()
    {
        return dodge;
    }

    public float getDamage()
    {
        return damage;
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

    public bool getDead()
    {
        return dead;
    }
    #endregion
}
