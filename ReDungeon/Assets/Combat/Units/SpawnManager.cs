using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Spawner", menuName = "Spawner")]
public class SpawnManager : ScriptableObject
{
    public Unit[] unitprebuilds;
    public int[] weight;
    public int minUnits;
    public int maxUnits;

    public Unit[] CreateUnits()
    {
        Unit[] units = new Unit[Random.Range(minUnits,maxUnits+1)];
        int sumweight = 0;
        foreach (int i in weight)
        {
            sumweight += i;
        }
        for (int i = 0; i < units.Length; i++)
        {
            int selector = Random.Range(1, sumweight+1);
            for (int j = 0; j < weight.Length; j++)
            {
                if(weight[j] < selector)
                {
                    selector -= weight[j];
                }
                else
                {
                    units[i] = Instantiate(unitprebuilds[j]);
                    break;
                }
            }
        }
        return units;
    }
}
