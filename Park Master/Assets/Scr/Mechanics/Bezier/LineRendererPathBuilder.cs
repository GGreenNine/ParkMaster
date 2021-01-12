using System;
using System.Collections.Generic;
using System.Linq;
using Scr.Configs;
using Scr.Input;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scr.Mechanics.Bezier
{
    public interface IPathBuilder
    {
        void StartBuilding(CarType type);
        IEnumerable<Vector3> StopBuilding(CarType type);
    }

    public class LineRendererPathBuilder : MonoBehaviour, IPathBuilder
    {
        [SerializeField] private CarPathControl[] _carPathControls;
        //todo make layers config
        private LayerMask groundLayer;
        private readonly CompositeDisposable carPathDrawindDisposable = new CompositeDisposable();
        private Dictionary<CarType, CarPathControl> carPathControlsDictionary = new Dictionary<CarType, CarPathControl>();
        private IRaycastingSystem _raycastingSystem;
        
        [Inject]
        private void SetDependencies(IRaycastingSystem raycastingSystem)
        {
            _raycastingSystem = raycastingSystem;
        }
        
        private void Awake()
        {
            groundLayer = LayerMask.GetMask("Floor");
            carPathControlsDictionary = _carPathControls.ToDictionary(control => control.CarType, control => control);
            DisableAllPathes();
        }

        public void StartBuilding(CarType type)
        {
            carPathControlsDictionary.TryGetValue(type, out var carPathControl);
            if (carPathControl is null) return;
            carPathControl.ClearPath();
            Observable.EveryUpdate().Subscribe(l => { GetPathNextPoint(carPathControl); })
                    .AddTo(carPathDrawindDisposable);
        }

        public IEnumerable<Vector3> StopBuilding(CarType type)
        {
            carPathControlsDictionary.TryGetValue(type, out var carPathControl);
            if (!(carPathControl is null))
            {
                carPathDrawindDisposable.Clear();
                return carPathControl.GetPath();
            }
            throw new Exception($"{type.ToString()} car type does not exist in path builder");
        }

        private void GetPathNextPoint(CarPathControl path)
        {
            var point = _raycastingSystem.TryToGetPoint(groundLayer.value);
            if (point != null)
            {
                BuildPath(path, point.Value);
            }
        }

        public void BuildPath(CarPathControl path, Vector3 newPoint)
        {
            if (path != null)
            {
                path.gameObject.SetActive(true);
                path.UpdatePath(newPoint);
            }
        }

        private void DisableAllPathes()
        {
            foreach (var carPathControl in _carPathControls)
            {
                carPathControl.gameObject.SetActive(false);
            }
        }

        private void EnablePath(CarPathControl carPathControl)
        {
            carPathControl.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            carPathDrawindDisposable.Dispose();
        }
    }
}
