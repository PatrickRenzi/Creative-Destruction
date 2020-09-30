using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollLeft : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public float ScrollingSpead;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x - (ScrollingSpead * Time.fixedDeltaTime), transform.position.y , transform.position.z);

        if (transform.position.x <= EndPoint.position.x)
            transform.position = new Vector3(StartPoint.position.x, StartPoint.position.y, StartPoint.position.z);
    }
}
