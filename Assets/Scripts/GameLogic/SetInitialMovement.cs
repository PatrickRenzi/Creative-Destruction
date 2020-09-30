using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInitialMovement : MonoBehaviour
{
    public Rigidbody2D RigidBody2D;

    public Vector2 InitialMovement;

    // Start is called before the first frame update
    void Start()
    {
        if(RigidBody2D == null)
        {
            RigidBody2D = this.gameObject.GetComponent<Rigidbody2D>();
        }

        RigidBody2D.velocity = InitialMovement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
