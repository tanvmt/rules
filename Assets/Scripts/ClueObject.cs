using UnityEngine;

public class ClueObject : MonoBehaviour
{
    [TextArea(3, 5)]
    public string clueDescription;

    private GameManager gameManager;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void CollectClue()
    {
        if (gameManager != null)
        {
            gameManager.AddClue(clueDescription);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GameManager not found in the scene.");
        }
    }
}
