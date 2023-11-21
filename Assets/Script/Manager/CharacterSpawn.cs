using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSpawn : MonoBehaviour
{
    [SerializeField] private GameObject characterObj = null;

    [Space(10)]
    [SerializeField] private GameObject[] character;
    [SerializeField] private Transform characterSpawnPos;

    [Space(10)]
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform enemySpawnPos;

    public void SpawnCharacter(int index)
    {
        if(characterObj == null)
        {
            GameObject clone = Instantiate(character[index], characterSpawnPos.position, Quaternion.identity);
            characterObj = clone;
        }
    }

    public void CreateEnemy()
    {
        Instantiate(enemy, enemySpawnPos.position, Quaternion.identity);
    }

    public void DesototyCharacter()
    {
        Destroy(characterObj);
        characterObj = null;
    }
}
