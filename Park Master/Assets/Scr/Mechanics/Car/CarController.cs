using System;
using System.Collections;
using System.Collections.Generic;
using Scr.Mechanics.Bezier;
using Zenject;

namespace Scr.Mechanics.Car
{
    public class CarController : SelectableGameObject
    {
        private IPathBuilder _pathBuilder;
        
        [Inject]
        private void SetDependencies(IPathBuilder pathBuilder)
        {
            _pathBuilder = pathBuilder;
        }

        protected override void OnSelected()
        {
            // _pathBuilder.BuildPath();
        }

        protected override void OnDeselect()
        {
            base.OnDeselect();
        }
    }
}