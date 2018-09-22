using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public struct WaveInfo
{
    [Range(0, 1)]
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
    [SerializeField]
    private Map mainMap;
    public List<Road> roads = new List<Road>();
    private int currentRoad;
    public Road CurrentRoad
    {
        get
        {
            return roads[currentRoad];
        }
    }
    public Map MainMap
    {
        get
        {
            return mainMap;
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

    public float StageLength;
    public float IntervalBetweenStages;
    private float stageLengthTimeStamp;
    private int currentStage = 0;

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
        stageLengthTimeStamp = Time.time;

        foreach (var s in stageList)
        {
            stages.Enqueue(s);
        }

        StartCoroutine(NextWave());
    }

    private EnemyType RandomEnemy(WaveInfo info)
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

        //int enemyType = Random.Range(0, Enemies.Length);
        float randomOffset = Random.Range(-1f, 1f);

        if(stages.Peek().finalStage != -1 && currentStage > stages.Peek().finalStage)
        {
            stages.Dequeue();
        }

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

        if (Time.time - stageLengthTimeStamp >= StageLength)
        {
            //end of stage
            currentStage++;

            yield return new WaitForSecondsRealtime(IntervalBetweenStages);

            stageLengthTimeStamp = Time.time;
            //StageLength += 3f;
            Debug.Log("next");
        }

        yield return StartCoroutine(NextWave());
    }

}

