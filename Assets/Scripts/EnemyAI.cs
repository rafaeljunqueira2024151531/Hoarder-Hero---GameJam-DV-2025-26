using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public float speed = 3f;
    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update() {
        if (player == null || UIManager.instance.jogoAcabou) {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            if(anim != null) anim.SetBool("isRunning", false);
            return;
        }

        float direction = Mathf.Sign(player.position.x - transform.position.x);
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        sr.flipX = direction <= 0;
        if(anim != null) anim.SetBool("isRunning", true);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            PlayerController pc = col.gameObject.GetComponent<PlayerController>();
            if (col.relativeVelocity.y < -0.1f) {
                pc.TocarSomKill();
                Destroy(gameObject);
            } else {
                pc.TakeDamage(5);
            }
        }
    }
}