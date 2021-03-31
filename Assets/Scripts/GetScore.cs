using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
    public Text Score;
    void Start()
    {
        if (PlayerPrefs.HasKey("Coin"))
        {
            Score.text = PlayerPrefs.GetInt("Coin").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("Coin", 0);
            Score.text = "0";
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
