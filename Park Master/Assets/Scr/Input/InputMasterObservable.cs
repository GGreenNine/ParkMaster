using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Scr.Input
{
    public interface IInputObservable
    {
        IReactiveProperty<bool> LeftHolded { get; }
        IReactiveProperty<bool> LeftClicked { get; }
    }

    public class InputMasterObservable : IInitializable, IDisposable, IInputObservable
    {
        private readonly InputMaster _inputActions = new InputMaster();
        
        private readonly ReactiveProperty<bool> leftHolded = new ReactiveProperty<bool>();
        private readonly Subject<Unit> leftClicked = new Subject<Unit>();

        public IReactiveProperty<bool> LeftHolded => leftHolded;
        public IReactiveProperty<bool> LeftClicked => leftHolded;


        public void Initialize()
        {
            InitializeInputs();
        }

        private void InitializeInputs()
        {
            _inputActions.Enable();
            _inputActions.GameControl.LeftHold.performed += context =>
            {
                Debug.Log("holded");
                leftHolded.Value = true;
            };
            _inputActions.GameControl.LeftHold.started += context =>
            {
                Debug.Log("holded started");
            };
            _inputActions.GameControl.LeftHold.canceled += context => leftHolded.Value = false;
        
            _inputActions.GameControl.LeftClick.performed += context =>
            {
                Debug.Log("Clicked");
                leftClicked.OnNext(Unit.Default);
            };
        }

        public void Dispose()
        {
            leftClicked.OnCompleted();
            leftClicked.Dispose();
            leftHolded.Dispose();
        }
    }
}
