using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public int PlayerLeftHealth;
    public int PlayerRightHealth;
    public GameObject PlayerLeft; //����|�אּScript_Controller
    public GameObject PlayerRight; //����|�אּScript_Controller

    public GameObject LeftCloth;
    public GameObject RightCloth;
    /*
    private float TargetDistance = 500f;
    private float StepDistance = 1000f;
    private float CurrentDistance = 0f;
    */
    float TargetDistance = 20f;
    float Duration = 1f;  // �`�@���ʪ��ɶ�
    float Interval = 0.02f;  // �C�����ʪ����j�ɶ�
    float Steps;  // �ݭn���ʪ�����
    float StepDistance;  // �C�����ʪ��Z��

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
