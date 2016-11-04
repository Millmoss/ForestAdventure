using UnityEngine;
using System.Collections;

public class Item{

	public string name;
	public string descript;
	public Sprite imageRepresentation;

	public Item(){
		name = "Hail Twas Noon";
		descript = "Todokete";
		imageRepresentation = Resources.Load<Sprite>("TempAssets/inventoryBackground");
	}

	public Sprite getImage(){
		
		return imageRepresentation;
	}

	public string getName()
	{
		return name;
	}

	public string getDescript(){
		return descript;
	}
}
