using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SystemHandler : MonoBehaviour {

	public GameObject[] AllUnits;
	//NOTE: This and tilemap both have AllUnits; prob. a way to get both of them to be the same to b laz
	public GameObject targetStatPanel;

	List<GameObject> AggroUnits = new List<GameObject>();

	public Text[] statPanelTextValues;

	public GameObject playerStatPanel;
	public Text[] playerStatPanelText;
	//0 is hp, 1 is mana, 2 is stam; add name eventaully?
	public GameObject playerUnit;
	Unit playerScript;
	PlayerScript givenPlayerScript;

	public GameObject statPanelImage;
	//rn 0 is the name, 1 is hp, 2 is mana, 3 is stamina
	// Use this for initialization

	public GameObject skillPanel;
	public Image[] IndependentSkillPanels;

	public Image[] playerInventoryPanels;
	public Image[] playerEquipmentPanels;
	//pretty self explanitory

	public Text selectedInventoryName;
	public Text selectedInventoryDescription;


	public GameObject mouseCube;
	public GameObject tileMap;

	TileMapMouse mouseScript;
	AbilityDictionary abilityDic;

	int endTurn=0;
	//0-false,1-true,2-finsihed.

	Color normalInventoryColor = new Color();
	Color selectedInventoryColor = new Color();

	void Start () {
		//right now curabilities is 4 because fuk u
		playerScript = playerUnit.GetComponent<Unit> ();
		givenPlayerScript = playerUnit.GetComponent<PlayerScript> ();
		mouseScript = tileMap.GetComponent<TileMapMouse> ();
		abilityDic = GetComponent<AbilityDictionary> ();
		SetPlayerPanel ();

		normalInventoryColor = playerInventoryPanels[0].color;
		selectedInventoryColor = Color.red;

	}
		
	public void resetPlayerInventoryPanels(){
		for (int i = 0; i < 16; i++) {
			playerInventoryPanels [i].sprite = givenPlayerScript.getPlayerInventory()[i].getImage();
		}
		playerEquipmentPanels [0].sprite = givenPlayerScript.getWeaponEquip ().getImage();
		playerEquipmentPanels [1].sprite = givenPlayerScript.getHatEquip().getImage();
		playerEquipmentPanels [2].sprite = givenPlayerScript.getChestEquip ().getImage();
	//	playerEquipmentPanels [3].sprite = givenPlayerScript.getRingEquip ().getImage();

	}

	public void setInventoryPanelColor(int number, bool tint, bool isEquip)
	{
		if (!isEquip) {
			if (tint)
				playerInventoryPanels [number].color = selectedInventoryColor;
			else
				playerInventoryPanels [number].color = normalInventoryColor;
		} else {
			if (tint)
				playerEquipmentPanels [number].color = selectedInventoryColor;
			else
				playerEquipmentPanels [number].color = normalInventoryColor;

		}
	}

	public void beginToEndTurn(){
		givenPlayerScript.clearNumberofMoves ();
		for (int i = 0; i < AllUnits.Length; i++) {
			if (AllUnits [i].GetComponent<Unit> ().getType () == "enemy") {
				AggroUnits.Add (AllUnits [i]);
			}
		}
		endTurn = 1;
	}

	void enableMouseCube(){
		mouseCube.SetActive (true);
	}

	void disableMouseCube(){
		mouseCube.SetActive (false);
	}

	public void setInventoryTargetText(int number, bool isEquip){

		if (number == -1) {
			selectedInventoryName.text = "";
			selectedInventoryDescription.text = "";
		} 
		else if (!isEquip) {
			selectedInventoryName.text = givenPlayerScript.getPlayerInventory () [number].getName ();
			selectedInventoryDescription.text = givenPlayerScript.getPlayerInventory () [number].getDescript ();			
		} else {
			selectedInventoryName.text = givenPlayerScript.getEquips() [number].getName ();
			selectedInventoryDescription.text = givenPlayerScript.getEquips () [number].getDescript ();

		}
	}

	public void setInventoryInvalidTargetText(){
		selectedInventoryDescription.text = "";
		selectedInventoryName.text = "Invalid Move";
	}

	public void OpenSkillMenu(){
		UpdateSkillMenu ();
		skillPanel.SetActive (true);
	}

	public void CloseSkillMenu(){
		skillPanel.SetActive (false);
	}

	void UpdateSkillMenu(){
		Ability[] abilityList = givenPlayerScript.getAbilities ();
		int largerNum = Mathf.Min (IndependentSkillPanels.Length, abilityList.Length);
		for (int i = 0; i < largerNum; i++) {
			for (int j = 0; j < IndependentSkillPanels [i].transform.childCount; j++) {

				switch (IndependentSkillPanels [i].transform.GetChild (j).name) {
				case "Name":
					IndependentSkillPanels [i].transform.GetChild (j).GetComponent<Text> ().text = abilityList [i].getName ();
					break;
				case "HealthCost":
					IndependentSkillPanels [i].transform.GetChild (j).GetComponent<Text> ().text = "HP: "+abilityList [i].getHealthCost();
					break;
				case "ManaCost":
					IndependentSkillPanels [i].transform.GetChild (j).GetComponent<Text> ().text = "MP: "+abilityList [i].getManaCost();
					break;
				case "StaminaCost":
					IndependentSkillPanels [i].transform.GetChild (j).GetComponent<Text> ().text = "SP: "+abilityList [i].getStaminaCost();
					break;
				}


			}
		}
	}

	public void nextEnemyMove(){
		if (AggroUnits.Count<=1) {
			endTurn = 2;
			return;
		}
		AggroUnits.RemoveAt (0);
		endTurn = 1;
	}

	void Update(){
		SetPlayerPanel ();
		//woo im lazy

		//code below activates and allows al of the enmy units to act at an endof turn when its their turn
		if (endTurn == 1) {
			AggroUnits [0].GetComponent<enemyAIOne> ().activate ();
			endTurn = 0;
		} else if(endTurn==2){
			givenPlayerScript.resetNumberofMoves ();
			endTurn = 0;
		}

		//this code might not be the best; refactor later
		if (skillPanel.activeSelf == true) {
			//this is for the nubmer key one
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				enableMouseCube ();
				mouseScript.setCurAbility (givenPlayerScript.getAbilities()[0].getName ());
				mouseScript.setGivenStats (playerScript.getUnitStatS ());
				CloseSkillMenu ();
			}
		}

		else if (mouseCube.activeSelf == true && Input.GetMouseButton (1)) {
			disableMouseCube ();
		}

		else if (Input.GetKeyDown(KeyCode.E)) {
			if (skillPanel.activeSelf == false) {
				OpenSkillMenu ();
			}
			else
			{
				CloseSkillMenu();
			}
		}
	}

	public void SetPlayerPanel(){
		playerStatPanelText [0].text = "HP : " + playerScript.getHealth() + "/" + playerScript.getMaxHealth ();
		playerStatPanelText [1].text = "MP : " + playerScript.getMana() + "/" + playerScript.getMaxMana ();
		playerStatPanelText [2].text = "SP : " + playerScript.getStamina() + "/" + playerScript.getMaxStamina();
		playerStatPanelText [3].text = "Moves: " + givenPlayerScript.getRemainingMoves ();
	}

	//take in the health, mana, stamina and set the text of the stat panel to it
	public void SetTargetPanelStats(Unit checkPos){
		statPanelTextValues [0].text = checkPos.getName ();
		statPanelTextValues [1].text = "HP : "+checkPos.getHealth()+"/"+checkPos.getMaxHealth();
		statPanelTextValues [2].text = "MP : "+checkPos.getMana()+"/"+checkPos.getMaxMana();
		statPanelTextValues [3].text = "SP : "+checkPos.getStamina()+"/"+checkPos.getMaxStamina();
		statPanelImage.GetComponent<Image>().sprite= checkPos.getImage ();
	}

	public void EnableTargetStatPanel(){
		targetStatPanel.SetActive (true);
	}

	public void DisableTargetStatPane(){
		targetStatPanel.SetActive(false);
	}

	//check if the targetted position is inhabitted by a unit or not
	public bool IsValidMove(int x, int y)
	{
		for (int i = 0; i < AllUnits.Length; i++) {
			Unit checkPos = AllUnits [i].GetComponent<Unit> ();
			if (x == checkPos.tileX) {
				if (y == checkPos.tileZ) {
					return false;
				}
			}
		}

		return true;
	}

	public void activateAbility(int[] stats, string ability, float targetX, float targetZ, int curX, int curZ){
		Unit target = AllUnits[0].GetComponent<Unit>();
		bool correctTarget = false;
		Ability used = abilityDic.getAbility (ability);
		int range = used.getRange ();
	
		for (int i = 0; i < AllUnits.Length; i++) {
			if ((int)targetX == AllUnits [i].GetComponent<Unit> ().tileX) {
				if ((int)targetZ == AllUnits [i].GetComponent<Unit> ().tileZ) {
					target = AllUnits [i].GetComponent<Unit>();
					if ((Mathf.Abs(curX-targetX) + Mathf.Abs(curZ-targetZ)) <= range) {
						correctTarget = true;
						break;
					}
				}
			}
		}

		if (correctTarget) {
			print ("Using ability " + ability + " on target " + target.getName ());
			if (used.getType () == "damage") {
				target.addHealth (-used.getValue (stats, 0));
				//right now we assume all skill leves are 0
				print("New target health is "+target.getHealth());
			}
			SetTargetPanelStats (target);
			EnableTargetStatPanel ();
		} else {
			print ("Target " + target.getName () + " is out of range of ability " + ability + "'s range of " + range + "!");
		}

	}

}
