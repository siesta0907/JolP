using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class MoneyManager : MonoBehaviour
{
    public static int money ;
    public Text coinText;

    void Start()
    {
        money = 500;    
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = money.ToString();
    }
}
