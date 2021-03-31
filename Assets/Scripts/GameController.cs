using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // Menuler
    // Fight'a giriş için düşman distance hesabı
    public TextMeshProUGUI CoinInGameTxt;
    public int TotalCoin;


    void Awake()
    {
        if (PlayerPrefs.HasKey("Coin"))
            TotalCoin = PlayerPrefs.GetInt("Coin");
        else
            PlayerPrefs.SetInt("Coin",0);
        

        CoinInGameTxt.text = TotalCoin.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseCoin()
    {
        TotalCoin += 10;
        PlayerPrefs.SetInt("Coin", TotalCoin);
        CoinInGameTxt.text = TotalCoin.ToString();
    }
}
