using UnityEngine;

public class RacketController : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _limitHorizontal;

    public bool _isUp;
    [SerializeField] private bool _isPlayer;
    [SerializeField] private float _aiSpeed;

    private float _horizontalInput;
    private Vector3 _startPosition;


    private void Awake()
    {
        _startPosition = transform.position;
    }
    void Update()
    {

        if (GameManager.Instance.isGameOver)
            return;

        Vector3 _newPosition = transform.position;

        if (_isPlayer)
        {

            _horizontalInput = Input.GetAxis("Horizontal");
            _newPosition.x += _horizontalInput * _speed * Time.deltaTime;
        }
        else
        {
            //AI gol yemesini istersek speed ekleyip yavaşlatabiliriz
            var _ai = Mathf.Lerp(_newPosition.x, BallController.Instance.transform.position.x, _aiSpeed * Time.deltaTime);
            //var _ai = BallController.Instance.transform.position.x;
            _newPosition.x = _ai;
        }
        _newPosition.x = Mathf.Clamp(_newPosition.x, -_limitHorizontal, _limitHorizontal);
        transform.position = _newPosition;
    }

    public void ResetPosition()
    {
        transform.position = _startPosition;
    }



}
