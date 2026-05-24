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
        Time.timeScale = 1f;
        jogoAcabou = false;
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
            
            float tempoParaMostrar = Mathf.Max(0, tempoRestante);
            int minutos = Mathf.FloorToInt(tempoParaMostrar / 60);
            int segundos = Mathf.FloorToInt(tempoParaMostrar % 60);
            tempoText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        } else {
            tempoRestante = 0;
            tempoText.text = "00:00";
            ShowGameOver("TEMPO ESGOTADO!");
        }
    }

    public void Pausar() {
        if (jogoAcabou) return;
        estaPausado = true;
        PlayerController p = FindFirstObjectByType<PlayerController>();
        if (p != null) p.TocarSomPause();
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
        if (jogoAcabou) return;
        jogoAcabou = true;
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);
        finalScoreText.text = "Score Final: " + score;
    }

    public void ShowGameOver(string mensagem) {
        if (jogoAcabou) return;
        jogoAcabou = true;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        gameOverTitleText.text = mensagem;
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