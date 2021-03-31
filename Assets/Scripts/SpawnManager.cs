using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject [] obstaclesPreFab;
    public GameObject[] enemiesPreFab;
    public GameObject[] coinPreFabs;
    public GameObject[] healthPreFabs;

    private int summonOfEnemy = 0;
    private float startValue = 3;

    public bool fightTrue = false;
   
    void Start()
    {
        StartCoroutine(EnemyAndObstacleSpawn());
        StartCoroutine(CoinSpawn());
        StartCoroutine(HealtSpawn());
    }
    
    void Update()
    {
        fightTrue = GameObject.Find("--CombatController--").gameObject.GetComponent<CombatMechanic>().isFighting;
    }

    IEnumerator EnemyAndObstacleSpawn()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
                float randomSpawnRate = Random.Range(2, 5);
            if (!fightTrue)
            {
                if (startValue > 0)
                {
                    SpawnObstacle();
                    startValue--;
                }
                else
                {
                    float randomEncounter = Random.Range(0, 10);
                    float randomEnemyRate = Random.Range(2, 4);

                    if (summonOfEnemy != randomEnemyRate)
                    {
                        if (randomEncounter % 3 == 0)
                        {
                            SummonEnemy();
                            summonOfEnemy++;
                        }
                        else
                        {
                            SpawnObstacle();
                            summonOfEnemy = 0;
                        }
                    }

                }
            }
            yield return new WaitForSeconds(randomSpawnRate);
        }
    }
    IEnumerator CoinSpawn()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            float randomSpawnRate = Random.Range(3, 7);
            if (!fightTrue)
            {
                SummonCoin();
            }
            yield return new WaitForSeconds(randomSpawnRate);
        }
        
    }
    IEnumerator HealtSpawn()
    {
        yield return new WaitForSeconds(6);
        while (true)
        {
            float randomSpawnRate = Random.Range(6, 12);
            if (!fightTrue)
            {
                SummonHealth();
            }
            yield return new WaitForSeconds(randomSpawnRate);
        }

    }
    private void SpawnObstacle()
    {
            fightTrue = GameObject.Find("--CombatController--").GetComponent<CombatMechanic>().isFighting;
            Vector2 obstaclePosition = new Vector2(3f, -2.3f);
            int obstacleIndex = Random.Range(0, obstaclesPreFab.Length);
            Instantiate(obstaclesPreFab[obstacleIndex], obstaclePosition, obstaclesPreFab[obstacleIndex].transform.rotation);
    }
    private void SummonEnemy()
    {
            summonOfEnemy++;
            Debug.Log(summonOfEnemy);
            fightTrue = GameObject.Find("--CombatController--").GetComponent<CombatMechanic>().isFighting;
            Vector2 enemyPosition = new Vector2(3f, -2.63f);
            int enemiesIndex = Random.Range(0, enemiesPreFab.Length);
            Instantiate(enemiesPreFab[enemiesIndex], enemyPosition, enemiesPreFab[enemiesIndex].transform.rotation);
    }
    private void SummonCoin()
    {
            fightTrue = GameObject.Find("--CombatController--").GetComponent<CombatMechanic>().isFighting;
            Vector2 coinPosition = new Vector2(4.4f, 0.3f);
            int coinIndex = Random.Range(0, coinPreFabs.Length);
            Instantiate(coinPreFabs[coinIndex], coinPosition, coinPreFabs[coinIndex].transform.rotation);
    }
    private void SummonHealth()
    {
            fightTrue = GameObject.Find("--CombatController--").GetComponent<CombatMechanic>().isFighting;
            Vector2 healthPosition = new Vector2(3.25f, 0.15f);
            int healthIndex = Random.Range(0, coinPreFabs.Length);
            Instantiate(healthPreFabs[healthIndex], healthPosition, healthPreFabs[healthIndex].transform.rotation);
    }
}
