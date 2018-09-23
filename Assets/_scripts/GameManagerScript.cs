using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[System.Serializable]

public struct Buildings
{
    public GameObject Tower;
    public GameObject Quarry;
    public GameObject Sawmill;
    public GameObject Alchemist;
    public GameObject ShootingRange;
    public GameObject Workshop;
    public GameObject ThievesGuild;
}

[System.Serializable]
public struct WaveInfo
{
    [Range(0, 1)]
    public float WeakChance;
    [Range(0, 1)]
    public float MediumChance;

    public int finalStage;
}

[System.Serializable]
public struct Path
{
    public GameObject[] Waypoints;
}

public class GameManagerScript : MonoBehaviour
{

    public GameObject UpgradePanel;

    public Buildings buildings;

    public BuildingMenu menu;
    public GameObject[] ToDisable;


    public Path[] paths;

    public Path[] currentPath
    {
        get
        {
            return paths;
        }
    }


    private static GameManagerScript GameManagerObject = null;

    private System.Random rand = new System.Random();

    public List<Transform> enemyList = new List<Transform>();
    private Queue<WaveInfo> stages = new Queue<WaveInfo>();
    public List<WaveInfo> stageList = new List<WaveInfo>();

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

    public static float EnemySpeedVaryfier = 1f;

    public static bool ScreenFree = true;

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

    public IEnumerator NextWave()
    {
        //int enemyType = Random.Range(0, Enemies.Length);
        float randomOffset = UnityEngine.Random.Range(-1f, 1f);
        int randomPath = UnityEngine.Random.Range(0, 3);

        if(stages.Peek().finalStage != -1 && currentStage > stages.Peek().finalStage)
        {
            stages.Dequeue();
        }

        var enemy = Instantiate(Enemies[(int)RandomEnemy(stages.Peek())], EnemySpawnPoint.position, Quaternion.identity) as GameObject;
        enemy.GetComponent<EnemyScript>().SetRoadAndOffset(randomPath, randomOffset);
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

    public void ClearScreen()
    {
        foreach (GameObject obj in ToDisable)
        {
            obj.SetActive(false);
        }
    }

}

