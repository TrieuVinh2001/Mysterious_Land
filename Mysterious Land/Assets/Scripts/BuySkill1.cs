using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySkill1 : MonoBehaviour
{
    public GameObject[] skills;//prefab các quái

    public Image[] imageEnemy;//ảnh các quái để ấn mua
    public SkillBase[] skillBase;//script các quái
    int spawnID = -1; //id của quái

    public GameObject backGroundBlack;
    public Vector2 pos;//Vị trí quái tạo ra
    public Vector3 pointPos;

    public GameObject[] coolDownEnemy;//GameObject Ảnh che
    public int[] timeCoolDown;//Thời gian giảm hồi chiêu(Tạo quái)
    public Image[] cool;//Ảnh che
    public Text[] textCoolDown;//Chữ thời gian giảm dần
    public float[] timeCool;//Thời gian giảm dần
    public bool[] isCoolDown;//Cái nào thời gian đang giảm

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
        if (CanSpawn()&&Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            pointPos = new Vector3(mousePos.x, mousePos.y, 0);

            pos = new Vector2(mousePos.x+2f, mousePos.y+6f);


            if (GameController.instance.coinAmount >= skillBase[spawnID].coin)
            {
                SpawnDefender(pos);
                isCoolDown[spawnID] = true;
                timeCool[spawnID] = timeCoolDown[spawnID];//cho thời gian giảm dần bằng thời gian hồi chiêu
                coolDownEnemy[spawnID].SetActive(true);//Hiện ảnh chặn k cho ấn
                StartCoroutine(CoolDownTime(spawnID));
                GameController.instance.coinAmount -= skillBase[spawnID].GetCoin();//Trừ tiền sau khi mua
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

    private void SpawnDefender(Vector2 roundedPos)//Quai
    {
        GameObject newDefender = Instantiate(skills[spawnID], roundedPos,Quaternion.Euler(0,0,-120)) as GameObject;

        newDefender.transform.parent = gameObject.transform;//khi tạo ra quái mới thì nó sẽ nằm trong GameObject Defenders
    }

    //Dùng trong event Trigger(Pointer Click) của ảnh các enemy
    public void SelectSkill(int id)//Khi nhấn chọn enemy để mua
    {
        DeselectEnemy();//Đổi lại màu tối

        spawnID = id;//Lấy id của enemy này(Ở đây là ảnh)

        imageEnemy[spawnID].color = Color.white;//Màu ban đầu

    }

    public void DeselectEnemy()//Đổi màu tối
    {
        spawnID = -1;

        foreach (var t in imageEnemy)//Đổi màu tất cả
        {
            t.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    private void CountDown0()
    {
        timeCool[0] -= Time.deltaTime;
        textCoolDown[0].text = Mathf.RoundToInt(timeCool[0]).ToString();//ép kiểu số nguyên
        cool[0].fillAmount = timeCool[0] / timeCoolDown[0];
        if (timeCool[0] <= 0)
        {
            isCoolDown[0] = false;
        }
    }
}
