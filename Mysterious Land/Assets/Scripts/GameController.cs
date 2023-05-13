using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject buildingPlayer;
    public GameObject buildingEnemy;
    //public SpawnEnemy spawnEnemy;
    public GameObject panelWin;
    public GameObject panelLose;
    public GameObject panelPause;
    public Text coinAmountText;
    public Text enemyMaxText;
    public Text enemyMaxText1;
    public float coinAmount;//Số lượng Coin
    public float timePlusCoin;//Thời gian tăng coin
    public float coinPlus;//Số coin mỗi lần tăng
    public float coinLevel;//Số coin tăng mỗi level
    public float coinUpLevel;//Số coin để nâng level
    public float enemyMax;//Số quái tối đa
    [HideInInspector]
    public float enemyCount;
    [HideInInspector]
    public float enemyCount1;

    public GameObject textCoin;//FloatingTextCoin
    public Transform pointFTCoin;

    private void Awake()
    {
        Time.timeScale = 1f;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //spawnEnemy = GetComponent<SpawnEnemy>();
        Time.timeScale = 1f;
        StartCoroutine(PlusCoin());

    }

    // Update is called once per frame
    void Update()
    {
        coinAmountText.text = coinAmount.ToString();
        enemyMaxText.text = enemyCount.ToString() + "/" + enemyMax.ToString();
        enemyMaxText1.text = enemyCount1.ToString() + "/" + enemyMax.ToString();
        if (!buildingEnemy || buildingEnemy.GetComponent<Health>().health <= 0 /*|| !buildingPlayer || buildingPlayer.GetComponent<Health>().health <= 0*/)
        {
            panelWin.SetActive(true);
            Time.timeScale = 0f;
        }
        else if(/*!buildingEnemy || buildingEnemy.GetComponent<Health>().health <= 0 ||*/ !buildingPlayer || buildingPlayer.GetComponent<Health>().health <= 0)
        {
            panelLose.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void PauseGame()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PlusEnemyCount()
    {
        enemyCount++;
    }

    public void MinusEnemyCount()
    {
        enemyCount-=1;
    }

    public void PlusEnemyCount1()
    {
        enemyCount1++;
    }

    public void MinusEnemyCount1()
    {
        enemyCount1 --;
    }

    IEnumerator PlusCoin()
    {
        yield return new WaitForSeconds(timePlusCoin);
        coinAmount += coinPlus;//Tăng số lượng coin
        GameObject prefab = Instantiate(textCoin, pointFTCoin.position, Quaternion.identity) as GameObject;//Sinh ra chữ
        prefab.GetComponentInChildren<TextMesh>().text = "+" +(coinPlus).ToString();//Gán sát thương cho chữ
        StartCoroutine(PlusCoin());
    }

    public void AddLevel()//Mỗi khi tăng level thì lượng coin được tăng sẽ tăng thêm
    {
        coinPlus += coinLevel;//Tăng số coin được tăng sau mỗi khoản thời gian
    }

    public void UpLevel()//Mỗi khi tăng level thì lượng coin được tăng sẽ tăng thêm
    {
        coinAmount -= coinUpLevel;//Trừ coin
        coinUpLevel += 10;//Tăng lượng coin cần để tăng level
        coinPlus += coinLevel;//Tăng số coin được tăng sau mỗi khoản thời gian
    }

    public void Hack()
    {
        coinAmount += 50;
    }



}
