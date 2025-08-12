using System;
using System.Collections.Generic;

public class ReactProperty<T>
{
    private T _value;
    private EqualityComparer<T> _comparer = EqualityComparer<T>.Default;
    public Action<T> EChanged;

    public ReactProperty(T value) => _value = value;
    public ReactProperty() : this(default(T)) { }

    public T Value
    {
        get => _value;
        set
        {
            var oldValue = _value;
            _value = value;

            if (_comparer.Equals(oldValue, value) == false)
                EChanged?.Invoke(_value);
        }
    }
}
