using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteriodSpawns", menuName = "ScriptableObjects/Asteriod" +
    "Wave", order = 1)]
public class SpawnSettings : ScriptableObject {
    // Start is called before the first frame update
    public List<GameObject> AsteriodSizes;

    public float VelocityModifier;

    public List<GameObject> Resource;

    public List<GameObject> PlanetOrders;
}
