using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "WeightedSpawner", menuName = "Spawners/Limited Weighted Spawner")]
public class SpawnManagerWeightedLimited : SpawnManagerWeighted
{
    public int[] mins;
    public int[] maxs;
    private int lowerLimit = 0;
    public override Unit[] CreateUnits()
    {
        if (mins.Sum()>minUnits)
        {
            Debug.LogError("The sum of minimum amounts of units can't be lower than the minimum amount of units possible", this);
            return null;
        }
        Unit[] units = new Unit[Random.Range(minUnits, maxUnits + 1)];
        for (int i = 0; i < mins.Length; i++)
        {
            for (int j = 0; j < mins[i]; j++)
            {
                units[i] = Unit.Create(unitprebuilds[i],level);
                maxs[i]--;
                lowerLimit++;
            }
        }
        int sumweight = 0;
        foreach (int i in weight)
        {
            sumweight += i;
        }
        for (int i = lowerLimit; i < units.Length; i++)
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
                    maxs[j]--;
                    if(maxs[j] == 0)
                    {
                        sumweight -= weight[j];
                        units.ToList().RemoveAt(j);
                        maxs.ToList().RemoveAt(j);
                    }
                    break;
                }
            }
        }
        return units;
    }
}
