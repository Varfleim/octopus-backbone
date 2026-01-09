
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

            //Добавляем покадровые системы

            //Добавляем системы рендеринга

            //Добавляем потиковые системы
            #region PreTick
            //Создание карт
            startup.PreTickSystem_Add(new S_Map_Creation());
            #endregion
        }

        public override void Data_Inject(GameStartup startup)
        {
            //Вводим данные
            startup.Data_Inject(mapData);
        }
    }
}
