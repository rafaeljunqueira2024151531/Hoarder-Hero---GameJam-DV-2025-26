using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public TextMeshProUGUI tempoText;
    public TextMeshProUGUI moedasText;
    public float tempoRestante = 180f;

    void Update() {
        if (tempoRestante > 0) {
            tempoRestante -= Time.deltaTime;
            int minutos = Mathf.FloorToInt(tempoRestante / 60);
            int segundos = Mathf.FloorToInt(tempoRestante % 60);
            tempoText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AtualizarMoedasUI(int total) {
        moedasText.text = total.ToString();
    }
}