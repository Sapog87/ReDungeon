using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "WeightedSpawner", menuName = "Spawners/Constant Spawner")]
public class SpawnManagerConstant : SpawnManager
{
    public int[] amounts;
    public override Unit[] CreateUnits()
    {
        if (amounts.Sum() > 5)
        {
            Debug.LogError("The amount of units Can't be larger then 5", this);
            return null;
        }
        List<Unit> list = new List<Unit>();
        for(int i = 0; i < unitprebuilds.Length; i++)
        {
            for(int j = 0; j < amounts[i]; j++)
            list.Add(Unit.Create(unitprebuilds[i], level));
        }
        return list.ToArray();
    }
}
