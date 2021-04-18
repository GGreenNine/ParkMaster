using System;
using UniRx;
using UnityEngine;

namespace UI
{
    public abstract class UIElementBase : MonoBehaviour
    {
        protected readonly CompositeDisposable OnDisableDisposables = new CompositeDisposable();
        protected readonly CompositeDisposable OnDestroyDisposables = new CompositeDisposable();

        public void AddOnDisableDisposables(IDisposable disposable)
        {
            OnDisableDisposables.Add(disposable);
        }

        public void AddOnDestroyDisposables(IDisposable disposable)
        {
            OnDestroyDisposables.Add(disposable);
        }

        protected virtual void OnDisable()
        {
            OnDisableDisposables.Clear();
        }
        
        protected virtual void OnDestroy()
        {
            OnDisableDisposables.Dispose();
            OnDestroyDisposables.Dispose();
        }

    }
}