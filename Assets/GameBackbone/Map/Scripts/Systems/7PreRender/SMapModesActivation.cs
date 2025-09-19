
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapModesActivation : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<CMapModeCore> mapModeCorePool = default;
        readonly EcsPoolInject<CActiveMapMode> activeMapModePool = default;


        readonly EcsPoolInject<RMapProvincesUpdate> mapProvincesUpdateRPool = default;

        readonly EcsFilterInject<Inc<CMapModeCore, SRMapModeUpdate>> mapModeUpdateSRFilter = default;
        readonly EcsPoolInject<SRMapModeUpdate> mapModeUpdateSRPool = default;

        public void Run(IEcsSystems systems)
        {
            //���������� ����� ����� �� �������
            MapModesActivation();

        }

        readonly EcsFilterInject<Inc<RMapModeActivation>> mapModeActivationRFilter = default;
        readonly EcsPoolInject<RMapModeActivation> mapModeActivationRPool = default;
        void MapModesActivation()
        {
            //��� ������� ������� ��������� ������ �����
            foreach(int requestEntity in mapModeActivationRFilter.Value)
            {
                //���� ������
                ref RMapModeActivation requestComp = ref mapModeActivationRPool.Value.Get(requestEntity);

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

        void MapModeActivation(
            ref RMapModeActivation requestComp)
        {
            //���� ����������� ����� �����
            requestComp.mapModePE.Unpack(world.Value, out int mapModeEntity);
            ref CMapModeCore mapMode = ref mapModeCorePool.Value.Get(mapModeEntity);

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

        readonly EcsFilterInject<Inc<CMapModeCore, CActiveMapMode>> activeMapModeFilter = default;
        bool MapModeDeactivationCheck(
            ref RMapModeActivation requestComp)
        {
            //���� �������� ����� �����
            foreach(int activeMapModeEntity in activeMapModeFilter.Value)
            {
                ref CMapModeCore activeMapMode = ref mapModeCorePool.Value.Get(activeMapModeEntity);

                //���� ��� �� ��� ����� �����, ������� ��������� ������������
                if(activeMapMode.selfPE.EqualsTo(requestComp.mapModePE) == false)
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
