using UnityEngine;

public class LeftBulletSpawner : MonoBehaviour {
    public GameObject LeftBullet;
    public ExecutorCooldown executorCooldown;

    public bool isBonus;
    private int executorMagazine;

    //Music
    public Music music;

    //Bullet

    public GameObject Weapon_R1;
    public GameObject Weapon_R2;
    public GameObject Weapon_R3;
    void Start() {
        executorMagazine = 3;
        UpdateBulletUI();
    }

    void Update() {
        Vector2 bullet_position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        //Reload 

        if(executorMagazine < 3 && !executorCooldown.IsCoolingDown) {
            executorMagazine++;
            UpdateBulletUI();
            Debug.Log("executor reloaded!" + "\n executorMagazine remaining = " + executorMagazine);
            executorCooldown.StartCooldown();
        }

        if(Input.GetKeyUp(KeyCode.A) && gameObject.name == "Executor" && executorMagazine > 0 && !isBonus) {
            if(executorMagazine == 3) executorCooldown.StartCooldown();
            ExecutorAttack(bullet_position);
            executorMagazine--;
            music.Music_Shoot();
            UpdateBulletUI();
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
        UpdateBulletUI();
    }

    void UpdateBulletUI()
    {

        Weapon_R1.SetActive(false);
        Weapon_R2.SetActive(false);
        Weapon_R3.SetActive(false);
        switch (executorMagazine)
        {
            case 0:
                break;
            case 1:
                Weapon_R1.SetActive(true);
                break;
            case 2:
                Weapon_R1.SetActive(true);
                Weapon_R2.SetActive(true);
                break;
            case 3:
                Weapon_R1.SetActive(true);
                Weapon_R2.SetActive(true);
                Weapon_R3.SetActive(true);
                break;
        }

    }
    public void ResetBullet()
    {
        executorMagazine = 3;
        UpdateBulletUI();
    }
}
