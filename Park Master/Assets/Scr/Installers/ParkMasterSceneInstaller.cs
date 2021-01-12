using System;
using Scr.Configs;
using Scr.Input;
using Scr.Mechanics.Bezier;
using Scr.Mechanics.Car;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Scr.Installers
{
    public class ParkMasterSceneInstaller : MonoInstaller
    {
        [SerializeField] private BezierSettingsConfig bezierSettingsConfig;
        [SerializeField] private CarsPathesConfig carsPathesConfig;
        [SerializeField] private Camera raycastCamera;

        [Inject] private PrefabPathConfig _prefabPathConfig;
    
        public override void InstallBindings()
        {
            Container.Bind<BezierSettingsConfig>().FromScriptableObject(bezierSettingsConfig).AsSingle();
            Container.Bind<CarsPathesConfig>().FromScriptableObject(carsPathesConfig).AsSingle();
            Container.BindInterfacesTo<BezierPointsCreator>().AsSingle().Lazy();



            Container.BindInterfacesAndSelfTo<Input.InputMaster>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ObjectSelector>().AsSingle().NonLazy();
            Container.BindInterfacesTo<RaycastingSystem>().AsSingle();

            Container.Bind<Camera>().FromInstance(raycastCamera).WhenInjectedInto<IRaycastingSystem>();

            BindCars();

            // Container.BindIFactory<>()<LineRendererPool>().WithInitialSize(5).FromNewComponentOnNewGameObject().NonLazy();
        }

        private void BindCars()
        {
            Container.BindInterfacesTo<ObjectPathMover>().AsTransient();

            Container.BindIFactory<CarType, CarController>().FromSubContainerResolve().ByNewPrefabResourceMethod(
                    _prefabPathConfig.blueCarPath,
                    (container, type) =>
                    {
                        var carModel = new CarModel(type, 25);
                        container.Bind<CarModel>().FromInstance(carModel).AsSingle();
                        container.BindInterfacesTo<ObjectPathMover>().AsSingle();
                        container.BindInterfacesAndSelfTo<LineRendererPathBuilder>()
                                .FromComponentInNewPrefabResource(_prefabPathConfig.lineRendererPathBuilder).AsSingle();
                        container.Bind<CarController>().FromComponentOnRoot().AsSingle();
                    });
            
            void CarInject(CarType type, DiContainer subContainer)
            {
                var model = new CarModel(type, 0.5f); // todo add Cars config to store cars parameters
                subContainer.BindInterfacesAndSelfTo<ObjectPathMover>().AsSingle();
                subContainer.BindInstance(model).AsSingle();
                subContainer.Bind<CarController>().FromComponentsOnRoot().AsSingle();
            }
            
            // Container.BindInterfacesAndSelfTo<CarFactory>().AsSingle();
        }

    }
}
