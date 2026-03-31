using System;
using System.Collections.Generic;
using ModestTree;
using Zenject;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public class NavigationController : 
        INavigationController,
        IActiveScreenInfoProvider
    {
        public string Title => _screenBackStack.Peek().ScreenTitle;
        
        private readonly IScreenProvider _popupScreenProvider;

        private readonly Stack<IScreen> _screenBackStack = new();

        private readonly IScreenProvider _screenProvider;
        
        public NavigationController(
            [Inject(Id = "Screen")] IScreenProvider screenProvider,
            [Inject(Id = "PopupScreen")] IScreenProvider popupScreenProvider)
        {
            _screenProvider = screenProvider;
            _popupScreenProvider = popupScreenProvider;
        }

        public event EventHandler AllScreensClosedEvent;

        public void ShowScreen<T, TArguments>(TArguments navArguments) where T : IScreen
        {
            Show<T, TArguments>(_screenProvider, navArguments);
        }

        public void ShowScreen<T>() where T : IScreen
        {
            ShowScreen<T, NavigationArguments>(new NavigationArguments());
        }

        public void ShowPopup<T>() where T : IScreen
        {
            ShowPopup<T, NavigationArguments>(new NavigationArguments());
        }

        public void ShowPopup<T, TArguments>(TArguments navArguments) where T : IScreen
        {
            Show<T, TArguments>(_popupScreenProvider, navArguments);
        }

        public void GoBack()
        {
            if (!_screenBackStack.IsEmpty())
            {
                _screenBackStack.Pop().Close();
                if (!_screenBackStack.IsEmpty())
                    _screenBackStack.Peek().Resume();
                else
                    AllScreensClosedEvent?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                AllScreensClosedEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public IBackHandler GetActiveBackHandler()
        {
            if (!_screenBackStack.IsEmpty()) return _screenBackStack.Peek();

            return null;
        }

        public void ClearAllStackScreens()
        {
            while (!_screenBackStack.IsEmpty())
            {
                _screenBackStack.Pop().Close();
            }
        }

        public float GetActiveScreenTime()
        {
            return _screenBackStack.Peek().GetScreenActiveTime();
        }

        private void Show<T, TArguments>(
            IScreenProvider screenProvider,
            TArguments navArguments) where T : IScreen
        {
            var screen = screenProvider.GetScreen<T>();

            if (screen != null)
            {
                if (!_screenBackStack.IsEmpty() && (_screenBackStack.Peek().IsPopup || !screen.IsPopup))
                {
                    // We skip hiding the current screen only if
                    // there is no active screen on the stack or
                    // the current screen is Screen and the new is Popup.
                    HideCurrentScreenIn(_screenBackStack);
                }
                
                if (_screenBackStack.Contains(screen))
                {
                    ClearStackToScreen(_screenBackStack, screen);
                    screen.Resume();
                }
                else
                {
                    _screenBackStack.Push(screen);
                    screen.Show(navArguments);
                }
            }
            else
            {
                throw new Exception($"Screen of type {typeof(T)} not found in screens list!");
            }
        }

        private void HideCurrentScreenIn(Stack<IScreen> stack)
        {
            if (stack.Count == 0)
            {
                return;
            }

            if (stack.Peek().IsPopup)
            {
                if (stack.Peek().IsBackStackable)
                {
                    stack.Peek().Hide();
                }
                else
                {
                    stack.Pop().Close();
                    HideCurrentScreenIn(stack);
                }
            }
            else
            {
                if (stack.Peek().IsBackStackable)
                {
                    stack.Peek().Hide();
                }
                else
                {
                    stack.Pop().Close();
                }
            }
            
        }

        private void ClearStackToScreen(Stack<IScreen> stack, IScreen screen)
        {
            while (!stack.IsEmpty())
                if (!stack.Peek().Equals(screen))
                    stack.Pop().Close();
                else break;
        }
    }
}