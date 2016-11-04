using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour {

	//public int meshSize;

	public int size_x=100;
	public int size_z=50;
	public float tileSize=1.0f;

	//public Texture2D terrainTiles;

	public int tileResolution;


	//this is a list of all units currently on the map
	public Unit[] AllUnits;

	int[,] tiles;
	public Node[,] graph;

	//right, so tiles represent the tiles of the map, and graph is used for pathfinding if we use it


	public TileType[] tileTypes;
	//right; this stuff should be used to like list the types of well tiles (mountain, impass, ect.)


	//int tileResolution=8;
	//number of tiles in x and z direction

	//same stuff as in unit
	SystemHandler unitHandler;
	public GameObject eventSystem;

	// Use this for initialization
	void Start () {
		BuildGrid ();
		BuildMesh ();
		unitHandler = eventSystem.GetComponent<SystemHandler> ();
		print (graph [0, 0]);
	}

	void OnMouseDown(){
		unitHandler.DisableTargetStatPane();
	}

	//this cuts up the tiles into parts; is VERY important.
	//notice that tileREsoultion MUST be the size of a single tile within the picture, or we're goingt o have a bad time lol
	/*Color[][]  ChopUpTiles(){
		int numTilesPerRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;	
	
		Color [][] tiles = new Color[numTilesPerRow*numRows][];
	
		for (int y = 0; y < numRows; y++) {
			for (int x = 0; x < numTilesPerRow; x++) {
				tiles[y*numTilesPerRow+x] = terrainTiles.GetPixels (x * tileResolution, y * tileResolution, tileResolution, tileResolution);

			}
		}
		return tiles;
	}*/

	/*void BuildTexture() {

		int texWidth = size_x * tileResolution;
		int texHeight = size_z * tileResolution;

		Texture2D texture = new Texture2D (texWidth,texHeight);

		Color [][] tiles = ChopUpTiles();


		for (int y = 0; y < size_x; y++) {
			for (int x = 0; x < size_z; x ++) {
				Color[] p = tiles[Random.Range(0,4)];
				//currently we have the 2nd tile as the 'grass tile', so
				p =tiles[1]; 
				texture.SetPixels (x*tileResolution, y*tileResolution, tileResolution, tileResolution, p);
			}
		}

		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply ();


		
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer> ();
		mesh_renderer.sharedMaterials [0].mainTexture = texture;	

	}*/

	//this code checks throughout all of the given thingy i mean units and sees if the given coords are already inhabited!
	public bool CheckIfAlreadyInhabited(int x, int z)
	{
		bool canMoveInto = true;
		for (int i = 0; i < AllUnits.Length; i++) {
			if (AllUnits [i].tileX == x) {
				if (AllUnits [i].tileZ == z) {
					canMoveInto = false;
				}
			}
		}
		return canMoveInto;
	}

	//check if the mentioned spot can be moved into. note that the x and y values should the position BEFORE moving, with the direction representing what direction is it that they moved in.
	//tbh, dictionaries would make this easier but so laz.
	public bool CheckIfCanPass(int x, int z, string direction){
		bool canMove = false;
		Node comparedSpot = graph [x, z];

		switch (direction) {

		//moving up 
		case "up":

			//becuase this is moving up, we need to add 1 from z.
			for (int i = 0; i < comparedSpot.neighbours.Count; i++) {
				//right, so first we check if the tile that the person wishes to move towards actually exits
				if (comparedSpot.neighbours [i].z == z + 1) {
					//now, we need to check that the tile that the unit wants ot move onto isn't occupied by another unit!
					if (CheckIfAlreadyInhabited(x,z+1)){
						canMove = true;
					}
				}
					
			}
			break;
		case "down":
			for (int i = 0; i < comparedSpot.neighbours.Count; i++) {
				if (comparedSpot.neighbours [i].z == z-1)
				if (CheckIfAlreadyInhabited(x,z-1)) {
						canMove = true;
					}
			}
			break;

		case "left":
			for (int i = 0; i < comparedSpot.neighbours.Count; i++) {
				if (comparedSpot.neighbours [i].x == x-1)
				if (CheckIfAlreadyInhabited(x-1,z)) {
						canMove = true;
					}
			}
			break;

		case "right":
			for (int i = 0; i < comparedSpot.neighbours.Count; i++) {
				if (comparedSpot.neighbours [i].x == x+1)
				if (CheckIfAlreadyInhabited(x+1,z)) {
						canMove = true;
					}												
			}
			break;
		}

		return canMove;
	}

	public Node getGraphNodeAtPosition(int x, int z){
		return graph [x, z];
	}

	void BuildGrid(){
		graph = new Node[size_x,size_z];
		// Initialize a Node for each spot in the array
		for(int x=0; x < size_x; x++) {
			for(int z=0; z < size_z; z++) {
				graph[x,z] = new Node();
				graph[x,z].x = x;
				graph[x,z].z = z;
			}
		}

		// Now that all the nodes exist, calculate their neighbours
		for(int x=0; x < size_x; x++) {
			for(int z=0; z < size_z; z++) {

				// This is the 4-way connection version:
				if(x > 0)
					graph[x,z].neighbours.Add( graph[x-1, z] );
				if(x < size_x-1)
					graph[x,z].neighbours.Add( graph[x+1, z] );
				if(z > 0)
					graph[x,z].neighbours.Add( graph[x, z-1] );
				if(z < size_z-1)
					graph[x,z].neighbours.Add( graph[x, z+1] );
			}
		}


	}

	public void BuildMesh(){ 

		int numTiles = size_x * size_z;
		int numTris = numTiles * 2;

		int vsize_x = size_x + 1;
		int vsize_z = size_z + 1;
		int numVerts = vsize_x * vsize_z;

		Vector3[] vertices = new Vector3[numVerts];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];

		int[] triangles = new int[ numTris * 3];

		int x, z;
		for (z = 0; z < vsize_z; z++) {
			for (x = 0; x < vsize_x; x++) {
				vertices[ z * vsize_x + x] = new Vector3(x*tileSize, 0, z*tileSize);
				normals [z * vsize_x + x] = Vector3.up;
				uv [z * vsize_x + x] = new Vector2 ((float)x / size_x, (float)z / size_z);
			}
		}

		for (z = 0; z < size_z; z++) {
			for (x = 0; x < size_x; x++) {
				int squareIndex = z * size_x + x;
				int triOffset = squareIndex * 6;

				triangles [triOffset + 0] = z * vsize_x + x + 0;
				triangles [triOffset + 1] = z * vsize_x + x + vsize_x + 0;
				triangles [triOffset + 2] = z * vsize_x + x + vsize_x + 1;

				triangles [triOffset + 3] = z * vsize_x + x + 0;
				triangles [triOffset + 4] = z * vsize_x + x + vsize_x + 1;
				triangles [triOffset + 5] = z * vsize_x + x + 1;

			}
		}

		for (int i = 0; i < vertices.Length; i++) {
//			vertices[i].
		}

		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;

		MeshFilter mesh_filter = GetComponent<MeshFilter> ();
		MeshCollider mesh_collider = GetComponent<MeshCollider> ();

		mesh_collider.sharedMesh = mesh;
		mesh_filter.mesh = mesh;

		//this is for a randomly generated texture
		//BuildTexture ();

		//but, we can just use this code instead!
		//BuildGrid();

	}





}
