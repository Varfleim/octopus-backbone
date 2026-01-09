
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map.Render
{
    public class S_MapModes_CreationMain : IEcsInitSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsCustomInject<MainMapMode_Data> mainMapMode_Data = default;

        public void Init(IEcsSystems systems)
        {
            //Создаём режимы карты по запросам
            MapModes_Creation();
        }

        readonly EcsFilterInject<Inc<SR_MapModeCore_Creation>> mMC_Creation_SR_F = default;
        readonly EcsPoolInject<SR_MapModeCore_Creation> mMC_Creation_SR_P = default;
        void MapModes_Creation()
        {
            //Для каждого запроса создания режима карты
            foreach(int mapModeRequestEntity in mMC_Creation_SR_F.Value)
            {
                //Берём запрос
                ref SR_MapModeCore_Creation requestComp = ref mMC_Creation_SR_P.Value.Get(mapModeRequestEntity);

                //Создаём режим карты
                MapMode_Creation(
                    mapModeRequestEntity,
                    ref requestComp);

                //ТЕСТ
                //Берём режим карты
                ref C_MapModeCore mapMode = ref mMC_P.Value.Get(mapModeRequestEntity);

                //Если данный режим карты указан как стандартный
                if(requestComp.defaultMapMode == true)
                {
                    //Сохраняем его как стандартный режим карты
                    mainMapMode_Data.Value.DefaultMapModePE = world.Value.PackEntity(mapModeRequestEntity);
                }
                //ТЕСТ

                //Удаляем запрос
                mMC_Creation_SR_P.Value.Del(mapModeRequestEntity);
            }
        }

        readonly EcsPoolInject<C_MapModeCore> mMC_P = default;
        void MapMode_Creation(
            int mapModeEntity,
            ref SR_MapModeCore_Creation requestComp)
        {
            //Назначаем сущности режима карты компонент режима карты
            ref C_MapModeCore mapMode = ref mMC_P.Value.Add(mapModeEntity);

            //Заполняем основные данные режима
            mapMode = new(
                requestComp.name);
        }
    }
}
