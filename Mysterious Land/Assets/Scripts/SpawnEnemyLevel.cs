using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public GameObject prefabEnemy;
    public float timeWaitSpawn;
}


public class SpawnEnemyLevel : MonoBehaviour
{
    public Transform pointSpawn;
    public EnemyWave[] enemyWaves;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeWaitSpawn, enemyWaves[i].prefabEnemy));//Tạo các wave
        }
    }

    IEnumerator CreateEnemyWave(float delay, GameObject Wave)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);//Thời gian chờ tạo wave

        //if (PlayerController.instance != null)
        Instantiate(Wave,pointSpawn.position,Quaternion.identity);//Tạo wave
        GameController.instance.PlusEnemyCount1();

    }
}
