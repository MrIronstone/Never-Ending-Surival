using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBandit : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public HealthBar healthBar;
    private GameObject Player;
    Animator animator;

    private bool stopEveryting = false;

    void Start()
    {
        //playeri bul
        Player = GameObject.FindWithTag("Player");
        animator = gameObject.GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if(gameObject.transform.position.x <= -0.5f && !stopEveryting)
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
            stopEveryting = true;
            GameObject.Find("--CombatController--").gameObject.GetComponent<CombatMechanic>().isFighting = true;
            GameObject.Find("--CombatController--").gameObject.GetComponent<CombatMechanic>().StartFight(gameObject);

            Debug.Log("AnimatorGO");
            //player idle animasyonu gir
            Player.GetComponent<Animator>().SetBool("isFighting",true);

        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Hit");
        if (currentHealth <= 0)
        {
            // Burada oyunu durdur
            Debug.LogError("Düşman Öldü");

            GameObject.Find("_GameController").GetComponent<GameController>().IncreaseCoin();

            GameObject.Find("--CombatController--").GetComponent<CombatMechanic>().StartMusicAgain();

            animator.SetTrigger("Death");
            GameObject.Find("--CombatController--").GetComponent<CombatMechanic>().isFighting = false;
            GameObject.Find("--CombatController--").GetComponent<CombatMechanic>().ClearCombination();
            GameObject.Find("--CombatController--").GetComponent<CombatMechanic>().play = false;
            Player.GetComponent<Animator>().SetBool("isFighting", false);
            transform.GetChild(0).gameObject.SetActive(false);
            // buradaki arkaplan ve yerin ilerlemesini durdur
            //GameObject.Find("Background").GetComponent<MoveLeft>().stop = true;
            
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }


}
