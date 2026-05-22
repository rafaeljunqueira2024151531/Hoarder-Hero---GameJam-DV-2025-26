using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float baseSpeed = 10f;
    public float baseJump = 12f;
    public int coins = 0;
    public float speedPenaltyPerCoin = 0.5f;
    public Transform ponto1;
    public Transform ponto2;
    public LayerMask groundLayer;
    public UIManager ui;
    
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool noChao;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        noChao = Physics2D.OverlapArea(ponto1.position, ponto2.position, groundLayer);

        float currentSpeed = Mathf.Max(2f, baseSpeed - (coins * speedPenaltyPerCoin));
        float currentJump = Mathf.Max(4f, baseJump - (coins * 0.2f));

        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * currentSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && noChao) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, currentJump);
        }

        if (move != 0) {
            sprite.flipX = move < 0;
        }

        anim.SetBool("isRunning", move != 0);
        anim.SetBool("isGrounded", noChao);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Q) && coins > 0) {
            coins--;
            ui.AtualizarMoedasUI(coins);
        }

        if (Input.GetKeyDown(KeyCode.F1)) {
            coins = 0;
            ui.AtualizarMoedasUI(coins);
        }
    }

    public void GetCoin() {
        coins++;
        ui.AtualizarMoedasUI(coins);
    }

    public void TakeDamage(int amount) {
        if (coins > 0) {
            coins = Mathf.Max(0, coins - amount);
            ui.AtualizarMoedasUI(coins);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}