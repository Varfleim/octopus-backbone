
using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map.Render
{
    public class MapRenderData : MonoBehaviour
    {
        public EcsPackedEntity ActiveMapPE
        {
            get
            {
                return activeMapPE;
            }
            internal set
            {
                activeMapPE = value;
            }
        }
        private EcsPackedEntity activeMapPE;

        public float MapPanelAltitude
        {
            get
            {
                return mapPanelAltitude;
            }
        }
        [SerializeField]
        private float mapPanelAltitude;

        public GO_Province ProvinceGOPrefab
        {
            get
            {
                return provinceGOPrefab;
            }
        }
        [SerializeField]
        private GO_Province provinceGOPrefab;
        public GO_ProvinceHighlight ProvinceHighlightGOPrefab
        {
            get
            {
                return provinceHighlightGOPrefab;
            }
        }
        [SerializeField]
        private GO_ProvinceHighlight provinceHighlightGOPrefab;
        public UnityEngine.UI.VerticalLayoutGroup ProvinceMapPanelGroupPrefab
        {
            get
            {
                return provinceMapPanelGroupPrefab;
            }
        }
        [SerializeField]
        private UnityEngine.UI.VerticalLayoutGroup provinceMapPanelGroupPrefab;

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля игры
        /// </summary>
        /// <param name="world"></param>
        /// <param name="requestPool"></param>
        /// <param name="mapPE"></param>
        public static void MapActivationRequest(
            EcsWorld world,
            EcsPool<R_MapActivation> requestPool,
            EcsPackedEntity mapPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_MapActivation requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                mapPE);
        }

        /// <summary>
        /// Внутренняя функция, поскольку запрашивается из подмодуля карты
        /// </summary>
        /// <param name="world"></param>
        /// <param name="requestPool"></param>
        /// <param name="isMaterialUpdated"></param>
        /// <param name="isHeightUpdated"></param>
        /// <param name="isColorUpdated"></param>
        internal static void MapProvincesUpdateRequest(
            EcsWorld world,
            EcsPool<R_MapProvincesUpdate> requestPool,
            bool isMaterialUpdated, bool isHeightUpdated, bool isColorUpdated)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_MapProvincesUpdate requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                isMaterialUpdated, isHeightUpdated, isColorUpdated);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля игры
        /// </summary>
        /// <param name="world"></param>
        /// <param name="requestPool"></param>
        /// <param name="parentProvincePE"></param>
        /// <param name="mapPanelGO"></param>
        public static void ProvinceMapPanelSetParentRequest(
            EcsWorld world,
            EcsPool<R_ProvinceMapPanelSetParent> requestPool,
            EcsPackedEntity parentProvincePE,
            GameObject mapPanelGO)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ProvinceMapPanelSetParent requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                parentProvincePE,
                mapPanelGO);
        }
    }
}
