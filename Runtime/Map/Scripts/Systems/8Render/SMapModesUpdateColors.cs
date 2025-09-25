
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapModesUpdateColors : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<CMapModeCore> mapModeCorePool = default;

        readonly EcsFilterInject<Inc<RMapModeUpdateColorsListSecond>> mapModeUpdateColorsListSecondRFilter = default;
        readonly EcsPoolInject<RMapModeUpdateColorsListSecond> mapModeUpdateColorsListSecondRPool = default;

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
                ref RMapModeUpdateColorsListSecond requestComp = ref mapModeUpdateColorsListSecondRPool.Value.Get(requestEntity);

                //���� ����� �����
                requestComp.mapModePE.Unpack(world.Value, out int mapModeEntity);
                ref CMapModeCore mapMode = ref mapModeCorePool.Value.Get(mapModeEntity);

                //��������� ������ ������
                MapModeUpdateColorsList(
                    ref mapMode,
                    ref requestComp);

                //������� ������
                mapModeUpdateColorsListSecondRPool.Value.Del(requestEntity);
            }
        }

        void MapModeUpdateColorsList(
            ref CMapModeCore mapMode,
            ref RMapModeUpdateColorsListSecond requestComp)
        {
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
