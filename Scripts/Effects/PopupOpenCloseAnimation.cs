using System;
using UnityEngine;
using PrimeTween;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PopupOpenCloseAnimation : MonoBehaviour
{
    [SerializeField]
    public CanvasGroup _canvasGroup;

    [SerializeField]
    private Ease _inEase = Ease.OutQuad;
    [SerializeField]
    private Ease _outEase = Ease.OutQuad;

    [SerializeField]
    [Tooltip("Optional dark background faded in and out with the popup.")]
    private Graphic _background;

    private Sequence? _openSequence;

    private Sequence? _closeSequence;

    private float? _backgroundAlpha;

    public void OpenPopup()
    {
        // Stop any ongoing animations
        StopSequences();

        // Reset initial state
        transform.localScale = Vector3.one * 0.1f;
        _canvasGroup.alpha = 0f;
        gameObject.SetActive(true);

        var sequence = Sequence.Create(useUnscaledTime: true)
            .Chain(Tween.Scale(transform, Vector3.one * 1f, 0.35f, _inEase, useUnscaledTime: true))
            .Group(Tween.Alpha(_canvasGroup, 1f, 0.2f, useUnscaledTime: true));

        if (_background != null)
        {
            var targetAlpha = GetBackgroundAlpha();
            SetBackgroundAlpha(0f);
            sequence.Group(Tween.Alpha(_background, targetAlpha, 0.2f, useUnscaledTime: true));
        }

        _openSequence = sequence.OnComplete(() =>
        {
            _openSequence = null;
        });
    }

    public void ClosePopup(Action onClosed)
    {
        // Stop any ongoing animations
        StopSequences();

        // Create close animation sequence (reverse of open)
        var sequence = Sequence.Create(useUnscaledTime: true)
            .Chain(Tween.Scale(transform, Vector3.zero, 0.2f, _outEase, useUnscaledTime: true))
            .Group(Tween.Alpha(_canvasGroup, 0f, 0.1f, useUnscaledTime: true));

        if (_background != null)
        {
            GetBackgroundAlpha();
            sequence.Group(Tween.Alpha(_background, 0f, 0.2f, useUnscaledTime: true));
        }

        _closeSequence = sequence.OnComplete(() =>
            {
                _closeSequence = null;
                onClosed?.Invoke();
            });
    }   

    // Cache the authored alpha once, before any tween changes it
    private float GetBackgroundAlpha()
    {
        _backgroundAlpha ??= _background.color.a;
        return _backgroundAlpha.Value;
    }

    private void SetBackgroundAlpha(float alpha)
    {
        var color = _background.color;
        color.a = alpha;
        _background.color = color;
    }

    private void StopSequences()
    {
        _openSequence?.Stop();
        _closeSequence?.Stop();
    }

    private void OnDestroy()
    {
        // Clean up tweens when object is destroyed
        StopSequences();
    }
}