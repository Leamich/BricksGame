using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float _velocity;
    [SerializeField] float _duration;

    private SpriteRenderer _renderer;

    private float _lastTouch = 0f;

    public float TiredCoefficient
    {
        get
        {
            return 1f - Mathf.Max(0, (_duration - Time.time + _lastTouch) / _duration);
        }
    }

    public void OnEnable()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _lastTouch = Time.time;
    }

    public void OnBallCollision()
    {
        _lastTouch = Time.time;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            var position = transform.position;
            position.x -= _velocity * Time.deltaTime;
            transform.position = position;
        }
        if (Input.GetKey(KeyCode.D))
        {
            var position = transform.position;
            position.x += _velocity * Time.deltaTime;
            transform.position = position;
        }

        Color color = Color.white;
        color.r = 1f;
        color.b = TiredCoefficient;
        color.g = TiredCoefficient;
        // Debug.Log(color);
        _renderer.color = color;
    }
}
