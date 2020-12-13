using System;
using System.Collections.Generic;
using System.Linq;
using Scr.Configs;
using UnityEngine;
using Zenject;

namespace Scr.Mechanics.Bezier
{
    public class CarPathControl : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private CarType _carType;

        public CarType CarType => _carType;

        private CarsPathesConfig _carsPathesConfig;
        
        [Inject]
        private void Inject(CarsPathesConfig carsPathesConfig)
        {
            _carsPathesConfig = carsPathesConfig;
        }

        private void Awake()
        {
            SetPathSettings(_carsPathesConfig.GetLineRendererSettings(_carType).LineRendererSettings);
        }

        public void UpdatePath(IEnumerable<Vector3> points)
        {
            _lineRenderer.positionCount = points.Count();
            _lineRenderer.SetPositions(points.ToArray());
        }

        public void SetPathSettings(CarsPathesConfig.LineRendererSettings lineRendererSettings)
        {
            _lineRenderer.startColor = lineRendererSettings.color;
            _lineRenderer.endColor = lineRendererSettings.color;
            _lineRenderer.startWidth = lineRendererSettings.width;
            _lineRenderer.endWidth = lineRendererSettings.width;
        }
    }
}
