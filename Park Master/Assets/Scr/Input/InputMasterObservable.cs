using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Scr.Input
{
    public interface IInputMaster
    {
        IReactiveProperty<bool> LeftHolded { get; }
        IObservable<Unit> LeftClicked { get; }
        Vector3 MousePosition { get; }
    }

    public class InputMaster : IInitializable, IDisposable, IInputMaster
    {
        private readonly global::InputMaster _inputActions = new global::InputMaster();
        
        private readonly ReactiveProperty<bool> leftHolded = new ReactiveProperty<bool>();
        private readonly Subject<Unit> leftClicked = new Subject<Unit>();

        public Vector3 MousePosition => _inputActions.GameControl.Mouseposition.ReadValue<Vector2>();

        public IReactiveProperty<bool> LeftHolded => leftHolded;
        public IObservable<Unit> LeftClicked => leftClicked;


        public void Initialize()
        {
            InitializeInputs();
        }

        private void InitializeInputs()
        {
            _inputActions.Enable();
            _inputActions.GameControl.LeftHold.performed += context =>
            {
                leftHolded.Value = true;
            };
            _inputActions.GameControl.LeftHold.started += context =>
            {
            };
            _inputActions.GameControl.LeftHold.canceled += context =>
            {
                leftHolded.Value = false;
            };
        
            _inputActions.GameControl.LeftClick.performed += context =>
            {
                leftClicked.OnNext(Unit.Default);
            };
        }

        public void Dispose()
        {
            leftClicked?.OnCompleted();
            leftClicked?.Dispose();
            leftHolded?.Dispose();
        }
    }
}
