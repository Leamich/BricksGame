using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Root _root;
    [SerializeField] Vector2 _startVelocity;
    [SerializeField] float _platformVelocityCoefficient;
    [SerializeField] float _floorVelocityCoefficient;

    private Vector3 _velocity = new(0f, 0f, 0f);

    public void OnEnable()
    {
        _root.OnGameStart += OnGameStart;
    }

    public void OnGameStart()
    {
        _velocity = _startVelocity;
    }

    public void Update()
    {
        if (_velocity.magnitude == 0) _root.EndGame();
        transform.position += _velocity * Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.gameObject);

        if (collision.gameObject.TryGetComponent(out Brick brick))
        {
            brick.OnBallCollision();
            _root.InvokeBrickDestroy();

            Vector2 contactPoint = collision.contacts[0].point;
            if (contactPoint.y != transform.position.y)
            {
                _velocity.y = -_velocity.y;
            }
            if (contactPoint.x != transform.position.x)
            {
                _velocity.x = -_velocity.x;
            }
        }
        else if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            platform.OnBallCollision();
            _velocity *= _platformVelocityCoefficient * Mathf.Max(platform.TiredCoefficient, 0.1f);
            _velocity.y = -_velocity.y;
        }
        else if (collision.gameObject.TryGetComponent(out Border border))
        {
            _velocity = border.AlterVector(_velocity);
        }
        else if (collision.gameObject.TryGetComponent(out Floor floor))
        {
            _velocity *= _floorVelocityCoefficient;
            _velocity.y = -_velocity.y;
        }
    }
}
