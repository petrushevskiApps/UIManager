using System;
using UnityEngine;

namespace TwoOneTwoGames.UIManager.Plugins.JoystickPlugin
{
    public interface IJoystick
    {
        float Horizontal { get; }
        float Vertical { get; }
        Vector3 Direction { get; }
        event Action<bool> OnJoystickState;
    }
}