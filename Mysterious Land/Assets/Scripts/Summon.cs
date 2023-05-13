using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public float timeSummon;//Thời gian chờ triệu hồi
    public GameObject batPrefab;//Dơi triệu hồi
    public Transform pointSummon;//Điểm triệu hồi
    private float nextSummonTime=5f;
    private EnemyBase enemy;
    private Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<EnemyBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.isAttack && Time.time>nextSummonTime)
        {
            enemy.speed = 0;//Đứng yên để triệu hồi
            anim.SetTrigger("Attack");
            nextSummonTime = Time.time + timeSummon;//Thời gian chờ triệu hồi
            Instantiate(batPrefab, pointSummon.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.6f, 0.6f),0), Quaternion.identity);
            Instantiate(batPrefab, pointSummon.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.6f, 0.6f), 0), Quaternion.identity);
            Instantiate(batPrefab, pointSummon.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.6f, 0.6f), 0), Quaternion.identity);
            StartCoroutine(Speed());//Di chuyển tiếp
        }
    }

    IEnumerator Speed()
    {
        yield return new WaitForSeconds(0.5f);
        enemy.speed = enemy.speedFirt;
    }
}
