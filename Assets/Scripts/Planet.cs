using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    private static Planet _currentPlanetFocus;

    public ParticleSystem CompletedEffectPrefab;

    public GameObject Highlight;

    public GameObject CloudModel;

    public GameObject LandModel;

    public GameObject OceanModel;

    private Material CloudMaterial;

    private Material OceanMaterial;

    private Material LandMaterial;

    private bool completed = false;

    public static Planet CurrentPlanetFocus
    {
        get
        {
            return _currentPlanetFocus;
        }
        set
        {
            if(_currentPlanetFocus != null)
                _currentPlanetFocus.Highlight.SetActive(false);

            _currentPlanetFocus = value;
            _currentPlanetFocus.Highlight.SetActive(true);
            _currentPlanetFocus.UpdateContractPanel();
        }

    }

    public int resources
    {
        get
        {
            return CurrentCarbon + CurrentIron + CurrentIce + CurrentMagma + CurrentUranium;
        }
    }

    public int necessaryresources
    {
        get
        {
            return 0;
        }
    }

    public int CurrentCarbon;
    public int CurrentIron;
    public int CurrentIce;
    public int CurrentMagma;
    public int CurrentUranium;

    public PlanetOrder_SO contract;

    public float timeLeft;

    public Text TimeLeft;

    // Start is called before the first frame update
    void Start()
    {
        if (contract == null)
            SetContract(SpawnManager.instance.GetRandomPlanetOrder());
        if (CurrentPlanetFocus == null)
            CurrentPlanetFocus = this;

        if(CloudModel != null)
        {
            CloudMaterial = CloudModel.GetComponent<Renderer>().material;
            CloudMaterial.color = new Color(.1f, .1f, .1f, .0f);
        }

        if(LandModel != null && OceanModel != null)
        {

            OceanMaterial = OceanModel.GetComponent<Renderer>().materials[0];
            LandMaterial = LandModel.GetComponent<Renderer>().materials[0];
            OceanMaterial.color = new Color(.5f, .5f, .5f, 1f);
            LandMaterial.color = new Color(.3f, .3f, .3f, 1f);
            OceanModel.SetActive(false);
        }
    }

    public void SetContract(PlanetOrder_SO myContract)
    {
        contract = myContract;
        timeLeft = myContract.Deadline;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
            timeLeft = 0f;

        if (CurrentPlanetFocus == this)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60F);
            int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            ContractPanel.instance.TimeLeft.text = niceTime;
        }
    }

    void UpdateContractPanel()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60F);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        
        ContractPanel.instance.TimeLeft.text = niceTime;

        ContractPanel.instance.ContractTitle.text = "Progress";

        ContractPanel.instance.IceStatus.text = CurrentIce + " / " + contract.IceGoal;
        ContractPanel.instance.MagmaStatus.text = CurrentMagma + " / " + contract.MagmaGoal;
        ContractPanel.instance.CarbonStatus.text = CurrentCarbon + " / " + contract.CarbonGoal;
        ContractPanel.instance.UraniumStatus.text = CurrentUranium + " / " + contract.UraniumGoal;
        ContractPanel.instance.IronStatus.text = CurrentIron + " / " + contract.IronGoal;

        ContractPanel.instance.CarbonSlider.maxValue = contract.CarbonGoal;
        ContractPanel.instance.CarbonSlider.value = CurrentCarbon;
        ContractPanel.instance.IronSlider.maxValue = contract.IronGoal;
        ContractPanel.instance.IronSlider.value = CurrentIron;
        ContractPanel.instance.IceSlider.maxValue = contract.IceGoal;
        ContractPanel.instance.IceSlider.value = CurrentIce;
        ContractPanel.instance.MagmaSlider.maxValue = contract.MagmaGoal;
        ContractPanel.instance.MagmaSlider.value = CurrentMagma;
        ContractPanel.instance.UraniumSlider.maxValue = contract.UraniumGoal;
        ContractPanel.instance.UraniumSlider.value = CurrentUranium;

        ContractPanel.instance.IceImage.color = contract.IceGoal == 0 ? new Color(.5f, .5f, .5f, .3f) : Color.white;
        ContractPanel.instance.CarbonImage.color = contract.CarbonGoal == 0 ? new Color(.5f, .5f, .5f, .3f) : Color.white;
        ContractPanel.instance.IronImage.color = contract.IronGoal == 0 ? new Color(.5f, .5f, .5f, .3f) : Color.white;
        ContractPanel.instance.MagmaImage.color = contract.MagmaGoal == 0 ? new Color(.5f, .5f, .5f, .3f) : Color.white;
        ContractPanel.instance.UraniumImage.color = contract.UraniumGoal == 0 ? new Color(.5f, .5f, .5f, .3f) : Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Resource")
        {

            Resource_SO resource = collision.gameObject.GetComponent<Resource_SO>();

            switch(resource.Type)
            {
                case Resource_SO.ResourceType.Carbon:
                    CurrentCarbon++;
                    break;
                case Resource_SO.ResourceType.Ice:
                    CurrentIce++;
                    break;
                case Resource_SO.ResourceType.Iron:
                    CurrentIron++;
                    break;
                case Resource_SO.ResourceType.Magma:
                    CurrentMagma++;
                    break;
                case Resource_SO.ResourceType.Uranium:
                    CurrentUranium++;
                    break;
                default:
                    break;
            }

            if(this == Planet._currentPlanetFocus)
                UpdateContractPanel();

            Invoke("CheckComplete", .5f);
            Destroy(collision.gameObject);
        }
    }

    public bool OrderComplete()
    {
        return CurrentCarbon >= contract.CarbonGoal &&
            CurrentIce >= contract.IceGoal &&
            CurrentMagma >= contract.MagmaGoal &&
            CurrentIron >= contract.IronGoal &&
            CurrentUranium >= contract.UraniumGoal;
    }

    private void CheckComplete()
    {

        if(OrderComplete() &&
            completed == false)
        {
            if (timeLeft > 0)
            {
                SpawnManager.instance.newContractPanel.Compensation.text = "You made " + contract.Reward + " Space Credits.";
                InputManager.instance.UpdateProfit(contract.Reward);
                SpawnManager.instance.SuccessCount++;
            }
            else
            {
                SpawnManager.instance.newContractPanel.Compensation.text = "You made " + contract.Reward/2 + " Space Credits.";
                InputManager.instance.UpdateProfit(contract.Reward / 2);
            }

           

            SpawnManager.instance.PlanetCount++;
            SpawnManager.instance.CheckEndState();

            ParticleSystem completedFX = Instantiate(CompletedEffectPrefab, new Vector3(transform.position.x, transform.position.y, this.transform.position.z - 3f), Quaternion.identity) as ParticleSystem;
            float totalDuration = completedFX.main.duration;

            completed = true;
            Destroy(completedFX, totalDuration);

            OceanModel.SetActive(true);

            if(CurrentIce > CurrentMagma)
                OceanMaterial.color = new Color(.2f, .2f, 1f, 1f);
            else
                OceanMaterial.color = new Color(1f, .05f, .05f, 1f);
            
            if(CurrentUranium > CurrentIce)
                CloudMaterial.color = new Color(.2f, 1f, .1f, .8f);
            else
                CloudMaterial.color = new Color(1f, 1f, 1f, .8f);

            if (CurrentCarbon > CurrentIron)
                LandMaterial.color = new Color(.2f, 1f, .2f, 1f);
            else
                LandMaterial.color = new Color(.5f, .2f, .2f, 1f);



            PlanetDashboard.instance.AttemptToRefocus();

            SpawnManager.instance.newContractPanel.gameObject.SetActive(true);
            SpawnManager.instance.newContractPanel.newPlanet.SetPlanetType(SpawnManager.instance.nextOrder);
            SpawnManager.instance.newContractPanel.PlanetToBuild.text = "Build " + SpawnManager.instance.nextOrder.PlanetName + "!";

            SpawnManager.instance.ScheduleSpawnPlanet();

            Destroy(this.gameObject, 1.5f);
        }

    }
}
