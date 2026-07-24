
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Map.Render
{
    public class A_CoreMapMode : ProtoAspectInject
    {
        public ProtoPool<C_MapModeCore> mMC_P;

        public ProtoPool<CT_ActiveMapMode> activeMM_P;
        public ProtoIt activeMM_I = new(It.Inc<C_MapModeCore, CT_ActiveMapMode>());

        public ProtoPool<SR_MapModeCore_Creation> mMC_Creation_SR_P;
        public ProtoIt mMC_Creation_SR_I = new (It.Inc<SR_MapModeCore_Creation>());

        public ProtoPool<R_MapMode_Activation> mM_Activation_R_P;
        public ProtoIt mM_Activation_R_I = new (It.Inc<R_MapMode_Activation>());

        internal ProtoPool<SR_MapMode_Update> mM_Update_SR_P;
        internal ProtoIt mM_Update_SR_I = new (It.Inc<C_MapModeCore, SR_MapMode_Update>());

        public ProtoPool<R_MapMode_UpdateColorsListFirst> mM_UpdateColorsListFirst_R_P;

        public ProtoPool<R_MapMode_UpdateColorsListSecond> mM_UpdateColorsListSecond_R_P;
        public ProtoIt mM_UpdateColorsListSecond_R_I = new (It.Inc<R_MapMode_UpdateColorsListSecond>());

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="mMEntity"></param>
        /// <param name="mapModeName"></param>
        /// <param name="defaultMapMode"></param>
        public void MapModeCore_Creation_SR(
            ProtoEntity mMEntity, string mapModeName,
            bool defaultMapMode)
        {
            //Назначаем сущности запрос создания режима карты
            ref SR_MapModeCore_Creation rComp = ref mMC_Creation_SR_P.Add(mMEntity);
            rComp = new(
                mapModeName,
                defaultMapMode);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля игры
        /// </summary>
        /// <param name="mMEntity"></param>
        public void MapMode_Activation_R(
            ProtoEntity mMEntity)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_MapMode_Activation rComp = ref mM_Activation_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                mMEntity);
        }

        /// <summary>
        /// Внутренняя функция, поскольку запрашивается из подмодуля карты
        /// </summary>
        /// <param name="mMEntity"></param>
        internal void MapMode_Update_SR(
            ProtoEntity mMEntity)
        {
            //Назначаем сущности режима карты запрос 
            ref SR_MapMode_Update rComp = ref mM_Update_SR_P.Add(mMEntity);

            rComp = new(0);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="coloredObjectType"></param>
        /// <param name="objectColors"></param>
        public void MapMode_UpdateColorsListFirst_R(
            string coloredObjectType,
            List<Color> objectColors)
        {
            //Создаём сущность и назначаем ей запрос
            ref R_MapMode_UpdateColorsListFirst rComp = ref mM_UpdateColorsListFirst_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                coloredObjectType,
                objectColors);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="mMEntity"></param>
        /// <param name="mapModeColors"></param>
        /// <param name="defaultColor"></param>
        public void MapMode_UpdateColorsListSecond_R(
            ProtoEntity mMEntity,
            List<Color> mapModeColors, Color defaultColor)
        {
            //Создаём сущность и назначаем ей запрос
            ref R_MapMode_UpdateColorsListSecond rComp = ref mM_UpdateColorsListSecond_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                mMEntity,
                mapModeColors, defaultColor);
        }

    }
}
