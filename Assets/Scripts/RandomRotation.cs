using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public float MaxRotationalSpeed;

    public Vector3 RotationalDirection;

    public Vector3 rotationStatus;

    public bool UseRandomRotation = true;

    // Start is called before the first frame update
    void Start()
    {

        if (UseRandomRotation == true)
        {
            int axisToRotate = Random.Range(0, 3);

            float rotation = Random.Range(-140, 140);

            transform.rotation = Random.rotation;

            switch (axisToRotate)
            {
                case 0: //rotate x
                    RotationalDirection = new Vector3(rotation, 0f, 0f);
                    break;
                case 1://rotate y
                    RotationalDirection = new Vector3(0f, rotation, 0f);
                    break;
                case 2: //rotate z
                    RotationalDirection = new Vector3(0f, 0f, rotation);
                    break;
                default:
                    Debug.Log("not 0, 1, or 2");
                    break;
            }
        }
        //create rotation vector;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
     

        transform.Rotate(new Vector3(RotationalDirection.x * Time.fixedDeltaTime, RotationalDirection.y * Time.fixedDeltaTime, RotationalDirection.z * Time.fixedDeltaTime));
    }
}
