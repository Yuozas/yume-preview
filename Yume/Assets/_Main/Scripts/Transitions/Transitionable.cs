using UnityEngine;

public class Transitionable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Character _character;
    public void Set(Vector3 position, Vector2 direction)
    {
        transform.position = position;
        _character.SetDirection(direction);
    }
}
