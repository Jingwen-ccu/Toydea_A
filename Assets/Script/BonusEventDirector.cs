using UnityEngine;

public class BonusEventDirector : MonoBehaviour {
    private bool isBonus;
    private float bonus_timetrigger;
    private GameObject[] bullets;
    public GameObject prisoner;
    public GameObject executor;
    public PrisonerController prisonerController;
    public ExecutorController executorController;
    public BonusRemainingTime bonusRemainingTime;
    private void Start() {
        isBonus = false;
        bonus_timetrigger = 0;
        prisonerController = prisoner.GetComponent<PrisonerController>();
        executorController = executor.GetComponent<ExecutorController>();
    }
    private void Update() {
        if(isBonus) {
            OnBonus();
        } else {
            bonus_timetrigger += Time.deltaTime;
            if(bonus_timetrigger > 1) {
                OnBonusStart();
            }
        }

    }
    public void OnBonusStart() {
        isBonus = true;
        BulletsWipe();
        bonusRemainingTime.StartTimer();
        prisonerController.OnBonusStart();
        executorController.OnBonusStart();
        /*
        int bonus_dice = Random.Range(1, 3);
                switch(bonus_dice) {
        case 1:
        case 2:
        case 3:
        }
        */

    }
    public void OnBonus() {
        if(!bonusRemainingTime.IsNotOver) { OnBonusEnd(); return; }
    }
    public void OnBonusEnd() {
        isBonus = false;
        if(prisonerController.click_counter < executorController.click_counter) { // executor win the bonus
            Debug.Log("Executor gets bonus!");
        } else if(prisonerController.click_counter > executorController.click_counter) { // prisoner win the bonus
            Debug.Log("Prisoner gets bonus!");
        } else {// equal -> no one win
            Debug.Log("Nobody get bonus :( !");
        }
        prisonerController.OnBonusEnd();
        executorController.OnBonusEnd();
    }
    public void BulletsWipe() {

        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject bullet in bullets) Destroy(bullet); //desstroy bullet
    }
    // Update is called once per frame

}
