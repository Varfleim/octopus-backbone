
using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map.Render
{
    public class MapRender_Data : MonoBehaviour
    {
        public int ActiveMapEntity
        {
            get
            {
                return activeMapEntity;
            }
            internal set
            {
                activeMapEntity = value;
            }
        }
        private int activeMapEntity = -1;

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
        public static void Map_Activation_SR(
            int mEntity,
            EcsPool<SR_Map_Activation> r_P)
        {
            //Если у сущности ещё нет запроса активации
            if(r_P.Has(mEntity) == false)
            {
                //Назначаем переданной сущности запрос активации карты
                ref SR_Map_Activation rComp = ref r_P.Add(mEntity);

                //Заполняем данные запроса
                rComp = new(0);
            }
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
