using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour {

    public ExecutorController PlayerLeft; //����|�אּScript_Controller
    public PrisonerController PlayerRight; //����|�אּScript_Controller

    //Contents
    public GameObject ContentsUI;
    //bool isContents = true;

    //Function: Timer
    private float WorldTime = 120f;
    public Text Times;

    //Function: Health
    public Text LHealth;
    public Text RHealth;


    //Function: Cloth
    public GameObject LeftCloth;
    public GameObject RightCloth;
    float TargetDistance = 20f;
    float Duration = 1f;  // �`�@���ʪ��ɶ�
    float Interval = 0.02f;  // �C�����ʪ����j�ɶ�
    float Steps;  // �ݭn���ʪ�����
    float StepDistance;  // �C�����ʪ��Z��
    bool isClose = true;
    public GameObject LPlayerBoard;
    public GameObject RPlayerBoard;

    


    //Function: Video
    public GameObject Camera_Video;
    public GameObject RightVideo;
    public GameObject LeftVideo;

    //Woose Added: Call round start method
    public ExecutorController executorController;
    public PrisonerController prisonerController;


    //GameOver
    public GameObject LeftWinnerCG;
    public GameObject RightWinnerCG;


    // Start is called before the first frame update
    void Start() {

        //�e�m�@�~
        Steps = Duration / Interval;
        StepDistance = TargetDistance / Steps;

        GameStart();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Q)) {
            OpenCloth();
        } else if(Input.GetKeyDown(KeyCode.W)) {
            CloseCloth();
        } else if(Input.GetKeyDown(KeyCode.E)) {
            PlayVideo(true);
        } else if(Input.GetKeyDown(KeyCode.R)) {
            PlayVideo(false);
        } else if(Input.GetKeyDown(KeyCode.T)) {
            StopVideo();
        }

    }

    void GameStart() 
    {
        ContentsUI.SetActive(false);
        
        //���}Cloth
        OpenCloth();

        

        //�}�l�p��
        StartCoroutine(StartCountdown());

        //�����H���I�O
        StartCoroutine(WaitHidePlayerBoard());

    }

    //�C�����s�}�l
    void GameInitiate() 
    {

        //�C���}�l
        //Time.timeScale = 1f;

        executorController.OnRoundStart();
        prisonerController.OnRoundStart();
        BulletsWipe();


    }


    //When Hit Somebody
    public void HitPlayer(bool isHitRight) {

        //��qUI��s
        UpdateHealth();

        //��l�Ʀ�m�l�u
        GameInitiate();

        //�C���Ȱ�
        Time.timeScale = 0f;

        //Check�C�������F��
        //�O�GCall GameOver(int Winner)
        if (PlayerLeft.Health==0)
        {
            GameOver(1); //winner�Gright
        }
        else if(PlayerRight.Health==0)
        {
            GameOver(0); //winner�Gleft
        }
        //���O�GCall GameTurnRound()
        if(isHitRight) 
        {
            GameTurnRound(true);
        } 
        else 
        {
            GameTurnRound(false);

        }
    }


    //���
    void GameTurnRound(bool isHitRight) 
    {
        StartCoroutine(PlayShortVideo(isHitRight));
        //Woose Added: Call round start method
        
    }

    void GameOver(int Winner) 
    {

    }
    //Woose Added Delete Bullet
    public void BulletsWipe() 
    {

        GameObject[] bullets;
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject bullet in bullets) Destroy(bullet); //desstroy bullet
    }
    void OpenCloth() {

        StartCoroutine(OpeningCloth());
    }

    void CloseCloth() {
        StartCoroutine(ClosingCloth());
    }

    void PlayVideo(bool isRightWin) {
        Camera_Video.SetActive(true);

        if(isRightWin) {
            LeftVideo.SetActive(false);
            RightVideo.SetActive(true);
        } else {
            LeftVideo.SetActive(true);
            RightVideo.SetActive(false);
        }

    }

    void StopVideo() {
        Camera_Video.SetActive(false);
        RightVideo.SetActive(false);
        LeftVideo.SetActive(false);
    }

    //�������ؼЮɩI�s
    void UpdateHealth() {
        LHealth.text = PlayerLeft.Health.ToString();
        RHealth.text = PlayerRight.Health.ToString();
    }


    IEnumerator OpeningCloth() 
    {
        if(isClose == false) //�w���ˬd
        {
            yield break;
        }

        isClose = false;
        float CurrentDistance = 0;

        while(CurrentDistance < TargetDistance) {
            LeftCloth.transform.position -= new Vector3(StepDistance, 0, 0);
            RightCloth.transform.position += new Vector3(StepDistance, 0, 0);
            CurrentDistance += StepDistance;
            //Debug.Log("Step");
            yield return new WaitForSecondsRealtime(Interval);
        }

        
        
    }

    IEnumerator ClosingCloth() 
    {
        if (isClose == true) //�w���ˬd
        {
            yield break;
        }


        isClose = true;
        float CurrentDistance = 0;

        while(CurrentDistance < TargetDistance) {
            LeftCloth.transform.position += new Vector3(StepDistance, 0, 0);
            RightCloth.transform.position -= new Vector3(StepDistance, 0, 0);
            CurrentDistance += StepDistance;
            //Debug.Log("Close");
            yield return new WaitForSecondsRealtime(Interval);
        }
        
        //isSuccessClose = true;
    }

    IEnumerator StartCountdown() {
        while(WorldTime > 0) {
            yield return new WaitForSeconds(1);
            WorldTime--;
            Times.text = WorldTime.ToString();
        }

        Debug.Log("Time out");
    }

    IEnumerator PlayShortVideo(bool isHitRight) 
    {
        //Debug.Log("PLAY video");

        if(isHitRight) 
        {
            PlayVideo(false);
        } else {
            PlayVideo(true);

        }

        yield return new WaitForSecondsRealtime(1.3f);
        //Debug.Log("stop video");
        StopVideo();

        //������
        CloseCloth();
        yield return new WaitForSecondsRealtime(Duration + 0.2f);
        OpenCloth();



        yield return new WaitForSecondsRealtime(0.9f);

        //�C���}�l
        Time.timeScale = 1f;
    }


    IEnumerator WaitHidePlayerBoard()
    {
        
        yield return new WaitForSecondsRealtime(1f);
        LPlayerBoard.SetActive(false);
        RPlayerBoard.SetActive(false);
    }

    IEnumerator WaitShowPlayerBoard()
    {
        yield return new WaitForSecondsRealtime(1f);
        LPlayerBoard.SetActive(true);
        RPlayerBoard.SetActive(true);
    }

    IEnumerator GameOverCG(int Winner)
    {
        if(Winner == 0)
        {
            LeftWinnerCG.SetActive(true);
        }
        else if(Winner ==1)
        {
            RightWinnerCG.SetActive(true);
        }


        CloseCloth();
        yield return new WaitForSecondsRealtime(Duration + 0.2f);
        LeftWinnerCG.SetActive(false);
        RightWinnerCG.SetActive(false);

        BackContents();

        
    }

    void BackContents()
    {
        ContentsUI.SetActive(true);
        
    }
}
