using UnityEngine;

public class RightBulletSpawner : MonoBehaviour {
    public GameObject RightBullet;
    public PrisonerCooldown prisonerCooldown;
    public bool isBonus;
    private int prisonerMagazine;
    // Start is called before the first frame update
    void Start() {
        prisonerMagazine = 3;
    }

    // Update is called once per frame
    void Update() {
        Vector2 bullet_position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        //Reload 

        if(prisonerMagazine < 3 && !prisonerCooldown.IsCoolingDown) {
            prisonerMagazine++;
            Debug.Log("prisoner reloaded!" + "\n prisonerMagazine remaining = " + prisonerMagazine);
            prisonerCooldown.StartCooldown();
        }

        if(Input.GetKeyUp(KeyCode.P) && gameObject.name == "Prisoner" && prisonerMagazine > 0 && !isBonus) {
            if(prisonerMagazine == 3) prisonerCooldown.StartCooldown();
            PrisonerAttack(bullet_position);
            prisonerMagazine--;
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
    }
}
