using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float baseSpeed = 10f;
    public float baseJump = 12f;
    public int coins = 0;
    public float penalty = 0.4f;
    public Transform ponto1;
    public Transform ponto2;
    public LayerMask groundLayer;
    public AudioClip somMoeda;
    public AudioClip somKill;
    public AudioClip somWin;
    public AudioClip somPause;
    
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private AudioSource audioS;
    private bool noChao;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audioS = GetComponent<AudioSource>();
    }

    void Update() {
        if (UIManager.instance.jogoAcabou || Time.timeScale == 0) {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            if (anim != null) anim.SetBool("isRunning", false);
            return; 
        }

        noChao = Physics2D.OverlapArea(ponto1.position, ponto2.position, groundLayer);
        
        float currentSpeed = Mathf.Max(2f, baseSpeed - (coins * penalty));
        float currentJump = Mathf.Max(4f, baseJump - (coins * 0.2f));
        float move = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(move * currentSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && noChao) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, currentJump);
        }

        if (move != 0) sr.flipX = move < 0;

        if (anim != null) {
            anim.SetBool("isRunning", move != 0);
            anim.SetBool("isGrounded", noChao);
            anim.SetFloat("yVelocity", rb.linearVelocity.y);
        }
    }

    public void GetCoin() {
        coins++;
        UIManager.instance.AtualizarMoedasUI(coins);
        if (somMoeda) audioS.PlayOneShot(somMoeda);
    }

    public void TocarSomKill() { if (somKill) audioS.PlayOneShot(somKill); }
    public void TocarSomPause() { if (somPause) audioS.PlayOneShot(somPause); }

    public void Ganhar() {
        if (UIManager.instance.jogoAcabou) return;
        if (somWin) audioS.PlayOneShot(somWin);
        UIManager.instance.ShowVictory(coins);
    }

    public void TakeDamage(int amount) {
        if (UIManager.instance.jogoAcabou) return;
        if (coins > 0) {
            coins = Mathf.Max(0, coins - amount);
            UIManager.instance.AtualizarMoedasUI(coins);
        } else {
            UIManager.instance.ShowGameOver("MORRESTE!");
        }
    }
}