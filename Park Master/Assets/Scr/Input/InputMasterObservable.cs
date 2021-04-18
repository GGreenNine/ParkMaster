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

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();


        public void Initialize()
        {
            InitializeInputs();
        }

        private void InitializeInputs()
        {
            _inputActions.Enable();
            
            Observable.FromEvent<InputAction.CallbackContext>(actions => _inputActions.GameControl.LeftHold.performed += actions,
                action => _inputActions.GameControl.LeftHold.performed -= action).Subscribe(context =>
                {
                    leftHolded.Value = true;
                }).AddTo(_compositeDisposable);
            
            Observable.FromEvent<InputAction.CallbackContext>(actions => _inputActions.GameControl.LeftHold.canceled += actions,
                action => _inputActions.GameControl.LeftHold.performed -= action).Subscribe(context =>
            {
                leftHolded.Value = false;
            }).AddTo(_compositeDisposable);
            
            Observable.FromEvent<InputAction.CallbackContext>(actions => _inputActions.GameControl.LeftClick.performed += actions,
                action => _inputActions.GameControl.LeftHold.performed -= action).Subscribe(context =>
            {
                leftClicked.OnNext(Unit.Default);
            }).AddTo(_compositeDisposable);
        }



        public void Dispose()
        {
            _inputActions.Dispose();
            _compositeDisposable?.Dispose();
            leftClicked?.OnCompleted();
            leftClicked?.Dispose();
            leftHolded?.Dispose();
        }
    }
}
