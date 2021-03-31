using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10;
    public GameObject obstacle;
    GameObject Player;

    public bool FightTrue = false;
    public bool stop = false;

    void Start()
    {
        Player = GameObject.Find("Hero");
    }

    void Update()
    {
        FightTrue = GameObject.Find("--CombatController--").GetComponent<CombatMechanic>().isFighting;
        int currentHealth = Player.GetComponent<PlayerScript>().currentHealth;

        if (!FightTrue && currentHealth > 0)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
            if (transform.position.x < -4)
            {
                Destroy(obstacle);
            }
        }
    }
}
