using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public GameObject[] npcPrefabs; // 8种NPC的prefab数组
    public int[] npcCounts; // 每种NPC的生成数量

    void Start()
    {
        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        for (int i = 0; i < npcPrefabs.Length; i++)
        {
            for (int j = 0; j < npcCounts[i]; j++)
            {
                Vector2 spawnPosition;

                // 确保生成位置不在半径为40的圆内
                do
                {
                    spawnPosition = Random.insideUnitCircle * 550;
                } while (spawnPosition.magnitude < 40);
                Instantiate(npcPrefabs[i], spawnPosition, Quaternion.identity);
            }
        }
    }
}
