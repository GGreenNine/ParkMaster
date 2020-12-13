using UniRx;
using UnityEngine;

namespace Scr.Mechanics.Car
{
    public abstract class GameObjectBase : MonoBehaviour
    {
        protected readonly CompositeDisposable OnDestroyDisposable = new CompositeDisposable();
        protected readonly CompositeDisposable OnDisableDisposable = new CompositeDisposable();

        protected virtual void OnDestroy()
        {
            OnDestroyDisposable.Dispose();
        }

        protected virtual void OnDisable()
        {
            OnDisableDisposable.Dispose();
        }
    }
}