
using UnityEngine;

namespace GBB.Map
{
    internal class MapSubmodule : GameSubmodule
    {
        [SerializeField]
        private MapData mapData;

        public override void AddSystems(GameStartup startup)
        {
            //Добавляем системы инициализации
            #region Init
            //Создание карт
            startup.AddInitSystem(new SMapCreation());

            //Создание главных компонентов провинций
            //startup.AddInitSystem(new SProvinceCoreCreation());
            #endregion

            //Добавляем покадровые системы

            //Добавляем системы рендеринга

            //Добавляем потиковые системы
            #region PreTick
            //Создание карт
            startup.AddPreTickSystem(new SMapCreation());
            #endregion
        }

        public override void InjectData(GameStartup startup)
        {
            //Вводим данные
            startup.InjectData(mapData);
        }
    }
}
