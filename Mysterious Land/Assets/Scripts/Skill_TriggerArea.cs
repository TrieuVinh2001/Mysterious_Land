using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_TriggerArea : MonoBehaviour
{
    private SkillBase skill;

    private void Awake()
    {
        skill = GetComponentInParent<SkillBase>();
    }

    private void OnTriggerEnter2D(Collider2D col)//Nếu va chạm
    {
        if (skill.isPlayer)//Nếu đang là player va chạm với tag enemy
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                skill.isAttack = true;//Chuyển trạng thái sang tấn công
            }
        }
        else
        {
            if (col.gameObject.CompareTag("Player"))
            {
                skill.isAttack = true;
            }
        }
    }
}
