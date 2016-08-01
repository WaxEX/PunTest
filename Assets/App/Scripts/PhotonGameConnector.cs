using UnityEngine;
using System.Collections;

public class PhotonGameConnector : Photon.PunBehaviour
{
	//for Unity instantiate
	public Transform _player;

	private PhotonView myPhotonView;

	// Use this for initialization
	void Start () {

		//Instantiate(_player, new Vector3(0, 5.0f, 0), Quaternion.identity);

		GameObject player = PhotonNetwork.Instantiate("player", new Vector3(0, 5.0f, 0), Quaternion.identity, 0);
		myPhotonView = player.GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
