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
        private readonly List<Vector3> pathPoints = new List<Vector3>();
        public IEnumerable<Vector3> GetPath() => pathPoints;

        private CarsPathesConfig.LineRendererSettings _lineRendererSettings;

        [Inject]
        private void Inject(CarsPathesConfig carsPathesConfig)
        {
            _carsPathesConfig = carsPathesConfig;
        }

        private void Awake()
        {
            _lineRendererSettings = _carsPathesConfig.GetLineRendererSettings(_carType).LineRendererSettings;
            SetPathSettings(_lineRendererSettings);
        }


        public void UpdatePath(Vector3 point)
        {
            pathPoints.Add(new Vector3(point.x, _lineRendererSettings.yOffset, point.z));
            if (pathPoints.Count > 3)
            {
                _lineRenderer.positionCount = pathPoints.Count();
                _lineRenderer.SetPositions(pathPoints.ToArray());
            }
        }

        public void ClearPath()
        {
            _lineRenderer.SetPositions(new Vector3[]{});
            pathPoints.Clear();
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
