using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float verticalVelocity;

    public float DefaultSpeed => defaultSpeed;
    public float JumpForce => jumpForce;
    public float Gravity => gravity;
    public float VerticalVelocity => verticalVelocity;
}
