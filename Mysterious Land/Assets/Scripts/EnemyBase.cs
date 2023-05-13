using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int coin;//Số vàng cần để mua 
    public float damage;//Tấn công
    public float speed;//Tốc độ chạy
    public float attackSpeed;//Tốc độ tấn công
    public float critRate;//Tỉ lệ chí mạng
    public float attackRange;//Khoảng cách tấn công
    private float nextAttackTime = 1f;//Thời gian tấn công tiếp theo
    public Transform attackPoint;//Vị trí tấn công
    public bool isPlayer;
    public bool isAttack;
    [HideInInspector]
    public float speedFirt;
    private float damageFirt;
    private float damageCrit;

    public LayerMask enemyLayers;

    public bool isVampire;
    public GameObject textHealth;
    public float bloodSuckRate;
    private Health healthMain;

    private Rigidbody2D rb;
    private Animator anim;
    //private GameObject[] players;
    //private Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        healthMain = GetComponent<Health>();
        speedFirt = speed;
        damageFirt = damage;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //players = GameObject.FindGameObjectsWithTag("Player");
        //enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Run", true);
        //rb.velocity = Vector2.right * speed;
        transform.Translate(Vector2.right * speed * Time.deltaTime);//di chuyển
        

        if (isAttack && isPlayer)//Nếu ở trạng thái tấn công và là player
        {
            
            StartCoroutine(PlayerAttack());
        }
        else if(isAttack&&!isPlayer)//Nếu ở trạng thái tấn công và là enemy
        {
            
            StartCoroutine(EnemyAttack());
        }
        
    }

    IEnumerator PlayerAttack()//Tấn công của player
    {
        anim.SetBool("Run", false);
        speed = 0;//Dừng lại
        Collider2D[] enemes = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);//Lấy ra các enemy trong khoảng tấn công
        if (enemes.Length <= 0)//Nếu k có (k có thì vẫn là mảng nên cẩn thận mảng rỗng)
        {
            isAttack = false;
            speed = speedFirt;//Đi tiếp
            anim.SetBool("Run", true);
        }
        else
        {
            Health health = enemes[0].GetComponent<Health>();//Lấy script của enemy này
            if (Time.time >= nextAttackTime)//Thời gian nghỉ tấn công
            {
                nextAttackTime = Time.time + attackSpeed;//xét lại thời gian tiếp theo tấn công
                anim.SetTrigger("Attack");
                yield return new WaitForSeconds(0.5f);//Chờ để gây damge
                
                if (health != null)
                {
                    int crit = Random.RandomRange(1, 100);//Chỉ số chí mạng mỗi lần đánh random
                    if (crit <= critRate)//Nếu nhỏ hơn chí mạng
                    {
                        damageCrit = damage * 2;//Nhân đôi dame
                    }
                    else
                    {
                        damageCrit = damageFirt;//Nếu không thì lấy dame gốc
                    } 
                    health.TakeDamage(damageCrit);//Gây damage
                    if (isVampire)//Nếu là Vampire thì hút máu
                    {
                        if (healthMain.health < healthMain.maxhealth)
                        {
                            healthMain.health += damage * bloodSuckRate * 0.01f;
                            GameObject prefab = Instantiate(textHealth, transform.position, Quaternion.identity) as GameObject;//Sinh ra chữ
                            prefab.GetComponentInChildren<TextMesh>().text = (damage * bloodSuckRate * 0.01f).ToString();//Gán sát thương cho chữ
                        }   
                    }
                }
            }
        }
    }

    IEnumerator EnemyAttack()//Tấn công của enemy
    {
        anim.SetBool("Run", false);
        speed = 0;
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        if (players.Length <= 0)
        {
            isAttack = false;
            speed = speedFirt;
            anim.SetBool("Run", true);
        }
        else
        {
            Health health = players[0].GetComponent<Health>();
            if (Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + attackSpeed;
                anim.SetTrigger("Attack");
                yield return new WaitForSeconds(0.5f);

                if (health != null)
                {
                    int crit = Random.RandomRange(1, 100);//Chỉ số chí mạng mỗi lần đánh random
                    if (crit <= critRate)//Nếu nhỏ hơn chí mạng
                    {
                        damageCrit = damage * 2;//Nhân đôi dame
                    }
                    else
                    {
                        damageCrit = damageFirt;//Nếu không thì lấy dame gốc
                    }
                    health.TakeDamage(damageCrit);//Gây damage
                    if (isVampire)//Nếu là Vampire thì hút máu
                    {
                        
                        healthMain.health += damage * bloodSuckRate * 0.01f ;//Lượng máu tăng lên

                        GameObject prefab = Instantiate(textHealth, transform.position, Quaternion.identity) as GameObject;//Sinh ra chữ
                        prefab.GetComponentInChildren<TextMesh>().text = (damage * bloodSuckRate * 0.01f).ToString();//Gán sát thương cho chữ
                    }
                }
                //players[0].GetComponent<Health>().TakeDamage(damage);
            }
        }
    }


    //private void Attack()
    //{
    //    anim.SetTrigger("Attack");
    //    Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
    //    if (enemies != null)
    //    {
    //        enemies[0].GetComponent<Health>().TakeDamage(damage);
    //        //foreach (Collider2D enemy in enemies)
    //        //{
    //        //    enemy.GetComponent<Health>().TakeDamage(damage);
    //        //}
    //    }
        
    //}

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
