using UnityEngine;
using System.Collections;

public class DeskTubiao : MonoBehaviour {
	public Ray Gunsight;
	public RaycastHit Hit;
	public Texture2D Cannon;
	public GameObject myCannon;
	bool isCreat=false;
	void Start () 
	{
	
	}
	Rect AutoFit(Rect rect)
	{
		return new Rect(rect.x/1024*Screen.width,rect.y/768*Screen.height,rect.width/1024*Screen.width,rect.height/768*Screen.height);
	}
	
	void Update () 
	{
		Gunsight = camera.ScreenPointToRay(Input.mousePosition);
		if(isCreat==true)
		{
			 if(Physics.Raycast(Gunsight, out Hit))
			{	
				if(Hit.collider.name=="Plane")
				{
					
				}else 
				{
					if(Input.GetMouseButtonDown(0)&&Hit.collider.renderer.material.color!=Color.red)
					{
						Hit.collider.renderer.material.color=Color.red;
						Hit.collider.renderer.enabled=true;
						Object.Instantiate(myCannon,Hit.collider.transform.position+new Vector3(0,0.6f,0),Quaternion.identity);
						isCreat=false;
					}
				}
			}
		}
	
	}
		void OnGUI()
	{
		if(GUI.Button(AutoFit(new Rect(0,702,64,64)),Cannon,GUIStyle.none))
		{
			isCreat=true;
		}
		if(isCreat==true)
		{
			GUI.Button(new Rect(Input.mousePosition.x-32,Screen.height-Input.mousePosition.y-32,64,64),Cannon,GUIStyle.none);
		}
	}

}
				