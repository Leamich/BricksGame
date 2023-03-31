using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float _velocity;
    [SerializeField] float _duration;
    [SerializeField] Root _root;
    [SerializeField] float _xMin;
    [SerializeField] float _xMax;

    private SpriteRenderer _renderer;

    private float _lastTouch = 0f;

    public float TiredCoefficient
    {
        get
        {
            return 1f - Mathf.Max(0, _duration - (Time.time - _lastTouch)) / _duration;
        }
    }

    public void OnEnable()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _root.OnGameStart += OnGameStart;
    }

    public void OnGameStart()
    {
        _lastTouch = Time.time;
    }

    public void OnBallCollision()
    {
        _lastTouch = Time.time;
    }

    public void Update()
    {
        if (!_root.IsPlaying) { return; }

        if (Input.GetKey(KeyCode.A))
        {
            var position = transform.position;
            position.x -= _velocity * Time.deltaTime;
            position.x = Mathf.Max(_xMin, Mathf.Min(position.x, _xMax));
            transform.position = position;
        }
        if (Input.GetKey(KeyCode.D))
        {
            var position = transform.position;
            position.x += _velocity * Time.deltaTime;
            position.x = Mathf.Max(_xMin, Mathf.Min(position.x, _xMax));
            transform.position = position;
        }

        Color color = Color.white;
        color.r = 1f;
        color.b = TiredCoefficient;
        color.g = TiredCoefficient;

        _renderer.color = color;
    }
}
