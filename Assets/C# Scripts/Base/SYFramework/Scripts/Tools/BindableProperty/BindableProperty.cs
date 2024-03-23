using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该类魔改自QFramework的BindableProperty
/// </summary>
public class BindableProperty<T>
{
    private T value;
    private T Value
    {
        get => value;
        set
        {
            if (value == null && this.value == null) return;
            if (value != null && ValueComparer(this.value, value)) return;

            this.value = value;
            onValueChanged?.Invoke(this.value);
        }
    }
    
    private Action<T> onValueChanged;
    
    //所有方法都重新指向T类型
    public override string ToString() => value?.ToString();
    public override int GetHashCode() => value.GetHashCode();
    public static Func<T, T, bool> ValueComparer { get; private set; } = (a, b) => a.Equals(b);

    public static bool operator ==(BindableProperty<T> a, BindableProperty<T> b)
    {
        if (a is null && b is null) return true;
        return (a is not null && b is not null) && ValueComparer(a.Value, b.Value);
    }

    public static bool operator !=(BindableProperty<T> a, BindableProperty<T> b) => (!(a == b));

    
    public BindableProperty(T value = default)
    {
        this.value = value;
    }
    
    public void SetValue(T target)
    {
        Value = target;
    }

    public T GetValue()
    {
        return value;
    }

    public void SetValueComparer(Func<T, T, bool> comparer)
    {
        ValueComparer = comparer;
    }

    public void SetValueWithoutPublish(BindableProperty<T> target)
    {
        if (target == null) return;
        value = target.Value;
    }

    public void SetValueWithoutPublish(T target)
    {
        value = target;
    }

    public void Register(Action<T> onValueChanged)
    {
        this.onValueChanged += onValueChanged;
    }

    public void UnRegister(Action<T> onValueChanged)
    {
        this.onValueChanged -= onValueChanged;
    }
    
}