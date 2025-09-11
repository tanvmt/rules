using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class InteractiveDoor : MonoBehaviour
{
    private bool isOpen = false;
    public bool IsOpen { get { return isOpen; } }
    public Quaternion initialLocalRotation;
    public float openAngle = 90f;

    public float animationTime = 1f;
    private bool isAnimating = false;

    void Start()
    {
        initialLocalRotation = transform.localRotation;
    }

    public void ToggleDoor()
    {
        Debug.Log("Toggling door");
        if (isAnimating) return;

        isOpen = !isOpen;
        Quaternion targetRotation = isOpen ? Quaternion.Euler(0, openAngle, 0) * initialLocalRotation : initialLocalRotation;
        StartCoroutine(AnimateDoor(targetRotation));
    }

    private IEnumerator AnimateDoor(Quaternion targetRotation)
    {
        isAnimating = true;
        Quaternion startRotation = transform.localRotation;
        float elapsedTime = 0f;
        while (elapsedTime < animationTime)
        {
            transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / animationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = targetRotation;
        isAnimating = false;
    }
}
