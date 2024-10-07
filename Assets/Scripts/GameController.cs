using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public GameObject[] npcPrefabs; // 8��NPC��prefab����
    public int[] npcCounts; // ÿ��NPC����������

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

                // ȷ������λ�ò��ڰ뾶Ϊ40��Բ��
                do
                {
                    spawnPosition = Random.insideUnitCircle * 550;
                } while (spawnPosition.magnitude < 40);
                Instantiate(npcPrefabs[i], spawnPosition, Quaternion.identity);
            }
        }
    }
}
