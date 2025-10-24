
using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map
{
    internal class MapData : MonoBehaviour
    {
        public static void MapActivationRequest(
            EcsWorld world,
            EcsPool<R_MapActivation> requestPool,
            EcsPackedEntity mapPE)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref R_MapActivation requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                mapPE);
        }

        public static void MapRenderInitializationRequest(
            EcsWorld world,
            EcsPool<R_MapRenderInitialization> requestPool)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref R_MapRenderInitialization requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(0);
        }

        public static void MapEdgesUpdateRequest(
            EcsWorld world,
            EcsPool<R_MapEdgesUpdate> requestPool,
            bool isThinUpdated, bool isThickUpdated)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref R_MapEdgesUpdate requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                isThinUpdated, isThickUpdated, false);
        }

        public static void MapProvincesUpdateRequest(
            EcsWorld world,
            EcsPool<R_MapProvincesUpdate> requestPool,
            bool isMaterialUpdated, bool isHeightUpdated, bool isColorUpdated)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref R_MapProvincesUpdate requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                isMaterialUpdated, isHeightUpdated, isColorUpdated);
        }
    }
}
