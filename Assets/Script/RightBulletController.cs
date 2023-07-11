using UnityEngine;

public class RightBulletController : MonoBehaviour {

    private float deathWall = -22;
    public float bulletspeed = 10.0f;
    public ExecutorController ExecutorController;
    public CameraController CameraController;

    void Start() {
        ExecutorController = GameObject.FindGameObjectWithTag("Executor").GetComponent<ExecutorController>();
        CameraController = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>();

    }

    void Update() {
        transform.Translate(-1 * bulletspeed * Time.deltaTime * Time.timeScale, 0, 0, Space.World);
        if(transform.position.x < deathWall) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Executor") {
            CameraController.ZoomCamera(false);
            Destroy(gameObject, 0.25f);
            //Debug.Log("Hit Prisoner");
            ExecutorController.OnHit();
            ExecutorController.OnRoundEnd();
        }

    }
}
