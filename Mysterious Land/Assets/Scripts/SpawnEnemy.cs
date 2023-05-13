using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemies;//Danh sách các quái
    //[SerializeField]
    //EnemyBase enemyBase;
    public Transform[] spawnerPoint;//Vị trí sinh ra quái

    //public float nextTime;
    public float timeWaitStart;
    public float timeRate;
    public Vector2 timeSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeStart());   
    }

    // Update is called once per frame
    void Update()
    {
        //if (Time.time > nextTime)
        //{
        //    nextTime = Time.time + timeRate;
        //    StartCoroutine(Spawner());
        //}
    }

    public void Spawner()
    {
        //yield return new WaitForSeconds(Random.Range(timeSpawn.x,timeSpawn.y));
        //yield return new WaitForSeconds(5f);
        if (GameController.instance.enemyCount1 < GameController.instance.enemyMax)
        {
            GameController.instance.PlusEnemyCount1();
            Instantiate(enemies[Random.Range(0, enemies.Length)], spawnerPoint[Random.Range(0,spawnerPoint.Length)].position, Quaternion.identity);//Sinh ra quái ngẫu nhiên
        }
    }

    IEnumerator TimeStart()
    {
        yield return new WaitForSeconds(timeWaitStart);
        InvokeRepeating("Spawner", timeRate, Random.Range(timeSpawn.x, timeSpawn.y));//Gọi Hàm Spawer sau 1 khoảng thời gian
    }
}
