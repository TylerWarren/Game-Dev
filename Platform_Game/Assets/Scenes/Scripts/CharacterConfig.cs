using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Character/Character Config")]
public class CharacterConfig : ScriptableObject
{
    public string characterName;
    public int jump;
    public float speed;
    public Sprite characterIcon;
    public int lives;
    public float jumpForce;
    public float gravity;
    
}