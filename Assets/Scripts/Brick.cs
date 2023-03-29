using UnityEngine;

public class Brick : MonoBehaviour
{
    public void OnBallCollision()
    {
        Destroy(gameObject);
    }
}
