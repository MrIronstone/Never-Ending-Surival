using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    
    //public ParticleSystem dirtParticle;
    public float jumpForce = 5.0f;
    public float health = 100f;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip hitObstacleSound;
    public AudioClip hearthSound;
    public AudioClip jumpSound;
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAnim.SetBool("Grounded", true);
    }

    void Update()
    {
        playerAnim.SetFloat("AirSpeedY", playerRb.velocity.y);
        isRunning();
           
        if (Input.GetKeyDown(KeyCode.Space) && playerAnim.GetBool("Grounded"))
        {
            Jump();
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Ground"))
        {

            playerAnim.SetBool("Grounded", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Obstacle")){
            playerAudio.PlayOneShot(hitObstacleSound, 0.6f);
        }
        if (other.gameObject.tag.Equals("Coin"))
        {
            playerAudio.PlayOneShot(moneySound, 1.4f);
            Destroy(other.gameObject);

            Debug.Log("Para Toplandı");
            GameObject.Find("_GameController").GetComponent<GameController>().IncreaseCoin();
        }
        if (other.gameObject.tag.Equals("Hearth"))
        {
            Debug.Log("You git the hearth");
            playerAudio.PlayOneShot(hearthSound, 0.5f);
            Destroy(other.gameObject);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().GiveHP();
            
        }
    }

    public void Jump()
    {
        

        playerRb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        playerAnim.SetBool("Grounded", false);
        playerAnim.SetTrigger("Jump");
        playerAudio.PlayOneShot(jumpSound, 0.3f);
        //obstacleHitParticle.Stop();
    }

    void isRunning()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            // eğer animator'un güncel state'i "Run" ise ayağımın altındaki toz sprite'ı aktif olacak
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Ayağındakini tozu AÇMA çalıştı");
        }
        else
        {
            // eğer değil ise kapanacak bu sadece sadece koşarken ayağımın altından toz çıkacak
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log("Ayağındakini tozu KAPATMA çalıştı");
        }
    }
}
