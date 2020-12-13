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
        // void BuildPath(CarType type, Vector3[] points);
    }

    public class LineRendererPathBuilder : MonoBehaviour, IPathBuilder
    {
        [SerializeField] private CarPathControl[] _carPathControls;
        //todo make layers config
        private LayerMask groundLayer;
 
        private Dictionary<CarType, CarPathControl> carPathControlsDictionary = new Dictionary<CarType, CarPathControl>();
        private IRaycastingSystem _raycastingSystem;
        private List<Vector3> points = new List<Vector3>();
        
        
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

        public void StartBuildingPath(CarType type)
        {
            Observable.EveryUpdate().Subscribe(l =>
            {
                GetPathNextPoint(type);
            });
        }

        private void GetPathNextPoint(CarType type)
        {
            var point = _raycastingSystem.TryToGetPoint(groundLayer.value);
            if (point != null)
            {
                points.Add(point.Value);
                if (points.Count > 3)
                {
                    BuildPath(type, points);
                }
            }
        }

        public void BuildPath(CarType type, IEnumerable<Vector3> path)
        {
            carPathControlsDictionary.TryGetValue(type, out var carPathControl);
            if (carPathControl != null)
            {
                carPathControl.gameObject.SetActive(true);
                carPathControl.UpdatePath(path);
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
    }
}
