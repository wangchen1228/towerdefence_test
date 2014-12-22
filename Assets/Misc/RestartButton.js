
//this script create a guiText object that act as a restart button
//attach the script to any empty gameObject in the scene
//upon clicking the text, the scene will restart


private var restartButton:GUIText;

function Start(){
	gameObject.transform.position=Vector3((Screen.width-200.0)/Screen.width, (Screen.height-30.0)/Screen.height, 10);
	restartButton=gameObject.AddComponent.<GUIText>();
	restartButton.text="Restart";
	restartButton.fontSize=75;
}

function Update () {
	if(Input.touchCount==1){
		var touch:Touch=Input.touches[0];
		if (touch.phase == TouchPhase.Began){
			var touchPos:Vector2 = touch.position;
			if(restartButton.HitTest(touchPos)){
				Application.LoadLevel(Application.loadedLevelName);
			}
		}
	}
}