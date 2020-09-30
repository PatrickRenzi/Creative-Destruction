using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlanetOrder", menuName = "ScriptableObjects/PlanetOrder", order = 2)]
public class PlanetOrder_SO : ScriptableObject
{
    public string PlanetName;
    public int IceGoal;
    public int CarbonGoal;
    public int IronGoal;
    public int MagmaGoal;
    public int UraniumGoal;

    public int Deadline;

    public int Reward;

    public PlanetOrder_SO(int myDeadlineInSeconds)
    {
        Deadline = myDeadlineInSeconds;
    }
}
