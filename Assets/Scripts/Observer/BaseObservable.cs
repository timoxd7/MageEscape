
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObservable : MonoBehaviour
{
    private List<IObserver> _observers = new List<IObserver>();

    protected void Register(IObserver observer)
    {
        _observers.Add(observer);
    }

    protected void Unregister(IObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.UpdateObserver();
        }
    }
}
