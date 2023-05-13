using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health instance;
    public float health;//Máu
    public float defense;
    [HideInInspector]
    public float maxhealth;//Máu tối đa (dùng cho healthBar)
    public GameObject floatingText;//Text popup (dùng để hiện sát thương)
    private Animator anim;
    private SpriteRenderer sp;
    private EnemyBase enemyBase;
    private bool isDie;
    public bool isSummon;
    private void Awake()
    {
        maxhealth = health;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    
    public void TakeDamage(float damage)
    {
        GameObject prefab = Instantiate(floatingText, transform.position, Quaternion.identity) as GameObject;//Sinh ra chữ
        prefab.GetComponentInChildren<TextMesh>().text = (damage-defense).ToString();//Gán sát thương cho chữ
        Destroy(prefab, 0.5f);//Xóa chữ
        health = health - (damage - defense); //Gây Damage
        StartCoroutine(DamageColor());//Đổi máu quái
        //anim.SetTrigger("Hurt");

        if (!isDie && health <= 0)
        {
            health = 0;
            isDie = true;
            Die();
        }
    }

    private void Die()
    {
        if (!isSummon)
        {
            if (enemyBase && enemyBase.isPlayer)
            {
                GameController.instance.MinusEnemyCount();
            }
            else if (enemyBase && !enemyBase.isPlayer)
            {
                GameController.instance.MinusEnemyCount1();
            }
        }
        

        anim.SetTrigger("Death");
        Destroy(gameObject, 0.5f);
    }

    IEnumerator DamageColor()
    {
        sp.color = Color.red;//Đổi sang màu đỏ
        yield return new WaitForSeconds(0.2f);
        sp.color = Color.white;//Trở về màu ban đầu
    }
}
