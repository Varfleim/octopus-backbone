
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapRender : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_ProvinceRender> pRPool = default;

        readonly EcsPoolInject<R_MapProvincesUpdate> mapProvincesUpdateRPool = default;
        
        public void Run(IEcsSystems systems)
        {
            //Обновляем данные граней
            MapEdgesDataUpdate();

            //Обновляем данные режимов карты
            MapModesDataUpdate();
        }

        readonly EcsPoolInject<R_MapEdgesUpdate> mapEdgesUpdateRPool = default;
        void MapEdgesDataUpdate()
        {
            //Проверяем, какие грани требуется обновить
            bool isThinUpdated = false;
            bool isThickUpdated = false;

            //Если фильтр обновления тонких граней не пуст
            if (provinceUpdateThinEdgesSRFilter.Value.GetEntitiesCount() > 0)
            {
                //Обновляем тонкие грани
                MapThinEdgesDataUpdate(out isThinUpdated);
            }

            //Если фильтр обновления толстых граней не пуст
            if(provinceUpdateThickEdgesSRFilter.Value.GetEntitiesCount() > 0)
            {
                //Обновляем толстые грани
                MapThickEdgesDataUpdate(out isThickUpdated);
            }

            //Если какие-либо грани были обновлены
            if(isThinUpdated == true
                || isThickUpdated == true)
            {
                //Запрашиваем обновление граней карты
                MapData.MapEdgesUpdateRequest(
                    world.Value,
                    mapEdgesUpdateRPool.Value,
                    isThinUpdated, isThickUpdated);
            }
        }

        readonly EcsFilterInject<Inc<C_ProvinceRender, SR_UpdateThinEdges>> provinceUpdateThinEdgesSRFilter = default;
        readonly EcsFilterInject<Inc<C_ProvinceRender>, Exc<SR_UpdateThinEdges>> provinceWithoutUpdateThinEdgesSRFilter = default;
        readonly EcsPoolInject<SR_UpdateThinEdges> updateThinEdgesSRPool = default;
        void MapThinEdgesDataUpdate(
            out bool isThinUpdated)
        {
            //Устанавливаем значение по умолчанию
            isThinUpdated = false;

            //Для каждой провинции без запроса обновления тонких граней
            foreach (int provinceEntity in provinceWithoutUpdateThinEdgesSRFilter.Value)
            {
                //Берём провинцию
                ref C_ProvinceRender pR = ref pRPool.Value.Get(provinceEntity);

                //Если индекс тонких граней был обновлён
                if(MapModeData.UpdateProvinceThinEdgesIndex(
                    ref pR,
                    -1) == true)
                {
                    //Отмечаем, что грани обновлены
                    isThinUpdated = true;
                }
            }

            //Если нет провинций с запросом
            if (provinceUpdateThinEdgesSRFilter.Value.GetEntitiesCount() == 0)
            {
                //Отмечаем, что требуется обновление карты
                isThinUpdated = true;
            }

            //Для каждой провинции с запросом
            foreach (int provinceEntity in provinceUpdateThinEdgesSRFilter.Value)
            {
                //Берём провинцию и запрос
                ref C_ProvinceRender pR = ref pRPool.Value.Get(provinceEntity);
                ref SR_UpdateThinEdges requestComp = ref updateThinEdgesSRPool.Value.Get(provinceEntity);

                //Если индекс тонких граней был обновлён
                if (MapModeData.UpdateProvinceThinEdgesIndex(
                    ref pR,
                    requestComp.edgeIndex) == true)
                {
                    //Отмечаем, что грани обновлены
                    isThinUpdated = true;
                }

                //Удаляем запрос
                updateThinEdgesSRPool.Value.Del(provinceEntity);
            }
        }

        readonly EcsFilterInject<Inc<C_ProvinceRender, SR_UpdateThickEdges>> provinceUpdateThickEdgesSRFilter = default;
        readonly EcsFilterInject<Inc<C_ProvinceRender>, Exc<SR_UpdateThickEdges>> provinceWithoutUpdateThickEdgesSRFilter = default;
        readonly EcsPoolInject<SR_UpdateThickEdges> updateThickEdgesSRPool = default;
        void MapThickEdgesDataUpdate(
            out bool isThickUpdated)
        {
            //Устанавливаем значение по умолчанию
            isThickUpdated = false;

            //Для каждой провинции без запроса обновления толстых граней
            foreach (int provinceEntity in provinceWithoutUpdateThickEdgesSRFilter.Value)
            {
                //Берём провинцию
                ref C_ProvinceRender pR = ref pRPool.Value.Get(provinceEntity);

                //Если индекс толстых граней был обновлён
                if (MapModeData.UpdateProvinceThickEdgesIndex(
                    ref pR,
                    -1) == true)
                {
                    //Отмечаем, что грани обновлены
                    isThickUpdated = true;
                }
            }

            //Если нет провинций с запросом
            if (provinceUpdateThickEdgesSRFilter.Value.GetEntitiesCount() == 0)
            {
                //Отмечаем, что требуется обновление карты
                isThickUpdated = true;
            }

            //Для каждой провинции с запросом
            foreach (int provinceEntity in provinceUpdateThickEdgesSRFilter.Value)
            {
                //Берём провинцию и запрос
                ref C_ProvinceRender pR = ref pRPool.Value.Get(provinceEntity);
                ref SR_UpdateThickEdges requestComp = ref updateThickEdgesSRPool.Value.Get(provinceEntity);

                //Если индекс толстых граней был обновлён
                if (MapModeData.UpdateProvinceThickEdgesIndex(
                    ref pR,
                    requestComp.edgeIndex) == true)
                {
                    //Отмечаем, что грани обновлены
                    isThickUpdated = true;
                }

                //Удаляем запрос
                updateThickEdgesSRPool.Value.Del(provinceEntity);
            }
        }

        readonly EcsFilterInject<Inc<C_MapModeCore, CT_ActiveMapMode, SR_MapModeUpdate>> activeMapModeFilter = default;
        readonly EcsPoolInject<C_MapModeCore> mapModePool = default;
        void MapModesDataUpdate()
        {
            //Для каждого активного режима карты с запросом обновления
            foreach (int activeMapModeEntity in activeMapModeFilter.Value)
            {
                //Берём режим карты
                ref C_MapModeCore activeMapMode = ref mapModePool.Value.Get(activeMapModeEntity);

                //Обновляем данные визуализации провинций
                MapProvinceRenderDataUpdate(
                    ref activeMapMode,
                    out bool isHeightUpdated,
                    out bool isColorUpdated);

                //Запрашиваем обновление визуализации провинций карты
                MapData.MapProvincesUpdateRequest(
                    world.Value,
                    mapProvincesUpdateRPool.Value,
                    false, isHeightUpdated, isColorUpdated);
            }
        }

        readonly EcsFilterInject<Inc<C_ProvinceRender, SR_UpdateProvinceRender>> provinceUpdateProvinceRenderSRFilter = default;
        readonly EcsFilterInject<Inc<C_ProvinceRender>, Exc<SR_UpdateProvinceRender>> provinceWithoutUpdateProvinceRenderSRFilter = default;
        readonly EcsPoolInject<SR_UpdateProvinceRender> updateProvinceRenderSRPool = default;
        void MapProvinceRenderDataUpdate(
            ref C_MapModeCore mapMode,
            out bool isHeightUpdated, out bool isColorUpdated)
        {
            //Устанавливаем значения по умолчанию
            isHeightUpdated = false;
            isColorUpdated = false;

            //Для каждой провинции без запроса обновления визуализации
            foreach (int provinceEntity in provinceWithoutUpdateProvinceRenderSRFilter.Value)
            {
                //Берём провинцию
                ref C_ProvinceRender pR = ref pRPool.Value.Get(provinceEntity);

                //Обновляем отображаемый объект провинции
                MapModeData.UpdateProvinceDisplayedObject(
                    ref mapMode,
                    ref pR,
                    new());

                //Изменяем параметры визуализации провинции
                //Если высота была обновлена
                if(MapModeData.UpdateProvinceHeight(
                    ref mapMode,
                    ref pR,
                    0.0f) == true)
                {
                    //Отмечаем, что высота обновлена
                    isHeightUpdated = true;
                }

                //Если индекс цвета был обновлён
                if(MapModeData.UpdateProvinceColorIndex(
                    ref mapMode,
                    ref pR,
                    -1) == true)
                {
                    //Отмечаем, что цвет обновлён
                    isColorUpdated = true;
                }
            }

            //Если нет провинций с запросом
            if(provinceUpdateProvinceRenderSRFilter.Value.GetEntitiesCount() == 0)
            {
                //Отмечаем, что требуется обновление карты
                isHeightUpdated = true;
                isColorUpdated = true;
            }

            //Для каждой провинции с запросом
            foreach (int provinceEntity in provinceUpdateProvinceRenderSRFilter.Value)
            {
                //Берём провинцию и запрос
                ref C_ProvinceRender pR = ref pRPool.Value.Get(provinceEntity);
                ref SR_UpdateProvinceRender requestComp = ref updateProvinceRenderSRPool.Value.Get(provinceEntity);

                //Обновляем отображаемый объект провинции
                MapModeData.UpdateProvinceDisplayedObject(
                    ref mapMode,
                    ref pR,
                    requestComp.displayedObjectPE);

                //Изменяем параметры визуализации провинции
                //Если высота была обновлена
                if (MapModeData.UpdateProvinceHeight(
                    ref mapMode,
                    ref pR,
                    requestComp.height) == true)
                {
                    //Отмечаем, что высота обновлена
                    isHeightUpdated = true;
                }

                //Если индекс цвета был обновлён
                if (MapModeData.UpdateProvinceColorIndex(
                    ref mapMode,
                    ref pR,
                    requestComp.colorIndex) == true)
                {
                    //Отмечаем, что цвет обновлён
                    isColorUpdated = true;
                }

                //Удаляем запрос
                updateProvinceRenderSRPool.Value.Del(provinceEntity);
            }
        }
    }
}
