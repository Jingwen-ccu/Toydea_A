using UnityEngine;

public class LeftBulletSpawner : MonoBehaviour {
    public GameObject LeftBullet;
    public ExecutorCooldown executorCooldown;

    public bool isBonus;
    private int executorMagazine;
    void Start() {
        executorMagazine = 3;
    }

    void Update() {
        Vector2 bullet_position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        //Reload 

        if(executorMagazine < 3 && !executorCooldown.IsCoolingDown) {
            executorMagazine++;
            Debug.Log("executor reloaded!" + "\n executorMagazine remaining = " + executorMagazine);
            executorCooldown.StartCooldown();
        }

        if(Input.GetKeyUp(KeyCode.A) && gameObject.name == "Executor" && executorMagazine > 0 && !isBonus) {
            if(executorMagazine == 3) executorCooldown.StartCooldown();
            ExecutorAttack(bullet_position);
            executorMagazine--;
            Debug.Log("executor Fire!" + "\n executorMagazine remaining = " + executorMagazine);
        }

    }

    private void ExecutorAttack(Vector2 bullet_position) {
        GameObject leftbullet = Instantiate(LeftBullet);
        leftbullet.transform.position = bullet_position;
        if(executorCooldown.IsCoolingDown) { return; }
    }

    public void OnRoundStart() {
        executorMagazine = 3;
    }
}
