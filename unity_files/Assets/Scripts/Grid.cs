using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Grid : MonoBehaviour {

	public Tile tilePrefab;
	public FBManager fbManager;
	public LinkSyncSCR connManager;
	public int numberOfTiles = 10;
	public float distanceBetweenTiles = 1f;
	public int tilesPerRow = 4;
	public int numberOfMines = 5;

	public static Tile[] tilesAll;
	public static List<Tile> tilesMined;
	public static List<Tile> tilesUnmined;

	public static string state;

	public static int minesMarkedCorrectly;
	public static int tilesUncovered;
	public static int minesRemaining;
	public bool bIsGameSetup = false;
	public TextMesh displayText;

	// Use this for initialization
	void Awake()
	{
		fbManager = new FBManager ();
		connManager = new LinkSyncSCR ();
	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(state == "inGame"){
			if((minesRemaining == 0 && minesMarkedCorrectly == numberOfMines) || (tilesUncovered == numberOfTiles - numberOfMines)){
				finishGame();
			}
		}
	}

	void setupGame(){
		CreateTiles ();
		minesMarkedCorrectly = 0;
		tilesUncovered = 0;
		minesRemaining = numberOfMines;
		state = "inGame";
		bIsGameSetup = true;

	}

	void finishGame(){
		state = "gameWon";
		//uncovers remaining fields if all nodes have been placed
		foreach(Tile currentTile in tilesAll){
			if(currentTile.state == "idle" && !currentTile.isMined){
				currentTile.uncoverTileExternal();
			}
		}

		//marks remaining mines if all nodes except the mines have been uncovered
		foreach(Tile currentTile in tilesAll){
			if(currentTile.state != "flagged"){
				currentTile.setFlag();
			}
		}
	}

	void CreateTiles(){

		tilesAll = new Tile[numberOfTiles];

		float xOffset = 0f;
		float zOffset = 0f;

		for (int tilesCreated = 0; tilesCreated < numberOfTiles; tilesCreated++) {
			xOffset += distanceBetweenTiles;

			if(tilesCreated % tilesPerRow == 0){
				zOffset += distanceBetweenTiles;
				xOffset = 0;
			}

			Tile newTile = (Tile)Instantiate(tilePrefab, new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z + zOffset), transform.rotation);
			newTile.ID = tilesCreated;
			newTile.tilesPerRow = tilesPerRow;
			tilesAll[tilesCreated] = newTile;
		}

		AssignMines ();
	}

	void AssignMines(){
		tilesUnmined = new List<Tile>(tilesAll);
		tilesMined = new List<Tile>();

		for (int minesAssigned = 0; minesAssigned < numberOfMines; minesAssigned++) {
		
			Tile currentTile = (Tile)tilesUnmined[Random.Range(0, tilesUnmined.Count)];

			currentTile.GetComponent<Tile>().isMined = true;

			//Add to Tiles mined
			tilesMined.Add(currentTile);
			//Remove from unmined
			tilesUnmined.Remove(currentTile);
		}
	}

	void OnGUI(){

		if (fbManager.isLoggedIn) {
			if(fbManager.hasImage) {
				GUI.DrawTexture(new Rect(10,200,128,128), fbManager.texture, ScaleMode.StretchToFill, true);
			}
			if(fbManager.hasUserName){
				GUI.Box(new Rect(10,350,200,50), "Hello " + fbManager.profile["first_name"].ToString() + " " + fbManager.profile["last_name"].ToString());
				GUI.Box(new Rect(10,410,200,50),  fbManager.profile["email"].ToString());
				GUI.Box(new Rect(10,470,200,50),  fbManager.profile["id"].ToString());
			
			}
			if (GUI.Button (new Rect (10, 70, 200, 50), "Share")) {
				fbManager.ShareWithFriends ();

			}

			if(!connManager.connected)
			{
				connManager.ConnectToServer();
			}else {
				//connected to server
				if(!bIsGameSetup)
				{
				setupGame();
				}

			}

		} else {
			if(GUI.Button(new Rect(10,70,200,50), "Facebook Login"))
			{
				fbManager.FBlogin ();
			}

		}
		if(state == "inGame"){
			GUI.Box(new Rect(10,10,200,50), "Mines Remaining " + minesRemaining.ToString());
		}
		else if(state == "gameOver"){
			GUI.Box(new Rect(10,10,200,50), "You lose!");

			if(GUI.Button(new Rect(10,130,200,50), "Restart")){
				restart();
			}
		}
		else if(state == "gameWon"){
			GUI.Box(new Rect(10,10,200,50), "You win!");

			if(GUI.Button(new Rect(10,130,200,50), "Restart")){
				restart();
			}
		}
	}

	void restart(){
		state = "loading";
		/*
		bIsGameSetup = false;
		foreach(Tile currentTile in tilesAll){
			currentTile.displayText.GetComponent<Renderer>().enabled = false;
			currentTile.displayFlag.GetComponent<Renderer>().enabled = false;
		}
		*/
		Application.LoadLevel (Application.loadedLevel);
	}


}