using UnityEngine;
using System.Collections;

public class Cursor_CTRL : MonoBehaviour {
    public bool IsShow_cursor = false;
    public Texture cursor_pen, cursor_rubber, cursor_nomal;
    public int tag_cursor = 0;
    Vector3 mousePositionNow;

	// Use this for initialization
	void Start () {
        Screen.showCursor = IsShow_cursor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        mousePositionNow = Input.mousePosition;
        if (tag_cursor == 0)
        {
            GUI.DrawTexture(new Rect(mousePositionNow.x - 10f, Screen.height - mousePositionNow.y - 10f, cursor_nomal.width * 0.1f, cursor_nomal.height * 0.1f), cursor_nomal);
        }
        else if (tag_cursor == 1)
        {
            GUI.DrawTexture(new Rect(mousePositionNow.x, Screen.height - mousePositionNow.y, cursor_pen.width, cursor_pen.height), cursor_pen);
        }
        else
        {
            GUI.DrawTexture(new Rect(mousePositionNow.x, Screen.height - mousePositionNow.y, cursor_rubber.width, cursor_rubber.height), cursor_rubber);
        }
    }
}
