
using UnityEngine;

namespace GBB.Map
{
    internal class Map_Submodule : GameSubmodule
    {
        [SerializeField]
        private Map_Data mapData;

        public override void Systems_Add(GameStartup startup)
        {
            //Добавляем системы инициализации
            #region Init
            //Создание карт
            startup.InitSystem_Add(new S_Map_Creation());
            #endregion
            #region PostInit
            //Создаём основные компоненты провинций
            startup.PostInitSystem_Add(new S_ProvinceCore_Creation());
            #endregion

            //Добавляем покадровые системы

            //Добавляем системы рендеринга

            //Добавляем потиковые системы
            #region PreTick
            //Создание карт
            startup.PreTickSystem_Add(new S_Map_Creation());
            #endregion
            #region PostTick
            //Создаём основные компоненты провинций
            startup.PostTickSystem_Add(new S_ProvinceCore_Creation());
            #endregion
        }

        public override void Aspects_Add(
            GameStartup startup,
            A_Aspect parentAspect)
        {
            //Создаём аспекты и присоединяем их к родительскому
            A_Map map_A = new();
            parentAspect.childrenAspects.Add(map_A);
        }

        public override void Data_Inject(GameStartup startup)
        {
            //Вводим данные
            startup.Data_Inject(mapData);
        }
    }
}
