using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public float speed = 3f;
    public int damageValue = 5;
    private Transform player;

    void Start() {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update() {
        if (player == null) return;
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            if (col.relativeVelocity.y < -0.1f) {
                Destroy(gameObject);
            } else {
                col.gameObject.GetComponent<PlayerController>().TakeDamage(damageValue);
            }
        }
    }
}