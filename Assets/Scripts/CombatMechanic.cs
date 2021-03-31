using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CombatMechanic : MonoBehaviour
{
    /* Combinasyon arrayi ( hangi sırayla tuşların yanması gerektiği tutulan array )
     * isActive arrayi ( hangi tuşun o anda yanıp yanmayacağını tuttuğumuz boolları tuttuğumuz array ) 
     * 
     * 
     * 
     */
    public GameObject Player;

    public GameObject LeftBtn;
    public GameObject RightBtn;
    public GameObject UpBtn;
    public GameObject DownBtn;

    public Text IndicatorTxt;

    public float WaitTime;

    public Sprite[] BtnLightArray;
    public Sprite[] BtnIdle;
    public Sprite[] BtnPressed;
    public Sprite[] BtnWrong;
    public AudioClip audio1;
    public AudioClip audio2;
    public AudioClip audio3;
    public AudioClip audio4;
    // public float audiovolume = 0.25f;

    private AudioSource LeftBtnAudioSrc;
    private AudioSource RightBtnAudioSrc;
    private AudioSource UpBtnAudioSrc;
    private AudioSource DownBtnAudioSrc;

    public AudioClip Fight;
    public AudioClip StartGame;

    public AudioSource FightSrc;
    public AudioSource StartGameSrc;

    public Text[] DebugText;

    public bool isFighting = false; // combat başlayınca true olacak 
    bool wait = false;
    public bool play = false;
    public bool DebugMode = false;

    private List<int> Combination = new List<int>();
    int CombinationIndex = -1;
    private bool[] isActiveArr;
    private GameObject Enemy;

    int indexAnim;

    void Start()
    {
        StartGameSrc.PlayOneShot(StartGame);
        LeftBtnAudioSrc = LeftBtn.GetComponent<AudioSource>();
        RightBtnAudioSrc = RightBtn.GetComponent<AudioSource>();
        UpBtnAudioSrc = UpBtn.GetComponent<AudioSource>();
        DownBtnAudioSrc = DownBtn.GetComponent<AudioSource>();

        if (!DebugMode)
        {
            DebugText[0].gameObject.SetActive(false);
            DebugText[1].gameObject.SetActive(false);
            DebugText[2].gameObject.SetActive(false);
            DebugText[3].gameObject.SetActive(false);
        }
    }

    public void StartFight(GameObject enemy)
    {
        if (StartGameSrc.isPlaying)
            StartGameSrc.Stop();

        FightSrc.PlayOneShot(Fight);

        Enemy = enemy;
        //Random Combinasyon al öldürülen düşman sayısına göre combinasyon sayısını arttır
        //3 = 4
        //5 = 5
        //10 = 6
        //20 = 7
        //PlayerScript playerScript;

        //for(int i =0; i<= Random.Range(4,5); i++)
        //{

        //}

        for (int i = 0; i <= Random.Range(2, 3); i++)
        {
            Combination.Add(Random.Range(1,4));
            Debug.Log("CEVAP" + i + ": " + Combination[i]);
            CombinationIndex = 0;
        }

        isFighting = true;

        StartCoroutine(LightBtn());
    }

    public void StartOneTimeFight(GameObject enemy)
    {
        CombinationIndex = 0;
        Combination.Clear();
        Combination.Add(3); // Only ADD Up
        IndicatorTxt.text = "ZIPLA!!";
        UpBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnLightArray[2];
        play = true;
        wait = true;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        UpBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnIdle[2];
        wait = false;
        play = false;
        IndicatorTxt.text = "";
    }

    IEnumerator TrueObsticle()
    {
        Debug.LogWarning("DOĞRU ANDA ZIPLADI");
        CombinationIndex = 0;
        Combination.Clear();
        IndicatorTxt.text = "";
        Player.GetComponent<PlayerController>().Jump();
        UpBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnPressed[2];
        yield return new WaitForSeconds(0.2f);
        UpBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnIdle[2];
    }

    IEnumerator FalseObsticle()
    {
        Debug.LogError("YANLIŞ DOĞRU ANDA ZIPLADI");
        CombinationIndex = 0;
        Combination.Clear();
        IndicatorTxt.text = "";
        UpBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnWrong[2];
        yield return new WaitForSeconds(0.2f);
        UpBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnIdle[2];
    }

    public void StartMusicAgain()
    {
        FightSrc.Stop();
        StartGameSrc.PlayOneShot(StartGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && play && wait)
        {
            //play sound
            Debug.Log("CombinationIndex: " + CombinationIndex);
            if (Combination[CombinationIndex] == 3)
            {
                StartCoroutine(TrueObsticle());
            }
            else
            {
                StartCoroutine(FalseObsticle());
            }
        }

        //Hepsi Doğru ise
        if (CombinationIndex == Combination.Count && Combination.Count != 0)
        {
            play = false;
            CombinationIndex = 0;
            IndicatorTxt.text = "HEPSI DOGRU!!";
            Combination.Clear();

            //YENI COMBINASYON BULUNUYOR
            for (int i = 0; i <= Random.Range(2, 3); i++)
            {
                Combination.Add(Random.Range(1, 4));
                Debug.Log("CEVAP" + i + ": " + Combination[i]);
                CombinationIndex = 0;
            }

            
            if(Enemy.GetComponent<EnemyBandit>().currentHealth > 0)
            {
                //Eğer düşman ölmediyse
                Debug.Log("Enemy Ölmedi Next Round");
                StartCoroutine(LightBtn());
            }
            else
            {
                //Win fight
                Debug.LogWarning("DÜŞMAN ÖLDÜ!!!");
                FightSrc.Stop();
                StartGameSrc.PlayOneShot(StartGame);
                Player.GetComponent<Animator>().SetBool("isFighting",false);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow) && play)
        {
            //play sound
            LeftBtnAudioSrc.PlayOneShot(audio1);
            Debug.Log("CombinationIndex: " + CombinationIndex + "Combination:"+ Combination[CombinationIndex].ToString());
            if(Combination[CombinationIndex] == 1)
            {
                Debug.Log("Doğru Cevap");
                StartCoroutine(TrueAnswer(LeftBtn,0));
            }
            else
            {
                //yanlış
                //hit ye 
                // * Hit yemek için coroutine aç 
                // * Playerın current animation state'i idle'a gelene kadar bekle
                // * idle olduğunda tekrar LightBtn çalıştır 
                Debug.LogError("Yanlış Cevap");
                StartCoroutine(FalseAnswer(LeftBtn, 0));
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && play)
        {
            //play sound
            RightBtnAudioSrc.PlayOneShot(audio2);
            Debug.Log("CombinationIndex: " + CombinationIndex);
            if (Combination[CombinationIndex] == 2)
            {
                Debug.Log("Doğru Cevap");
                StartCoroutine(TrueAnswer(RightBtn, 1));
            }
            else
            {
                //yanlış
                //hit ye 
                // * Hit yemek için coroutine aç 
                // * Playerın current animation state'i idle'a gelene kadar bekle
                // * idle olduğunda tekrar LightBtn çalıştır 
                Debug.LogError("Yanlış Cevap");
                StartCoroutine(FalseAnswer(RightBtn, 1));
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && play)
        {
            //play sound
            UpBtnAudioSrc.PlayOneShot(audio3);
            Debug.Log("CombinationIndex: " + CombinationIndex);
            if (Combination[CombinationIndex] == 3)
            {
                Debug.Log("Doğru Cevap");
                StartCoroutine(TrueAnswer(UpBtn, 2));
            }
            else
            {
                //yanlış
                //hit ye 
                // * Hit yemek için coroutine aç 
                // * Playerın current animation state'i idle'a gelene kadar bekle
                // * idle olduğunda tekrar LightBtn çalıştır 
                Debug.LogError("Yanlış Cevap");
                StartCoroutine(FalseAnswer(UpBtn, 2));
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && play)
        {
            //play sound
            DownBtnAudioSrc.PlayOneShot(audio4);
            Debug.Log("CombinationIndex: " + CombinationIndex);
            if (Combination[CombinationIndex] == 4)
            {
                Debug.Log("Doğru Cevap");
                StartCoroutine(TrueAnswer(DownBtn, 3));
            }
            else
            {
                //yanlış
                //hit ye 
                // * Hit yemek için coroutine aç 
                // * Playerın current animation state'i idle'a gelene kadar bekle
                // * idle olduğunda tekrar LightBtn çalıştır 
                //Combination Listesini Sıfırla
                Debug.LogError("Yanlış Cevap");
                StartCoroutine(FalseAnswer(DownBtn, 3));
            }
        }
    }

    IEnumerator TrueAnswer(GameObject button, int pressedindex)
    {
        indexAnim = 1;
        if (indexAnim > 3)
            indexAnim = 1;

        //doğru
        //pressed sprite
        CombinationIndex++;
        Debug.LogWarning("DOĞRU CEVAP");
        IndicatorTxt.text = "DOGRU CEVAP";
        button.transform.GetChild(0).GetComponent<Image>().sprite = BtnPressed[pressedindex];
        yield return new WaitForSeconds(0.2f);
        button.transform.GetChild(0).GetComponent<Image>().sprite = BtnIdle[pressedindex];
        IndicatorTxt.text = "";

        //attack
        Enemy.GetComponent<EnemyBandit>().TakeDamage(20);

        switch (indexAnim)
        {
            case 1:
                Player.GetComponent<Animator>().SetTrigger("Attack1");
                break;
            case 2:
                Player.GetComponent<Animator>().SetTrigger("Attack2");
                break;
            case 3:
                Player.GetComponent<Animator>().SetTrigger("Attack3");
                break;
            default:
                Debug.LogError("Default switch state");
                break;
        }

        indexAnim++;
    }

    public void ClearCombination()
    {
        Combination.Clear();
    }

    IEnumerator FalseAnswer(GameObject button, int pressedindex)
    {
        IndicatorTxt.text = "YANLIS MENU!!";
        button.transform.GetChild(0).GetComponent<Image>().sprite = BtnWrong[pressedindex];
        yield return new WaitForSeconds(0.2f);
        button.transform.GetChild(0).GetComponent<Image>().sprite = BtnIdle[pressedindex];

        //hit player
        Player.GetComponent<PlayerScript>().TakeDamage(20);
        play = false;
        CombinationIndex = 0;
        yield return new WaitForSeconds(0.5f);
        Debug.LogError("YANLIŞ CEVAP TEKRAR DENE");
        StartCoroutine(LightBtn());
    }

    IEnumerator LightBtn()
    {
        //kullanıcıya kombinasyonu öğretmek için butonları yanıp söndürür

        IndicatorTxt.text = "ONCE IZLE";
        yield return new WaitForSeconds(0.3f);
        IndicatorTxt.text = "IZLE";

        //Debug.Log(Combination.Count);
        for (int i = 0; i< Combination.Count; i++)
        {
            yield return new WaitForSeconds(0.2f);
            switch (Combination[i])
            {
                case 1:
                    //tuşu yak
                    LeftBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnLightArray[0];
                    // Play Note Sound
                    LeftBtnAudioSrc.PlayOneShot(audio1);
                    yield return new WaitForSeconds(WaitTime);

                    //tuşu kapat
                    LeftBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnIdle[0];
                    break;
                case 2:
                    //tuşu yak
                    RightBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnLightArray[1];
                    // Play Note Sound
                    RightBtnAudioSrc.PlayOneShot(audio2);
                    yield return new WaitForSeconds(WaitTime);

                    //tuşu kapat
                    RightBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnIdle[1];
                    break;
                case 3:
                    //tuşu yak
                    UpBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnLightArray[2];
                    // Play Note Sound
                    UpBtnAudioSrc.PlayOneShot(audio3);
                    yield return new WaitForSeconds(WaitTime);

                    //tuşu kapat
                    UpBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnIdle[2];
                    break;
                case 4:
                    //tuşu yak
                    DownBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnLightArray[3];
                    // Play Note Sound
                    DownBtnAudioSrc.PlayOneShot(audio4);
                    yield return new WaitForSeconds(WaitTime);

                    //tuşu kapat
                    DownBtn.transform.GetChild(0).GetComponent<Image>().sprite = BtnIdle[3];
                    break;
                default:
                    Debug.LogError("Default switch state");
                    break;
                
            }
        }

        // Start to play

        //COUNTDOWN
        IndicatorTxt.text = "SENIN SIRAN";
        play = true;
        Debug.Log("Play");
    }
    
}
