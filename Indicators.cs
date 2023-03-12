using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour
{
    
    public Image healthBar, foodBar, energyBar;
    [Header("Set in Inspector")]
    public float healthAmount = 100;
    public float foodAmount = 100;
    public float energyAmount = 100;
    public float secondsToEmptyHealth = 300f;
    public float secondsToEmptyFood = 120f;

    void Start()
    {
        healthBar.fillAmount = healthAmount / 100;
        foodBar.fillAmount = foodAmount / 100;
    }


    void Update()
    {
        if(foodAmount > 0)
        {
            foodAmount -= 100 / secondsToEmptyFood * Time.deltaTime;
            foodBar.fillAmount = foodAmount / 100;
        }

        if(foodAmount <= 0 )
        {
            healthAmount -= 100 / secondsToEmptyHealth * Time.deltaTime;
        }
        healthBar.fillAmount = healthAmount / 100;
    }
}
