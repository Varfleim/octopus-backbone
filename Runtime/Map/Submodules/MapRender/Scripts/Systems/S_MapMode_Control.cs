
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map.Render
{
    public class S_MapMode_Control : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<SR_MapMode_Update> mM_Update_SR_P = default;


        readonly EcsCustomInject<MainMapMode_Data> mainMapMode_Data = default;

        public void Run(IEcsSystems systems)
        {
            //Активируем режим карты по запросу
            MapModes_Activation();
        }

        readonly EcsFilterInject<Inc<R_MapMode_Activation>> mM_Activation_R_F = default;
        readonly EcsPoolInject<R_MapMode_Activation> mM_Activation_R_P = default;
        void MapModes_Activation()
        {
            //Для каждого запроса активации режима карты
            foreach(int rEntity in mM_Activation_R_F.Value)
            {
                //Берём запрос
                ref R_MapMode_Activation rComp = ref mM_Activation_R_P.Value.Get(rEntity);

                //Если текущий активный режим карты был деактивирован
                if(MapMode_DeactivationCheck(ref rComp))
                {
                    //Активируем режим карты
                    MapMode_Activation(ref rComp);
                }

                //Удаляем запрос
                mM_Activation_R_P.Value.Del(rEntity);
            }
        }

        /// <summary>
        /// Возвращает False, если запрошенный режим карты уже активен, то есть его не требуется деактивировать
        /// </summary>
        /// <param name="rComp"></param>
        /// <returns></returns>
        bool MapMode_DeactivationCheck(
            ref R_MapMode_Activation rComp)
        {
            //Если активен не тот режим карты, который требуется активировать
            if(mainMapMode_Data.Value.ActiveMapModePE.EqualsTo(rComp.mapModePE) == false)
            {
                //Удаляем PE активного режима
                mainMapMode_Data.Value.ActiveMapModePE = new();

                //Возвращаем, что режим карты деактивирован
                return true;
            }

            return false;
        }

        readonly EcsPoolInject<C_MapModeCore> mMC_P = default;
        readonly EcsPoolInject<R_Map_UpdateProvincesRender> map_UpdatePR_R_P = default;
        void MapMode_Activation(
            ref R_MapMode_Activation rComp)
        {
            //Берём запрошенный режим карты
            rComp.mapModePE.Unpack(world.Value, out int mapModeEntity);
            ref C_MapModeCore mapMode = ref mMC_P.Value.Get(mapModeEntity);

            //Сохраняем PE режима как активного
            mainMapMode_Data.Value.ActiveMapModePE = world.Value.PackEntity(mapModeEntity);

            //Отменяем все запросы обновления режимов карты
            MapMode_UpdatesCancel();

            //Запрашиваем обновление режима карты
            MainMapMode_Data.MapMode_Update_R(
                mM_Update_SR_P.Value,
                mapModeEntity);

            //Запрашиваем обновление провинций карты
            MapRender_Data.Map_UpdateProvincesRender_Request(
                world.Value,
                map_UpdatePR_R_P.Value,
                true, false, false);

            UnityEngine.Debug.LogWarning(mapMode.selfName);
        }

        readonly EcsFilterInject<Inc<SR_MapMode_Update>> mM_Update_SR_F = default;
        /// <summary>
        /// Удаление всех существующих запросов обновления режима карты
        /// </summary>
        void MapMode_UpdatesCancel()
        {
            //Для каждого запроса обновления режима карты
            foreach(int mapModeEntity in mM_Update_SR_F.Value)
            {
                //Удаляем запрос
                mM_Update_SR_P.Value.Del(mapModeEntity);
            }
        }
    }
}
