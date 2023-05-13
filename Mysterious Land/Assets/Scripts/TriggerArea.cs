using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    private EnemyBase enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter2D(Collider2D col)//Nếu va chạm
    {
        if (enemy.isPlayer)//Nếu đang là player va chạm với tag enemy
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                enemy.isAttack = true;//Chuyển trạng thái sang tấn công
            }
        }
        else
        {
            if (col.gameObject.CompareTag("Player"))
            {
                enemy.isAttack = true;
            }
        }
    }

    //private void OnTriggerExit2D()
    //{
    //    enemy.isAttack = false;
    //    //enemy.speed = -1;
    //}

}
