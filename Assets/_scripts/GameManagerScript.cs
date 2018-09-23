using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

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
    public BuildingMenu menu;
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
        mainMap = GetComponent<Map>();
        mainMap.Recovery();
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
    public static void GetAllBuilding()
    {
        Debug.Log(AssetDatabase.LoadAllAssetsAtPath("Assets\\_buildings").Length);
    }
    private EnemyType RandomEnemy(WaveInfo info)
    {
        int rand = UnityEngine.Random.Range(1, 1000);
        int mediumStart = Mathf.RoundToInt(1000 * info.WeakChance);
        int hardStart = mediumStart + Mathf.RoundToInt(1000 * info.MediumChance);
        if (rand < mediumStart)
            return EnemyType.small;
        if (rand < hardStart)
            return EnemyType.medium;
        return EnemyType.big;

    }

    public static int Hash(string s)
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

    internal void OpenBuildMenu(TileCoords coords)
    {
        menu.gameObject.SetActive(true);
        menu.Show(coords);
    }

    public IEnumerator NextWave()
    {
        currentRoad = rand.Next(0, roads.Count);

        //int enemyType = Random.Range(0, Enemies.Length);
        float randomOffset = UnityEngine.Random.Range(-1f, 1f);

        if(stages.Peek().finalStage != -1 && currentStage > stages.Peek().finalStage)
        {
            stages.Dequeue();
        }

        Vector3 enemyPosition = new Vector3(roads[currentRoad].WayPoints[0].transform.position.x + randomOffset, roads[currentRoad].WayPoints[0].transform.position.y - 0.5f, -10f);
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

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            var tempTile = mainMap.FromVector(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (!MainMap.Exists(tempTile.x, tempTile.y)) return;
            MainMap[tempTile].gameObject.SendMessage("OnUse");
        }
    }

}

