using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [HideInInspector]
    public int level;
    [HideInInspector]
    public SpawnManager currentManager;
    [HideInInspector]
    public bool isBossBattle = false;

    public static LevelManager instance;

    static string[] managersPaths =
    {
        "Spawners/Spawner_LVL-1", // 1-й уровень      Test/TestSpawner
        "Spawners/Spawner_LVL-2", // 2-й уровень
        "Spawners/Spawner_LVL-3", // 3-й уровень
        "Spawners/Spawner_LVL-4", // 4-й уровень
    };

    static string[] bossManagersPaths =
    {
        "Spawners/BossSpawner_LVL-1", // босс 1-го уровня
        "Spawners/BossSpawner_LVL-2", // босс 2-го уровня
        "Spawners/BossSpawner_LVL-3", // босс 3-го уровня
        "Spawners/BossSpawner_LVL-4", // босс 4-го уровня
    };

    static string[] peacefulSoundtrackNames =
    {
        "Peaceful_LVL-1",
        "Peaceful_LVL-2",
        "Peaceful_LVL-1",
        "Peaceful_LVL-2"
    };

    static string[] combatSoundtrackNames =
    {
        "Combat_LVL-1",
        "Combat_LVL-2",
        "Combat_LVL-1",
        "Combat_LVL-2"
    };

    static string[] bossCombatSoundtrackNames =
    {
        "BossCombat_LVL-1",
        "BossCombat_LVL-2",
        "BossCombat_LVL-3",
        "FinalBattle_Phase1"
    };


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
        {
            instance.level = 0;
            Destroy(gameObject);
            return;
        }
    }

    public void SetCurrentManager()
    {
        if (level > 0 && level <= 4)
            currentManager = Resources.Load<SpawnManagerWeighted>(managersPaths[level - 1]);
    }

    public void SetCurrentManager_Boss()
    {
        if (level > 0 && level <= 4)
            currentManager = Resources.Load<SpawnManagerConstant>(bossManagersPaths[level - 1]);
    }

    public string GetPeacefulSoundtrackName()
    {
        if (level > 0 && level <= 4)
            return peacefulSoundtrackNames[level - 1];
        return peacefulSoundtrackNames[0];
    }

    public string GetCombatSoundtrackName()
    {
        if (level > 0 && level <= 4)
            return combatSoundtrackNames[level - 1];
        return combatSoundtrackNames[0];
    }

    public string GetBossCombatSoundtrackName()
    {
        if (level > 0 && level <= 4)
            return bossCombatSoundtrackNames[level - 1];
        return bossCombatSoundtrackNames[0];
    }

}
