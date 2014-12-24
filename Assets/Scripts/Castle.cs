using UnityEngine;
using System.Collections;

public class Castle : MonoBehaviour {

    private int health = 100;
    public GUIText healthText;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void shooted(int hurt) {
        health -= hurt;
        healthText.text = "" + health;
    }
}
