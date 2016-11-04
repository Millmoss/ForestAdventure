using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData : MonoBehaviour
{
    public GameObject grassBlock;
    public GameObject enemy1;
    private LinkedList<LevelObject>[,] levelData; //holds all tiles, items, and creatures in level by height they are at //for example, monster is stacked above floor tile is stacked above player is stacked above dropped potion is stacked above grass tile
    
    void Start ()
    {
        //insert file reading code here so that it reads off level layout for ground and placement of stuff above ground
        int xSize = 50; //x
        int ySize = 50; //z technically
        //temporary ground code
        levelData = new LinkedList<LevelObject>[xSize, ySize];
        for(int y=0;y<ySize;y++)
        {
            for(int x=0;x<xSize;x++)
            {
                levelData[x, y] = new LinkedList<LevelObject>();
                levelData[x, y].AddFirst(new LevelObject(-1, 'G'));
            }
        }
        levelData[11, 11].AddLast(new LevelObject(3, 'E'));
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                LinkedListNode<LevelObject> temp = levelData[x, y].First;
                bool end = false;
                do
                {
                    if (temp.Value.getObjectType() == 'G')
                    {
                        Instantiate(grassBlock);
                        grassBlock.transform.position = new Vector3(x + .5f, temp.Value.getHeight(), y - .5f);
                    }
                    else if (temp.Value.getObjectType() == 'E')
                    {
                        Instantiate(enemy1);
                        print("agh");
                    }
                    if (temp.Next != null)
                        temp = temp.Next;
                    else
                        end = true;
                } while (!end);
            }
        }
    }
	void Update ()
    {
	    
	}
}

public class LevelObject
{
    private float height;
    private char objectType;

    public LevelObject(float h,char t)
    {
        height = h;
        objectType = t;
    }

    public float getHeight()
    {
        return height;
    }

    public char getObjectType()
    {
        return objectType;
    }
}
