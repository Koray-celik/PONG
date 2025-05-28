using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController Instance { get; private set; }


    [SerializeField] private new Rigidbody2D rigidbody2D;

    [SerializeField] private float _startSpeed;
    [SerializeField] private float _freezeTime;
    private Coroutine startBallCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    public void OnStart()
    {

        if (GameManager.Instance.isGameOver)
            return;

        if (startBallCoroutine != null)
        {
            StopCoroutine(startBallCoroutine);
            startBallCoroutine = null;
        }
        startBallCoroutine = StartCoroutine(StartBallAfterDelay());

        rigidbody2D.linearVelocity = Vector2.down * _startSpeed;
        transform.position = Vector2.zero;
    }

    private IEnumerator StartBallAfterDelay()
    {

        rigidbody2D.simulated = false;
        rigidbody2D.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;

        yield return new WaitForSeconds(_freezeTime);

        rigidbody2D.simulated = true;
        rigidbody2D.linearVelocity = Vector2.down * _startSpeed;
    }

    private IEnumerator RestartBall()
    {
        rigidbody2D.linearVelocity = Vector2.zero;
        transform.position = Vector2.zero;

        yield return new WaitForSeconds(_freezeTime);

        rigidbody2D.linearVelocity = Vector2.down * _startSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        SoundManager.Instance.PlayRacketHit();

        if (GameManager.Instance.isGameOver)
            return;

        if (other.transform.CompareTag("Racket") || other.transform.CompareTag("aiRacket"))
        {

            var racket = other.transform.GetComponent<RacketController>();
            var directionVertical = racket._isUp ? -1 : 1;
            var directionHorizontal = (transform.position.x - racket.transform.position.x) / other.collider.bounds.extents.x;

            rigidbody2D.linearVelocity = new Vector2(directionHorizontal, directionVertical).normalized * _startSpeed;

        }

        if (other.transform.CompareTag("GoalGameOver"))
        {
            SoundManager.Instance.PlayGoalAgainst();
            GameManager.Instance.AIScore++;
            StartCoroutine(RestartBall());
        }
        if (other.transform.CompareTag("Goal"))
        {
            SoundManager.Instance.PlayGoal();
            GameManager.Instance.PlayerScore++;
            StartCoroutine(RestartBall());
        }
    }

    public void StopBall()
    {
        StopAllCoroutines();
        rigidbody2D.linearVelocity = Vector2.zero;
        rigidbody2D.simulated = false;
    }
    public void EnablePhysics()
    {
        rigidbody2D.simulated = true;
    }


}