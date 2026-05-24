using UnityEngine;

public class Coin : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {

            other.GetComponent<PlayerController>().GetCoin();

            EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();

            foreach (EnemyAI enemy in enemies)
            {
                enemy.IncreaseSpeed(0.5f);
            }

            Destroy(gameObject);
        }
    }
}