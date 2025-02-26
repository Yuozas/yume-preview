﻿using UnityEngine;
using UnityEngine.UI;

public class PortraitUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _face;
    [SerializeField] private Image _hair;

    private Portrait _portrait;

    public void Initialize(Portrait handler)
    {
        _portrait = handler;
        _portrait.OnUpdated += Set;

        Set(_portrait.Settings);
    }

    private void OnDestroy()
    {
        _portrait.OnUpdated -= Set;
    }

    private void Set(PortraitSettings settings)
    {
        Set(_face, settings.Face);
        Set(_hair, settings.Hair);
    }

    private void Set(Image image, Sprite sprite)
    {
        image.sprite = sprite;
        image.color = sprite == null ? Color.clear : Color.white;
    }
}
