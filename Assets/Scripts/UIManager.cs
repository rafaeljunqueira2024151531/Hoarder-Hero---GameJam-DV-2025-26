using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public static UIManager instance;
    public TextMeshProUGUI tempoText;
    public TextMeshProUGUI moedasText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI gameOverTitleText;
    public GameObject victoryPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public float tempoRestante = 180f;
    public bool jogoAcabou = false;
    private bool estaPausado = false;

    void Awake() {
        instance = this;
    }

    void Update() {
        if (jogoAcabou) return;

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (estaPausado) Continuar();
            else Pausar();
        }

        if (estaPausado) return;

        if (tempoRestante > 0) {
            tempoRestante -= Time.deltaTime;
            int minutos = Mathf.FloorToInt(tempoRestante / 60);
            int segundos = Mathf.FloorToInt(tempoRestante % 60);
            tempoText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        } else {
            ShowGameOver("TEMPO ESGOTADO!");
        }
    }

    public void Pausar() {
        estaPausado = true;
        FindAnyObjectByType<PlayerController>().TocarSomPause();
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void Continuar() {
        estaPausado = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void AtualizarMoedasUI(int total) {
        moedasText.text = total.ToString();
    }

    public void ShowVictory(int score) {
        jogoAcabou = true;
        victoryPanel.SetActive(true);
        finalScoreText.text = "Score Final: " + score;
    }

    public void ShowGameOver(string mensagem) {
        jogoAcabou = true;
        gameOverPanel.SetActive(true);
        gameOverTitleText.text = mensagem;
        Time.timeScale = 0f;
    }

    public void Reiniciar() {
        Time.timeScale = 1f;
        jogoAcabou = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrParaMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Sair() {
        Application.Quit();
    }
}