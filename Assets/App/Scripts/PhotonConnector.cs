using UnityEngine;
// using System.Collections;

public class PhotonConnector : Photon.PunBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("Start");


		Debug.Log(PhotonNetwork.lobby);
		Debug.Log(PhotonNetwork.insideLobby);

		PhotonNetwork.ConnectUsingSettings("0.1");

	}



	public override void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby");

		Debug.Log(PhotonNetwork.lobby);
		Debug.Log(PhotonNetwork.insideLobby);

		_loggingLobbyList ();
	}

	// when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
	public override void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedToMaster");



		Debug.Log(PhotonNetwork.lobby);
		Debug.Log(PhotonNetwork.insideLobby);


		_loggingLobbyList ();




		TypedLobby targetLobby = new TypedLobby("testLobby", LobbyType.Default); 

		PhotonNetwork.JoinLobby(targetLobby);






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




}
