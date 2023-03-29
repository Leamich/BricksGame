using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] bool _yRevert;
    [SerializeField] bool _xRevert;

    public Vector2 AlterVector(Vector2 vector)
    {
        if (_xRevert) vector.x = -vector.x;
        if (_yRevert) vector.y = -vector.y;
        return vector;
    }
}
