using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlanet : MonoBehaviour
{
    private static UIPlanet instance;

    public GameObject CloudModel;

    public GameObject LandModel;

    public GameObject OceanModel;

    private Material CloudMaterial;

    private Material OceanMaterial;

    private Material LandMaterial;

    public static UIPlanet Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }

    }

    public PlanetOrder_SO contract;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        CloudMaterial = CloudModel.GetComponent<Renderer>().material;
        OceanMaterial = OceanModel.GetComponent<Renderer>().materials[0];
        LandMaterial = LandModel.GetComponent<Renderer>().materials[0];
    }

    public void SetPlanetType(PlanetOrder_SO myPlanetOrder)
    {
        if (myPlanetOrder.IceGoal > myPlanetOrder.MagmaGoal)
            OceanMaterial.color = new Color(.2f, .2f, 1f, 1f);
        else
            OceanMaterial.color = new Color(1f, .05f, .05f, 1f);

        if (myPlanetOrder.UraniumGoal > myPlanetOrder.IceGoal)
            CloudMaterial.color = new Color(.2f, 1f, .1f, .8f);
        else
            CloudMaterial.color = new Color(1f, 1f, 1f, .9f);

        if (myPlanetOrder.CarbonGoal > myPlanetOrder.IronGoal)
            LandMaterial.color = new Color(.2f, 1f, .2f, 1f);
        else
            LandMaterial.color = new Color(.5f, .2f, .2f, 1f);
    }

    public void SetPlanetType(int MyCarbon, int MyIce, int MyIron, int MyMagma, int MyUranium)
    {
        if (MyIce > MyMagma)
            OceanMaterial.color = new Color(.2f, .2f, 1f, 1f);
        else
            OceanMaterial.color = new Color(1f, .05f, .05f, 1f);

        if (MyUranium > MyIce)
            CloudMaterial.color = new Color(.2f, 1f, .1f, .8f);
        else
            CloudMaterial.color = new Color(1f, 1f, 1f, .8f);

        if (MyCarbon > MyIron)
            LandMaterial.color = new Color(.2f, 1f, .2f, 1f);
        else
            LandMaterial.color = new Color(.5f, .2f, .2f, 1f);
    }
}
