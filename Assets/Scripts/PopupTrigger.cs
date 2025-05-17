using UnityEngine;

public class PopupTrigger : MonoBehaviour
{
    public GameObject popupPrefab;
    public PopupGroup group;
    public PopupSlot targetSlot;

    public void ShowPopup()
    {
        if (targetSlot != null)
        {
            PopupManager.Instance.OpenPopupInSlot(popupPrefab, group, targetSlot);
        }
    }
}