using Scr.Configs;
using UnityEngine;
using Zenject;

namespace Scr.Installers
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private PrefabPathConfig _prefabPathConfig;

        public override void InstallBindings()
        {
            Container.Bind<PrefabPathConfig>().FromScriptableObject(_prefabPathConfig).AsSingle();
        }
    }
}
