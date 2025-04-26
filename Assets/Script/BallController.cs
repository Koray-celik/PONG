using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController Instance { get; private set; }


    [SerializeField] private new Rigidbody2D rigidbody2D;

    [SerializeField] private float _startSpeed;

    private void Awake()
    {
        Instance = this;
    }

    public void OnStart()
    {
        rigidbody2D.linearVelocity = Vector2.down * _startSpeed;
        transform.position = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.transform.CompareTag("Racket")||other.transform.CompareTag("aiRacket"))
        {

            var racket = other.transform.GetComponent<RacketController>();
            var directionVertical = racket._isUp ? -1 : 1;
            var directionHorizontal = (transform.position.x - racket.transform.position.x) / other.collider.bounds.extents.x;

            rigidbody2D.linearVelocity = new Vector2(directionHorizontal, directionVertical).normalized * _startSpeed;

        }

        if (other.transform.CompareTag("Racket"))
        {
            GameManager.Instance.Score++;
            //OnStart();

        }
        if (other.transform.CompareTag("GoalGameOver"))
        {
            GameManager.Instance.OnGameOver();
            rigidbody2D.linearVelocity = Vector2.zero;
        }
    }
}
