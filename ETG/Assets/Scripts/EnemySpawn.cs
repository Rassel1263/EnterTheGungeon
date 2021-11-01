using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemyPrefabs;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(GetComponent<ParticleSystem>().duration);

        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, 4)]);
        enemy.transform.position = transform.position;
    }
}
