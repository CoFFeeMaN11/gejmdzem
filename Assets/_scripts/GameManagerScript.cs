using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Road
{
    public Transform[] WayPoints;
}

[System.Serializable]
public struct WaveInfo
{
    [Range (0,1)]
    public float WeakChance;
    [Range(0, 1)]
    public float MediumChance;

    public int finalStage;
}

public class GameManagerScript : MonoBehaviour
{
    private static GameManagerScript GameManagerObject = null;

    private System.Random rand = new System.Random();

    public List<Transform> enemyList = new List<Transform>();
    private Queue<WaveInfo> stages = new Queue<WaveInfo>();
    public List<WaveInfo> stageList = new List<WaveInfo>();

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
        foreach(var s in stageList)
        {
            stages.Enqueue(s);
        }

        StartCoroutine(NextWave());
    }

    private EnemyType RandomEnemy( WaveInfo info )
    {
        int rand = Random.Range(1, 1000);
        int mediumStart = Mathf.RoundToInt(1000 * info.WeakChance);
        int hardStart = mediumStart + Mathf.RoundToInt(1000 * info.MediumChance);
        if (rand < mediumStart)
            return EnemyType.small;
        if (rand < hardStart)
            return EnemyType.medium;
        return EnemyType.big;
        
    }

    public IEnumerator NextWave()
    {
        currentRoad = rand.Next(0, roads.Count);

        int enemyType = Random.Range(0, Enemies.Length);
        float randomOffset = Random.Range(-1f, 1f);

        Vector3 enemyPosition = new Vector3(EnemySpawnPoint.position.x + randomOffset, EnemySpawnPoint.position.y - 0.5f, 0);
        var enemy = Instantiate(Enemies[(int)RandomEnemy(stages.Peek())], enemyPosition, Quaternion.identity) as GameObject;
        enemy.GetComponent<EnemyScript>().SetRoadAndOffset(roads[currentRoad], randomOffset);
        enemyList.Add(enemy.transform);

        /*
        randomOffset = Random.Range(-1f, 1f);
        enemyPosition = new Vector3(EnemySpawnPoint.position.x + randomOffset, EnemySpawnPoint.position.y - 0.5f, 0);
        enemy = Instantiate(Enemies[enemyType], enemyPosition, Quaternion.identity);
        enemy.GetComponent<EnemyScript>().SetRoadAndOffset(roads[currentRoad], randomOffset);

        randomOffset = Random.Range(-1f, 1f);
        enemyPosition = new Vector3(EnemySpawnPoint.position.x + randomOffset, EnemySpawnPoint.position.y - 0.5f, 0);
        enemy = Instantiate(Enemies[enemyType], enemyPosition, Quaternion.identity);
        enemy.GetComponent<EnemyScript>().SetRoadAndOffset(roads[currentRoad], randomOffset);
        */
        yield return new WaitForSecondsRealtime(0.3f);

        yield return StartCoroutine(NextWave());
    }

}
