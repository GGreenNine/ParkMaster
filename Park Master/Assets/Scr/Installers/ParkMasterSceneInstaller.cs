using Scr.Configs;
using Scr.Input;
using Scr.Mechanics.Bezier;
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

            Container.BindInterfacesAndSelfTo<LineRendererPathBuilder>()
                    .FromComponentInNewPrefabResource(_prefabPathConfig.lineRendererPathBuilder).AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<InputMasterObservable>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ObjectSelector>().AsSingle().NonLazy();
            Container.BindInterfacesTo<RaycastingSystem>().AsSingle();

            Container.Bind<Camera>().FromInstance(raycastCamera).WhenInjectedInto<IRaycastingSystem>();

            // Container.BindIFactory<>()<LineRendererPool>().WithInitialSize(5).FromNewComponentOnNewGameObject().NonLazy();
        }
    
    }
}
