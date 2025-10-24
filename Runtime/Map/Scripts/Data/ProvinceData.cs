
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map
{
    public class ProvinceData : MonoBehaviour
    {
        [SerializeField]
        private float mapPanelAltitude;

        [SerializeField]
        private GO_Province provinceGOPrefab;
        [SerializeField]
        private GO_ProvinceHighlight provinceHighlightGOPrefab;
        [SerializeField]
        private UnityEngine.UI.VerticalLayoutGroup provinceMapPanelGroupPrefab;

        public static void ProvinceCoreCreationRequest(
            EcsPool<SR_ProvinceCoreCreation> requestPool,
            int provinceEntity,
            EcsPackedEntity parentMapPE,
            List<EcsPackedEntity> neighbours)
        {
            //��������� �������� ������
            ref SR_ProvinceCoreCreation requestComp = ref requestPool.Add(provinceEntity);

            //��������� ������ �������
            requestComp = new(
                parentMapPE,
                neighbours.ToArray());
        }

        public static void ProvinceCoreCreation(
            EcsWorld world,
            ref SR_ProvinceCoreCreation requestComp,
            int provinceEntity,
            EcsPool<C_ProvinceCore> pCPool,
            List<EcsPackedEntity> mapProvincesList)
        {
            //��������� �������� ��������� PC
            ref C_ProvinceCore pC = ref pCPool.Add(provinceEntity);

            //��������� �������� ������ PC
            pC = new(
                world.PackEntity(provinceEntity),
                requestComp.neighbourProvincePEs);

            //������� ��������� � ������
            mapProvincesList.Add(pC.selfPE);
        }

        public static void ProvinceMapPanelSetParentRequest(
            EcsWorld world,
            EcsPool<R_ProvinceMapPanelSetParent> requestPool,
            EcsPackedEntity parentProvincePE,
            GameObject mapPanelGO)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref R_ProvinceMapPanelSetParent requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                parentProvincePE,
                mapPanelGO);
        }
    }
}
