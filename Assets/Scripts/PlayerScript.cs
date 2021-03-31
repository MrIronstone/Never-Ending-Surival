using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    Animator animator;

    public static int SlayedEnemies;

    void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);
        
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    TakeDamage(20);
        //}
    }

    public void TakeDamage ( int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Debug.Log("HIT YEDIN");
        Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Hit");
        GameObject.Find("VignetteEffect").GetComponent<Animator>().SetTrigger("Vignette");

        //EnemyHit
        if (GameObject.FindWithTag("Enemy"))
        {
            Debug.Log("Enemy Bulundu");
            GameObject.FindWithTag("Enemy").GetComponent<Animator>().SetTrigger("Attack");
        }

        if (currentHealth <= 0)
        {
            // Burada oyunu durdur
            Debug.LogError("ÖLDÜN");
            animator.SetTrigger("Death");
            StartCoroutine(GameOverScene());
            // buradaki arkaplan ve yerin ilerlemesini durdur
            //GameObject.Find("Background").GetComponent<MoveLeft>().stop = true;
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        // Buradaki fonksiyon eğer karakterimiz bir engel
        // ile karşılaşırsa hurt adlı animasyonu canlandırıyor
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(20);
        }
    }

    public void GiveHP()
    {
        if(currentHealth <= 90)
        {
            currentHealth += 10;
            healthBar.SetHealth(currentHealth);
        }
    }

    IEnumerator GameOverScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOverScene");
    }

    internal void TakeDamage()
    {
        throw new NotImplementedException();
    }
}
