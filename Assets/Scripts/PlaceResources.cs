using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceResources : MonoBehaviour
{
    public List<Transform> SpawnPoints;

    public Transform asteriod;

    public SphereCollider SphereCollider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceRandomResources(List<GameObject> ResourcePrefabs)
    {
        
        foreach (GameObject resourcePrefab in ResourcePrefabs)
        {
            if (SpawnPoints.Count > 0)
            {
                int indexSpawn = Random.Range(0, SpawnPoints.Count);
                var newResource = Instantiate(resourcePrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
                newResource.transform.localScale = new Vector3(newResource.transform.localScale.x / asteriod.localScale.x, newResource.transform.localScale.y / asteriod.localScale.y, newResource.transform.localScale.z / asteriod.localScale.z);
                newResource.transform.SetParent(SpawnPoints[indexSpawn], false);

                SpawnPoints.RemoveAt(indexSpawn);
            }
        }
    }
}
