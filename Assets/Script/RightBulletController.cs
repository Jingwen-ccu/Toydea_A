using UnityEngine;

public class RightBulletController : MonoBehaviour {

    private float deathWall = -22;

    public ExecutorController ExecutorController;
    void Start() {
        ExecutorController = GameObject.FindGameObjectWithTag("Executor").GetComponent<ExecutorController>();
    }

    void Update() {
        transform.Translate(-10f * Time.deltaTime, 0, 0);
        if(transform.position.x < deathWall) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Executor") {
            Destroy(gameObject);
            //Debug.Log("Hit Prisoner");
            ExecutorController.OnHit();
            ExecutorController.OnRoundEnd();
        }

    }
}
