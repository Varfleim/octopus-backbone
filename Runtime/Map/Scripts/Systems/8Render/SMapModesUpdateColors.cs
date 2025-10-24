
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapModesUpdateColors : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_MapModeCore> mapModeCorePool = default;

        readonly EcsFilterInject<Inc<R_MapModeUpdateColorsListSecond>> mapModeUpdateColorsListSecondRFilter = default;
        readonly EcsPoolInject<R_MapModeUpdateColorsListSecond> mapModeUpdateColorsListSecondRPool = default;

        public void Run(IEcsSystems systems)
        {
            //�������� ��������� ������ ������ ������� �����
            MapModesUpdateColorsListSecond();
        }

        void MapModesUpdateColorsListSecond()
        {
            //��� ������� ������� ���������� ���������� ������ ������ ������ �����
            foreach (int requestEntity in mapModeUpdateColorsListSecondRFilter.Value)
            {
                //���� ������
                ref R_MapModeUpdateColorsListSecond requestComp = ref mapModeUpdateColorsListSecondRPool.Value.Get(requestEntity);

                //��������� ������ ������
                MapModeUpdateColorsList(
                    ref requestComp);

                //������� ������
                mapModeUpdateColorsListSecondRPool.Value.Del(requestEntity);
            }
        }

        void MapModeUpdateColorsList(
            ref R_MapModeUpdateColorsListSecond requestComp)
        {
            //���� ����� �����
            requestComp.mapModePE.Unpack(world.Value, out int mapModeEntity);
            ref C_MapModeCore mapMode = ref mapModeCorePool.Value.Get(mapModeEntity);

            //������� ������ ������ ������ �����
            mapMode.colors.Clear();

            //��� ������� ����� � �������
            for (int a = 0; a < requestComp.mapModeColors.Count; a++)
            {
                //������� ���� � ������
                mapMode.colors.Add(requestComp.mapModeColors[a]);
            }

            //��������� ����������� ����
            mapMode.defaultColor = requestComp.defaultColor;
        }
    }
}
