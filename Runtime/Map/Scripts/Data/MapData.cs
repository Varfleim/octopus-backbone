
using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map
{
    internal class MapData : MonoBehaviour
    {
        public static void MapActivationRequest(
            EcsWorld world,
            EcsPool<RMapActivation> requestPool,
            EcsPackedEntity mapPE)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref RMapActivation requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                mapPE);
        }

        public static void MapRenderInitializationRequest(
            EcsWorld world,
            EcsPool<RMapRenderInitialization> requestPool)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref RMapRenderInitialization requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(0);
        }

        public static void MapEdgesUpdateRequest(
            EcsWorld world,
            EcsPool<RMapEdgesUpdate> requestPool,
            bool isThinUpdated, bool isThickUpdated)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref RMapEdgesUpdate requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                isThinUpdated, isThickUpdated, false);
        }

        public static void MapProvincesUpdateRequest(
            EcsWorld world,
            EcsPool<RMapProvincesUpdate> requestPool,
            bool isMaterialUpdated, bool isHeightUpdated, bool isColorUpdated)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref RMapProvincesUpdate requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                isMaterialUpdated, isHeightUpdated, isColorUpdated);
        }
    }
}
