using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfter3Seconds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("KillSelf", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void KillSelf()
    {
        Destroy(this.gameObject);
    }
}
