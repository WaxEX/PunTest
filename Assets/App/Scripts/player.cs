using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	public int union = 1;
	public int level = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
