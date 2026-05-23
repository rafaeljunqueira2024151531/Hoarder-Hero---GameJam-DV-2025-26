using UnityEngine;
using System.Collections.Generic;

public class FixedSpawner : MonoBehaviour {
    public GameObject coinPrefab;
    public GameObject enemyPrefab;
    public LayerMask groundLayer;
    
    public int seed = 42; 
    public float startX = -10f;
    public float endX = 200f;
    public float stepX = 2.5f;
    public float spawnYOffset = 1f;

    void Start() {
        Random.InitState(seed);
        PopulateLevel();
    }

    void PopulateLevel() {
        for (float x = startX + 15f; x < endX; x += stepX) {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, 10f), Vector2.down, 20f, groundLayer);

            if (hit.collider != null) {
                float chance = Random.value;
                Vector3 spawnPos = new Vector3(x, hit.point.y + spawnYOffset, 0);

                if (chance < 0.3f) {
                    Instantiate(coinPrefab, spawnPos, Quaternion.identity);
                } 
                else if (chance > 0.85f) {
                    Instantiate(enemyPrefab, spawnPos + Vector3.up * 0.5f, Quaternion.identity);
                }
            }
        }
    }
}