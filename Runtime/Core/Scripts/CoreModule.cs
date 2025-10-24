
using UnityEngine;

namespace GBB.Core
{
    internal class CoreModule : GameSubmodule
    {
        [SerializeField]
        private CoreData coreData;

        public override void AddSystems(GameStartup startup)
        {
            //Добавляем системы инициализации
            #region PreInit
            //Инициализация RNG
            startup.AddPreInitSystem(new SRandom());
            #endregion
            #region Init

            #endregion
            #region PostInit
            //Очистка событий
            startup.AddPostInitSystem(new SEventsClear());
            #endregion

            //Добавляем покадровые системы
            #region PostFrame
            //Очистка событий
            startup.AddPostFrameSystem(new SEventsClear());
            #endregion

            //Добавляем системы рендеринга
            #region PostRender
            //Очистка событий
            startup.AddPostRenderSystem(new SEventsClear());
            #endregion

            //Добавляем потиковые системы
            #region PostTick
            //Очистка событий
            startup.AddPostTickSystem(new SEventsClear());
            #endregion
        }

        public override void InjectData(GameStartup startup)
        {
            //Вводим данные
            startup.InjectData(coreData);
        }
    }
}
