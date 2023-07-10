using UnityEngine;

public class BulletSpawner : MonoBehaviour {
    public GameObject RightBullet;
    public GameObject LeftBullet;
    public bool isBonusTime;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update() {
        Vector2 bullet_position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        if(Input.GetKeyUp(KeyCode.P) && gameObject.name == "Prisoner") {
            GameObject rightbullet = Instantiate(RightBullet);
            rightbullet.transform.position = bullet_position;
        } else if(Input.GetKeyUp(KeyCode.A) && gameObject.name == "Executor") {
            GameObject leftbullet = Instantiate(LeftBullet);
            leftbullet.transform.position = bullet_position;
        }

    }
}
