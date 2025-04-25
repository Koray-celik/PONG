using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI _textPlay;
    [SerializeField] private TextMeshProUGUI _textTryAgain;
    [SerializeField] private TextMeshPro _textScore;

    public int Score

    {
        get => _score;
        set
        {
            _textScore.text = value.ToString();
            _score = value;
        }
    }
    private int _score;


    private void Awake()
    {
        Instance = this;
    }
    public void OnClick_StartButton()
    {

        Score = 0;
        canvas.enabled = false;
        BallController.Instance.OnStart();
    }

    public void OnGameOver()
    {

        canvas.enabled = true;
        _textPlay.enabled = false;
        _textTryAgain.enabled = true;
    }

}
