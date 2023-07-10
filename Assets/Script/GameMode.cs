using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public int PlayerLeftHealth;
    public int PlayerRightHealth;
    public GameObject PlayerLeft; //之後會改為Script_Controller
    public GameObject PlayerRight; //之後會改為Script_Controller

    public GameObject LeftCloth;
    public GameObject RightCloth;
    /*
    private float TargetDistance = 500f;
    private float StepDistance = 1000f;
    private float CurrentDistance = 0f;
    */
    float TargetDistance = 20f;
    float Duration = 1f;  // 總共移動的時間
    float Interval = 0.02f;  // 每次移動的間隔時間
    float Steps;  // 需要移動的次數
    float StepDistance;  // 每次移動的距離

    // Start is called before the first frame update
    void Start()
    {
        Steps = Duration / Interval;
        StepDistance = TargetDistance / Steps;
        OpenCloth();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            OpenCloth();
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            CloseCloth();
        }
    }

    void GameStart()
    {

    }

    void GameInitiate()
    {

    }


    //When Hit Somebody
    void HitPlayer()
    {

    }

    void GameTurnRound()
    {

    }

    void GameOver(int Winner)
    {

    }

    void OpenCloth()
    {
        
        StartCoroutine(OpeningCloth());
    }

    void CloseCloth()
    {
        StartCoroutine(ClosingCloth());
    }

    /*
    IEnumerator OpeningCloth()
    {
        float CurrentDistance = 0;

        while (CurrentDistance < TargetDistance)
        {
            LeftCloth.transform.position -= new Vector3(StepDistance, 0, 0) * Time.deltaTime;
            RightCloth.transform.position += new Vector3(StepDistance, 0, 0) * Time.deltaTime;
            CurrentDistance += StepDistance * Time.deltaTime;
            Debug.Log("Step");
            yield return new WaitForSeconds(0.05f);
        }

    }
    */
    IEnumerator OpeningCloth()
    {
        float CurrentDistance = 0;

        while (CurrentDistance < TargetDistance)
        {
            LeftCloth.transform.position -= new Vector3(StepDistance, 0, 0);
            RightCloth.transform.position += new Vector3(StepDistance, 0, 0);
            CurrentDistance += StepDistance;
            //Debug.Log("Step");
            yield return new WaitForSeconds(Interval);
        }
    }

    IEnumerator ClosingCloth()
    {
        float CurrentDistance = 0;

        while (CurrentDistance < TargetDistance)
        {
            LeftCloth.transform.position += new Vector3(StepDistance, 0, 0);
            RightCloth.transform.position -= new Vector3(StepDistance, 0, 0);
            CurrentDistance += StepDistance;
            //Debug.Log("Close");
            yield return new WaitForSeconds(Interval);
        }
    }
}
