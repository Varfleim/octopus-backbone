
using UnityEngine;

namespace GBB.Core
{
    internal class Core_Submodule : GameSubmodule
    {
        [SerializeField]
        private Core_Data coreData;

        public override void Systems_Add(GameStartup startup)
        {
            //Добавляем системы инициализации
            #region PreInit
            //Инициализация RNG
            startup.PreInitSystem_Add(new S_Random_Test());
            #endregion
            #region PostInit
            //Очистка событий
            startup.PostInitSystem_Add(new S_Events_Clear());
            #endregion

            //Добавляем покадровые системы
            #region PostFrame
            //Очистка событий
            startup.PostFrameSystem_Add(new S_Events_Clear());
            #endregion

            //Добавляем системы рендеринга
            #region PostRender
            //Очистка событий
            startup.PostRenderSystem_Add(new S_Events_Clear());
            #endregion

            //Добавляем потиковые системы
            #region PostTick
            //Очистка событий
            startup.PostTickSystem_Add(new S_Events_Clear());
            #endregion
        }

        public override void Data_Inject(GameStartup startup)
        {
            //Вводим данные
            startup.Data_Inject(coreData);
        }
    }
}
