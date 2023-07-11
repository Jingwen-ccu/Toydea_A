using UnityEngine;

public class ExecutorController : MonoBehaviour {
    public bool is_round_start = true;
    public bool is_Bonus = false;
    public float final_velocity = 32;
    public float start_velocity = 2;
    public float acceration = 1200;
    private Rigidbody2D rb;
    private Vector3 lastVelocity;
    public float multiplier = 4;
    public float Health = 3;
    public int click_counter;


    public BonusBuffController bonusBuffController;
    //JingWem Add
    public GameMode GM;




    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0, start_velocity * -1, 0);
        Vector3 relativeVelocity = transform.InverseTransformPoint(rb.velocity);
        Health = 3;
    }

    void Update() {
        if(is_Bonus) { // Bonus time
            if(Input.GetKeyDown(KeyCode.A)) {
                click_counter++;
            }
        } else {
            ExecutorMovement();
            lastVelocity = rb.velocity;
        }

    }
    private void ExecutorMovement() {
        if(Input.GetKey(KeyCode.A)) {
            if(rb.velocity.y <= final_velocity && rb.velocity.y > 0) {
                rb.AddForce(new Vector2(0, acceration * Time.deltaTime));

            } else if(rb.velocity.y >= -1 * final_velocity && rb.velocity.y <= 0) {
                rb.AddForce(new Vector2(0, -1 * acceration * Time.deltaTime));
            }
        } else {
            if(rb.velocity.y > start_velocity) {
                rb.AddForce(new Vector2(0, -1 * acceration * Time.deltaTime));
            } else if(rb.velocity.y < start_velocity * -1) {
                rb.AddForce(new Vector2(0, acceration * Time.deltaTime));
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        //Reflect if Wall has been touched
        float speed = lastVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = direction * speed;
    }
    public void OnHit() {
        Health--;

        //JingWen Add
        GM.HitPlayer(false);

        //Debug.Log(Health);
        if(Health == 0) {
            Destroy(gameObject);
        }
    }
    public void InitExecutor() {
        rb.velocity = new Vector3(0, start_velocity * -1, 0);
        transform.position = new Vector2(transform.position.x, 0);
    }
    public void OnBonusStart() {
        transform.position = new Vector2(transform.position.x, 0);
        rb.velocity = new Vector3(0, 0, 0);
        click_counter = 0;
        is_Bonus = true;
    }
    public void OnBonusEnd() {
        rb.velocity = new Vector3(0, start_velocity * -1, 0);
        is_Bonus = false;
    }
    public void OnGameStart() {
        Health = 3;
        is_Bonus = false;
        is_round_start = true;
        InitExecutor();
    }
    public void OnGameEnd() {
        is_Bonus = false;
        is_round_start = false;
    }

    public void OnRoundStart() {
        is_Bonus = false;
        is_round_start = true;
        InitExecutor();
    }
    public void OnRoundEnd() {
        //Curtain close
        is_Bonus = false;
        is_round_start = false;
    }
}
