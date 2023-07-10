using UnityEngine;

public class BonusEventDirector : MonoBehaviour {
    private bool isBonus;
    private float bonus_timetrigger;
    private GameObject[] bullets;
    public GameObject prisoner;
    public GameObject executor;
    public PrisonerController prisonerController;
    public ExecutorController executorController;
    private void Start() {
        isBonus = false;
        bonus_timetrigger = 0;
        prisonerController = prisoner.GetComponent<PrisonerController>();
        executorController = executor.GetComponent<ExecutorController>();
    }
    private void Update() {
        if(isBonus) {
            return;
        } else {
            bonus_timetrigger += Time.deltaTime;
            if(bonus_timetrigger > 1) {
                OnBonusStart();
            }
        }

    }
    public void OnBonusStart() {
        isBonus = true;
        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject bullet in bullets) Destroy(bullet); //desstroy bullet
        prisoner.transform.position = new Vector2(prisoner.transform.position.x, 0);
        executor.transform.position = new Vector2(executor.transform.position.x, 0);
        /*
        int bonus_dice = Random.Range(1, 3);
                switch(bonus_dice) {
        case 1:
        case 2:
        case 3:
        }
        */

    }
    // Update is called once per frame

}
