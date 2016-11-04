using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {

	string name;
	int level=1;
	//pretty sure most skills start at level one, if not all of them.
	int maxLevel;


	public Skill(){
		name = "katiekamper";
		level = 1;
		maxLevel = 99;
	}

	public Skill(string givenName, int givenMaxLevel)
	{
		name = givenName;
		maxLevel = givenMaxLevel;
	}

	public void addLevel(int addNum)
	{
		level += addNum;
		if (level > maxLevel)
			level = maxLevel;
	}

	public int getLevel(){
		return level;
	}

	public int getMaxLevel(){
		return maxLevel;
	}

}
