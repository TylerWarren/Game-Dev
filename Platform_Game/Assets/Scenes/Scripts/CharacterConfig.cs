using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterConfig", menuName = "Character/Character Config")]
public class CharacterConfig : ScriptableObject
{
    public string characterName;
    public int health;
    public float speed;
    public GameObject characterPrefab;
    public Sprite characterIcon;
    public int attackPower;
}