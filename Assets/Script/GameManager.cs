using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private TextMeshProUGUI _textPlay;
    [SerializeField] private TextMeshPro _textAIScore;
    [SerializeField] private TextMeshPro _textPlayerScore;
    [SerializeField] private GameObject _winLosePopup;
    [SerializeField] private Button _oneMoreButton;
    [SerializeField] private Button _menuButton;

    [SerializeField] private RacketController playerRacket;

    public bool isGameOver = false;
    private bool isPaused = false;


    private void Awake()
    {
        Instance = this;
        _pausePanel.SetActive(false);
    }
    private void Start()
    {
        _oneMoreButton.onClick.AddListener(RestartGame);
        _menuButton.onClick.AddListener(() =>
        {
            // Ana menü yok şimdilik boş
            Debug.Log("Ana Menü Butonu Tıklandı");
        });

        _winLosePopup.SetActive(false);
    }
    public void TogglePause()
    {
        if (!isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }
    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        _pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
    }

    private void RestartGame()
    {
        isGameOver = false;
        _winLosePopup.SetActive(false);

        AIScore = 0;
        PlayerScore = 0;

        playerRacket.ResetPosition();

        BallController.Instance.EnablePhysics();
        BallController.Instance.OnStart();
    }


    public int AIScore
    {
        get => _aiScore;
        set
        {
            _textAIScore.text = value.ToString();
            _aiScore = value;
            CheckForWinner();
        }
    }
    private int _aiScore = 0;

    public int PlayerScore
    {
        get => _playerScore;
        set
        {
            _textPlayerScore.text = value.ToString();
            _playerScore = value;
            CheckForWinner();
        }
    }
    private void CheckForWinner()
    {
        if (_playerScore >= 9)
        {
            OnGameOver(true);
        }
        else if (_aiScore >= 9)
        {
            OnGameOver(false);
        }
    }
    public void OnGameOver(bool playerWon)
    {

        _textPlay.enabled = true;
        _textPlay.text = playerWon ? "You Won!" : "You Lose :(";

        isGameOver = true;
        BallController.Instance.StopBall();

        _winLosePopup.SetActive(true);
    }

    private int _playerScore = 0;

    public void OnClick_StartButton()//menu olmadığ için boyle menu gelince değişecek
    {

        Destroy(canvas.gameObject);
        BallController.Instance.OnStart();
    }
}
