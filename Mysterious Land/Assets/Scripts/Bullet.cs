using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;//Đạn
    public Transform pointBullet;//Vị trí bắn

    public void BulletAttack()
    {
        Instantiate(bullet, pointBullet.position, Quaternion.identity);//Tạo đạn
    }
}
