
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map
{
    public class MapModeData : MonoBehaviour
    {
        internal EcsPackedEntity defaultMapModePE;

        public EcsPackedEntity activeMapModePE;

        public static void MapModeCreationRequest(
            EcsPool<SR_MapModeCreation> requestPool,
            int mapModeEntity, string mapModeName,
            bool defaultMapMode)
        {
            //Назначаем сущности запрос создания режима карты
            ref SR_MapModeCreation requestComp = ref requestPool.Add(mapModeEntity);

            //Заполняем данные запроса
            requestComp = new(
                mapModeName,
                defaultMapMode);
        }

        public static void MapModeUpdateColorsListFirstRequest(
            EcsWorld world,
            EcsPool<R_MapModeUpdateColorsListFirst> requestPool,
            string coloredObjectType,
            List<Color> objectColors)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_MapModeUpdateColorsListFirst requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                coloredObjectType,
                objectColors);
        }

        public static void MapModeUpdateColorsListSecondRequest(
            EcsWorld world,
            EcsPool<R_MapModeUpdateColorsListSecond> requestPool,
            EcsPackedEntity mapModePE,
            List<Color> mapModeColors, Color defaultColor)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_MapModeUpdateColorsListSecond requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                mapModePE,
                mapModeColors, defaultColor);
        }

        internal static void MapModeActivationRequest(
            EcsWorld world,
            EcsPool<R_MapModeActivation> requestPool,
            EcsPackedEntity mapModePE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_MapModeActivation requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                mapModePE);
        }

        internal static void MapModeUpdateRequest(
            EcsPool<SR_MapModeUpdate> requestPool,
            int mapModeEntity)
        {
            //Назначаем сущности режима карты запрос 
            ref SR_MapModeUpdate requestComp = ref requestPool.Add(mapModeEntity);

            //Заполняем данные запроса
            requestComp = new(0);
        }

        public static void UpdateThinEdgesRequest(
            EcsPool<SR_UpdateThinEdges> requestPool,
            int targetEntity,
            int edgeIndex)
        {
            //Назначаем сущности запрос
            ref SR_UpdateThinEdges requestComp = ref requestPool.Add(targetEntity);

            //Заполняем данные запроса
            requestComp = new(
                edgeIndex);
        }
        
        public static void UpdateThickEdgesRequest(
            EcsPool<SR_UpdateThickEdges> requestPool,
            int targetEntity,
            int edgeIndex)
        {
            //Назначаем сущности запрос
            ref SR_UpdateThickEdges requestComp = ref requestPool.Add(targetEntity);

            //Заполняем данные запроса
            requestComp = new(
                edgeIndex);
        }

        public static void UpdateProvinceRenderRequestFull(
           EcsPool<SR_UpdateProvinceRender> requestPool,
           //ref CMapModeCore mapMode,
           int targetEntity,
           EcsPackedEntity displayedObjectPE,
           float height,
           int colorIndex)
        {
            //Назначаем сущности запрос
            ref SR_UpdateProvinceRender requestComp = ref requestPool.Add(targetEntity);

            //Заполняем данные запроса
            requestComp = new(
                displayedObjectPE,
                height,
                colorIndex);
        }

        public static void UpdateProvinceRenderRequestCreation(
            EcsPool<SR_UpdateProvinceRender> requestPool,
            int targetEntity)
        {
            //Назначаем сущности запрос
            ref SR_UpdateProvinceRender requestComp = ref requestPool.Add(targetEntity);
        }

        public static void UpdateProvinceRenderRequestUpdate(
            ref C_MapModeCore mapMode,
            ref SR_UpdateProvinceRender requestComp,
            EcsPackedEntity displayedObjectPE,
            float height,
            int colorIndex)
        {
            //Заполняем данные запроса
            requestComp = new(
                displayedObjectPE,
                height,
                colorIndex);
        }

        public static void ShowMapHoverHighlightRequest(
            EcsPool<SR_ShowMapHoverHighlight> requestPool,
            ref C_MapModeCore mapMode,
            int targetEntity)
        {
            //Назначаем сущности запрос
            ref SR_ShowMapHoverHighlight requestComp = ref requestPool.Add(targetEntity);

            //Заполняем данные запроса
            requestComp = new(0);
        }

        internal static void UpdateProvinceDisplayedObject(
            ref C_MapModeCore mapMode,
            ref C_ProvinceRender pR,
            EcsPackedEntity displayedObjectPE)
        {
            //Если отображаемый объект провинции не равен переданному
            if (pR.DisplayedObjectPE.EqualsTo(in displayedObjectPE) == false)
            {
                //Обновляем его
                pR.SetDisplayedObject(displayedObjectPE);
            }
        }

        internal static bool UpdateProvinceThinEdgesIndex(
            ref C_ProvinceRender pR,
            int newThinEdgesIndex)
        {
            //Если индекс тонких граней провинции не равен переданному
            if(pR.ThinEdgesIndex != newThinEdgesIndex)
            {
                //Обновляем его
                pR.SetThinEdgesIndex(newThinEdgesIndex);

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

        internal static bool UpdateProvinceThickEdgesIndex(
            ref C_ProvinceRender pR,
            int newThickEdgesIndex)
        {
            //Если индекс толстых граней провинции не равен переданному
            if (pR.ThickEdgesIndex != newThickEdgesIndex)
            {
                //Обновляем его
                pR.SetThickEdgesIndex(newThickEdgesIndex);

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

        internal static bool UpdateProvinceHeight(
            ref C_MapModeCore mapMode,
            ref C_ProvinceRender pR,
            float newProvinceHeight)
        {
            //Если высота провинции не равна переданной
            if (pR.ProvinceHeight != newProvinceHeight)
            {
                //Обновляем её
                pR.SetHeight(newProvinceHeight);

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

        internal static bool UpdateProvinceColorIndex(
            ref C_MapModeCore mapMode,
            ref C_ProvinceRender pR,
            int newProvinceColorIndex)
        {
            //Если индекс цвета провинции не равен переданному
            if (pR.ProvinceColorIndex != newProvinceColorIndex)
            {
                //Обновляем его
                pR.SetColorIndex(newProvinceColorIndex);

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
