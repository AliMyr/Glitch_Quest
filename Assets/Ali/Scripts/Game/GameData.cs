using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game/Data")]
public class GameData : ScriptableObject
{
    [SerializeField] private CharacterData defaultCharacterData;
    
    public CharacterData DefaultCharacterData => defaultCharacterData;
    
    // Создать инстанс CharacterData с настройками по умолчанию
    public static CharacterData CreateDefaultCharacterData()
    {
        CharacterData characterData = ScriptableObject.CreateInstance<CharacterData>();
        
        // Модификатор доступа не позволяет прямо изменить defaultSpeed и другие поля,
        // но можно использовать метод для настройки
        Debug.Log("GameData: Creating default CharacterData");
        
        return characterData;
    }
}
