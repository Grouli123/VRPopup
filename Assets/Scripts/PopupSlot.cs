using UnityEngine;
using DG.Tweening;

public class PopupSlot : MonoBehaviour, ISlot
{
    private Vector3 defaultLocalPosition;
    private IPopup currentPopup;

    private void Awake()
    {
        defaultLocalPosition = transform.localPosition;
    }

    public bool IsOccupied => currentPopup != null;

    public void AssignWindow(IPopup popup)
    {
        currentPopup = popup;
        var mono = popup as MonoBehaviour;
        if (mono != null)
        {
            mono.transform.SetParent(transform, false);
            mono.transform.localPosition = Vector3.zero;
            mono.transform.localRotation = Quaternion.identity;
        }
    }

    public void ReleaseSlot() => currentPopup = null;

    public IPopup GetPopup() => currentPopup;

    public void AnimateToOffset(Vector3 offset)
    {
        transform.DOLocalMove(defaultLocalPosition + offset, 0.4f).SetEase(Ease.InOutSine);
    }

    public void ResetPosition()
    {
        transform.DOLocalMove(defaultLocalPosition, 0.4f).SetEase(Ease.InOutSine);
    }
}