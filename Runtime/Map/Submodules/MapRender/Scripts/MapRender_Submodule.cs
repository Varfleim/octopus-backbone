
using UnityEngine;

namespace GBB.Map.Render
{
    public class MapRender_Submodule : GameSubmodule
    {
        [SerializeField]
        private MapRender_Data mapRenderData;
        [SerializeField]
        private MainMapMode_Data mainMapModeData;

        public override void Systems_Add(GameStartup startup)
        {
            //Добавляем системы инициализации
            #region Init
            //Создание главных компонентов режимов карты
            startup.InitSystem_Add(new S_MapModes_CreationMain());
            #endregion

            //Добавляем покадровые системы

            //Добавляем системы рендеринга
            #region PreRender
            //Управление картами
            startup.PreRenderSystem_Add(new S_Map_Control());

            //Управление режимами карты
            startup.PreRenderSystem_Add(new S_MapMode_Control());
            //Включение группы систем визуализации режимов карты
            startup.PreRenderSystem_Add(new S_MapMode_RenderStart());
            #endregion
            #region Render
            //Обновление цветов режимов карты
            startup.RenderSystem_Add(new S_MapModes_UpdateColors());
            #endregion
            #region PostRender
            //Изменение параметров рендера карты
            startup.PostRenderSystem_Add(new S_Map_Render());

            //Выключение группы систем визуализации режима карты
            startup.PostRenderSystem_Add(new S_MapMode_RenderEnd());
            #endregion

            //Добавляем потиковые системы
            #region PostTick
            //Запрос обновления активного режима карты
            startup.PostTickSystem_Add(new S_MapMode_Update());
            #endregion

        }

        public override void Data_Inject(GameStartup startup)
        {
            //Вводим данные
            startup.Data_Inject(mapRenderData);

            //Вводим данные
            startup.Data_Inject(mainMapModeData);
        }
    }
}
