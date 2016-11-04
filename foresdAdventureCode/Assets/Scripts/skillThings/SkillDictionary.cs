using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillDictionary : MonoBehaviour {
	
	Dictionary<string, Skill> storedSkills = new Dictionary<string, Skill>();

	// Use this for initialization
	void Start () {

		string skillName = "bluntSkill";
		Skill tempSkill = new Skill (skillName, 100);
		storedSkills.Add (skillName,tempSkill);

		skillName = "slashSkill";
		tempSkill = new Skill (skillName, 100);
		storedSkills.Add (skillName,tempSkill);

		skillName = "pierceSkill";
		tempSkill = new Skill (skillName, 100);
		storedSkills.Add (skillName,tempSkill);

	}
	
	public Skill getSkill(string givenName){
		return storedSkills[givenName];
	}

	public Dictionary<string,Skill> getAllSkills(){
		return storedSkills;
	}
	//this is used to instantaite the player's skills initially
}
