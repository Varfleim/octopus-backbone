
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapControl : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<CMap> mapPool = default;
        readonly EcsPoolInject<CActiveMap> activeMapPool = default;

        readonly EcsFilterInject<Inc<CProvinceCore, CProvinceRender>> pRFilter = default;
        readonly EcsPoolInject<CProvinceRender> pRPool = default;


        readonly EcsPoolInject<RMapRenderInitialization> mapRenderInitializationRPool = default;


        readonly EcsCustomInject<MapModeData> mapModeData = default;

        public void Run(IEcsSystems systems)
        {
            //���������� ����� �� �������
            MapsActivation();
        }

        readonly EcsFilterInject<Inc<RMapActivation>> mapActivationRFilter = default;
        readonly EcsPoolInject<RMapActivation> mapActivationRPool = default;
        void MapsActivation()
        {
            //��� ������� ������� ��������� �����
            foreach(int requestEntity in mapActivationRFilter.Value)
            {
                //���� ������
                ref RMapActivation requestComp = ref mapActivationRPool.Value.Get(requestEntity);

                //������������ �������� �����
                bool isMapDeactivated = MapDeactivationCheck(ref requestComp);

                //���� ����� ��������������
                if(isMapDeactivated == true)
                {
                    //���������� �����
                    MapActivation(ref requestComp);
                }

                //������� ������
                mapActivationRPool.Value.Del(requestEntity);
            }
        }

        void MapActivation(
            ref RMapActivation requestComp)
        {
            //���� ����������� �����
            requestComp.mapPE.Unpack(world.Value, out int mapEntity);
            ref CMap map = ref mapPool.Value.Get(mapEntity);

            //��������� �� ��������� �������� �����
            activeMapPool.Value.Add(mapEntity);

            //��� ������ ��������� �����
            for(int a = 0; a < map.provincePEs.Length; a++)
            {
                //���� �������� ��������� � ��������� �� ��������� PR
                map.provincePEs[a].Unpack(world.Value, out int provinceEntity);
                ref CProvinceRender pR = ref pRPool.Value.Add(provinceEntity);

                //��������� ������ PR
                pR = new(0);
            }

            //����������� ������������� �����
            MapData.MapRenderInitializationRequest(
                world.Value,
                mapRenderInitializationRPool.Value);

            //����������� ��������� ������������ ������ �����
            MapModeDefaultActivation();
        }

        readonly EcsFilterInject<Inc<CMap, CActiveMap>> activeMapFilter = default;
        bool MapDeactivationCheck(
            ref RMapActivation requestComp)
        {
            //���� �������� �����
            foreach(int activeMapEntity in activeMapFilter.Value)
            {
                ref CMap activeMap = ref mapPool.Value.Get(activeMapEntity);

                //���� ��� �� �� �����, ������� ��������� ������������
                if(activeMap.selfPE.EqualsTo(in requestComp.mapPE) == false)
                {
                    //��� ������ ��������� � ����������� PR
                    foreach(int provinceEntity in pRFilter.Value)
                    {
                        //���� ��������� PR

                        //������� ��������� � ���������
                        pRPool.Value.Del(provinceEntity);
                    }

                    //������� ��������� �������� �����
                    activeMapPool.Value.Del(activeMapEntity);

                    //����������, ��� ����� ��������������
                    return true;
                }
                //�����
                else
                {
                    //����������, ��� ����� �� ��������������
                    return false;
                }
            }

            //����������, ��� ����� ��������������
            return true;
        }

        readonly EcsPoolInject<RMapModeActivation> mapModeActivationRPool = default;
        void MapModeDefaultActivation()
        {
            //����������� ��������� ������������ ������ �����
            MapModeData.MapModeActivationRequest(
                world.Value,
                mapModeActivationRPool.Value,
                mapModeData.Value.defaultMapModePE);
        }
    }
}
