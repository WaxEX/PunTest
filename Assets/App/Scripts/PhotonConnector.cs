using UnityEngine;
// using System.Collections;

public class PhotonConnector : Photon.PunBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("Start");

		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	// when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
	public override void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedToMaster");

		_loggingLobbyList ();

		TypedLobby targetLobby = new TypedLobby("testLobby", LobbyType.Default); 
		PhotonNetwork.JoinLobby(targetLobby);



		// コレ使えば楽なんじゃねー？
		//static bool PhotonNetwork.JoinOrCreateRoom(	string		roomName,
		//												RoomOptions	roomOptions,
		//												TypedLobby	typedLobby 
		//)	
	}

	public override void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby");

		_loggingLobbyList ();

		_loggingRoomList();


		if(PhotonNetwork.room != null) Debug.Log(PhotonNetwork.room.ToString());

		PhotonNetwork.JoinRandomRoom();
	}


	public void OnPhotonRandomJoinFailed()
	{
		Debug.Log("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom(null);
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");

		_loggingRoomList ();

		if(PhotonNetwork.room != null) Debug.Log(PhotonNetwork.room.ToString());
	}



	
	// Update is called once per frame
	void Update () {
	
	}


	public void OnGUI()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}
		

// for DEBUG
	private void _loggingLobbyList(){
		var list = "===Lobbies: ";
		list += PhotonNetwork.LobbyStatistics.Count+"===\n";
		PhotonNetwork.LobbyStatistics.ForEach(q => list += q.ToString()+"\n");
		Debug.Log(list);
	}



	private void _loggingRoomList(){
		if(!PhotonNetwork.insideLobby){
			Debug.Log("You're out of lobby...");
			return;
		}

		var rooms = PhotonNetwork.GetRoomList();

		var list = "===Rooms: ";
		list += rooms.Length+"===\n";
		//rooms.ForEach(q => list += q.ToString()+"\n");
		Debug.Log(list);
	}


}
