using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractPanel : MonoBehaviour
{
    public static ContractPanel instance;
    public Text IceStatus;
    public Text CarbonStatus;
    public Text IronStatus;
    public Text MagmaStatus;
    public Text UraniumStatus;
    public Text TimeLeft;
    public Text ProfitValue;
    public Text ContractTitle;

    public Slider IceSlider;
    public Slider CarbonSlider;
    public Slider IronSlider;
    public Slider MagmaSlider;
    public Slider UraniumSlider;


    public Image IceImage;
    public Image CarbonImage;
    public Image IronImage;
    public Image MagmaImage;
    public Image UraniumImage;

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
}
