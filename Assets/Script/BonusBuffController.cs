using UnityEngine;

public class BonusBuffController : MonoBehaviour {
    public bool is_round_start = true;
    public bool is_Bonus = false;
    private float final_velocity;
    private float velocity = 200;
    private float start_velocity;
    private Rigidbody2D rb;
    private Vector3 lastVelocity;
    private float acceration;
    private float decend;
    private float multiplier = 2;
    public float Health = 3;
    public int click_counter;
    private int suit_time = 5;
    private int bandana_time = 5;
    private int zawarudo_time = 3;
    private int blind_time = 5;
    private float time;

    public BonusEventDirector bonusEventDirector;
    public GameMode GM;

    void Start() {

        is_Bonus = false;
        acceration = multiplier * 2;
        decend = multiplier;
        start_velocity = velocity / 100 * multiplier;
        final_velocity = start_velocity * (multiplier * 2);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0, start_velocity, 0);
        Health = 3;
    }
    private void OnEnable() {
        InitPrisoner();
        time = 0;
    }
    void Update() {
        time += Time.deltaTime;
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
            Destroy(gameObject);
            OnGameStart();
        }
    }
    public void InitPrisoner() {
        rb.velocity = new Vector3(0, start_velocity, 0);
        transform.position = new Vector2(transform.position.x, 0);
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

    public void OnRoundEnd() {
        //Curtain close
        is_Bonus = false;
        is_round_start = false;
    }
}
