
using UnityEngine;

namespace GBB.Core
{
    [CreateAssetMenu]
    public class CoreModule : GameModule
    {
        public int seed;

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

            //Добавляем системы рендеринга

            //Добавляем потиковые системы

        }

        public override void InjectData(GameStartup startup)
        {
            //Создаём компонент данных
            CoreData coreData = startup.AddDataObject().AddComponent<CoreData>();

            //Переносим в него данные
            coreData.seed = seed;

            //Вводим данные
            startup.InjectData(coreData);
        }
    }
}
