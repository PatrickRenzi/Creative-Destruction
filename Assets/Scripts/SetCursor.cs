using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    public Texture2D cursorGraphic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.SetCursor(cursorGraphic, new Vector2(cursorGraphic.width / 2, 5), CursorMode.ForceSoftware);
    }
}
