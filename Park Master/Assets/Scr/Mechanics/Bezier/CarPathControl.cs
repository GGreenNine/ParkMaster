using System;
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

        public void UpdatePath(Vector3[] points)
        {
            _lineRenderer.positionCount = points.Length;
            _lineRenderer.SetPositions(points);
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
