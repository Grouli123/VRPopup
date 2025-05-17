using System.Collections.Generic;
using UnityEngine;

public class PopupSlotController
{
    private readonly List<PopupSlot> slots;
    private readonly float slideDistance;

    public PopupSlotController(List<PopupSlot> slots, float slideDistance)
    {
        this.slots = slots;
        this.slideDistance = slideDistance;
    }

    public void UpdateSlotOffsets()
    {
        var active = slots.FindAll(s => s.IsOccupied);

        foreach (var slot in slots)
            slot.ResetPosition();

        if (active.Count == 2)
        {
            if (!slots[0].IsOccupied)
            {
                slots[1].AnimateToOffset(new Vector3(-slideDistance, 0, 0));
                slots[2].AnimateToOffset(new Vector3(-slideDistance, 0, 0));
            }
            else if (!slots[1].IsOccupied)
            {
                slots[0].AnimateToOffset(new Vector3(slideDistance, 0, 0));
                slots[2].AnimateToOffset(new Vector3(-slideDistance, 0, 0));
            }
            else if (!slots[2].IsOccupied)
            {
                slots[0].AnimateToOffset(new Vector3(slideDistance, 0, 0));
                slots[1].AnimateToOffset(new Vector3(slideDistance, 0, 0));
            }
        }
        else if (active.Count == 1)
        {
            active[0].AnimateToOffset(Vector3.zero);
        }
    }
}