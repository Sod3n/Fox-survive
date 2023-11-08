using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Controlls
{
    [CreateAssetMenu(fileName = "Installer", menuName = "Installers/ControllsInstaller")]
    public class Installer : ScriptableObjectInstaller<Installer>
    {
        public override void InstallBindings()
        {
            Container.Bind<StandartControlls>().AsSingle();
        }
    }
}
