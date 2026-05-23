using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float spawnRate = 3f;
    private float nextSpawn = 0f;

    public PlayerController player; 
    void Update() {
        float currentSpawnRate = Mathf.Max(0.5f, spawnRate - (player.coins * 0.1f));

        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + currentSpawnRate;
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}