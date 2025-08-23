using System;
using UnityEngine;
using PrimeTween;
using UnityEngine.Serialization;

public class PopupOpenCloseAnimation : MonoBehaviour
{
    [SerializeField]
    public CanvasGroup _canvasGroup;

    [SerializeField]
    private Ease _inEase = Ease.OutQuad;
    [SerializeField]
    private Ease _outEase = Ease.OutQuad;

    private Sequence? _openSequence;

    private Sequence? _closeSequence;

    public void OpenPopup()
    {
        // Stop any ongoing animations
        StopSequences();

        // Reset initial state
        transform.localScale = Vector3.one * 0.1f;
        _canvasGroup.alpha = 0f;
        gameObject.SetActive(true);

        _openSequence = Sequence.Create(useUnscaledTime: true)
            .Chain(Tween.Scale(transform, Vector3.one * 1f, 0.9f, _inEase, useUnscaledTime: true))
            .Group(Tween.Alpha(_canvasGroup, 1f, 0.5f, useUnscaledTime: true))
            .OnComplete(() =>
            {
                _openSequence = null;
            });
    }

    public void ClosePopup(Action onClosed)
    {
        // Stop any ongoing animations
        StopSequences();
        
        // Create close animation sequence (reverse of open)
        _closeSequence = Sequence.Create(useUnscaledTime: true)
            .Chain(Tween.Scale(transform, Vector3.zero, 0.9f, _outEase, useUnscaledTime: true))
            .Group(Tween.Alpha(_canvasGroup, 0f, 0.5f, useUnscaledTime: true))
            .OnComplete(() =>
            {
                _closeSequence = null;
                onClosed?.Invoke();
            });
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