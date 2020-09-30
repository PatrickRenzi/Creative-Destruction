using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportToPlanet : MonoBehaviour
{
    public GameObject destination;

    public Transform SpawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        SpawnLocation = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDestination(GameObject newDestination)
    {
        var rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(0f, 0f);
        destination = newDestination;
    }

    private void FixedUpdate()
    {
        if (destination != null)
        {
            transform.position = Vector3.Lerp(SpawnLocation.position, destination.transform.position, Time.deltaTime * 2f);
        }
    }
}
