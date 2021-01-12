using Scr.Input;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scr.Mechanics.Car
{
    public abstract class SelectableGameObject : GameObjectBase
    {
        protected IObjectSelector ObjectSelector;
        protected bool IsSelected = false;
    
        [Inject]
        private void SetObjectSelector(IObjectSelector objectSelector)
        {
            ObjectSelector = objectSelector;
        }

        protected virtual void Awake()
        {
            ObjectSelector.SelectedGameobject.Subscribe(OnSelectedGameObjectChange).AddTo(OnDestroyDisposable);
        }

        private void OnSelectedGameObjectChange(GameObject obj)
        {
            if (obj == this.gameObject)
            {
                OnSelected();
                IsSelected = true;
            }
            else
            {
                if (IsSelected)
                {
                    OnDeselect();
                    IsSelected = false;
                }
            }
        }


        protected virtual void OnSelected()
        {
        }

        protected virtual void OnDeselect()
        {
        }
    }
}