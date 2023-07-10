using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BonusEventDirector : MonoBehaviour {
    private int BonusCounter;
    private bool isBonus;
    private float bonus_timetrigger;
    public int buff_id;

    public GameObject prisoner;
    public GameObject executor;
    public PrisonerController prisonerController;
    public ExecutorController executorController;
    public RightBulletSpawner rightBulletSpawner;
    public LeftBulletSpawner leftBulletSpawner;
    public BonusRemainingTime bonusRemainingTime;
    public GameMode GM;
    public Text bonustext;
    public Text bonustexttimer;
    public float RemainingTime;

    private void Start() {
        isBonus = false;
        BonusCounter = 0;
        bonus_timetrigger = 0;
    }

    private void Update() {
        if(isBonus && !bonusRemainingTime.IsNotOver) {
            OnBonusEnd();
        }
        switch(BonusCounter) {
        case 0: {
            bonus_timetrigger += Time.deltaTime;
            if(bonus_timetrigger > 30) {
                OnBonusStart();
            }
            return;
        }

        case 1: {
            bonus_timetrigger += Time.deltaTime;
            if(bonus_timetrigger > 60) {
                OnBonusStart();
            }
            return;
        }
        case 2: {
            bonus_timetrigger += Time.deltaTime;
            if(bonus_timetrigger > 90) {
                OnBonusStart();
            }
            return;
        }
        }


    }

    public void OnBonusStart() {
        GM.BulletsWipe();

        bonustexttimer.gameObject.SetActive(true);
        bonustext.text = "5";
        RemainingTime = 5;

        rightBulletSpawner.isBonus = true;
        leftBulletSpawner.isBonus = true;
        isBonus = true;
        StartCoroutine(StartCountdown());
        BonusCounter++;

        bonusRemainingTime.StartTimer();
        prisonerController.OnBonusStart();
        executorController.OnBonusStart();

        buff_id = Random.Range(1, 6);


    }
    public void OnBonus() {
        if(!bonusRemainingTime.IsNotOver) { OnBonusEnd(); return; }
    }
    public void OnBonusEnd() {
        bonustexttimer.gameObject.SetActive(false);
        rightBulletSpawner.isBonus = false;
        leftBulletSpawner.isBonus = false;
        isBonus = false;
        Debug.Log(string.Format("Prisoner Point: {0} \n Executor Point: {1}", prisonerController.click_counter, executorController.click_counter));
        if(prisonerController.click_counter < executorController.click_counter) { // executor win the bonus
            executor.GetComponent<BonusBuffController>().enabled = true;
            executor.GetComponent<BonusBuffController>().OnBonusEnd();
            Debug.Log("Executor gets bonus!");
        } else if(prisonerController.click_counter > executorController.click_counter) { // prisoner win the bonus
            prisoner.GetComponent<BonusBuffController>().enabled = true;
            Debug.Log("Prisoner gets bonus!");
        } else {// equal -> no one win
            Debug.Log("Nobody get bonus :( !");
        }

        prisonerController.OnBonusEnd();
        executorController.OnBonusEnd();
    }
    public void OnRoundStart() {
        BonusCounter = 0;
        bonus_timetrigger = 0;
    }
    IEnumerator StartCountdown() {
        while(RemainingTime > 0) {
            yield return new WaitForSeconds(1);
            RemainingTime--;
            bonustext.text = RemainingTime.ToString();
        }

        Debug.Log("Time out");
    }

}
