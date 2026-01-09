
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map.Render
{
    public class MainMapMode_Data : MonoBehaviour
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
        /// <param name="sR_P"></param>
        /// <param name="mapModeEntity"></param>
        /// <param name="mapModeName"></param>
        /// <param name="defaultMapMode"></param>
        public static void MapModeCore_Creation_Request(
            EcsPool<SR_MapModeCore_Creation> sR_P,
            int mapModeEntity, string mapModeName,
            bool defaultMapMode)
        {
            //Назначаем сущности запрос создания режима карты
            ref SR_MapModeCore_Creation requestComp = ref sR_P.Add(mapModeEntity);

            //Заполняем данные запроса
            requestComp = new(
                mapModeName,
                defaultMapMode);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="world"></param>
        /// <param name="r_P"></param>
        /// <param name="coloredObjectType"></param>
        /// <param name="objectColors"></param>
        public static void MapMode_UpdateColorsListFirst_Request(
            EcsWorld world,
            EcsPool<R_MapMode_UpdateColorsListFirst> r_P,
            string coloredObjectType,
            List<Color> objectColors)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_MapMode_UpdateColorsListFirst requestComp = ref r_P.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                coloredObjectType,
                objectColors);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="world"></param>
        /// <param name="r_P"></param>
        /// <param name="mapModePE"></param>
        /// <param name="mapModeColors"></param>
        /// <param name="defaultColor"></param>
        public static void MapMode_UpdateColorsListSecond_Request(
            EcsWorld world,
            EcsPool<R_MapMode_UpdateColorsListSecond> r_P,
            EcsPackedEntity mapModePE,
            List<Color> mapModeColors, Color defaultColor)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_MapMode_UpdateColorsListSecond requestComp = ref r_P.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                mapModePE,
                mapModeColors, defaultColor);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля игры
        /// </summary>
        /// <param name="world"></param>
        /// <param name="r_P"></param>
        /// <param name="mapModePE"></param>
        public static void MapMode_Activation_Request(
            EcsWorld world,
            EcsPool<R_MapMode_Activation> r_P,
            EcsPackedEntity mapModePE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_MapMode_Activation requestComp = ref r_P.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                mapModePE);
        }

        /// <summary>
        /// Внутренняя функция, поскольку запрашивается из подмодуля карты 
        /// </summary>
        /// <param name="sR_P"></param>
        /// <param name="mapModeEntity"></param>
        internal static void MapMode_Update_Request(
            EcsPool<SR_MapMode_Update> sR_P,
            int mapModeEntity)
        {
            //Назначаем сущности режима карты запрос 
            ref SR_MapMode_Update requestComp = ref sR_P.Add(mapModeEntity);

            //Заполняем данные запроса
            requestComp = new(0);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="sR_P"></param>
        /// <param name="targetEntity"></param>
        /// <param name="edgeIndex"></param>
        public static void ThinEdges_Update_Request(
            EcsPool<SR_ProvinceRender_UpdateThinEdges> sR_P,
            int targetEntity,
            int edgeIndex)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceRender_UpdateThinEdges requestComp = ref sR_P.Add(targetEntity);

            //Заполняем данные запроса
            requestComp = new(
                edgeIndex);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="sR_P"></param>
        /// <param name="targetEntity"></param>
        /// <param name="edgeIndex"></param>
        public static void ThickEdges_Update_Request(
            EcsPool<SR_ProvinceRender_UpdateThickEdges> sR_P,
            int targetEntity,
            int edgeIndex)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceRender_UpdateThickEdges requestComp = ref sR_P.Add(targetEntity);

            //Заполняем данные запроса
            requestComp = new(
                edgeIndex);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="sR_P"></param>
        /// <param name="mapMode"></param>
        /// <param name="targetEntity"></param>
        /// <param name="displayedObjectPE"></param>
        /// <param name="height"></param>
        /// <param name="colorIndex"></param>
        public static void ProvinceRender_Update_Request_Full(
           EcsPool<SR_ProvinceRender_Update> sR_P,
           ref C_MapModeCore mapMode,
           int targetEntity,
           EcsPackedEntity displayedObjectPE,
           float height,
           int colorIndex)
        {
            //Создаём запрос
            ProvinceRender_Update_Request_Creation(
                sR_P,
                targetEntity);

            //Берём запрос
            ref SR_ProvinceRender_Update requestComp = ref sR_P.Get(targetEntity);

            //Заполняем запрос
            ProvinceRender_Update_Request_Update(
                ref mapMode,
                ref requestComp,
                displayedObjectPE, height, colorIndex);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="sR_P"></param>
        /// <param name="targetEntity"></param>
        public static void ProvinceRender_Update_Request_Creation(
            EcsPool<SR_ProvinceRender_Update> sR_P,
            int targetEntity)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceRender_Update requestComp = ref sR_P.Add(targetEntity);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="mapMode"></param>
        /// <param name="requestComp"></param>
        /// <param name="displayedObjectPE"></param>
        /// <param name="height"></param>
        /// <param name="colorIndex"></param>
        public static void ProvinceRender_Update_Request_Update(
            ref C_MapModeCore mapMode,
            ref SR_ProvinceRender_Update requestComp,
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
        /// <param name="sR_P"></param>
        /// <param name="mapMode"></param>
        /// <param name="targetEntity"></param>
        public static void ProvinceHoverHighlight_Show_Request(
            EcsPool<SR_ProvinceHoverHighlight_Show> sR_P,
            ref C_MapModeCore mapMode,
            int targetEntity)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceHoverHighlight_Show requestComp = ref sR_P.Add(targetEntity);

            //Заполняем данные запроса
            requestComp = new(0);
        }
    }
}
