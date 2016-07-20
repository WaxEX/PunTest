using UnityEngine;
using System;
using System.Collections;

using Hashtable = ExitGames.Client.Photon.Hashtable;


public class PhotonConnector : Photon.PunBehaviour {


	private GameObject player;


/*********************************************************************
 * Unity funcs
 *********************************************************************/
	void Start () {
		player = GameObject.Find("Player");

		Debug.Log("Start");

		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	public void OnGUI()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
		GUILayout.Label (PhotonNetwork.lobby.ToString ());
		if(PhotonNetwork.room != null) GUILayout.Label (PhotonNetwork.room.ToString ());
	}


/*********************************************************************
 * PUN
 *********************************************************************/
	// when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
	public override void OnConnectedToMaster(){
		Debug.Log("OnConnectedToMaster");
		PhotonNetwork.JoinLobby(getLobbyInfo());

		//static bool PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby); //コレ使えば楽なんじゃねー？
	}

	public override void OnJoinedLobby(){
		Debug.Log("OnJoinedLobby");
		_loggingLobbyList();

		StartCoroutine ("waitLog");
	}

	// コルーチンで処理を遅くする
	private IEnumerator waitLog() { 
		yield return new WaitForSeconds (1.0f);  
		_loggingRoomList();
	} 
		
	// ボタンアタッチとかで起動。ROOMに入る
	public void JoinRoom(){
		Debug.Log("tryJoinRoom");

		// 部屋に入ってる場合
		if (PhotonNetwork.room != null) {
			PhotonNetwork.LeaveRoom();
			return;
		}

		PhotonNetwork.JoinRandomRoom(getRoomProperties(), 0);
	}
		

	public void OnPhotonRandomJoinFailed(){
		Debug.Log("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom(null, getRoomOptions(), null);
	}
		
	public override void OnJoinedRoom(){
		Debug.Log("OnJoinedRoom");
	}

	private TypedLobby getLobbyInfo(){
		string lobby_name = "Union0"+player.GetComponent<player>().union;
		return new TypedLobby(lobby_name, LobbyType.Default);
	}

	private Hashtable getRoomProperties(){
		return new Hashtable(){
			{"LvZone", (int)player.GetComponent<player>().level /10}
		};
	}

	private RoomOptions getRoomOptions(){
		RoomOptions option = new RoomOptions();
		option.IsVisible  = true;
		option.IsOpen     = true;
		option.maxPlayers = 3;

		option.CustomRoomProperties = getRoomProperties();
		option.customRoomPropertiesForLobby = new string[] {"LvZone"};

		return option;
	}
		

/*********************************************************************
 * for DEBUG
 *********************************************************************/
	private void _loggingLobbyList(){
		var list = "===Lobbies: ";
		list += PhotonNetwork.LobbyStatistics.Count+"===\n";
		PhotonNetwork.LobbyStatistics.ForEach(q => list += q.ToString()+"\n");
		Debug.LogError(list);
	}

	public void _loggingRoomList(){
		if(!PhotonNetwork.insideLobby){
			Debug.Log("You're out of lobby...");
			return;
		}

		var rooms = PhotonNetwork.GetRoomList();
		var list = "===Rooms: ";
		list += rooms.Length+"===\n";
		Array.ForEach(rooms, q => list += q.ToString()+"\n");
		Debug.LogError(list);
	}
}
