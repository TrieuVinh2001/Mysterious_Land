using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyEnemy : MonoBehaviour
{
    public static BuyEnemy instance;
    public GameObject[] enemies;//prefab các quái

    public Image[] imageEnemy;//ảnh các quái để ấn mua
    public EnemyBase[] enemyBase;//script các quái
    //[HideInInspector]
    public int spawnID = -1; //id của quái
    public GameObject[] coolDownEnemy;//GameObject Ảnh che
    public int[] timeCoolDown;//Thời gian giảm hồi chiêu(Tạo quái)
    public GameObject backGroundBlack;//Nền đen lúc ấn đặt quái
    public Vector2 pos;//Vị trí quái tạo ra
    public Image[] cool;//Ảnh che
    public Text[] textCoolDown;//Chữ thời gian giảm dần
    public float[] timeCool;//Thời gian giảm dần
    public bool[] isCoolDown;//Cái nào thời gian đang giảm

    void Start()
    {
        cool[0].fillAmount = 1f;
        //cool[spawnID].fillAmount = 0.0f; 
    }

    void Update()
    {
        if (CanSpawn())
        {
            if (imageEnemy[spawnID].color == new Color(0.5f, 0.5f, 0.5f))//Khi màu tối thì id=-1 (tránh lỗi sau khi tạo enemy 2 hàng còn lại vẫn giữ id đó)
            {
                spawnID = -1;
            }
        }

        if (isCoolDown[0])
        {
            CountDown0();
        }
        if (isCoolDown[1])
        {
            CountDown1();
        }
        if (isCoolDown[2])
        {
            CountDown2();
        }
        if (isCoolDown[3])
        {
            CountDown3();
        }
        if (isCoolDown[4])
        {
            CountDown4();
        }
        if (isCoolDown[5])
        {
            CountDown5();
        }
        if (isCoolDown[6])
        {
            CountDown6();
        }


        //CountDown(spawnID);
    }

    private void OnMouseDown()//Khi ấn chuột
    {
        DetectSpawn();     //Tạo quái
        backGroundBlack.SetActive(true);
        StartCoroutine(BGBlack());
    }

    IEnumerator BGBlack()
    {
        yield return new WaitForSeconds(0.1f);
        backGroundBlack.SetActive(false);
    }

    bool CanSpawn()
    {
        if (spawnID == -1)//Nếu k có enemy nào để mua
            return false;
        else
            return true;
    }

    void DetectSpawn()//Sinh các enemy
    {
        if (CanSpawn())
        {
            if (GameController.instance.coinAmount >= enemyBase[spawnID].coin && GameController.instance.enemyCount < GameController.instance.enemyMax)
            {
                GameController.instance.PlusEnemyCount();//Tăng số lượng
                SpawnDefender(pos);
                //if (spawnID == 0)
                //{
                    isCoolDown[spawnID] = true;
                //}
                
                timeCool[spawnID] = timeCoolDown[spawnID];
                coolDownEnemy[spawnID].SetActive(true);//Hiện ảnh chặn k cho ấn

                StartCoroutine(CoolDownTime(spawnID));

                GameController.instance.coinAmount -= enemyBase[spawnID].GetCoin();//Trừ tiền sau khi mua
                DeselectEnemy();
            }
            else
            {
                Debug.Log("Not enough coin or max enemy");
            }
        }
       
        DeselectEnemy();//Đổi màu tối
    }

    IEnumerator CoolDownTime(int id)
    {
        yield return new WaitForSeconds(timeCoolDown[spawnID]);
        coolDownEnemy[id].SetActive(false);
        isCoolDown[id] = false;
    }

    

    private void SpawnDefender(Vector2 roundedPos)//cây
    {
        GameObject newDefender = Instantiate(enemies[spawnID], roundedPos, Quaternion.identity) as GameObject;

        newDefender.transform.parent = gameObject.transform;//khi tạo ra quái mới thì nó sẽ nằm trong GameObject Defenders
    }

    //Dùng trong event Trigger(Pointer Click) của ảnh các enemy
    public void SelectEnemy(int id)//Khi nhấn chọn enemy để mua
    {
        DeselectEnemy();//Đổi lại màu tối

        spawnID = id;//Lấy id của enemy này(Ở đây là ảnh)

        imageEnemy[spawnID].color = Color.white;//Màu ban đầu
        
    }

    public void DeselectEnemy()//Đổi màu tối
    {
        spawnID = -1;

        foreach(var t in imageEnemy)//Đổi màu tất cả
        {
            t.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    private void CountDown0()//Giảm thời gian hồi chiêu và xét text thời gian giảm
    {
        timeCool[0] -= Time.deltaTime;
        textCoolDown[0].text = Mathf.RoundToInt(timeCool[0]).ToString();//ép kiểu số nguyên
        cool[0].fillAmount = timeCool[0] / timeCoolDown[0];//Ảnh fill
        if (timeCool[0] <= 0)
        {
            isCoolDown[0] = false;
        }
    }
    private void CountDown1()
    {
        timeCool[1] -= Time.deltaTime;
        textCoolDown[1].text = Mathf.RoundToInt(timeCool[1]).ToString();//ép kiểu số nguyên
        cool[1].fillAmount = timeCool[1] / timeCoolDown[1];
        if (timeCool[1] <= 0)
        {
            isCoolDown[1] = false;
        }
    }
    private void CountDown2()
    {
        timeCool[2] -= Time.deltaTime;
        textCoolDown[2].text = Mathf.RoundToInt(timeCool[2]).ToString();//ép kiểu số nguyên
        cool[2].fillAmount = timeCool[2] / timeCoolDown[2];
        if (timeCool[2] <= 0)
        {
            isCoolDown[2] = false;
        }
    }
    private void CountDown3()
    {
        timeCool[3] -= Time.deltaTime;
        textCoolDown[3].text = Mathf.RoundToInt(timeCool[3]).ToString();//ép kiểu số nguyên
        cool[3].fillAmount = timeCool[3] / timeCoolDown[3];
        if (timeCool[3] <= 0)
        {
            isCoolDown[3] = false;
        }
    }
    private void CountDown4()
    {
        timeCool[4] -= Time.deltaTime;
        textCoolDown[4].text = Mathf.RoundToInt(timeCool[4]).ToString();//ép kiểu số nguyên
        cool[4].fillAmount = timeCool[4] / timeCoolDown[4];
        if (timeCool[4] <= 0)
        {
            isCoolDown[4] = false;
        }
    }
    private void CountDown5()
    {
        timeCool[5] -= Time.deltaTime;
        textCoolDown[5].text = Mathf.RoundToInt(timeCool[5]).ToString();//ép kiểu số nguyên
        cool[5].fillAmount = timeCool[5] / timeCoolDown[5];
        if (timeCool[5] <= 0)
        {
            isCoolDown[5] = false;
        }
    }
    private void CountDown6()
    {
        timeCool[6] -= Time.deltaTime;
        textCoolDown[6].text = Mathf.RoundToInt(timeCool[6]).ToString();//ép kiểu số nguyên
        cool[6].fillAmount = timeCool[6] / timeCoolDown[6];
        if (timeCool[6] <= 0)
        {
            isCoolDown[6] = false;
        }
    }

}
