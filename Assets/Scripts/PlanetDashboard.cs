using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDashboard : MonoBehaviour
{
    public static PlanetDashboard instance;

   
    public List<Transform> PlanetSpots;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttemptToRefocus()
    {
        if (Planet.CurrentPlanetFocus == null || Planet.CurrentPlanetFocus.OrderComplete())
        {
            for (int i = 0; i < PlanetSpots.Count; i++)
            {
                if (PlanetSpots[i].childCount > 0)
                {
                    var candidate = PlanetSpots[i].GetChild(0).GetComponent<Planet>();
                    if (candidate.OrderComplete() == false)
                        Planet.CurrentPlanetFocus = candidate;
                }
            }
        }
    }

    public bool EmptySpotExists()
    {
        var result = false;

        for (int i = 0; i < PlanetSpots.Count; i++)
        {
            if (PlanetSpots[i].childCount == 0)
                result = true;
        }

        return result;
    }

    public void PlacePlanet(Planet newPlanet)
    {
        for (int i = 0; i < PlanetSpots.Count; i++)
        {
            if (PlanetSpots[i].childCount == 0)
            {
                newPlanet.transform.SetParent(PlanetSpots[i], false);
            }
        }
    }

    public List<PlanetOrder_SO> CurrentOrders()
    {
        var result = new List<PlanetOrder_SO>();

        for (int i = 0; i < PlanetSpots.Count; i++)
        {

                if (PlanetSpots[i].childCount > 0)
                    result.Add(PlanetSpots[i].GetChild(0).GetComponent<Planet>().contract);
        }

        return result;
    }
}
