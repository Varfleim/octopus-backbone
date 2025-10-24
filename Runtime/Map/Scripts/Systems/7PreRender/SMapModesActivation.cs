
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapModesActivation : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_MapModeCore> mapModeCorePool = default;
        readonly EcsPoolInject<CT_ActiveMapMode> activeMapModePool = default;


        readonly EcsPoolInject<R_MapProvincesUpdate> mapProvincesUpdateRPool = default;

        public void Run(IEcsSystems systems)
        {
            //���������� ����� ����� �� �������
            MapModesActivation();
        }

        readonly EcsFilterInject<Inc<R_MapModeActivation>> mapModeActivationRFilter = default;
        readonly EcsPoolInject<R_MapModeActivation> mapModeActivationRPool = default;
        void MapModesActivation()
        {
            //��� ������� ������� ��������� ������ �����
            foreach(int requestEntity in mapModeActivationRFilter.Value)
            {
                //���� ������
                ref R_MapModeActivation requestComp = ref mapModeActivationRPool.Value.Get(requestEntity);

                //������������ �������� ����� �����
                bool isMapModeDeactivated = MapModeDeactivationCheck(ref requestComp);

                //���� ����� ����� �������������
                if(isMapModeDeactivated == true)
                {
                    //���������� ����� �����
                    MapModeActivation(ref requestComp);
                }

                //������� ������
                mapModeActivationRPool.Value.Del(requestEntity);
            }
        }

        readonly EcsFilterInject<Inc<C_MapModeCore, CT_ActiveMapMode>> activeMapModeFilter = default;
        bool MapModeDeactivationCheck(
            ref R_MapModeActivation requestComp)
        {
            //���� �������� ������������ ������ �����
            requestComp.mapModePE.Unpack(world.Value, out int requestedMapModeEntity);

            //�������� ����� ����� ���� ������ ����, �� ����� ������������ ����
            foreach (int activeMapModeEntity in activeMapModeFilter.Value)
            {
                //���� �������� ����� �����
                ref C_MapModeCore activeMapMode = ref mapModeCorePool.Value.Get(activeMapModeEntity);

                //���� ��� �� ��� ����� �����, ������� ��������� ������������
                if (activeMapModeEntity != requestedMapModeEntity)
                {
                    //������� ��������� ��������� ������
                    activeMapModePool.Value.Del(activeMapModeEntity);

                    //����������, ��� ����� ����� �������������
                    return true;
                }
                //�����
                else
                {
                    //����������, ��� ����� ����� �� �������������
                    return false;
                }
            }

            //����������, ��� ����� ����� �������������
            return true;
        }

        void MapModeActivation(
            ref R_MapModeActivation requestComp)
        {
            //���� ����������� ����� �����
            requestComp.mapModePE.Unpack(world.Value, out int mapModeEntity);
            ref C_MapModeCore mapMode = ref mapModeCorePool.Value.Get(mapModeEntity);

            //��������� ��� ��������� ��������� ������
            activeMapModePool.Value.Add(mapModeEntity);

            //������� ��� ������� ���������� ������� �����
            MapModeUpdatesCancel();

            //����������� ���������� ������ �����
            MapModeData.MapModeUpdateRequest(
                mapModeUpdateSRPool.Value,
                mapModeEntity);

            //����������� ���������� ��������� �����
            MapData.MapProvincesUpdateRequest(
                world.Value,
                mapProvincesUpdateRPool.Value,
                true, false, false);
        }


        readonly EcsFilterInject<Inc<C_MapModeCore, SR_MapModeUpdate>> mapModeUpdateSRFilter = default;
        readonly EcsPoolInject<SR_MapModeUpdate> mapModeUpdateSRPool = default;
        /// <summary>
        /// �������� ���� ������������ �������� ���������� ������ �����
        /// </summary>
        void MapModeUpdatesCancel()
        {
            //��� ������� ������� ���������� ������ �����
            foreach(int mapModeEntity in mapModeUpdateSRFilter.Value)
            {
                //������� ������
                mapModeUpdateSRPool.Value.Del(mapModeEntity);
            }
        }
    }
}
