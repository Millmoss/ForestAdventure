/*
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnitHandler_Old : MonoBehaviour {

	public GameObject[] AllUnits;
	//NOTE: This and tilemap both have AllUnits; prob. a way to get both of them to be the same to b laz
	public GameObject targetStatPanel;

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

	void Start () {
		playerScript = playerUnit.GetComponent<Unit> ();
		givenPlayerScript = playerUnit.GetComponent<PlayerScript> ();
		SetPlayerPanel ();
	}

	public void OpenSkillMenu(){
		UpdateSkillMenu ();
		skillPanel.SetActive (true);
	}

	public void CloseSkillMenu(){
		skillPanel.SetActive (false);
	}

	void UpdateSkillMenu(){
		for (int i = 0; i < IndependentSkillPanels.Length; i++) {
			
		}
	}

	//pretty obvious; this changes the stat panel's well stats in order to make usre that you get a good blowjob i mean to make sure that its showing the correct stats
	//RN This code is deprecated; ignore plz
	/*
	public void SetTargetStatPanelStatS(int x, int y){
		for (int i = 0; i < AllUnits.Length; i++) {
			Unit checkPos = AllUnits [i].GetComponent<Unit> ();
			if (x == checkPos.tileX) {
				if (y == checkPos.tileZ) {

					statPanelTextValues [0].text = checkPos.getName ();
					statPanelTextValues [1].text = "HP : "+checkPos.getHealth()+"/"+checkPos.getMaxHealth();
					statPanelTextValues [2].text = "MP : "+checkPos.getMana()+"/"+checkPos.getMaxMana();
					statPanelTextValues [3].text = "SP : "+checkPos.getStamina()+"/"+checkPos.getMaxStamina();
					statPanelImage.GetComponent<Image>().sprite= checkPos.getImage ();
				}
			}
		}
	}


	public void SetPlayerPanel(){
		playerStatPanelText [0].text = "HP : " + playerScript.getMaxHealth() + "/" + playerScript.getHealth ();
		playerStatPanelText [1].text = "MP : " + playerScript.getMaxMana() + "/" + playerScript.getMana ();
		playerStatPanelText [2].text = "SP : " + playerScript.getMaxStamina() + "/" + playerScript.getStamina();
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

	// Update is called once per frame
	void Update () {
	
	}
}
*/ 