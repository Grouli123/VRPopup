using System;
using UnityEngine;
using DG.Tweening;

public class PopupWindow : MonoBehaviour, IPopup
{
    public PopupGroup popupGroup;
    public Action CloseCallback;

    public void Open()
    {
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }

    public void Close()
    {
        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            CloseCallback?.Invoke();
            Destroy(gameObject);
        });
    }
}