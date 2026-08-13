
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
            startup.InitSystem_Add(
                System_New<S_Map_Creation>(SystemWeight.SystemWeight));

            //Создаём основные компоненты провинций
            startup.InitSystem_Add(
                System_New<S_ProvinceCore_Creation>(SystemWeight.SystemWeight));
            #endregion

            //Добавляем покадровые системы

            //Добавляем системы рендеринга

            //Добавляем потиковые системы
            #region Tick
            //Создание карт
            startup.TickSystem_Add(
                System_New<S_Map_Creation>(SystemWeight.SystemWeight));

            //Создаём основные компоненты провинций
            startup.TickSystem_Add(
                System_New<S_ProvinceCore_Creation>(SystemWeight.SystemWeight));
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
