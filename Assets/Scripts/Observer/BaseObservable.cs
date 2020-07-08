
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObservable : MonoBehaviour
{
    private List<IObserver> _observers = new List<IObserver>();

    public void Register(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Unregister(IObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void Notify()
    {
        foreach (IObserver observer in _observers)
        {
            observer.UpdateState();
        }
    }
}
