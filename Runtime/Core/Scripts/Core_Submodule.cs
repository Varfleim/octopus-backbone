
using UnityEngine;

namespace GBB.Core
{
    internal class Core_Submodule : GameSubmodule
    {
        [SerializeField]
        private Core_Data coreData;

        public override void Systems_Add(
            GameStartup startup)
        {
            //Добавляем системы инициализации
            #region Init
            //Инициализация RNG
            startup.InitSystem_Add(
                System_New<S_Random_Test>(SystemWeight.StartSystemWeight));

            //Очистка событий
            startup.InitSystem_Add(
                System_New<S_Events_Clear>(SystemWeight.EndSystemWeight));
            #endregion

            //Добавляем покадровые системы
            #region Frame
            //Очистка событий
            startup.FrameSystem_Add(
                System_New<S_Events_Clear>(SystemWeight.EndSystemWeight));
            #endregion

            //Добавляем системы рендеринга
            #region Render
            //Очистка событий
            startup.RenderSystem_Add(
                System_New<S_Events_Clear>(SystemWeight.EndSystemWeight));
            #endregion

            //Добавляем потиковые системы
            #region Tick
            //Очистка событий
            startup.TickSystem_Add(
                System_New<S_Events_Clear>(SystemWeight.EndSystemWeight));
            #endregion
        }

        public override void Aspects_Add(
            GameStartup startup,
            A_Aspect parentAspect)
        {
            //Создаём аспекты и присоединяем их к родительскому
            //Aspect# = new();
            //parentAspect.childrenAspects.Add(Aspect#);
        }

        public override void Data_Inject(
            GameStartup startup)
        {
            //Вводим данные
            startup.Data_Inject(coreData);
        }
    }
}
