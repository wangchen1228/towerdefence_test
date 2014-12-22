
#pragma strict
#pragma downcast

function OnGUI(){
	GUI.BeginGroup(Rect(Screen.width/2-75, 500, 150, 240));
		if(GUI.Button(Rect(0, 0, 150, 30), "Example Level 1")){
			Application.LoadLevel(1);
		}
		if(GUI.Button(Rect(0, 40, 150, 30), "Example Level 2")){
			Application.LoadLevel(2);
		}
		if(GUI.Button(Rect(0, 80, 150, 30), "Example Level 3")){
			Application.LoadLevel(3);
		}
		if(GUI.Button(Rect(0, 120, 150, 30), "Example Level 4")){
			Application.LoadLevel(4);
		}
		if(GUI.Button(Rect(0, 160, 150, 30), "Example Level 5")){
			Application.LoadLevel(5);
		}
		if(GUI.Button(Rect(0, 200, 150, 30), "Example Level 6")){
			Application.LoadLevel(6);
		}
	GUI.EndGroup();
	
	GUI.Label(Rect(Screen.width-150, Screen.height-30, 150, 25), "Created by Song Tan");
}