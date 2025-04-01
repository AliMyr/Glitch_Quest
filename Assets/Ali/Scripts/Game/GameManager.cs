using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();

        character.Initialize();
    }

    
}
