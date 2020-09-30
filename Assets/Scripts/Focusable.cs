using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focusable : MonoBehaviour
{
    public GameObject Highlight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFocus(bool inFocus)
    {
       Highlight.SetActive(inFocus);
    }
}
