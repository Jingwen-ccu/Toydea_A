using UnityEngine;

public class RightBulletSpawner : MonoBehaviour {
    public GameObject RightBullet;
    public PrisonerCooldown prisonerCooldown;
    public bool isBonus;
    private int prisonerMagazine;

    public Music music;

    //Bullet

    public GameObject Weapon_R1;
    public GameObject Weapon_R2;
    public GameObject Weapon_R3;
    // Start is called before the first frame update
    void Start() {
        prisonerMagazine = 3;
        UpdateBulletUI();
    }

    // Update is called once per frame
    void Update() {
        Vector2 bullet_position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        //Reload 

        if(prisonerMagazine < 3 && !prisonerCooldown.IsCoolingDown) {
            prisonerMagazine++;
            UpdateBulletUI();
            Debug.Log("prisoner reloaded!" + "\n prisonerMagazine remaining = " + prisonerMagazine);
            prisonerCooldown.StartCooldown();
        }

        if(Input.GetKeyUp(KeyCode.P) && gameObject.name == "Prisoner" && prisonerMagazine > 0 && !isBonus) {
            if(prisonerMagazine == 3) prisonerCooldown.StartCooldown();
            PrisonerAttack(bullet_position);
            prisonerMagazine--;
            music.Music_Shoot();
            UpdateBulletUI();
            Debug.Log("prisoner Fire!" + "\n prisonerMagazine remaining = " + prisonerMagazine);
        }

    }
    private void PrisonerAttack(Vector2 bullet_position) {
        GameObject rightbullet = Instantiate(RightBullet);
        rightbullet.transform.position = bullet_position;
        if(prisonerCooldown.IsCoolingDown) { return; }// if already in cooldown then skip
    }

    public void OnRoundStart() {
        prisonerMagazine = 3;
        UpdateBulletUI();
    }


    void UpdateBulletUI()
    {

        Weapon_R1.SetActive(false);
        Weapon_R2.SetActive(false);
        Weapon_R3.SetActive(false);
        switch (prisonerMagazine)
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
        prisonerMagazine = 3;
        UpdateBulletUI();
    }
}
