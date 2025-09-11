using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using System.Collections.Generic;
using System.Text;
using StarterAssets;

public class JournalUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject journalPanel;
    public TextMeshProUGUI clueListText;

    [Header("Player Components to Disable")]
    public FirstPersonController playerController;
    public PlayerInteraction playerInteraction;

    private GameManager gameManager;
    private bool isJournalOpen = false;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        journalPanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            isJournalOpen = !isJournalOpen;
            journalPanel.SetActive(isJournalOpen);

            if (isJournalOpen && gameManager != null)
            {
                UpdateClueList();
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (playerController != null) playerController.enabled = false;
                if (playerInteraction != null) playerInteraction.enabled = false;
            }
            else
            {
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if (playerController != null) playerController.enabled = true;
                if (playerInteraction != null) playerInteraction.enabled = true;
            }
        }
    }

    void UpdateClueList()
    {
        List<string> clues = gameManager.GetCollectedClues();

        if (clues.Count == 0)
        {
            clueListText.text = "Chưa có manh mối nào.";
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < clues.Count; i++)
            {
                sb.Append("- ");
                sb.Append(clues[i]);
                sb.Append("\n\n");
            }
            clueListText.text = sb.ToString();
        }
    }
}
