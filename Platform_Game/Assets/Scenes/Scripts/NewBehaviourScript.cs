using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterConfig characterConfig;

    private int currentLives;

    void Start()
    {
        if (characterConfig != null)
        {
            InitializeCharacter();
        }
    }

    private void InitializeCharacter()
    {
        currentLives = characterConfig.lives;
        gameObject.name = characterConfig.characterName;
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = new Vector3(characterConfig.speed, rb.velocity.y, rb.velocity.z);
        }
    }
}