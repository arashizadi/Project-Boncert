using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// credits to Dapper Dino's "How to Make a Multiplayer Game in Unity - Character Selection" vid on YouTube!

[CreateAssetMenu(fileName = "New Character", menuName = "Character Selection/Character")]
public class Character : ScriptableObject
{
    [SerializeField] private GameObject characterPreviewPrefab = default;
    [SerializeField] private GameObject gameplayCharacterPrefab = default;

    // getters
    public GameObject CharacterPreviewPrefab => characterPreviewPrefab;
    public GameObject GameplayCharacterPrefab => gameplayCharacterPrefab;
}
