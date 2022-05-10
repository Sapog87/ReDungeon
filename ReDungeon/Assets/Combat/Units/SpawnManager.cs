using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class SpawnManager : ScriptableObject
{
    public Unit[] unitprebuilds;
    public int level;
    public abstract Unit[] CreateUnits();
}
