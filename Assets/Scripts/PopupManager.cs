using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    public List<PopupSlot> slots;
    public float slideDistance = 0.15f;

    private PopupSlotController slotController;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        slotController = new PopupSlotController(slots, slideDistance);
    }

    public void OpenPopupInSlot(GameObject prefab, PopupGroup group, PopupSlot slot)
    {
        if (slot.IsOccupied)
        {
            var old = slot.GetPopup() as PopupWindow;
            if (old == null) return;

            old.CloseCallback = () =>
            {
                slot.ReleaseSlot();
                SpawnPopup(prefab, group, slot);
            };

            old.Close();
        }
        else
        {
            SpawnPopup(prefab, group, slot);
        }
    }

    private void SpawnPopup(GameObject prefab, PopupGroup group, PopupSlot slot)
    {
        var popup = Instantiate(prefab).GetComponent<PopupWindow>();
        popup.popupGroup = group;

        popup.CloseCallback = () =>
        {
            slot.ReleaseSlot();
            slotController.UpdateSlotOffsets();
        };

        slot.AssignWindow(popup);
        popup.Open();
        slotController.UpdateSlotOffsets();
    }
}