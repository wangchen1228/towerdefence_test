
#pragma strict
#pragma downcast

private var parentCom:Button;

function Start(){
	parentCom=transform.parent.gameObject.GetComponent.<Button>();
}

function OnMouseEnter(){
	parentCom.OnMouseEnter();
}

function OnMouseExit(){
	parentCom.OnMouseExit();
}
