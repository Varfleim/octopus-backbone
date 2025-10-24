
using UnityEngine;

namespace GBB.Map
{
    internal class MapModule : GameSubmodule
    {
        [SerializeField]
        private MapData mapData;
        [SerializeField]
        private MapModeData mapModeData;
        [SerializeField]
        private ProvinceData provinceData;

        public override void AddSystems(GameStartup startup)
        {
            //Добавляем системы инициализации
            #region PreInit

            #endregion
            #region Init
            //Создание карт и запросов генерации
            startup.AddInitSystem(new SMapCreation());

            //Создание главных компонентов провинций
            //startup.AddInitSystem(new SProvinceCoreCreation());

            //Создание главных компонентов режимов карты
            startup.AddInitSystem(new SMainMapModesCreation());
            #endregion

            //Добавляем покадровые системы


            //Добавляем системы рендеринга
            #region PreRender
            //Управление картами
            startup.AddPreRenderSystem(new SMapControl());

            //Активация режимов карты
            startup.AddPreRenderSystem(new SMapModesActivation());
            //Включение группы систем визуализации режимов карты
            startup.AddPreRenderSystem(new SMapModeRenderStart());
            #endregion
            #region Render
            //Обновление цветов режимов карты
            startup.AddRenderSystem(new SMapModesUpdateColors());
            #endregion
            #region PostRender
            //Изменение параметров рендера карты
            startup.AddPostRenderSystem(new SMapRender());

            //Выключение группы систем визуализации режима карты
            startup.AddPostRenderSystem(new SMapModeRenderEnd());
            #endregion

            //Добавляем потиковые системы
            #region PostTick
            //Запрос обновления активного режима карты
            startup.AddPostTickSystem(new SMapModeUpdate());
            #endregion
        }

        public override void InjectData(GameStartup startup)
        {
            //Вводим данные
            startup.InjectData(mapData);

            //Вводим данные
            startup.InjectData(mapModeData);

            //Вводим данные
            startup.InjectData(provinceData);
        }
    }
}
