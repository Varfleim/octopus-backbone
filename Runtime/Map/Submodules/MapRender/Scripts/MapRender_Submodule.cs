
using UnityEngine;

namespace GBB.Map.Render
{
    internal class MapRender_Submodule : GameSubmodule
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
            startup.InitSystem_Add(
                System_New<S_MapModes_CreationMain>(SystemWeight.SystemWeight));
            #endregion

            //Добавляем покадровые системы

            //Добавляем системы рендеринга
            #region Render
            //Управление картами
            startup.RenderSystem_Add(
                System_New<S_Map_Control>(SystemWeight.PreSystemWeight));

            //Управление режимами карты
            startup.RenderSystem_Add(
                System_New<S_MapMode_Control>(SystemWeight.PreSystemWeight));

            //Обновление цветов режимов карты
            startup.RenderSystem_Add(
                System_New<S_MapModes_UpdateColors>(SystemWeight.SystemWeight));

            //Непосредственно рендер карты
            startup.RenderSystem_AddGroup(
                //Условие работы
                new MapRender_Solver(),
                //Изменение параметров рендера карты
                System_New<S_Map_Render>(SystemWeight.PostSystemWeight));
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
