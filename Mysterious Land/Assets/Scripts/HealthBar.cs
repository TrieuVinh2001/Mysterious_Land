using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image healthBar;// Ảnh máu
    Text healthText;//Số máu
    public Health healthScript;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        healthText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = healthScript.health / healthScript.maxhealth;//Điều khiển thanh máu nhờ fill 
        healthText.text = healthScript.health.ToString() + "/" + healthScript.maxhealth.ToString();//Hiện ra số máu
    }
}
