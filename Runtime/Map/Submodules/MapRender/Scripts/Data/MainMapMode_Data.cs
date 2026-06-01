
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
        /// <param name="r_P"></param>
        /// <param name="mapModeEntity"></param>
        /// <param name="mapModeName"></param>
        /// <param name="defaultMapMode"></param>
        public static void MapModeCore_Creation_R(
            EcsPool<SR_MapModeCore_Creation> r_P,
            int mapModeEntity, string mapModeName,
            bool defaultMapMode)
        {
            //Назначаем сущности запрос создания режима карты
            ref SR_MapModeCore_Creation rComp = ref r_P.Add(mapModeEntity);

            //Заполняем данные запроса
            rComp = new(
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
        public static void MapMode_UpdateColorsListFirst_R(
            EcsWorld world,
            EcsPool<R_MapMode_UpdateColorsListFirst> r_P,
            string coloredObjectType,
            List<Color> objectColors)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_MapMode_UpdateColorsListFirst rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
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
        public static void MapMode_UpdateColorsListSecond_R(
            EcsWorld world,
            EcsPool<R_MapMode_UpdateColorsListSecond> r_P,
            EcsPackedEntity mapModePE,
            List<Color> mapModeColors, Color defaultColor)
        {
            //Создаём новую сущность и назначаем ей запрос
            int Entity = world.NewEntity();
            ref R_MapMode_UpdateColorsListSecond rComp = ref r_P.Add(Entity);

            //Заполняем данные запроса
            rComp = new(
                mapModePE,
                mapModeColors, defaultColor);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля игры
        /// </summary>
        /// <param name="world"></param>
        /// <param name="r_P"></param>
        /// <param name="mapModePE"></param>
        public static void MapMode_Activation_R(
            EcsWorld world,
            EcsPool<R_MapMode_Activation> r_P,
            EcsPackedEntity mapModePE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int rEntity = world.NewEntity();
            ref R_MapMode_Activation rComp = ref r_P.Add(rEntity);

            //Заполняем данные запроса
            rComp = new(
                mapModePE);
        }

        /// <summary>
        /// Внутренняя функция, поскольку запрашивается из подмодуля карты 
        /// </summary>
        /// <param name="r_P"></param>
        /// <param name="mapModeEntity"></param>
        internal static void MapMode_Update_R(
            EcsPool<SR_MapMode_Update> r_P,
            int mapModeEntity)
        {
            //Назначаем сущности режима карты запрос 
            ref SR_MapMode_Update rComp = ref r_P.Add(mapModeEntity);

            //Заполняем данные запроса
            rComp = new(0);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="r_P"></param>
        /// <param name="targetEntity"></param>
        /// <param name="edgeIndex"></param>
        public static void ThinEdges_Update_R(
            EcsPool<SR_ProvinceRender_UpdateThinEdges> r_P,
            int targetEntity,
            int edgeIndex)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceRender_UpdateThinEdges rComp = ref r_P.Add(targetEntity);

            //Заполняем данные запроса
            rComp = new(
                edgeIndex);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="r_P"></param>
        /// <param name="targetEntity"></param>
        /// <param name="edgeIndex"></param>
        public static void ThickEdges_Update_R(
            EcsPool<SR_ProvinceRender_UpdateThickEdges> r_P,
            int targetEntity,
            int edgeIndex)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceRender_UpdateThickEdges rComp = ref r_P.Add(targetEntity);

            //Заполняем данные запроса
            rComp = new(
                edgeIndex);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="r_P"></param>
        /// <param name="mapMode"></param>
        /// <param name="targetEntity"></param>
        /// <param name="displayedObjectPE"></param>
        /// <param name="height"></param>
        /// <param name="colorIndex"></param>
        public static void ProvinceRender_Update_R_Full(
           EcsPool<SR_ProvinceRender_Update> r_P,
           ref C_MapModeCore mapMode,
           int targetEntity,
           EcsPackedEntity displayedObjectPE,
           float height,
           int colorIndex)
        {
            //Создаём запрос
            ProvinceRender_Update_R_Creation(
                r_P,
                targetEntity);

            //Берём запрос
            ref SR_ProvinceRender_Update rComp = ref r_P.Get(targetEntity);

            //Заполняем запрос
            ProvinceRender_Update_R_Update(
                ref mapMode,
                ref rComp,
                displayedObjectPE, height, colorIndex);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="r_P"></param>
        /// <param name="targetEntity"></param>
        public static void ProvinceRender_Update_R_Creation(
            EcsPool<SR_ProvinceRender_Update> r_P,
            int targetEntity)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceRender_Update rComp = ref r_P.Add(targetEntity);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="mapMode"></param>
        /// <param name="rComp"></param>
        /// <param name="displayedObjectPE"></param>
        /// <param name="height"></param>
        /// <param name="colorIndex"></param>
        public static void ProvinceRender_Update_R_Update(
            ref C_MapModeCore mapMode,
            ref SR_ProvinceRender_Update rComp,
            EcsPackedEntity displayedObjectPE,
            float height,
            int colorIndex)
        {
            //Заполняем данные запроса
            rComp = new(
                displayedObjectPE,
                height,
                colorIndex);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="r_P"></param>
        /// <param name="mapMode"></param>
        /// <param name="targetEntity"></param>
        public static void ProvinceHoverHighlight_Show_R(
            int targetEntity,
            EcsPool<SR_ProvinceHoverHighlight_Show> r_P,
            ref C_MapModeCore mapMode)
        {
            //Если у сущности ещё нет запроса отображения подсветки наведения
            if(r_P.Has(targetEntity) == false)
            {
                //Назначаем переданной сущности запрос отображения
                ref SR_ProvinceHoverHighlight_Show rComp = ref r_P.Add(targetEntity);

                //Заполняем данные запроса
                rComp = new(0);
            }
        }
    }
}
