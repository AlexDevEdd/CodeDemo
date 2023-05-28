using _Game.Scripts.Components.Base;
using _Game.Scripts.Systems.Base;

namespace _Game.Scripts.Components.Loader
{
    /// <summary>
    /// Класс, добавляет задержку в конце загрузки.
    /// </summary>
    public class EndLoadingComponent : BaseComponent
    {
        public override void Start()
        {
            InvokeSystem.StartExternalInvoke(End, 90);
        }
    }
}