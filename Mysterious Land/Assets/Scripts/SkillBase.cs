using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    public static SkillBase instance;
    public int coin;//Số vàng cần để mua 
    public float damage;//Tấn công
    public float attackRange;//Khoảng cách tấn công
    public bool isPlayer;
    public bool isAttack;
    public float speed;
    public float timeDestroy;
    public GameObject vFXDestroy;

    public Transform attackPoint;

    public LayerMask enemyLayers;

    private Rigidbody2D rb;
    private Animator anim;
    private BuySkill skill;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        skill = GetComponentInParent<BuySkill>();
        StartCoroutine(DestroySkill());
    }

    // Update is called once per frame
    void Update()
    {
        SkillPlayer();
        Move(skill.pointPos);
    }

    public void SkillPlayer()//Tấn công của player
    { 
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);//Lấy ra các enemy trong khoảng tấn công
        if (enemies.Length <= 0)//Nếu k có (k có thì vẫn là mảng nên cẩn thận mảng rỗng)
        {
            
        }
        else
        {
            foreach (Collider2D enemy in enemies)
            {
                Health health = enemy.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);//Gây damage
                }
            }
            Instantiate(vFXDestroy, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject); 
        }
    }

    IEnumerator DestroySkill()
    {
        yield return new WaitForSeconds(timeDestroy);
        Instantiate(vFXDestroy, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Move(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    private void OnDrawGizmosSelected()//Vẽ vòng tròn của attackRange
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public int GetCoin()//Lấy số coin dùng trong BuyButton
    {
        return coin;
    }
}
