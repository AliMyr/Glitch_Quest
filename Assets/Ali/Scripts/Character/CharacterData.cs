using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private float defaultSpeed;

    public float DefaultSpeed => defaultSpeed;
}
