
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Map.Render
{
    public class S_MapModes_UpdateColors : VFSystem, IProtoRunSystem
    {
        [DI] A_CoreMapMode coreMapMode_A;

        public void Run()
        {
            //Вторично обновляем списки цветов режимов карты
            MapModes_UpdateColorsListSecond();
        }

        void MapModes_UpdateColorsListSecond()
        {
            //Для каждого запроса вторичного обновления списка цветов режима карты
            foreach (ProtoEntity rEntity in coreMapMode_A.mM_UpdateColorsListSecond_R_I)
            {
                //Берём запрос
                ref R_MapMode_UpdateColorsListSecond rComp = ref coreMapMode_A.mM_UpdateColorsListSecond_R_P.Get(rEntity);

                //Обновляем список цветов
                MapMode_UpdateColorsList(
                    ref rComp);

                //Удаляем запрос
                coreMapMode_A.mM_UpdateColorsListSecond_R_P.Del(rEntity);
            }
        }

        void MapMode_UpdateColorsList(
            ref R_MapMode_UpdateColorsListSecond rComp)
        {
            //Берём режим карты
            ref C_MapModeCore mapMode = ref coreMapMode_A.mMC_P.Get(rComp.mMEntity);

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
