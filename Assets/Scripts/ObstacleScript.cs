using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleScript : MonoBehaviour
{
    private GameObject Player;
    private bool stopEveryting = false;
    public float Distance;

    private CombatMechanic CombatController;

    void Start()
    {
        //playeri bul
        Player = GameObject.FindWithTag("Player");
        CombatController = GameObject.Find("--CombatController--").gameObject.GetComponent<CombatMechanic>();
    }

    void Update()
    {
        if (Vector3.Distance(Player.transform.position, gameObject.transform.position) <= Distance && !stopEveryting)
        {
            stopEveryting = true;

            // Tek Seferlik Sadece Up Tuşu Yak
            Debug.Log("ONE TIME JUMP EVENT");
            //CombatController.StartOneTimeFight(gameObject);

        }
    }
}
