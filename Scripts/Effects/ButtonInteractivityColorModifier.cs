using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TwoOneTwoGames.UIManager.Components.Interactive;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(InteractivityMonitor))]
public class ButtonInteractivityColorModifier : MonoBehaviour
{
    [SerializeField]
    private List<ImageState> _states;
    
    private InteractivityMonitor _interactivityMonitor;

    private void Awake()
    {
        _interactivityMonitor = GetComponent<InteractivityMonitor>();
    }

    private void OnEnable()
    {
        SetColors(_interactivityMonitor.IsInteractive);
        _interactivityMonitor.InteractivityChangedEvent.AddListener(OnInteractivityChanged);
    }

    private void OnDisable()
    {
        _interactivityMonitor.InteractivityChangedEvent.AddListener(OnInteractivityChanged);
    }

    private void OnInteractivityChanged(bool isInteractive)
    {
        SetColors(isInteractive);
    }
    
    private void SetColors(bool isInteractive)
    {
        foreach (ImageState state in _states)
        {
            if (isInteractive)
            {
                state.Image.color = state.ActiveColor;
            }
            else
            {
                state.Image.color = state.InactiveColor;
            }
        }
    }
}

[Serializable]
public struct ImageState
{
    [SerializeField]
    public Image Image;

    [SerializeField]
    public Color ActiveColor;

    [SerializeField]
    public Color InactiveColor;
}
