
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map.Render
{
    public class S_Map_Render : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_ProvinceRender> pR_P = default;

        readonly EcsPoolInject<R_Map_UpdateProvincesRender> map_UpdatePR_R_P = default;

        public void Run(IEcsSystems systems)
        {
            //Обновляем данные граней
            Map_EdgesUpdateData();

            //Обновляем данные режимов карты
            MapModes_UpdateData();
        }

        void Map_EdgesUpdateData()
        {
            //Проверяем, какие грани требуется обновить
            bool isThinUpdated = false;
            bool isThickUpdated = false;

            //Если фильтр обновления тонких граней не пуст
            if (pR_UpdateThinEdges_SR_F.Value.GetEntitiesCount() > 0)
            {
                //Обновляем тонкие грани
                Map_ThinEdges_UpdateData(out isThinUpdated);
            }

            //Если фильтр обновления толстых граней не пуст
            if(pR_UpdateThickEdges_SR_F.Value.GetEntitiesCount() > 0)
            {
                //Обновляем толстые грани
                Map_ThickEdges_UpdateData(out isThickUpdated);
            }

            //Если какие-либо грани были обновлены
            if(isThinUpdated == true
                || isThickUpdated == true)
            {
                //Запрашиваем обновление граней карты
                Map_UpdateEdges_Request(
                    isThinUpdated, isThickUpdated);
            }
        }

        readonly EcsPoolInject<R_Map_UpdateEdges> map_UpdateEdges_R_P = default;
        void Map_UpdateEdges_Request(
            bool isThinUpdated, bool isThickUpdated)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.Value.NewEntity();
            ref R_Map_UpdateEdges requestComp = ref map_UpdateEdges_R_P.Value.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                isThinUpdated, isThickUpdated, false);
        }

        readonly EcsFilterInject<Inc<C_ProvinceRender, SR_ProvinceRender_UpdateThinEdges>> pR_UpdateThinEdges_SR_F = default;
        readonly EcsFilterInject<Inc<C_ProvinceRender>, Exc<SR_ProvinceRender_UpdateThinEdges>> pR_WithoutUpdateThinEdges_SR_F = default;
        readonly EcsPoolInject<SR_ProvinceRender_UpdateThinEdges> pR_UpdateThinEdges_SR_P = default;
        void Map_ThinEdges_UpdateData(
            out bool isThinUpdated)
        {
            //Устанавливаем значение по умолчанию
            isThinUpdated = false;

            //Для каждой провинции без запроса обновления тонких граней
            foreach (int provinceEntity in pR_WithoutUpdateThinEdges_SR_F.Value)
            {
                //Берём провинцию
                ref C_ProvinceRender pR = ref pR_P.Value.Get(provinceEntity);

                //Если индекс тонких граней был обновлён
                if(ProvinceRender_UpdateThinEdgesIndex(
                    ref pR,
                    -1) == true)
                {
                    //Отмечаем, что грани обновлены
                    isThinUpdated = true;
                }
            }

            //Если нет провинций с запросом
            if (pR_UpdateThinEdges_SR_F.Value.GetEntitiesCount() == 0)
            {
                //Отмечаем, что требуется обновление карты
                isThinUpdated = true;
            }

            //Для каждой провинции с запросом
            foreach (int provinceEntity in pR_UpdateThinEdges_SR_F.Value)
            {
                //Берём провинцию и запрос
                ref C_ProvinceRender pR = ref pR_P.Value.Get(provinceEntity);
                ref SR_ProvinceRender_UpdateThinEdges requestComp = ref pR_UpdateThinEdges_SR_P.Value.Get(provinceEntity);

                //Если индекс тонких граней был обновлён
                if (ProvinceRender_UpdateThinEdgesIndex(
                    ref pR,
                    requestComp.edgeIndex) == true)
                {
                    //Отмечаем, что грани обновлены
                    isThinUpdated = true;
                }

                //Удаляем запрос
                pR_UpdateThinEdges_SR_P.Value.Del(provinceEntity);
            }
        }

        bool ProvinceRender_UpdateThinEdgesIndex(
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

        readonly EcsFilterInject<Inc<C_ProvinceRender, SR_ProvinceRender_UpdateThickEdges>> pR_UpdateThickEdges_SR_F = default;
        readonly EcsFilterInject<Inc<C_ProvinceRender>, Exc<SR_ProvinceRender_UpdateThickEdges>> pR_WithoutUpdateThickEdges_SR_F = default;
        readonly EcsPoolInject<SR_ProvinceRender_UpdateThickEdges> pR_UpdateThickEdges_SR_P = default;
        void Map_ThickEdges_UpdateData(
            out bool isThickUpdated)
        {
            //Устанавливаем значение по умолчанию
            isThickUpdated = false;

            //Для каждой провинции без запроса обновления толстых граней
            foreach (int provinceEntity in pR_WithoutUpdateThickEdges_SR_F.Value)
            {
                //Берём провинцию
                ref C_ProvinceRender pR = ref pR_P.Value.Get(provinceEntity);

                //Если индекс толстых граней был обновлён
                if (ProvinceRender_UpdateThickEdgesIndex(
                    ref pR,
                    -1) == true)
                {
                    //Отмечаем, что грани обновлены
                    isThickUpdated = true;
                }
            }

            //Если нет провинций с запросом
            if (pR_UpdateThickEdges_SR_F.Value.GetEntitiesCount() == 0)
            {
                //Отмечаем, что требуется обновление карты
                isThickUpdated = true;
            }

            //Для каждой провинции с запросом
            foreach (int provinceEntity in pR_UpdateThickEdges_SR_F.Value)
            {
                //Берём провинцию и запрос
                ref C_ProvinceRender pR = ref pR_P.Value.Get(provinceEntity);
                ref SR_ProvinceRender_UpdateThickEdges requestComp = ref pR_UpdateThickEdges_SR_P.Value.Get(provinceEntity);

                //Если индекс толстых граней был обновлён
                if (ProvinceRender_UpdateThickEdgesIndex(
                    ref pR,
                    requestComp.edgeIndex) == true)
                {
                    //Отмечаем, что грани обновлены
                    isThickUpdated = true;
                }

                //Удаляем запрос
                pR_UpdateThickEdges_SR_P.Value.Del(provinceEntity);
            }
        }

        bool ProvinceRender_UpdateThickEdgesIndex(
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

        readonly EcsFilterInject<Inc<C_MapModeCore, SR_MapMode_Update>> mM_Updated_F = default;
        readonly EcsPoolInject<C_MapModeCore> mMC_P = default;
        void MapModes_UpdateData()
        {
            //Для каждого режима карты с запросом обновления
            foreach (int activeMapModeEntity in mM_Updated_F.Value)
            {
                //Берём режим карты
                ref C_MapModeCore activeMapMode = ref mMC_P.Value.Get(activeMapModeEntity);

                //Обновляем данные визуализации провинций
                Map_ProvinceRenderUpdateData(
                    ref activeMapMode,
                    out bool isHeightUpdated,
                    out bool isColorUpdated);

                //Запрашиваем обновление визуализации провинций карты
                MapRender_Data.Map_UpdateProvincesRender_Request(
                    world.Value,
                    map_UpdatePR_R_P.Value,
                    false, isHeightUpdated, isColorUpdated);
            }
        }

        readonly EcsFilterInject<Inc<C_ProvinceRender, SR_ProvinceRender_Update>> pR_Updated_F = default;
        readonly EcsFilterInject<Inc<C_ProvinceRender>, Exc<SR_ProvinceRender_Update>> pR_NotUpdated_F = default;
        readonly EcsPoolInject<SR_ProvinceRender_Update> pR_Update_SR_P = default;
        void Map_ProvinceRenderUpdateData(
            ref C_MapModeCore mapMode,
            out bool isHeightUpdated, out bool isColorUpdated)
        {
            //Устанавливаем значения по умолчанию
            isHeightUpdated = false;
            isColorUpdated = false;

            //Для каждой провинции без запроса обновления визуализации
            foreach (int provinceEntity in pR_NotUpdated_F.Value)
            {
                //Берём провинцию
                ref C_ProvinceRender pR = ref pR_P.Value.Get(provinceEntity);

                //Обновляем отображаемый объект провинции
                ProvinceRender_UpdateDisplayedObject(
                    ref mapMode,
                    ref pR,
                    new());

                //Изменяем параметры визуализации провинции
                //Если высота была обновлена
                if(ProvinceRender_UpdateHeight(
                    ref mapMode,
                    ref pR,
                    0.0f) == true)
                {
                    //Отмечаем, что высота обновлена
                    isHeightUpdated = true;
                }

                //Если индекс цвета был обновлён
                if(ProvinceRender_UpdateColorIndex(
                    ref mapMode,
                    ref pR,
                    -1) == true)
                {
                    //Отмечаем, что цвет обновлён
                    isColorUpdated = true;
                }
            }

            //Если нет провинций с запросом
            if(pR_Updated_F.Value.GetEntitiesCount() == 0)
            {
                //Отмечаем, что требуется обновление карты
                isHeightUpdated = true;
                isColorUpdated = true;
            }

            //Для каждой провинции с запросом
            foreach (int provinceEntity in pR_Updated_F.Value)
            {
                //Берём провинцию и запрос
                ref C_ProvinceRender pR = ref pR_P.Value.Get(provinceEntity);
                ref SR_ProvinceRender_Update requestComp = ref pR_Update_SR_P.Value.Get(provinceEntity);

                //Обновляем отображаемый объект провинции
                ProvinceRender_UpdateDisplayedObject(
                    ref mapMode,
                    ref pR,
                    requestComp.displayedObjectPE);

                //Изменяем параметры визуализации провинции
                //Если высота была обновлена
                if (ProvinceRender_UpdateHeight(
                    ref mapMode,
                    ref pR,
                    requestComp.height) == true)
                {
                    //Отмечаем, что высота обновлена
                    isHeightUpdated = true;
                }

                //Если индекс цвета был обновлён
                if (ProvinceRender_UpdateColorIndex(
                    ref mapMode,
                    ref pR,
                    requestComp.colorIndex) == true)
                {
                    //Отмечаем, что цвет обновлён
                    isColorUpdated = true;
                }

                //Удаляем запрос
                pR_Update_SR_P.Value.Del(provinceEntity);
            }
        }

        void ProvinceRender_UpdateDisplayedObject(
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

        bool ProvinceRender_UpdateHeight(
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

        bool ProvinceRender_UpdateColorIndex(
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
