using UnityEngine;
using System.Collections;

public class player : Photon.MonoBehaviour
{
	public int union = 1;
	public int level = 1;

	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {

		// 自分以外は動かさない
		if (!photonView.isMine) return;

		float newX = Input.GetAxis("Horizontal") *2.0f;
		float newZ = Input.GetAxis("Vertical") *2.0f;
		float newY = Input.GetButtonDown("Jump") ? 5.0f : 0;

		if(newX == 0 || newY == 0 || newZ == 0){
			if(newY == 0) newY = rigidBody.velocity.y;
			rigidBody.velocity = new Vector3 (newX, newY, newZ);
		}
	}

	public void OnGUI()
	{
		// 何か右端に出すおまじない
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
		GUILayout.BeginHorizontal();
		{
			GUILayout.FlexibleSpace();

			GUILayout.BeginVertical();
			{
				GUILayout.Label("Union:"+union);
				GUILayout.Label("Level:"+level);
			}
			GUILayout.EndVertical();
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea(); 

	}


	public void setUnion(float val){
		union = (int)val;
	}

	public void setLevel(float val){
		level = (int)val;
	}


}
