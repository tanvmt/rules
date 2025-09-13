using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 2f;

    public Camera playerCamera;

    public TextMeshProUGUI interactionPromptText;

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, interactionDistance))
        {
            if (hitInfo.collider.GetComponent<ClueObject>() != null)
            {
                interactionPromptText.text = "Nhấn chuột trái để xem";
                interactionPromptText.gameObject.SetActive(true);
                
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    hitInfo.collider.GetComponent<ClueObject>().CollectClue();
                }
            }

            else if (hitInfo.collider.GetComponent<InteractiveDoor>() != null)
            {
                interactionPromptText.text = "Nhấn chuột trái để mở/đóng cửa";
                interactionPromptText.gameObject.SetActive(true);

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    hitInfo.collider.GetComponent<InteractiveDoor>().ToggleDoor();
                }
            }
            else
            {
                interactionPromptText.gameObject.SetActive(false);
            }
        }
        else
        {
            interactionPromptText.gameObject.SetActive(false);
        }        
    }
}
