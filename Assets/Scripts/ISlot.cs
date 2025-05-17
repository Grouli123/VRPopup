public interface ISlot
{
    bool IsOccupied { get; }
    void AssignWindow(IPopup popup);
    void ReleaseSlot();
}