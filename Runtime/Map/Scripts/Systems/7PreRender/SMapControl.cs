
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapControl : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_Map> mapPool = default;
        readonly EcsPoolInject<CT_ActiveMap> activeMapPool = default;

        readonly EcsFilterInject<Inc<C_ProvinceCore, C_ProvinceRender>> pRFilter = default;
        readonly EcsPoolInject<C_ProvinceRender> pRPool = default;


        readonly EcsPoolInject<R_MapRenderInitialization> mapRenderInitializationRPool = default;


        readonly EcsCustomInject<MapModeData> mapModeData = default;

        public void Run(IEcsSystems systems)
        {
            //���������� ����� �� �������
            MapsActivation();
        }

        readonly EcsFilterInject<Inc<R_MapActivation>> mapActivationRFilter = default;
        readonly EcsPoolInject<R_MapActivation> mapActivationRPool = default;
        void MapsActivation()
        {
            //��� ������� ������� ��������� �����
            foreach(int requestEntity in mapActivationRFilter.Value)
            {
                //���� ������
                ref R_MapActivation requestComp = ref mapActivationRPool.Value.Get(requestEntity);

                //������������ �������� �����
                bool isMapDeactivated = MapDeactivation(ref requestComp);

                //���� ����� ���� ��������������
                if(isMapDeactivated == true)
                {
                    //���������� ����������� �����
                    MapActivation(ref requestComp);
                }

                //������� ������
                mapActivationRPool.Value.Del(requestEntity);
            }
        }

        readonly EcsFilterInject<Inc<C_Map, CT_ActiveMap>> activeMapFilter = default;
        /// <summary>
        /// ���������� False, ���� ����������� ����� ��� �������, �� ���� � �� ��������� ��������������
        /// </summary>
        /// <param name="requestComp"></param>
        /// <returns></returns>
        bool MapDeactivation(
            ref R_MapActivation requestComp)
        {
            //���� �������� ����������� �����
            requestComp.mapPE.Unpack(world.Value, out int requestedMapEntity);

            //�������� ����� ����� ���� ������ ����, �� ����� ������������ ����
            foreach (int activeMapEntity in activeMapFilter.Value)
            {
                //���� �������� �����
                ref C_Map activeMap = ref mapPool.Value.Get(activeMapEntity);

                //���� ��� �� �� �����, ������� ��������� ������������
                if(activeMapEntity != requestedMapEntity)
                {
                    //��� ������ ��������� � ����������� PR
                    foreach (int provinceEntity in pRFilter.Value)
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

        void MapActivation(
            ref R_MapActivation requestComp)
        {
            //���� ����������� �����
            requestComp.mapPE.Unpack(world.Value, out int mapEntity);
            ref C_Map map = ref mapPool.Value.Get(mapEntity);

            //��������� �� ��������� �������� �����
            activeMapPool.Value.Add(mapEntity);

            //��� ������ ��������� �����
            for(int a = 0; a < map.provincePEs.Length; a++)
            {
                //���� �������� ��������� � ��������� �� ��������� PR
                map.provincePEs[a].Unpack(world.Value, out int provinceEntity);
                ref C_ProvinceRender pR = ref pRPool.Value.Add(provinceEntity);

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

        readonly EcsPoolInject<R_MapModeActivation> mapModeActivationRPool = default;
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
