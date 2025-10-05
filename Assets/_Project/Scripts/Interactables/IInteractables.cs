namespace NenNhangSinhMenh.Interactables
{
    public interface IInteractable
    {
        string InteractionPrompt { get; }
        bool Interact();
    }
}