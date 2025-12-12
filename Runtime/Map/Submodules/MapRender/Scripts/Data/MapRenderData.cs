
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
        internal float mapPanelAltitude;

        public GO_Province ProvinceGOPrefab
        {
            get
            {
                return provinceGOPrefab;
            }
        }
        [SerializeField]
        internal GO_Province provinceGOPrefab;
        public GO_ProvinceHighlight ProvinceHighlightGOPrefab
        {
            get
            {
                return provinceHighlightGOPrefab;
            }
        }
        [SerializeField]
        internal GO_ProvinceHighlight provinceHighlightGOPrefab;
        public UnityEngine.UI.VerticalLayoutGroup ProvinceMapPanelGroupPrefab
        {
            get
            {
                return provinceMapPanelGroupPrefab;
            }
        }
        [SerializeField]
        internal UnityEngine.UI.VerticalLayoutGroup provinceMapPanelGroupPrefab;

        internal static void MapActivationRequest(
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

        public static void MapRenderInitializationRequest(
            EcsWorld world,
            EcsPool<R_MapRenderInitialization> requestPool)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_MapRenderInitialization requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(0);
        }

        public static void MapEdgesUpdateRequest(
            EcsWorld world,
            EcsPool<R_MapEdgesUpdate> requestPool,
            bool isThinUpdated, bool isThickUpdated)
        {
            //Создаём новую сущность и назначаем ей запрос
            int requestEntity = world.NewEntity();
            ref R_MapEdgesUpdate requestComp = ref requestPool.Add(requestEntity);

            //Заполняем данные запроса
            requestComp = new(
                isThinUpdated, isThickUpdated, false);
        }

        public static void MapProvincesUpdateRequest(
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
