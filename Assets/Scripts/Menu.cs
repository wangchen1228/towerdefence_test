using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    public static bool bStart = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUILayout.Button("Start"))
        {
            bStart = true;
        }
    }
}
