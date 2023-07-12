using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour {

    public GameObject LDeadIMG;
    public GameObject RDeadIMG;

    public ExecutorController PlayerLeft; //����|�אּScript_Controller
    public PrisonerController PlayerRight; //����|�אּScript_Controller
    public Collider2D colliderplayerLeft;
    public Collider2D colliderplayerRight;
    public CameraController CameraController;
    public GameObject LeftBlood;
    public GameObject RightBlood;
    public GameObject LeftDeathBlood;
    public GameObject RightDeathBlood;


    //Contents
    public GameObject ContentsUI;
    //bool isContents = true;

    //Function: Timer
    private float WorldTime = 120f;
    private float HightlightTime = 1f;
    public Text Times;
    Coroutine TimerCoroutine;


    //Function: Health
    /*
    public Text LHealth;
    public Text RHealth;
    */

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


    //Health
    public GameObject LHealth1;
    public GameObject LHealth2;
    public GameObject LHealth3;

    public GameObject RHealth1;
    public GameObject RHealth2;
    public GameObject RHealth3;

    //Bullet
    public RightBulletSpawner RBS;
    public LeftBulletSpawner LBS;

    //CG
    public GameObject OpeningCG1;
    public GameObject OpeningCG2;
    public GameObject OpeningCG3;
    public GameObject OpeningCG4;


    //Music
    public Music Script_Music;


    // Start is called before the first frame update
    void Start() {

        //�e�m�@�~
        Steps = Duration / Interval;
        StepDistance = TargetDistance / Steps;

        Script_Music.Music_Contents();
        colliderplayerLeft = PlayerLeft.GetComponent<Collider2D>();
        colliderplayerRight = PlayerRight.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update() {
        /*
        if(Input.GetKeyDown(KeyCode.Q)) {
            OpenCloth();
        } else if(Input.GetKeyDown(KeyCode.W)) {
            CloseCloth();
        } else if(Input.GetKeyDown(KeyCode.E)) {
            PlayVideo(true);
        } else if(Input.GetKeyDown(KeyCode.R)) {
            PlayVideo(false);
        } else if(Input.GetKeyDown(KeyCode.T)) {
            GameStart();
        }
        */

    }

    void GameStart() {



        Time.timeScale = 1f;
        WorldTime = 120f;


        //PlayerLeft.OnBonusStart();
        //PlayerRight.OnGameStart();
        UpdateHealth();
        //�l�u��s
        RBS.ResetBullet();
        LBS.ResetBullet();



        //���}Cloth
        OpenCloth();


        PlayerLeft.OnGameStart();
        PlayerRight.OnGameStart();
        UpdateHealth();

        //�}�l�p��
        TimerCoroutine = StartCoroutine(StartCountdown());

        //�����H���I�O
        StartCoroutine(WaitHidePlayerBoard());

    }

    //�C�����s�}�l
    void GameInitiate() {

        //�C���}�l
        //Time.timeScale = 1f;
        CameraController.InitCamera();
        executorController.OnRoundStart();
        prisonerController.OnRoundStart();
        BulletsWipe();


    }


    //When Hit Somebody
    public void HitPlayer(bool isHitRight) {


        Script_Music.Music_Hit();

        //��qUI��s
        UpdateHealth();



        //Check�C�������F��
        //�O�GCall GameOver(int Winner)
        if(PlayerLeft.Health == 0) {
            StartCoroutine(GameOver(1)); //winner�Gright
            return;
        } else if(PlayerRight.Health == 0) {
            StartCoroutine(GameOver(0)); //winner�Gleft
            return;
        }
        //���O�GCall GameTurnRound()
        if(isHitRight) {
            GameTurnRound(true);
        } else {
            GameTurnRound(false);

        }
    }


    //���
    void GameTurnRound(bool isHitRight) {
        StartCoroutine(PlayShortVideo(isHitRight));
        //Woose Added: Call round start method



    }

    IEnumerator GameOver(int Winner) {
        Time.timeScale = 0.3875f;


        if(Winner == 0) {
            CameraController.ZoomCamera(true);
            RightBlood.SetActive(true);
            RightBlood.GetComponent<Animator>().Play("newbloodRight");
        } else {

            CameraController.ZoomCamera(false);
            LeftBlood.SetActive(true);
            LeftBlood.GetComponent<Animator>().Play("newbloodLeft");
        }
        yield return new WaitForSecondsRealtime(HightlightTime);
        CameraController.InitCamera();
        RightBlood.SetActive(false);
        LeftBlood.SetActive(false);
        //�C���Ȱ�
        Time.timeScale = 0f;
        CloseCloth();
        BulletsWipe();

        yield return new WaitForSecondsRealtime(Duration + 0.2f);
        WorldTime = 120f;
        Times.text = "120";
        StopCoroutine(TimerCoroutine);
        TimerCoroutine = null;


        if(Winner == 0) {
            LeftWinnerCG.SetActive(true);
            Script_Music.UnMusic_Fighting();
            Script_Music.Music_LWinner();
        } else if(Winner == 1) {
            RightWinnerCG.SetActive(true);
            Script_Music.UnMusic_Fighting();
            Script_Music.Music_RWinner();
        }
        OpenCloth();

        yield return new WaitForSecondsRealtime(5f);
        LPlayerBoard.SetActive(true);
        RPlayerBoard.SetActive(true);
        CloseCloth();

        //��l�Ʀ�m�l�u
        GameInitiate();
        yield return new WaitForSecondsRealtime(Duration + 0.2f);

        RightWinnerCG.SetActive(false);
        LeftWinnerCG.SetActive(false);

        BackContents();

        Script_Music.UnMusic_LWinner();
        Script_Music.UnMusic_RWinner();

        Script_Music.Music_Contents();



    }


    //Woose Added Delete Bullet
    public void BulletsWipe() {

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

        StartCoroutine(DelayScream());

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
        LHealth1.SetActive(false);
        LHealth2.SetActive(false);
        LHealth3.SetActive(false);
        switch(PlayerLeft.Health) {
        case 0:
        break;
        case 1:
        LHealth1.SetActive(true);
        break;
        case 2:
        LHealth1.SetActive(true);
        LHealth2.SetActive(true);
        break;
        case 3:
        LHealth1.SetActive(true);
        LHealth2.SetActive(true);
        LHealth3.SetActive(true);
        break;
        }


        RHealth1.SetActive(false);
        RHealth2.SetActive(false);
        RHealth3.SetActive(false);
        switch(PlayerRight.Health) {
        case 0:
        break;
        case 1:
        RHealth1.SetActive(true);
        break;
        case 2:
        RHealth1.SetActive(true);
        RHealth2.SetActive(true);
        break;
        case 3:
        RHealth1.SetActive(true);
        RHealth2.SetActive(true);
        RHealth3.SetActive(true);
        break;
        }

    }


    IEnumerator OpeningCloth() {
        if(isClose == false) //�w���ˬd
        {
            yield break;
        }
        Script_Music.Music_Cloth();
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

    IEnumerator ClosingCloth() {
        if(isClose == true) //�w���ˬd
        {
            yield break;
        }

        Script_Music.Music_Cloth();
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
            yield return new WaitForSecondsRealtime(1f);
            WorldTime--;
            Times.text = WorldTime.ToString();
        }

        Debug.Log("Time out");
    }

    IEnumerator PlayShortVideo(bool isHitRight) {



        colliderplayerRight.enabled = false;
        colliderplayerLeft.enabled = false;

        //Debug.Log("PLAY video");


        //�C���Ȱ�
        Time.timeScale = 0f;
        //yield return new WaitForSecondsRealtime(1.3f);
        if(isHitRight) {
            PlayVideo(false);
        } else {
            PlayVideo(true);

        }

        yield return new WaitForSecondsRealtime(2f);
        //Debug.Log("stop video");


        /*
        if (isHitRight)
        {
            RDeadIMG.SetActive(true);
        }
        else
        {
            LDeadIMG.SetActive(true);

        }
        */



        //������
        CloseCloth();
        Debug.Log("CCCCCCCC");



        //��l�Ʀ�m�l�u
        GameInitiate();
        StopVideo();
        yield return new WaitForSecondsRealtime(Duration + 0.2f);

        //RDeadIMG.SetActive(false);
        //LDeadIMG.SetActive(false);

        OpenCloth();



        yield return new WaitForSecondsRealtime(0.9f);




        //�C���}�l
        Time.timeScale = 1f;


        colliderplayerRight.enabled = true;
        colliderplayerLeft.enabled = true;

        //�l�u��s
        RBS.ResetBullet();
        LBS.ResetBullet();
    }


    IEnumerator WaitHidePlayerBoard() {

        yield return new WaitForSecondsRealtime(1f);
        LPlayerBoard.SetActive(false);
        RPlayerBoard.SetActive(false);
    }

    /*
    IEnumerator WaitShowPlayerBoard()
    {
        yield return new WaitForSecondsRealtime(1f);
        LPlayerBoard.SetActive(true);
        RPlayerBoard.SetActive(true);
    }
    */
    /*
    IEnumerator GameOverCG(int Winner)
    {
        CloseCloth();
        yield return new WaitForSecondsRealtime(Duration + 0.2f);
        LeftWinnerCG.SetActive(false);
        RightWinnerCG.SetActive(false);

        BackContents();

        
    }
    */

    void BackContents() {
        ContentsUI.SetActive(true);

    }

    IEnumerator PlayOpeningAnimation() {
        Time.timeScale = 0f;
        OpenCloth();

        ContentsUI.SetActive(false);

        //CG
        OpeningCG1.SetActive(true);
        OpeningCG2.SetActive(false);
        OpeningCG3.SetActive(false);
        OpeningCG4.SetActive(false);
        yield return new WaitForSecondsRealtime(0.8f);

        OpeningCG1.SetActive(false);
        OpeningCG2.SetActive(true);
        OpeningCG3.SetActive(false);
        OpeningCG4.SetActive(false);
        yield return new WaitForSecondsRealtime(0.8f);

        OpeningCG1.SetActive(false);
        OpeningCG2.SetActive(false);
        OpeningCG3.SetActive(true);
        OpeningCG4.SetActive(false);
        yield return new WaitForSecondsRealtime(0.8f);

        OpeningCG1.SetActive(false);
        OpeningCG2.SetActive(false);
        OpeningCG3.SetActive(false);
        OpeningCG4.SetActive(true);
        yield return new WaitForSecondsRealtime(0.8f);

        OpeningCG1.SetActive(false);
        OpeningCG2.SetActive(false);
        OpeningCG3.SetActive(false);
        OpeningCG4.SetActive(false);


        GameStart();
    }

    public void StartPlay() {
        Script_Music.UnMusic_Contents();
        Script_Music.Music_Fighting();
        StartCoroutine(PlayOpeningAnimation());
    }

    IEnumerator DelayScream() {

        yield return new WaitForSecondsRealtime(0.5f);
        Script_Music.Music_Scream();
    }

    IEnumerator CloseAndOpenCloth() {
        CloseCloth();
        yield return new WaitForSecondsRealtime(Duration + 0.2f);
        OpenCloth();
    }

    public void CloseAndOpen() {
        StartCoroutine(CloseAndOpenCloth());
    }

}
