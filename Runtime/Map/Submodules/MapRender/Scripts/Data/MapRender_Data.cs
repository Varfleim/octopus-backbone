
using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map.Render
{
    public class MapRender_Data : MonoBehaviour
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
        /// <param name="r_P"></param>
        /// <param name="mapPE"></param>
        public static void Map_Activation_Request(
            EcsWorld world,
            EcsPool<R_Map_Activation> r_P,
            EcsPackedEntity mapPE)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_Map_Activation requestComp = ref r_P.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                mapPE);
        }

        /// <summary>
        /// Внутренняя функция, поскольку запрашивается из подмодуля карты
        /// </summary>
        /// <param name="world"></param>
        /// <param name="r_P"></param>
        /// <param name="isMaterialUpdated"></param>
        /// <param name="isHeightUpdated"></param>
        /// <param name="isColorUpdated"></param>
        internal static void Map_UpdateProvincesRender_Request(
            EcsWorld world,
            EcsPool<R_Map_UpdateProvincesRender> r_P,
            bool isMaterialUpdated, bool isHeightUpdated, bool isColorUpdated)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_Map_UpdateProvincesRender requestComp = ref r_P.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                isMaterialUpdated, isHeightUpdated, isColorUpdated);
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля игры
        /// </summary>
        /// <param name="world"></param>
        /// <param name="r_P"></param>
        /// <param name="parentProvincePE"></param>
        /// <param name="mapPanelGO"></param>
        public static void ProvinceMapPanel_SetParent_Request(
            EcsWorld world,
            EcsPool<R_ProvinceMapPanel_SetParent> r_P,
            EcsPackedEntity parentProvincePE,
            GameObject mapPanelGO)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_ProvinceMapPanel_SetParent requestComp = ref r_P.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                parentProvincePE,
                mapPanelGO);
        }
    }
}
