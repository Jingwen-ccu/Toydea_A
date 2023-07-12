using UnityEngine;

public class PrisonerController : MonoBehaviour {
    public bool is_round_start = true;
    public bool is_Bonus = false;
    public float final_velocity = 32;
    public float start_velocity = 2;
    private Rigidbody2D rb;
    private Vector3 lastVelocity;
    public float acceration = 1200;
    public float multiplier = 4;
    public float Health = 3;
    public int click_counter;

    public Animator blood;
    public Animator death;
    //public BonusBuffController bonusBuffController;
    public GameMode GM;

    void Start() {
        is_Bonus = false;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0, start_velocity, 0);
        Health = 3;
    }

    void Update() {
        if(is_Bonus) {//Bonus time
            if(Input.GetKeyDown(KeyCode.P)) {
                click_counter++;
            }
        } else {
            PrisonerMovement();
            lastVelocity = rb.velocity;
        }

    }
    private void PrisonerMovement() {
        if(Input.GetKey(KeyCode.P)) {
            if(rb.velocity.y <= final_velocity && rb.velocity.y > 0) {
                rb.AddForce(new Vector2(0, acceration * Time.deltaTime));
            } else if(rb.velocity.y >= final_velocity * -1 && rb.velocity.y <= 0) {
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
        GM.HitPlayer(true);
        Debug.Log(Health);
        if(Health == 0) {
            //Destroy(gameObject);
            //OnGameStart();
        } else {
        }
    }
    public void InitPrisoner() {
        rb.velocity = new Vector3(0, start_velocity, 0);
        transform.position = new Vector2(transform.position.x, 0);

    }
    public void OnBonusStart() {
        rb.velocity = new Vector3(0, 0, 0);
        transform.position = new Vector2(transform.position.x, 0);
        click_counter = 0;
        is_Bonus = true;
    }
    public void OnBonusEnd() {
        rb.velocity = new Vector3(0, start_velocity, 0);
        is_Bonus = false;
    }
    public void OnGameStart() {
        Health = 3;
        is_Bonus = false;
        is_round_start = true;
        InitPrisoner();
    }
    public void OnGameEnd() {
        is_Bonus = false;
        is_round_start = false;
    }

    public void OnRoundStart() {
        is_Bonus = false;
        is_round_start = true;
        InitPrisoner();
    }
    public void OnRoundEnd() {
        //Curtain close
        is_Bonus = false;
        is_round_start = false;
    }



}
