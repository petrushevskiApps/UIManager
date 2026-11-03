using System;
using System.Collections.Generic;

namespace TwoOneTwoGames.UIManager.Utilities.ReactiveProperty
{
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        // Compared through the generic comparer, not object.Equals: the static overload takes two
        // objects, so every set of a struct T boxed both operands and then fell back to the
        // reflection-based ValueType.Equals. View data structs are set per frame.
        private static readonly EqualityComparer<T> Comparer = EqualityComparer<T>.Default;

        private readonly bool _alwaysUpdate;
        private T _value;

        public ReactiveProperty(T initialValue = default, bool alwaysUpdate = false)
        {
            _value = initialValue;
            _alwaysUpdate = alwaysUpdate;
        }

        public T Value
        {
            get => _value;
            set
            {
                if (!_alwaysUpdate && Comparer.Equals(_value, value)) return;
                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }

        // A screen's Resume runs again when a popup on top of it closes, with no Hide in between, so a listener that
        // is already subscribed is never added a second time.
        public void Subscribe(Action<T> onValueChangeListener, bool triggerOnSubscribe = true)
        {
            ValueChanged -= onValueChangeListener;
            ValueChanged += onValueChangeListener;
            if (triggerOnSubscribe) onValueChangeListener?.Invoke(Value);
        }

        public void Unsubscribe(Action<T> onValueChangeListener)
        {
            ValueChanged -= onValueChangeListener;
        }

        public event Action<T> ValueChanged;
    }
}