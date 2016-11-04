using UnityEngine;
using System.Collections;

public class Armor : Item {

	public Armor(){
		addHealth = 10;
		name = "Angel Angst My";
		descript = "op";
	}

	public Armor(string givenName, string givenDescript, int givenHealth, string givenImagePath){
		name = givenName;
		addHealth = givenHealth;
		descript = givenDescript;
		imageRepresentation = Resources.Load<Sprite>(givenImagePath) as Sprite;
	}



	//we havemultiple types of armor:
	/*
	 * kigurumis
	 * rings
	 * /

	//kigurumi types:
	/*red panda
	 * panda
	 * dinosaur
	 * fox
	 * tabby cat
	 * hedgehog
	 * blue unicorn
	 * zingore
	 * gold hedgehog
	 * rathain
	 * rainbow panda
	 * chicken
	 * gore magala
	 * dreamin panda
	 * abyss watchers
	 * royalnt
	 * cow
	 * cat
	 * reindeer
	 * dog
	 * doggo
	 * doggy
	 * dogiest
	 * doeer?
	 * suschi cat
	 * the entire population of iceland
	 * hen wearing tie
	 * bear
	 * computer part bear
	 * furyhorn
	 * chimera
	 * rathalos
	 * styngian zynogre
	 * beholder (dnd)
	 * beholder (ff)
	 * gandalf
	 * mimichu
	 * bahamut
	 * lgith metatron
	 * isis (dragon quest/dog)
	 * a katana
	 */

   /*
	 * AND BUILDY OUR OWN KIGURUMI
	 *
	 */
}
