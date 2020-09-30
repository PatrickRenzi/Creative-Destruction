using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTouch : MonoBehaviour
{
    //public static Collider2D CurrentFocus;

    public Collider2D MBT_Collider2D;

    public bool InFocus;
    // Start is called before the first frame update
    void Awake()
    {
        if (MBT_Collider2D == null)
            MBT_Collider2D = this.gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            TouchAction(Input.GetTouch(0).position);
        else if (Input.GetMouseButtonDown(0))
            TouchAction(Input.mousePosition);
        else if (Input.GetMouseButtonUp(0))
            InFocus = false;

    }

    private void TouchAction(Vector2 paramScreenPosition)
    {
        var worldPosition = Camera.main.ScreenToWorldPoint(paramScreenPosition);

        RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);


        if (hitData)
        {
            if (hitData.collider == MBT_Collider2D)
                InFocus = true;
            else
                InFocus = false;
        }
    }

    private void FixedUpdate()
    {
        if (InFocus == true)
        {
            Vector3 mousePos = Input.mousePosition;
            var target = (Vector2)Camera.main.ScreenToWorldPoint(mousePos);

            target = Vector2.Lerp((Vector2)transform.position, (Vector2)target, Time.fixedDeltaTime * 4);
            
            transform.position = new Vector3(target.x, transform.position.y, transform.position.z);

        }
    }
}
