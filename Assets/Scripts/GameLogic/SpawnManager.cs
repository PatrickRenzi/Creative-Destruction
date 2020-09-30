using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public float AsteriodSpawnIntervalInSeconds = 1.2f;

    public int PlanetCount = 0;

    public int MaxPlanets = 2;

    public int FailureCount = 0;

    public int SuccessCount = 0;

    public int AsteriodCount = 0;

    public List<GameObject> AsteriodPrefab;

    public List<GameObject> Resources;

    public List<GameObject> DecoratorResources;

    public List<PlanetOrder_SO> PlanetOrders;

    public Planet PlanetPrefab;

    public Transform MinSpawnPoint;

    public Transform MaxSpawnPoint;

    public bool Xaxis;

    public EndPanel endPanel;

    public NewContractPanel newContractPanel;

    public Transform TextEffect;

    public PlanetOrder_SO nextOrder;

    private void Awake()
    {
        Time.timeScale = 1f;
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
      
        SpawnPlanet();
        //InvokeRepeating("SpawnPlanet", 2f, 1f);
        InvokeRepeating("SpawnNewAsteriod", AsteriodSpawnIntervalInSeconds, AsteriodSpawnIntervalInSeconds);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnNewAsteriod()
    {
        var prefabIndex = GetRandomAsteriodPrefab();

        Vector3 randomPosition;

        if (Xaxis)
            randomPosition = new Vector3(Random.Range(MinSpawnPoint.position.x, MaxSpawnPoint.position.x), MaxSpawnPoint.position.y, 0);
        else
            randomPosition = new Vector3(MaxSpawnPoint.position.x, Random.Range(MinSpawnPoint.position.y, MaxSpawnPoint.position.y), 0);

        GameObject newAsteriod = Instantiate(AsteriodPrefab[prefabIndex], randomPosition, Quaternion.identity) as GameObject;
        AsteriodCollision newAsteriodCollision = newAsteriod.GetComponent<AsteriodCollision>();
        PlaceResources newAsteriodResourceSpots = newAsteriod.GetComponent<PlaceResources>();
        AsteriodCount++;
        newAsteriod.name = AsteriodPrefab[prefabIndex].name + AsteriodCount;

        List<int> resourcesToAdd = RandomResourceRewardInts(AsteriodPrefab[prefabIndex].name);
        //newAsteriodCollision.ResourcesAfterCollision = RandomResourceReward(AsteriodPrefab[prefabIndex].name);

        for (int i = 0; i < resourcesToAdd.Count; i++)
        {
            newAsteriodCollision.ResourcesAfterCollision.Add(Resources[resourcesToAdd[i]]);
            newAsteriodCollision.DecoratorsForAsteriod.Add(DecoratorResources[resourcesToAdd[i]]);
        }

        newAsteriodResourceSpots.PlaceRandomResources(newAsteriodCollision.DecoratorsForAsteriod);

    }

    private int GetRandomAsteriodPrefab()
    {
        return (int)Random.Range(0, AsteriodPrefab.Count);
    }

    private List<GameObject> RandomResourceReward(string prefabName)
    {
        List<GameObject> result = new List<GameObject>();
        int amountToAdd;
        switch(prefabName)
        {
            case "GiganticAsteriod":
                amountToAdd = (int)Random.Range(1, 4);
                break;
            case "LargeAsteriod":
                amountToAdd = (int)Random.Range(2, 5);
                break;
            case "MediumAsteriod":
                amountToAdd = (int)Random.Range(1, 3);
                break;
            default:
                amountToAdd = (int)Random.Range(1, 2);
                break;
        }


        for(int i = 0; i < amountToAdd; i++)
        {
            result.Add(Resources[(int)Random.Range(0, Resources.Count)]);
        }

        return result;
    }

    private List<int> RandomResourceRewardInts(string prefabName)
    {
        List<int> result = new List<int>();
        int amountToAdd;
        switch (prefabName)
        {
            case "GiganticAsteriod":
                amountToAdd = (int)Random.Range(1, 4);
                break;
            case "LargeAsteriod3D":
                amountToAdd = (int)Random.Range(2, 4);
                break;
            case "MediumAsteriod3D":
                amountToAdd = (int)Random.Range(1, 3);
                break;
            default:
                amountToAdd = (int)Random.Range(0, 2);
                break;
        }

        var currentContracts = PlanetDashboard.instance.CurrentOrders();

        if (currentContracts.Count == 0)
            currentContracts.Add(this.nextOrder);

        var resourceDistribution = GetRandomResourceDistribution(currentContracts);

        for (int i = 0; i < amountToAdd; i++)
        {
            int rewardIndex = (int)Random.Range(0, resourceDistribution.Count);
            result.Add(resourceDistribution[rewardIndex]);
        }

        return result;
    }

    public List<int> GetRandomResourceDistribution(List<PlanetOrder_SO> currentContracts)
    {
        var distribution = new List<int>();

        foreach(var planetOrder_SO in currentContracts)
        {
            if (planetOrder_SO.CarbonGoal > 0)
                distribution.Add(0);
            if (planetOrder_SO.CarbonGoal > 1)
                distribution.Add(0);
            if (planetOrder_SO.IceGoal > 0)
                distribution.Add(1);
            if (planetOrder_SO.IceGoal > 1)
                distribution.Add(1);
            if (planetOrder_SO.IronGoal > 0)
                distribution.Add(2);
            if (planetOrder_SO.IronGoal > 1)
                distribution.Add(2);
            if (planetOrder_SO.MagmaGoal > 0)
                distribution.Add(3);
            if (planetOrder_SO.MagmaGoal > 1)
                distribution.Add(3);
            if (planetOrder_SO.UraniumGoal > 0)
                distribution.Add(4);
            if (planetOrder_SO.UraniumGoal > 1)
                distribution.Add(4);
        }

        return distribution;
    }

    public PlanetOrder_SO GetRandomPlanetOrder()
    {
        var randomIndex = Random.Range(0, this.PlanetOrders.Count);

        return this.PlanetOrders[randomIndex];
    }

    public void ScheduleSpawnPlanet()
    {
        Invoke("SpawnPlanet", 3f);
    }

    public void SpawnPlanet()
    {
        if(PlanetDashboard.instance.EmptySpotExists())
        {
            GeneratePlanet();
        }
    }

    public void GeneratePlanet()
    {
        var newPlanet = Instantiate(PlanetPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity) as Planet;

        if(this.nextOrder != null)
            newPlanet.SetContract(this.nextOrder);
        else
            newPlanet.SetContract(GetRandomPlanetOrder());

        this.nextOrder = this.GetRandomPlanetOrder();

        PlanetDashboard.instance.PlacePlanet(newPlanet);

        this.newContractPanel.gameObject.SetActive(false);

    }

    public void HideNewContractPanel()
    {
        newContractPanel.gameObject.SetActive(false);
    }

    public void CheckEndState()
    {
        if(PlanetCount >= MaxPlanets)
        {
            Time.timeScale = 0f;
            endPanel.ResultText.text = InputManager.instance.Profit > 12000? "You've completed the orders with a total profit of " + InputManager.instance.Profit.ToString() + "! You're rolling in it!" :
                "You've completed the orders with a total profit of " + InputManager.instance.Profit.ToString() + "!";
            endPanel.gameObject.SetActive(true);
        }
    }
}
