using System;

namespace TwoOneTwoGames.UIManager.Interfaces
{
    public interface IUiGameEconomyPresenter
    {
        event Action<int, long> EarnedResourceEvent;
        event Action<int, long> UsedResourceEvent;
        long GetResourceValueWithId(int id);
    }
}