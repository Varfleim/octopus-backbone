
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapCreation : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_Map> mapPool = default;

        readonly EcsPoolInject<R_MapActivation> mapActivationRPool = default;

        public void Init(IEcsSystems systems)
        {
            //������ �����
            MapsCreation();
        }

        public void Run(IEcsSystems systems)
        {
            //������ �����
            MapsCreation();
        }

        readonly EcsFilterInject<Inc<SR_MapCreation>> mapCreationSRFilter = default;
        readonly EcsPoolInject<SR_MapCreation> mapCreationSRPool = default;
        void MapsCreation()
        {
            //��� ������� ������� �������� �����
            foreach(int mapRequestEntity in mapCreationSRFilter.Value)
            {
                //���� ������
                ref SR_MapCreation requestComp = ref mapCreationSRPool.Value.Get(mapRequestEntity);

                //������ �����
                MapCreation(
                    ref requestComp,
                    mapRequestEntity);

                //����
                //���� �����
                ref C_Map map = ref mapPool.Value.Get(mapRequestEntity);

                if (true)
                {
                    //����������� ��������� �����
                    MapData.MapActivationRequest(
                        world.Value,
                        mapActivationRPool.Value,
                        world.Value.PackEntity(mapRequestEntity));
                }
                //����

                //������� ������
                mapCreationSRPool.Value.Del(mapRequestEntity);
            }
        }

        void MapCreation(
            ref SR_MapCreation requestComp,
            int mapEntity)
        {
            //��������� ���������� �������� ��������� �����
            ref C_Map map = ref mapPool.Value.Add(mapEntity);

            //��������� �������� ������ �����
            map = new(
                world.Value.PackEntity(mapEntity), requestComp.mapName);
        }
    }
}
