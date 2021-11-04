using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour // S (se encargarga solamente de administar la monedas del player)
{
    static int money = 0;
    private TextMeshProUGUI CoinsDisplay;
    int displayMoney;

    #region Singleton
    public static Inventory Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        CoinsDisplay = GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>();
    }

    public void GetMoney()
    {
        money++;
    }

    public void MoneyDisplay()
    {
        displayMoney = money;
        CoinsDisplay.text = displayMoney.ToString();
    }
}

