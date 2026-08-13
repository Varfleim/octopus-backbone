
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Map.Render
{
    public class A_MapRender : ProtoAspectInject
    {
        public ProtoPool<CT_ActiveMap> activeMap_P;
        public ProtoIt activeMap_I = new(It.Inc<C_Map, CT_ActiveMap>());

        public ProtoPool<SR_Map_Activation> map_Activation_SR_P;
        public ProtoIt map_Activation_SR_I = new(It.Inc<C_Map, SR_Map_Activation>());

        public ProtoPool<SR_Map_RenderInitialization> map_RenderInitialization_SR_P;
        public ProtoIt map_RenderInitialization_SR_I = new(It.Inc<C_Map, SR_Map_RenderInitialization>());

        public ProtoPool<R_Map_UpdateProvincesRender> map_UpdateProvincesRender_R_P;
        public ProtoIt map_UpdateProvinceRender_R_I = new(It.Inc<R_Map_UpdateProvincesRender>());

        public ProtoPool<R_Map_UpdateEdges> map_UpdateEdges_R_P;
        public ProtoIt map_UpdateEdges_R_I = new(It.Inc<R_Map_UpdateEdges>());

        public ProtoPool<C_ProvinceRender> pR_P;
        public ProtoItCached pR_I = new(It.Inc<C_ProvinceCore, C_ProvinceRender>());

        public ProtoPool<SR_ProvinceRender_Update> pR_Update_P;
        public ProtoIt pR_Update_I = new(It.Inc<C_ProvinceRender, SR_ProvinceRender_Update>());
        public ProtoItExc pR_WithoutUpdate_I = new(
            It.Inc<C_ProvinceRender>(),
            It.Exc<SR_ProvinceRender_Update>());

        public ProtoPool<SR_ProvinceRender_UpdateThinEdges> pR_UpdateThinEdges_SR_P;
        public ProtoIt pR_UpdateThinEdges_SR_I = new(It.Inc<C_ProvinceRender, SR_ProvinceRender_UpdateThinEdges>());
        public ProtoItExc pR_WithoutUpdateThinEdges_SR_I = new(
            It.Inc<C_ProvinceRender>(),
            It.Exc<SR_ProvinceRender_UpdateThinEdges>());

        public ProtoPool<SR_ProvinceRender_UpdateThickEdges> pR_UpdateThickEdges_SR_P;
        public ProtoIt pR_UpdateThickEdges_SR_I = new(It.Inc<C_ProvinceRender, SR_ProvinceRender_UpdateThickEdges>());
        public ProtoItExc pR_WithoutUpdateThickEdges_SR_I = new(
            It.Inc<C_ProvinceRender>(),
            It.Exc<SR_ProvinceRender_UpdateThickEdges>());

        public ProtoPool<C_ProvinceHoverHighlight> pHH_P;
        public ProtoIt pHH_I = new(It.Inc<C_ProvinceRender, C_ProvinceHoverHighlight>());

        public ProtoPool<SR_ProvinceHoverHighlight_Show> pHH_Show_SR_P;
        public ProtoItExc pHH_WithoutShow_I = new(
            It.Inc<C_ProvinceRender, C_ProvinceHoverHighlight>(),
            It.Exc<SR_ProvinceHoverHighlight_Show>());
        public ProtoIt pHH_Show_I = new(It.Inc<C_ProvinceRender, C_ProvinceHoverHighlight, SR_ProvinceHoverHighlight_Show>());
        public ProtoIt pR_ShowPHH_I = new(It.Inc<C_ProvinceRender, SR_ProvinceHoverHighlight_Show>());

        public ProtoPool<C_ProvinceMapPanels> pMP_P;
        public ProtoIt pMP_I = new(It.Inc<C_ProvinceRender, C_ProvinceMapPanels>());

        public ProtoPool<R_ProvinceMapPanel_SetParent> pMP_SetParent_R_P;
        public ProtoIt pMP_SetParent_R_I = new(It.Inc<R_ProvinceMapPanel_SetParent>());

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля игры
        /// </summary>
        /// <param name="mEntity"></param>
        public void Map_Activation_SR(
            ProtoEntity mEntity)
        {
            //Назначаем компонент запроса активации, если его ещё нет
            ref SR_Map_Activation rComp = ref map_Activation_SR_P.Add(mEntity);
            rComp = new(0);
        }

        /// <summary>
        /// Внутренняя функция, поскольку запрашивается из подмодуля карты
        /// </summary>
        /// <param name="isMaterialUpdated"></param>
        /// <param name="isHeightUpdated"></param>
        /// <param name="isColorUpdated"></param>
        internal void Map_UpdateProvincesRender_R(
            bool isMaterialUpdated, bool isHeightUpdated, bool isColorUpdated)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_Map_UpdateProvincesRender rComp = ref map_UpdateProvincesRender_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                isMaterialUpdated, isHeightUpdated, isColorUpdated);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="mapMode"></param>
        /// <param name="targetEntity"></param>
        /// <param name="displayedObjectEntity"></param>
        /// <param name="height"></param>
        /// <param name="colorIndex"></param>
        public void ProvinceRender_Update_R_Full(
           ref C_MapModeCore mapMode,
           ProtoEntity targetEntity,
           ProtoEntity displayedObjectEntity,
           float height,
           int colorIndex)
        {
            //Создаём запрос
            ProvinceRender_Update_R_Creation(targetEntity);

            //Берём запрос
            ref SR_ProvinceRender_Update rComp = ref pR_Update_P.Get(targetEntity);

            //Заполняем запрос
            ProvinceRender_Update_R_Update(
                ref mapMode,
                ref rComp,
                displayedObjectEntity, height, colorIndex);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="targetEntity"></param>
        public void ProvinceRender_Update_R_Creation(
            ProtoEntity targetEntity)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceRender_Update rComp = ref pR_Update_P.GetOrAdd(targetEntity);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="mapMode"></param>
        /// <param name="rComp"></param>
        /// <param name="displayedObjectEntity"></param>
        /// <param name="height"></param>
        /// <param name="colorIndex"></param>
        public void ProvinceRender_Update_R_Update(
            ref C_MapModeCore mapMode,
            ref SR_ProvinceRender_Update rComp,
            ProtoEntity displayedObjectEntity,
            float height,
            int colorIndex)
        {
            //Заполняем данные запроса
            rComp = new(
                displayedObjectEntity,
                height,
                colorIndex);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="targetEntity"></param>
        /// <param name="edgeIndex"></param>
        public void ThinEdges_Update_SR(
            ProtoEntity targetEntity,
            int edgeIndex)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceRender_UpdateThinEdges rComp = ref pR_UpdateThinEdges_SR_P.Add(targetEntity);
            rComp = new(edgeIndex);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="targetEntity"></param>
        /// <param name="edgeIndex"></param>
        public void ThickEdges_Update_SR(
            ProtoEntity targetEntity,
            int edgeIndex)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceRender_UpdateThickEdges rComp = ref pR_UpdateThickEdges_SR_P.Add(targetEntity);
            rComp = new(edgeIndex);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается режимами карты
        /// </summary>
        /// <param name="targetEntity"></param>
        /// <param name="mapMode"></param>
        public void ProvinceHoverHighlight_Show_R(
            ProtoEntity targetEntity,
            ref C_MapModeCore mapMode)
        {
            //Назначаем компонент запроса отображения подсветки наведения, если его ещё нет
            ref SR_ProvinceHoverHighlight_Show rComp = ref pHH_Show_SR_P.Add(targetEntity);
            rComp = new(0);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля игры
        /// </summary>
        /// <param name="parentProvincePE"></param>
        /// <param name="mapPanelGO"></param>
        public void ProvinceMapPanel_SetParent_Request(
            ProtoPackedEntity parentProvincePE,
            UnityEngine.GameObject mapPanelGO)
        {
            //Создаём новую сущность и назначаем ей запрос
            ref R_ProvinceMapPanel_SetParent rComp = ref pMP_SetParent_R_P.NewEntity(out ProtoEntity rEntity);

            //Заполняем данные запроса
            rComp = new(
                parentProvincePE,
                mapPanelGO);
        }
    }
}
