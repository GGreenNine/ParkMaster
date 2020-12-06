using Scr.Configs;
using Scr.Mechanics.Bezier;
using UnityEngine;
using Zenject;

namespace Scr.Installers
{
    public class ParkMasterSceneInstaller : MonoInstaller
    {
        [SerializeField] private BezierSettingsConfig bezierSettingsConfig;
        [SerializeField] private CarsPathesConfig carsPathesConfig;
    
        public override void InstallBindings()
        {
            Container.Bind<BezierSettingsConfig>().FromScriptableObject(bezierSettingsConfig).AsSingle();
            Container.Bind<CarsPathesConfig>().FromScriptableObject(carsPathesConfig).AsSingle();
            Container.BindInterfacesTo<BezierPointsCreator>().AsSingle().Lazy();

            // Container.BindIFactory<>()<LineRendererPool>().WithInitialSize(5).FromNewComponentOnNewGameObject().NonLazy();
        }
    
    }
}
