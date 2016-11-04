using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public int tileX;
	public int tileZ;

	public string type; 
	//enemy/friend/player

	//base stats //getStat returns stat value //addStat adds whatever value is given, negatives matter and please give it integers in float form
	private int strength;
	private int speed;
	private int dexterity;
	private int constitution;
	private int intelligence;
	private int willpower;
	private int luck;

	//resultant stats //getStat rules apply for all resultant stats, not addStat //later will have modStat, a float that is factored into calculations, used for items bonuses and whatnot
	private int health;
	private int mana; //regenMana method should be called at the end of every turn by whatever handles turns, same with regenStamina method
	private int stamina;
	private int moveSpeed; //amount of free moves in a turn
	private int sprintCost; //additive cost when moving beyond number of free moves (first extra move would cost 1*sprintCost, next 2*sprintCost, next 3*sprintCost and so on)
	private int bluntAccuracy; //accuracy is percent chance to hit, added or subtracted from according to enemy dodge and status effects and so on
	private int slashAccuracy;
	private int pierceAccuracy;
	private int dodge; //dodge is subtracted directly from the enemy accuracy percentage, if enemy has 90 accuracy and player has 10 dodge, enemy will have 80 percent chance to hit
	private int bluntDamage=0; //this is the amount of damage the player does atm, if needs buff, change weaponDamage in Start method
	private int slashDamage=0; //at the moment, damage is flat for debugging purposes, once game works and stuff, damage will be something like (damage + random(.1f * damage, .4f * damage))
	private int pierceDamage=0;

	private int[] unitStats;
	//this is done for like stuff and things e.g. skills

	//TODO: set up equipment via creating an 'equpitment' class and stuff. also create one for weapons

	//status effects; this should be an enum but for now we're going to not do that because laz
	private string curStatus; 
	//dead/"literally blank for no cur status"

	//resource stats
	public int maxHealth;
	public int maxMana;
	public int maxStamina;
	public string givenName;
	public Sprite givenImage;

	//other stats go here!


	//this is for positional data
	public Vector3 currentTileCoord;

	//so you can click on these objects and display da stats; this should be instantiated upon creaitong
	//rn we'll get it via gameobject getComponent
	public GameObject eventSystem;
	SystemHandler unitHandler;

	// Use this for initialization
	void Start () {
		unitHandler = eventSystem.GetComponent<SystemHandler> ();

		print ("GIVEN IMAGE I S " + givenImage);

		strength=24;
		speed=20;
		dexterity=26;
		constitution=24;
		intelligence=22;
		willpower=18;
		luck=16;
		unitStats = new int[7];


		setUnitStats ();
		calculateOtherStats ();
	}

	public string getType(){
		return type;
	}

	void calculateOtherStats(){
		health = (int)Mathf.Floor(100 * (constitution/20));
		mana = (int)Mathf.Floor(25 * (((1.3f * willpower) + (.7f * intelligence)) / 40));
		stamina = (int)Mathf.Floor(50 * ((strength + dexterity) / 40));
		maxHealth = health;
		maxMana = mana;
		maxStamina = stamina;
		moveSpeed = (int)Mathf.Floor(1.5f * (speed / 10));
		sprintCost = 3 * (1 / ((strength + speed) / 40));
		bluntAccuracy = 90;
		slashAccuracy = 90 + (dexterity / 2);
		pierceAccuracy = 90 + dexterity;
		dodge = (int)Mathf.Floor(((1.3f * dexterity) + (.7f * speed)) / 4);
		curStatus = "";
		setUnitStats ();
	}

	public int[] getUnitStatS(){
		return unitStats;
	}

	void setUnitStats(){
		unitStats [0] = strength;
		unitStats [1] = speed;
		unitStats [2] = dexterity;
		unitStats [3] = constitution;
		unitStats [4] = intelligence;
		unitStats [5] = willpower;
		unitStats [6] = luck;
	}

	public int getStrength()
	{
		return strength;
	}

	public void addStrength(int add)
	{
		strength += add;
		recalculate();
	}

	public int getSpeed()
	{
		return speed;
	}

	public void addSpeed(int add)
	{
		speed += add;
		recalculate();
	}

	public int getDexterity()
	{
		return dexterity;
	}

	public void addDexterity(int add)
	{
		dexterity += add;
		recalculate();
	}

	public int getConstitution()
	{
		return constitution;
	}

	public void addConstitution(int add)
	{
		constitution += add;
		recalculate();
	}

	public int getIntelligence()
	{
		return intelligence;
	}

	public void addIntelligence(int add)
	{
		intelligence += add;
		recalculate();
	}

	public int getWillpower()
	{
		return willpower;
	}

	public void addWillpower(int add)
	{
		willpower += add;
		recalculate();
	}

	public int getLuck()
	{
		return luck;
	}

	public void addLuck(int add)
	{
		luck += add;
		recalculate();
	}

	public int getHealth()
	{
		return health;
	}

	public int getMana()
	{
		return mana;
	}

	public int getStamina()
	{
		return stamina;
	}

	public float getManaRegen()
	{
		return (10 * (intelligence / 20));
	}

	public float getStaminaRegen()
	{
		return (10 * ((maxStamina - stamina) / maxStamina));
	}

	public int getMoveSpeed()
	{
		return moveSpeed;
	}

	public int getSprintCost()
	{
		return sprintCost;
	}

	public int getBluntAccuracy()
	{
		return bluntAccuracy;
	}

	public int getSlashAccuracy()
	{
		return slashAccuracy;
	}

	public int getPierceAccuracy()
	{
		return pierceAccuracy;
	}

	public int getDodge()
	{
		return dodge;
	}

	public int getBluntDamage()
	{
		return bluntDamage;
	}

	public int getSlashDamage()
	{
		return slashDamage;
	}

	public int getPierceDamage()
	{
		return pierceDamage;
	}

	public void addHealth(int add)
	{
		health+= add;
		if (health> maxHealth)
			health = maxHealth;
		if (health <= 0)
			curStatus= "dead";

	}

	public void addMana(int add)
	{
		mana += add;
		if (mana > maxMana)
			mana = maxMana;
		if (mana < 0)
			mana = 0;
	}

	public void addStamina(int add)
	{
		stamina += add;
		if (stamina> maxStamina)
			stamina = maxStamina;
		if (stamina < 0)
			stamina= 0;
	}

	public string getCurStatus()
	{
		return curStatus;
	}

	public void regenMana()
	{
		mana += 10 * (intelligence / 20);
		if (mana > maxMana)
			mana = maxMana;
	}

	public void regenStamina()
	{
		stamina += 10 * ((maxStamina - stamina) / maxStamina);
	}

	//total recalculation method
	private void recalculate()
	{
		health = (int)Mathf.Floor(100 * (constitution / 20));
		mana = (int)Mathf.Floor(25 * (((1.3f * willpower) + (.7f * intelligence)) / 40));
		stamina = (int)Mathf.Floor(50 * ((strength + dexterity) / 40));
		moveSpeed = (int)Mathf.Floor(1.5f * (speed / 10));
		sprintCost = 3 * (1 / ((strength + speed) / 40));
		bluntAccuracy = 90;
		slashAccuracy = 90 + (dexterity / 2);
		pierceAccuracy = 90 + dexterity;
		dodge = (int)Mathf.Floor(((1.3f * dexterity) + (.7f * speed)) / 4);
		setUnitStats ();
	}

	public Sprite getImage(){
		return givenImage;
	}

	public int getMaxHealth(){
		return maxHealth;
	}

	public int getMaxMana(){
		return maxMana;
	}

	public int getMaxStamina(){
		return maxStamina;
	}

	public string getName(){
		return givenName;
	}

	void OnMouseDown(){
		unitHandler.SetTargetPanelStats (this);
		unitHandler.EnableTargetStatPanel ();
	}

	void Update () {
		//make sure that the object is at its given tileX/Y location.

		//make sure that the object is at its given tileX/Y location.
		if(currentTileCoord.x != tileX + 0.5f)
		{
			currentTileCoord.x = Mathf.Lerp(currentTileCoord.x, tileX + 0.5f, 10f * Time.deltaTime);
		}
		if(currentTileCoord.z != tileZ + 0.5f)
		{
			currentTileCoord.z = Mathf.Lerp(currentTileCoord.z, tileZ + 0.5f, 10f * Time.deltaTime);
		}
		transform.transform.position = currentTileCoord;
	

	}
}
