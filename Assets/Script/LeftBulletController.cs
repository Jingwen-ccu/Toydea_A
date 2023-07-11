using UnityEngine;

public class LeftBulletController : MonoBehaviour {

    private float deathWall = 22;
    public float bulletspeed = 10.0f;
    public PrisonerController PrisonerController;
    void Start() {
        PrisonerController = GameObject.FindGameObjectWithTag("Prisoner").GetComponent<PrisonerController>();
    }

    void Update() {
        transform.Translate(bulletspeed * Time.deltaTime, 0, 0, Space.World);
        if(transform.position.x > deathWall) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Prisoner") {
            Destroy(gameObject);
            Debug.Log("Hit Prisoner");
            PrisonerController.OnHit();
            PrisonerController.OnRoundEnd();
        }
    }
}
