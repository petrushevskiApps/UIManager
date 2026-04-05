
using System;
using UnityEngine;

namespace TwoOneTwoGames.UIManager.Interfaces
{
    public interface IUiFunnelPresenter
    {
        event EventHandler FunnelLoadedEvent;
        event EventHandler<int> FunnelUnlockedEvent;
        string GetCurrentFunnelTitle();
        int GetCompletedLevelsCount();
        int GetLevelsCount();
        bool IsLastFunnel();
        bool IsLockedFunnel();
        bool IsFirstFunnel();
        int GetFunnelCost();
    }
}