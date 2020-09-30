using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByAsteriod : MonoBehaviour
{
    public AudioSource CollectedResourceDing;

    public Collider2D HBA_Collider2D;
    // Start is called before the first frame update
    void Start()
    {
        if (HBA_Collider2D == null)
            HBA_Collider2D = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Resource")
        {
            CollectedResourceDing.Play();
            Destroy(collision.gameObject);
        }
    }
}
