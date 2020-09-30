using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotationOnce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Random.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
