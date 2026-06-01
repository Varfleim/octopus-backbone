
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map.Render
{
    public class S_MapModes_UpdateColors : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;

        public void Run(IEcsSystems systems)
        {
            //Вторично обновляем списки цветов режимов карты
            MapModes_UpdateColorsListSecond();
        }

        readonly EcsFilterInject<Inc<R_MapMode_UpdateColorsListSecond>> mM_UpdateColorsListSecond_R_F = default;
        readonly EcsPoolInject<R_MapMode_UpdateColorsListSecond> mM_UpdateColorsListSecond_R_P = default;
        void MapModes_UpdateColorsListSecond()
        {
            //Для каждого запроса вторичного обновления списка цветов режима карты
            foreach (int rEntity in mM_UpdateColorsListSecond_R_F.Value)
            {
                //Берём запрос
                ref R_MapMode_UpdateColorsListSecond rComp = ref mM_UpdateColorsListSecond_R_P.Value.Get(rEntity);

                //Обновляем список цветов
                MapMode_UpdateColorsList(
                    ref rComp);

                //Удаляем запрос
                mM_UpdateColorsListSecond_R_P.Value.Del(rEntity);
            }
        }

        readonly EcsPoolInject<C_MapModeCore> mMC_P = default;
        void MapMode_UpdateColorsList(
            ref R_MapMode_UpdateColorsListSecond rComp)
        {
            //Берём режим карты
            rComp.mapModePE.Unpack(world.Value, out int mapModeEntity);
            ref C_MapModeCore mapMode = ref mMC_P.Value.Get(mapModeEntity);

            //Очищаем список цветов режима карты
            mapMode.colors.Clear();

            //Для каждого цвета в запросе
            for (int a = 0; a < rComp.mapModeColors.Count; a++)
            {
                //Заносим цвет в список
                mapMode.colors.Add(rComp.mapModeColors[a]);
            }

            //Обновляем стандартный цвет
            mapMode.defaultColor = rComp.defaultColor;
        }
    }
}
