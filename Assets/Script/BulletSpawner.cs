using UnityEngine;

public class BulletSpawner : MonoBehaviour {
    public GameObject RightBullet;
    public GameObject LeftBullet;
    public bool isBonusTime;
    public PrisonerCooldown prisonerCooldown;
    public ExecutorCooldown executorCooldown;
    private int prisonerMagazine;
    private int executorMagazine;
    // Start is called before the first frame update
    void Start() {
        prisonerMagazine = 3;
        executorMagazine = 3;
    }

    // Update is called once per frame
    void Update() {
        Vector2 bullet_position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        //Reload 
        if(prisonerMagazine < 3 && !prisonerCooldown.IsCoolingDown) {
            prisonerMagazine = 3;
            Debug.Log("prisoner reloaded!");
        }
        if(executorMagazine < 3 && !executorCooldown.IsCoolingDown) {
            executorMagazine = 3;
            Debug.Log("executor reloaded!");
        }

        if(Input.GetKeyUp(KeyCode.P) && gameObject.name == "Prisoner" && prisonerMagazine > 0) {
            PrisonerAttack(bullet_position);
            prisonerMagazine--;
            Debug.Log("prisoner Fire!");

        } else if(Input.GetKeyUp(KeyCode.A) && gameObject.name == "Executor" && executorMagazine > 0) {
            ExecutorAttack(bullet_position);
            executorMagazine--;
            Debug.Log("executor Fire!");
        }

    }
    private void PrisonerAttack(Vector2 bullet_position) {
        GameObject rightbullet = Instantiate(RightBullet);
        rightbullet.transform.position = bullet_position;
        if(prisonerCooldown.IsCoolingDown) { return; }// if already in cooldown then skip
        prisonerCooldown.StartCooldown();
    }
    private void ExecutorAttack(Vector2 bullet_position) {
        GameObject leftbullet = Instantiate(LeftBullet);
        leftbullet.transform.position = bullet_position;
        if(executorCooldown.IsCoolingDown) { return; }
        executorCooldown.StartCooldown();
    }

    public void OnRoundStart() {
        prisonerMagazine = 3;
        executorMagazine = 3;
    }
}
