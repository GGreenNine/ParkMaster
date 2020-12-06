using System;
using System.Collections.Generic;
using System.Linq;
using Scr.Configs;
using UnityEngine;
using Zenject;

namespace Scr.Mechanics.Bezier
{
    public class LineRendererPathBuilder : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform[] points;


        [SerializeField] private CarPathControl[] _carPathControls;
 
        private IPointsCreator _pointsCreator;
        private Dictionary<CarType, CarPathControl> carPathControlsDictionary = new Dictionary<CarType, CarPathControl>();

        [Inject]
        private void Inject(IPointsCreator pointsCreator)
        {
            _pointsCreator = pointsCreator;
        }

        private void Awake()
        {
            carPathControlsDictionary = _carPathControls.ToDictionary(control => control.CarType, control => control);
        }

        public void BuildPath(CarType type, Vector3[] points)
        {
            carPathControlsDictionary.TryGetValue(type, out var carPathControl);
            if (carPathControl != null)
            {
                carPathControl.UpdatePath(points);
            }
        }

        private void Start()
        {
            var bezierPoints = _pointsCreator.GetPoints(points.Select(transform1 => transform1.position).ToArray());
            lineRenderer.positionCount = bezierPoints.Count;
            lineRenderer.SetPositions(bezierPoints.ToArray());
        }
    }
}
