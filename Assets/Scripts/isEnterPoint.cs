using UnityEngine;
using System.Collections;

public class isEnterPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseEnter()
	{
		if(renderer.material.color==Color.red)
		{
			
		}else
		{
			renderer.enabled=true;
		}
	}
//	void OnMouseDown()
//	{
//	  renderer.enabled=true;
//	  renderer.material.color=Color.red;
//	} 
	void OnMouseExit()
	{
		if(renderer.material.color==Color.red)
		{
			
		}else
		{
			renderer.enabled=false;
		}
	}
}
