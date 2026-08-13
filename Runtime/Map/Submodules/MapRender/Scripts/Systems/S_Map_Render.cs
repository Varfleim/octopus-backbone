
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Map.Render
{
    public class S_Map_Render : VFSystem, IProtoRunSystem
    {
        [DI] A_MapRender mapRender_A;
        [DI] A_CoreMapMode coreMapMode_A;

        public void Run()
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
            if (mapRender_A.pR_UpdateThinEdges_SR_I.IsEmptySlow() == false)
            {
                //Обновляем тонкие грани
                Map_ThinEdges_UpdateData(out isThinUpdated);
            }

            //Если фильтр обновления толстых граней не пуст
            if(mapRender_A.pR_UpdateThickEdges_SR_I.IsEmptySlow() == false)
            {
                //Обновляем толстые грани
                Map_ThickEdges_UpdateData(out isThickUpdated);
            }

            //Если какие-либо грани были обновлены
            if(isThinUpdated == true
                || isThickUpdated == true)
            {
                //Запрашиваем обновление граней карты
                Map_UpdateEdges_R(
                    isThinUpdated, isThickUpdated);
            }
        }

        void Map_UpdateEdges_R(
            bool isThinUpdated, bool isThickUpdated)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_Map_UpdateEdges rComp = ref mapRender_A.map_UpdateEdges_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                isThinUpdated, isThickUpdated, false);
        }

        void Map_ThinEdges_UpdateData(
            out bool isThinUpdated)
        {
            //Устанавливаем значение по умолчанию
            isThinUpdated = false;

            //Для каждой провинции без запроса обновления тонких граней
            foreach (ProtoEntity pEntity in mapRender_A.pR_WithoutUpdateThinEdges_SR_I)
            {
                //Берём провинцию
                ref C_ProvinceRender pR = ref mapRender_A.pR_P.Get(pEntity);

                //Если индекс тонких граней был обновлён
                if(PR_UpdateThinEdgesIndex(
                    ref pR,
                    -1) == true)
                {
                    //Отмечаем, что грани обновлены
                    isThinUpdated = true;
                }
            }

            //Если нет провинций с запросом
            if (mapRender_A.pR_UpdateThinEdges_SR_I.IsEmptySlow())
            {
                //Отмечаем, что требуется обновление карты
                isThinUpdated = true;
            }

            //Для каждой провинции с запросом
            foreach (ProtoEntity pEntity in mapRender_A.pR_UpdateThinEdges_SR_I)
            {
                //Берём провинцию и запрос
                ref C_ProvinceRender pR = ref mapRender_A.pR_P.Get(pEntity);
                ref SR_ProvinceRender_UpdateThinEdges rComp = ref mapRender_A.pR_UpdateThinEdges_SR_P.Get(pEntity);

                //Если индекс тонких граней был обновлён
                if (PR_UpdateThinEdgesIndex(
                    ref pR,
                    rComp.edgeIndex) == true)
                {
                    //Отмечаем, что грани обновлены
                    isThinUpdated = true;
                }

                //Удаляем запрос
                mapRender_A.pR_UpdateThinEdges_SR_P.Del(pEntity);
            }
        }

        bool PR_UpdateThinEdgesIndex(
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

        void Map_ThickEdges_UpdateData(
            out bool isThickUpdated)
        {
            //Устанавливаем значение по умолчанию
            isThickUpdated = false;

            //Для каждой провинции без запроса обновления толстых граней
            foreach (ProtoEntity pEntity in mapRender_A.pR_WithoutUpdateThickEdges_SR_I)
            {
                //Берём провинцию
                ref C_ProvinceRender pR = ref mapRender_A.pR_P.Get(pEntity);

                //Если индекс толстых граней был обновлён
                if (PR_UpdateThickEdgesIndex(
                    ref pR,
                    -1) == true)
                {
                    //Отмечаем, что грани обновлены
                    isThickUpdated = true;
                }
            }

            //Если нет провинций с запросом
            if (mapRender_A.pR_UpdateThickEdges_SR_I.IsEmptySlow())
            {
                //Отмечаем, что требуется обновление карты
                isThickUpdated = true;
            }

            //Для каждой провинции с запросом
            foreach (ProtoEntity pEntity in mapRender_A.pR_UpdateThickEdges_SR_I)
            {
                //Берём провинцию и запрос
                ref C_ProvinceRender pR = ref mapRender_A.pR_P.Get(pEntity);
                ref SR_ProvinceRender_UpdateThickEdges rComp = ref mapRender_A.pR_UpdateThickEdges_SR_P.Get(pEntity);

                //Если индекс толстых граней был обновлён
                if (PR_UpdateThickEdgesIndex(
                    ref pR,
                    rComp.edgeIndex) == true)
                {
                    //Отмечаем, что грани обновлены
                    isThickUpdated = true;
                }

                //Удаляем запрос
                mapRender_A.pR_UpdateThickEdges_SR_P.Del(pEntity);
            }
        }

        bool PR_UpdateThickEdgesIndex(
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

        void MapModes_UpdateData()
        {
            //Для каждого активного режима карты
            foreach (ProtoEntity activeMMEntity in coreMapMode_A.activeMM_I)
            {
                //Берём режим карты
                ref C_MapModeCore activeMM = ref coreMapMode_A.mMC_P.Get(activeMMEntity);

                //Обновляем данные визуализации провинций
                Map_ProvinceRenderUpdateData(
                    ref activeMM,
                    out bool isHeightUpdated,
                    out bool isColorUpdated);

                //Запрашиваем обновление визуализации провинций карты
                mapRender_A.Map_UpdateProvincesRender_R(
                    false, isHeightUpdated, isColorUpdated);
            }
        }

        void Map_ProvinceRenderUpdateData(
            ref C_MapModeCore mapMode,
            out bool isHeightUpdated, out bool isColorUpdated)
        {
            //Устанавливаем значения по умолчанию
            isHeightUpdated = false;
            isColorUpdated = false;

            //Для каждой провинции без запроса обновления визуализации
            foreach (ProtoEntity provinceEntity in mapRender_A.pR_WithoutUpdate_I)
            {
                //Берём провинцию
                ref C_ProvinceRender pR = ref mapRender_A.pR_P.Get(provinceEntity);

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
            if(mapRender_A.pR_Update_I.IsEmptySlow())
            {
                //Отмечаем, что требуется обновление карты
                isHeightUpdated = true;
                isColorUpdated = true;
            }

            //Для каждой провинции с запросом
            foreach (ProtoEntity provinceEntity in mapRender_A.pR_Update_I)
            {
                //Берём провинцию и запрос
                ref C_ProvinceRender pR = ref mapRender_A.pR_P.Get(provinceEntity);
                ref SR_ProvinceRender_Update rComp = ref mapRender_A.pR_Update_P.Get(provinceEntity);

                //Обновляем отображаемый объект провинции
                ProvinceRender_UpdateDisplayedObject(
                    ref mapMode,
                    ref pR,
                    rComp.displayedObjectEntity);

                //Изменяем параметры визуализации провинции
                //Если высота была обновлена
                if (ProvinceRender_UpdateHeight(
                    ref mapMode,
                    ref pR,
                    rComp.height) == true)
                {
                    //Отмечаем, что высота обновлена
                    isHeightUpdated = true;
                }

                //Если индекс цвета был обновлён
                if (ProvinceRender_UpdateColorIndex(
                    ref mapMode,
                    ref pR,
                    rComp.colorIndex) == true)
                {
                    //Отмечаем, что цвет обновлён
                    isColorUpdated = true;
                }
            }
        }

        void ProvinceRender_UpdateDisplayedObject(
            ref C_MapModeCore mapMode,
            ref C_ProvinceRender pR,
            ProtoEntity displayedObjectPE)
        {
            //Если отображаемый объект провинции не равен переданному
            if (pR.DisplayedObjectPE != displayedObjectPE)
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
