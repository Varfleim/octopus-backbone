
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
            #endregion
            #region Render
            //Обновление цветов режимов карты
            startup.RenderSystem_Add(new S_MapModes_UpdateColors());
            #endregion
            #region PostRender
            //Изменение параметров рендера карты
            startup.PostRenderSystem_Add(new S_Map_Render());
            #endregion

            //Добавляем потиковые системы

        }

        public override void Aspects_Add(
            GameStartup startup,
            A_Aspect parentAspect)
        {
            //Создаём аспекты и присоединяем их к родительскому
            A_MapRender mapRender_A = new();
            parentAspect.childrenAspects.Add(mapRender_A);
            A_CoreMapMode coreMapMode_A = new();
            parentAspect.childrenAspects.Add(coreMapMode_A);
        }

        public override void Data_Inject(GameStartup startup)
        {
            //ТЕСТ
            GO_Province.provinceGOPrefab = mapRenderData.ProvinceGOPrefab;
            GO_ProvinceHighlight.provinceHighlightPrefab = mapRenderData.ProvinceHighlightGOPrefab;
            //ТЕСТ

            //Вводим данные
            startup.Data_Inject(mapRenderData);

            //Вводим данные
            startup.Data_Inject(mainMapModeData);
        }
    }
}
