using System;
using Managment;
using Scr.Configs;
using Scr.Input;
using Scr.Mechanics;
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

            Container.BindInterfacesAndSelfTo<InGameBonusCollector>().AsSingle();
            Container.BindInterfacesTo<InGamePathCollector>().AsSingle();
            Container.BindInterfacesTo<InGameCarMovesCollector>().AsSingle();

            Container.BindInterfacesAndSelfTo<Input.InputMaster>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ObjectSelector>().AsSingle().NonLazy();
            Container.BindInterfacesTo<RaycastingSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateHolder>().AsSingle();

            Container.Bind<Camera>().FromInstance(raycastCamera).WhenInjectedInto<IRaycastingSystem>();
            Container.BindInterfacesTo<LevelLoader>().AsSingle();
            
            BindCars();
            BindBonuses();

            // Container.BindIFactory<>()<LineRendererPool>().WithInitialSize(5).FromNewComponentOnNewGameObject().NonLazy();
        }

        private void BindCars()
        {
            Container.BindInterfacesTo<ObjectPathMover>().AsTransient();

            Container.BindIFactory<CarModel, CarController>().FromSubContainerResolve().ByMethod(
                    (container, model) =>
                    {
                        container.Bind<CarModel>().FromInstance(model).AsSingle();
                        container.BindInterfacesTo<ObjectPathMover>().AsSingle();
                        container.BindInterfacesAndSelfTo<LineRendererPathBuilder>()
                                .FromComponentInNewPrefabResource(_prefabPathConfig.lineRendererPathBuilder).AsSingle();
                        var car = container.InstantiatePrefabResource(_prefabPathConfig.GetCarPathByCarType(model.CarType)).GetComponent<CarController>();
                        container.Bind<CarController>().FromInstance(car);
                    });
        }

        private void BindBonuses()
        {
            Container.BindIFactory<InGameBonusType, InGameTriggeredBonusBase>().FromSubContainerResolve().ByMethod(
                (container, type) =>
                {
                    var car = container.InstantiatePrefabResource(_prefabPathConfig.GetBonusPathByBonusType(type)).GetComponent<InGameTriggeredBonusBase>();
                    container.Bind<InGameTriggeredBonusBase>().FromInstance(car);
                });
        }

    }

}
