using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fling : MonoBehaviour
{
    public Collider2D F_Collider2D;

    public Rigidbody2D F_RigidBody2D;

    public SetInitialMovement InitialMovement;

    public float MaxForce = 5;

    public TrailRenderer FlingTrail;

    public bool InFocus
    {
        get
        {
            return InputManager.instance.CurrentFocus == this.gameObject;
        }
    }

    public Transform FlingStartPosition;

    public float FlingTimeElapsed;

    // Start is called before the first frame update
    void Awake()
    {
        if (F_Collider2D == null)
            F_Collider2D = this.gameObject.GetComponent<Collider2D>();
        if (F_RigidBody2D == null)
            F_RigidBody2D = this.gameObject.GetComponent<Rigidbody2D>();
        if(InitialMovement == null)
            InitialMovement = this.gameObject.GetComponent<SetInitialMovement>();
    }

}
