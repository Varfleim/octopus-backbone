
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map.Render
{
    public class MainMapModeData : MonoBehaviour
    {
        public EcsPackedEntity DefaultMapModePE
        {
            get
            {
                return defaultMapModePE;
            }
            internal set
            {
                defaultMapModePE = value;
            }
        }
        private EcsPackedEntity defaultMapModePE;

        public EcsPackedEntity ActiveMapModePE
        {
            get
            {
                return activeMapModePE;
            }
            internal set
            {
                activeMapModePE = value;
            }
        }
        private EcsPackedEntity activeMapModePE;

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="requestPool"></param>
        /// <param name="mapModeEntity"></param>
        /// <param name="mapModeName"></param>
        /// <param name="defaultMapMode"></param>
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

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="world"></param>
        /// <param name="requestPool"></param>
        /// <param name="coloredObjectType"></param>
        /// <param name="objectColors"></param>
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

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="world"></param>
        /// <param name="requestPool"></param>
        /// <param name="mapModePE"></param>
        /// <param name="mapModeColors"></param>
        /// <param name="defaultColor"></param>
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

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля игры
        /// </summary>
        /// <param name="world"></param>
        /// <param name="requestPool"></param>
        /// <param name="mapModePE"></param>
        public static void MapModeActivationRequest(
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

        /// <summary>
        /// Внутренняя функция, поскольку запрашивается из подмодуля карты 
        /// </summary>
        /// <param name="requestPool"></param>
        /// <param name="mapModeEntity"></param>
        internal static void MapModeUpdateRequest(
            EcsPool<SR_MapModeUpdate> requestPool,
            int mapModeEntity)
        {
            //Назначаем сущности режима карты запрос 
            ref SR_MapModeUpdate requestComp = ref requestPool.Add(mapModeEntity);

            //Заполняем данные запроса
            requestComp = new(0);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="requestPool"></param>
        /// <param name="targetEntity"></param>
        /// <param name="edgeIndex"></param>
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

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="requestPool"></param>
        /// <param name="targetEntity"></param>
        /// <param name="edgeIndex"></param>
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

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="requestPool"></param>
        /// <param name="mapMode"></param>
        /// <param name="targetEntity"></param>
        /// <param name="displayedObjectPE"></param>
        /// <param name="height"></param>
        /// <param name="colorIndex"></param>
        public static void UpdateProvinceRenderRequestFull(
           EcsPool<SR_UpdateProvinceRender> requestPool,
           ref C_MapModeCore mapMode,
           int targetEntity,
           EcsPackedEntity displayedObjectPE,
           float height,
           int colorIndex)
        {
            //Создаём запрос
            UpdateProvinceRenderRequestCreation(
                requestPool,
                targetEntity);

            //Берём запрос
            ref SR_UpdateProvinceRender requestComp = ref requestPool.Get(targetEntity);

            //Заполняем запрос
            UpdateProvinceRenderRequestUpdate(
                ref mapMode,
                ref requestComp,
                displayedObjectPE, height, colorIndex);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="requestPool"></param>
        /// <param name="targetEntity"></param>
        public static void UpdateProvinceRenderRequestCreation(
            EcsPool<SR_UpdateProvinceRender> requestPool,
            int targetEntity)
        {
            //Назначаем сущности запрос
            ref SR_UpdateProvinceRender requestComp = ref requestPool.Add(targetEntity);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="mapMode"></param>
        /// <param name="requestComp"></param>
        /// <param name="displayedObjectPE"></param>
        /// <param name="height"></param>
        /// <param name="colorIndex"></param>
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

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="requestPool"></param>
        /// <param name="mapMode"></param>
        /// <param name="targetEntity"></param>
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
    }
}
