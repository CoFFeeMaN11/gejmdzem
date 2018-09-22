using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



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

    private Dictionary<int, BuildingScript> buildings = new Dictionary<int, BuildingScript>();

    public GameObject[] ToDisable;


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

        foreach(GameObject obj in ToDisable)
        {
            obj.SetActive(false);
        }

        StartCoroutine(NextWave());
    }
    public bool RegisterBuildings(BuildingScript _building)
    {
        if (buildings.ContainsKey(Hash(_building.name)))
            return false;
        buildings.Add(Hash(_building.name), _building);
        return true;
    }
    public IEnumerable<BuildingScript> GetAllBuildings()
    {
        foreach (var b in buildings)
            yield return b.Value;
    }
    public BuildingScript GetBuilding(string id)
    {
        if (!buildings.ContainsKey(Hash(id)))
            return null;
        return buildings[Hash(id)];
    }
    public static void GetAllBuilding()
    {
        Debug.Log(AssetDatabase.LoadAllAssetsAtPath("Assets\\_buildings").Length);
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

    private int Hash(string s)
    {
        int ret = 0;
        for(int i = 0; i < s.Length; i++)
        {
            ret += s[i];
            ret += ret << 10;
            ret ^= ret >> 6;
        }
        ret += ret << 3;
        ret ^= ret >> 11;
        ret += ret << 15;
        return ret;
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
        }

        yield return StartCoroutine(NextWave());
    }

}

