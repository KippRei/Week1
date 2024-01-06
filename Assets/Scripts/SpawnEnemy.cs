using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    private GameObject basicEnemy;
    private int enemyCount;
    public Vector3 enemySpawnPoint = new Vector3(5, 5, 0);
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy, enemySpawnPoint, transform.rotation);
        enemyCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount <= 0)
        {
            Instantiate(enemy, enemySpawnPoint, transform.rotation);
            enemyCount++;
        }
    }

    public void EnemyDied()
    {
        enemyCount--;
    }
}
