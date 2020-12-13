using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scr.Input
{
    public interface IObjectSelector
    {
        IReactiveProperty<GameObject> SelectedGameobject { get; }
    }

    public class ObjectSelector : IDisposable, IInitializable, IObjectSelector
    {
        private float rayMaxRange = 1000;
        private LayerMask _layerMask = LayerMask.GetMask("SelectableGameobject");
        
        private readonly ReactiveProperty<GameObject> _selectedGameobject = new ReactiveProperty<GameObject>();
        public IReactiveProperty<GameObject> SelectedGameobject => _selectedGameobject;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly IInputObservable _inputObservable;
        private readonly IRaycastingSystem _raycastingSystem;

        public ObjectSelector(IInputObservable inputObservable, IRaycastingSystem raycastingSystem)
        {
            _inputObservable = inputObservable;
            _raycastingSystem = raycastingSystem;
        }
        
        public void Initialize()
        {
            _inputObservable.LeftHolded.Subscribe(OnHolded).AddTo(_disposable);
        }
        
        private void OnHolded(bool isHolding)
        {
            if (isHolding)
            {
                var hitObject = _raycastingSystem.TryToGetGameObject(_layerMask.value);
                if (hitObject != null)
                {
                    _selectedGameobject.Value = hitObject;
                }
            }
            else
            {
                _selectedGameobject.Value = null;
            }
        }

        public void Dispose()
        {
            _selectedGameobject?.Dispose();
            _disposable?.Dispose();
        }

    }
}
