using UnityEngine;

public class LeftBulletController : MonoBehaviour {

    private float deathWall = 22;
    public float bulletspeed = 10.0f;
    public PrisonerController PrisonerController;
    public CameraController CameraController;
    void Start() {
        PrisonerController = GameObject.FindGameObjectWithTag("Prisoner").GetComponent<PrisonerController>();
        CameraController = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>();
    }

    void Update() {
        transform.Translate(bulletspeed * Time.deltaTime * Time.timeScale, 0, 0, Space.World);
        if(transform.position.x > deathWall) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Prisoner") {
            CameraController.ZoomCamera(true);
            Destroy(gameObject, 0.25f);
            Debug.Log("Hit Prisoner");
            PrisonerController.OnHit();
            PrisonerController.OnRoundEnd();
        }
    }
}
