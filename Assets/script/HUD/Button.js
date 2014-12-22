
#pragma strict
#pragma downcast

private var defaultRect:Rect;
private var defaultColor:Color;
private var activeRect:Rect;
private var activeColor:Color;

function Start(){
	defaultRect=guiTexture.pixelInset;
	defaultColor=guiTexture.color;
	activeRect=Rect(defaultRect.x-1.5, defaultRect.y-1.5, defaultRect.width+3, defaultRect.height+3);
	activeColor=Color(defaultColor.r+0.05, defaultColor.g+0.05, defaultColor.b+0.05, defaultColor.a);
}


function OnMouseEnter(){
	guiTexture.pixelInset=activeRect;
	guiTexture.color=activeColor;
}

function OnMouseExit(){
	guiTexture.pixelInset=defaultRect;
	guiTexture.color=defaultColor;
}
