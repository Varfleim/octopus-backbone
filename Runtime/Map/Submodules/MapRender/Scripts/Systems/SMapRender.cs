
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map.Render
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
                MapEdgesUpdateRequest(
                    isThinUpdated, isThickUpdated);
            }
        }

        readonly EcsPoolInject<R_MapEdgesUpdate> mapEdgesUpdateRPool = default;
        void MapEdgesUpdateRequest(
            bool isThinUpdated, bool isThickUpdated)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.Value.NewEntity();
            ref R_MapEdgesUpdate requestComp = ref mapEdgesUpdateRPool.Value.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                isThinUpdated, isThickUpdated, false);
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
                if(UpdateProvinceThinEdgesIndex(
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
                if (UpdateProvinceThinEdgesIndex(
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

        bool UpdateProvinceThinEdgesIndex(
            ref C_ProvinceRender pR,
            int newThinEdgesIndex)
        {
            //Если индекс тонких граней провинции не равен переданному
            if (pR.ThinEdgesIndex != newThinEdgesIndex)
            {
                //Обновляем его
                pR.ThinEdgesIndex = newThinEdgesIndex;

                //Возвращаем, что индекс был обновлён
                return true;
            }
            //Иначе
            else
            {
                //Возвращаем, что индекс не был обновлён
                return false;
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
                if (UpdateProvinceThickEdgesIndex(
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
                if (UpdateProvinceThickEdgesIndex(
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

        bool UpdateProvinceThickEdgesIndex(
            ref C_ProvinceRender pR,
            int newThickEdgesIndex)
        {
            //Если индекс толстых граней провинции не равен переданному
            if (pR.ThickEdgesIndex != newThickEdgesIndex)
            {
                //Обновляем его
                pR.ThickEdgesIndex = newThickEdgesIndex;

                //Возвращаем, что индекс был обновлён
                return true;
            }
            //Иначе
            else
            {
                //Возвращаем, что индекс не был обновлён
                return false;
            }
        }

        readonly EcsFilterInject<Inc<C_MapModeCore, SR_MapModeUpdate>> activeMapModeFilter = default;
        readonly EcsPoolInject<C_MapModeCore> mapModePool = default;
        void MapModesDataUpdate()
        {
            //Для каждого режима карты с запросом обновления
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
                MapRenderData.MapProvincesUpdateRequest(
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
                UpdateProvinceDisplayedObject(
                    ref mapMode,
                    ref pR,
                    new());

                //Изменяем параметры визуализации провинции
                //Если высота была обновлена
                if(UpdateProvinceHeight(
                    ref mapMode,
                    ref pR,
                    0.0f) == true)
                {
                    //Отмечаем, что высота обновлена
                    isHeightUpdated = true;
                }

                //Если индекс цвета был обновлён
                if(UpdateProvinceColorIndex(
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
                UpdateProvinceDisplayedObject(
                    ref mapMode,
                    ref pR,
                    requestComp.displayedObjectPE);

                //Изменяем параметры визуализации провинции
                //Если высота была обновлена
                if (UpdateProvinceHeight(
                    ref mapMode,
                    ref pR,
                    requestComp.height) == true)
                {
                    //Отмечаем, что высота обновлена
                    isHeightUpdated = true;
                }

                //Если индекс цвета был обновлён
                if (UpdateProvinceColorIndex(
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

        void UpdateProvinceDisplayedObject(
            ref C_MapModeCore mapMode,
            ref C_ProvinceRender pR,
            EcsPackedEntity displayedObjectPE)
        {
            //Если отображаемый объект провинции не равен переданному
            if (pR.DisplayedObjectPE.EqualsTo(in displayedObjectPE) == false)
            {
                //Обновляем его
                pR.DisplayedObjectPE = displayedObjectPE;
            }
        }

        bool UpdateProvinceHeight(
            ref C_MapModeCore mapMode,
            ref C_ProvinceRender pR,
            float newProvinceHeight)
        {
            //Если высота провинции не равна переданной
            if (pR.ProvinceHeight != newProvinceHeight)
            {
                //Обновляем её
                pR.ProvinceHeight = newProvinceHeight;

                //Возвращаем, что высота была обновлена
                return true;
            }
            //Иначе
            else
            {
                //Возвращаем, что высота не была обновлена
                return false;
            }
        }

        bool UpdateProvinceColorIndex(
            ref C_MapModeCore mapMode,
            ref C_ProvinceRender pR,
            int newProvinceColorIndex)
        {
            //Если индекс цвета провинции не равен переданному
            if (pR.ProvinceColorIndex != newProvinceColorIndex)
            {
                //Обновляем его
                pR.ProvinceColorIndex = (newProvinceColorIndex);

                //Возвращаем, что индекс цвета был обновлён
                return true;
            }
            //Иначе
            else
            {
                //Возвращаем, что индекс цвета не был обновлён
                return false;
            }
        }
    }
}
