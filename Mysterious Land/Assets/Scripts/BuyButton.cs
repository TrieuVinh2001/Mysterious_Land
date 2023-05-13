using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public static BuyButton instance;
    public GameObject enemy;//Prefab quái
    private Text coinText;
    [SerializeField]
    EnemyBase enemyBase;//Script quái đó
    //public Transform spawnerPoint;//Vị trí tạo quái
    
    // Start is called before the first frame update
    void Start()
    {
        coinText = GetComponentInChildren<Text>();//Lấy Text Component của con
        if (coinText&& enemyBase)
        {
            coinText.text = enemyBase.GetCoin().ToString();//Xét số coin ra text
        }
        else
        {
            coinText.text = GameController.instance.coinUpLevel.ToString();
        }
    }

    void Update()
    {
        if (coinText && !enemyBase)
        {
            coinText.text = GameController.instance.coinUpLevel.ToString();
        }
        
    }

    //public void BuyEnemy()
    //{
    //    if (GameController.instance.coinAmount >= enemyBase.coin && GameController.instance.enemyCount<GameController.instance.enemyMax)
    //    {
    //        GameController.instance.PlusEnemyCount();
    //        Instantiate(enemy, spawnerPoint.position, Quaternion.identity);//Sinh quái
    //        GameController.instance.coinAmount -= enemyBase.GetCoin();//Trừ tiền sau khi mua
    //    }
    //    else
    //    {
    //        Debug.Log("Not enough coin or max enemy");
    //    }
    //}

    public void UpLevel()
    {
        if (GameController.instance.coinAmount >= GameController.instance.coinUpLevel)
        {
            GameController.instance.UpLevel();
        }
        
    }
}
