using System;

namespace TwoOneTwoGames.UIManager.Interfaces
{
    public interface IUiGameEconomyPresenter
    {
        event EventHandler<(int, float)> EarnedResourceEvent;
        event EventHandler<(int, float)> UsedResourceEvent;
        int GetResourceValueWithId(int id);
    }
}