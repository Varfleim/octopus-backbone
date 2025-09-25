
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map
{
    public class ProvinceData : MonoBehaviour
    {
        public float mapPanelAltitude;

        public static void ProvinceCoreCreationRequest(
            EcsPool<SRProvinceCoreCreation> requestPool,
            int provinceEntity,
            EcsPackedEntity parentMapPE,
            List<EcsPackedEntity> neighbours)
        {
            //��������� �������� ������
            ref SRProvinceCoreCreation requestComp = ref requestPool.Add(provinceEntity);

            //��������� ������ �������
            requestComp = new(
                parentMapPE,
                neighbours.ToArray());
        }

        public static void ProvinceCoreCreation(
            EcsWorld world,
            ref SRProvinceCoreCreation requestComp,
            int provinceEntity,
            EcsPool<CProvinceCore> pCPool,
            List<EcsPackedEntity> mapProvincesList)
        {
            //��������� �������� ��������� PC
            ref CProvinceCore pC = ref pCPool.Add(provinceEntity);

            //��������� �������� ������ PC
            pC = new(
                world.PackEntity(provinceEntity),
                requestComp.neighbourProvincePEs);

            //������� ��������� � ������
            mapProvincesList.Add(pC.selfPE);
        }

        public static void ProvinceMapPanelSetParentRequest(
            EcsWorld world,
            EcsPool<RProvinceMapPanelSetParent> requestPool,
            EcsPackedEntity parentProvincePE,
            GameObject mapPanelGO)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref RProvinceMapPanelSetParent requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                parentProvincePE,
                mapPanelGO);
        }
    }
}
