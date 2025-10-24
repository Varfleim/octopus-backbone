
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
            //Вторично обновляем списки цветов режимов карты
            MapModesUpdateColorsListSecond();
        }

        void MapModesUpdateColorsListSecond()
        {
            //Для каждого запроса вторичного обновления списка цветов режима карты
            foreach (int requestEntity in mapModeUpdateColorsListSecondRFilter.Value)
            {
                //Берём запрос
                ref R_MapModeUpdateColorsListSecond requestComp = ref mapModeUpdateColorsListSecondRPool.Value.Get(requestEntity);

                //Обновляем список цветов
                MapModeUpdateColorsList(
                    ref requestComp);

                //Удаляем запрос
                mapModeUpdateColorsListSecondRPool.Value.Del(requestEntity);
            }
        }

        void MapModeUpdateColorsList(
            ref R_MapModeUpdateColorsListSecond requestComp)
        {
            //Берём режим карты
            requestComp.mapModePE.Unpack(world.Value, out int mapModeEntity);
            ref C_MapModeCore mapMode = ref mapModeCorePool.Value.Get(mapModeEntity);

            //Очищаем список цветов режима карты
            mapMode.colors.Clear();

            //Для каждого цвета в запросе
            for (int a = 0; a < requestComp.mapModeColors.Count; a++)
            {
                //Заносим цвет в список
                mapMode.colors.Add(requestComp.mapModeColors[a]);
            }

            //Обновляем стандартный цвет
            mapMode.defaultColor = requestComp.defaultColor;
        }
    }
}
