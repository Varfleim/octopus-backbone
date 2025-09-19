
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapCreation : IEcsInitSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<CMap> mapPool = default;

        readonly EcsFilterInject<Inc<SRMapCreation>> mapCreationSRFilter = default;
        readonly EcsPoolInject<SRMapCreation> mapCreationSRPool = default;

        readonly EcsPoolInject<RMapActivation> mapActivationRPool = default;

        public void Init(IEcsSystems systems)
        {
            //������ �����
            MapsCreation();
        }

        void MapsCreation()
        {
            //��� ������� ������� �������� �����
            foreach(int mapRequestEntity in mapCreationSRFilter.Value)
            {
                //���� ������
                ref SRMapCreation requestComp = ref mapCreationSRPool.Value.Get(mapRequestEntity);

                //������ �����
                MapCreation(
                    ref requestComp,
                    mapRequestEntity);

                //���� �����
                ref CMap map = ref mapPool.Value.Get(mapRequestEntity);

                if (true)
                {
                    //����������� ��������� �����
                    MapData.MapActivationRequest(
                        world.Value,
                        mapActivationRPool.Value,
                        map.selfPE);
                }

                //������� ������
                mapCreationSRPool.Value.Del(mapRequestEntity);
            }
        }

        void MapCreation(
            ref SRMapCreation requestComp,
            int mapEntity)
        {
            //��������� ���������� �������� ��������� �����
            ref CMap map = ref mapPool.Value.Add(mapEntity);

            //��������� �������� ������ �����
            map = new(
                world.Value.PackEntity(mapEntity), requestComp.mapName);
        }
    }
}
