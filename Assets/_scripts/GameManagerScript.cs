using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Road
{
    public Transform[] WayPoints;
}

public class GameManagerScript : MonoBehaviour
{
    private static GameManagerScript GameManagerObject = null;

    private System.Random rand = new System.Random();

    public List<Road> roads = new List<Road>();
    private int currentRoad;
    public Road CurrentRoad
    {
        get
        {
            return roads[currentRoad];
        }
    }
    public Transform EnemySpawnPoint;
    public GameObject[] Enemies;

    public static GameManagerScript Get
    {
        get
        {
            return GameManagerObject;
        }
    }

    // Use this for initialization
    void Awake()
    {
        if (GameManagerObject == null)
            GameManagerObject = this;
        else if (GameManagerObject != this)
            Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(NextWave());
    }

    public IEnumerator NextWave()
    {
        currentRoad = rand.Next(0, roads.Count);

        int enemyType = Random.Range(0, Enemies.Length);
        Debug.Log(enemyType);

        Vector3 enemyPosition = new Vector3(EnemySpawnPoint.position.x - 1f, EnemySpawnPoint.position.y - 0.5f, 0);
        Instantiate(Enemies[enemyType], enemyPosition, Quaternion.identity);
        enemyPosition = new Vector3(EnemySpawnPoint.position.x, EnemySpawnPoint.position.y - 0.5f, 0);
        Instantiate(Enemies[enemyType], enemyPosition, Quaternion.identity);
        enemyPosition = new Vector3(EnemySpawnPoint.position.x + 1f, EnemySpawnPoint.position.y - 0.5f, 0);
        Instantiate(Enemies[enemyType], enemyPosition, Quaternion.identity);

        yield return new WaitForSecondsRealtime(1f);

        yield return StartCoroutine(NextWave());
    }

}
