using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeightedSpawner", menuName = "Spawners/Weighted Spawner")]
public class SpawnManagerWeighted : SpawnManager
{
    public int[] weight;
    public int minUnits;
    public int maxUnits;
    public override Unit[] CreateUnits()
    {
        if (maxUnits > 5)
        {
            Debug.LogError("The amount of units Can't be larger then 5", this);
            return null;
        }
        Unit[] units = new Unit[Random.Range(minUnits, maxUnits + 1)];
        int sumweight = 0;
        foreach (int i in weight)
        {
            sumweight += i;
        }
        for (int i = 0; i < units.Length; i++)
        {
            int selector = Random.Range(1, sumweight + 1);
            for (int j = 0; j < weight.Length; j++)
            {
                if (weight[j] < selector)
                {
                    selector -= weight[j];
                }
                else
                {
                    units[i] = Unit.Create(unitprebuilds[j],level);
                    break;
                }
            }
        }
        return units;
    }
}
