using UnityEngine;

public class PrisonerController : MonoBehaviour {
    private bool is_round_start = true;
    private float final_velocity;
    private float velocity = 200;
    private float start_velocity;
    private Rigidbody2D rb;
    private Vector3 lastVelocity;
    private float acceration;
    private float decend;
    private float multiplier = 2;
    private float Health = 3;
    void Start() {
        acceration = multiplier * 2;
        decend = multiplier;
        start_velocity = velocity / 100 * multiplier;
        final_velocity = start_velocity * (multiplier * 2);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0, start_velocity, 0);
        Health = 3;
    }

    void Update() {
        if(Input.GetKey(KeyCode.P)) {
            if(rb.velocity.y <= final_velocity && rb.velocity.y > 0) {
                rb.AddForce(new Vector2(0, velocity * acceration * Time.deltaTime));
            } else if(rb.velocity.y >= final_velocity * -1 && rb.velocity.y <= 0) {
                rb.AddForce(new Vector2(0, -1 * velocity * acceration * Time.deltaTime));
            }
        } else {
            if(rb.velocity.y > start_velocity) {
                rb.AddForce(new Vector2(0, -1 * decend));
            } else if(rb.velocity.y < start_velocity * -1) {
                rb.AddForce(new Vector2(0, decend));
            }
        }




        lastVelocity = rb.velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        //Reflect if Wall has been touched
        float speed = lastVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = direction * speed;
    }
    public void OnHit() {
        Health--;
        Debug.Log(Health);
        if(Health == 0) {
            Destroy(gameObject);
            OnGameStart();
        }
    }
    public void OnGameStart() {

    }
    public void OnGameEnd() {

    }
    public void OnRoundStart() {

    }

    public void OnRoundEnd() {
        //Curtain close
    }
}
