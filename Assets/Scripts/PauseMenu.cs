using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public AudioSource FightMusicSource;
    public AudioSource MainThemeMusicSource;

    private void Start()
    {
        FightMusicSource = GameObject.Find("--CombatController--").gameObject.GetComponent<CombatMechanic>().FightSrc;
        MainThemeMusicSource = GameObject.Find("--CombatController--").gameObject.GetComponent<CombatMechanic>().StartGameSrc;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
                
            else
            {
                Pause();
            }
                
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;


        // resume ekranında müziğin geri oynaması
        if (!FightMusicSource.isPlaying)
        {
            FightMusicSource.UnPause();
        }
        if (!MainThemeMusicSource.isPlaying)
        {
            MainThemeMusicSource.UnPause();
        }

    }

    public void Pause () 
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // Pause ekranında müziğin durması
        if (FightMusicSource.isPlaying)
        {
            FightMusicSource.Pause();
        }
        else if (MainThemeMusicSource.isPlaying)
        {
            MainThemeMusicSource.Pause();
        }

        
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();

    }
}
