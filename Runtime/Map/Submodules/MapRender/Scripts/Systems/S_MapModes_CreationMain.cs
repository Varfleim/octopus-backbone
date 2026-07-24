
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Map.Render
{
    public class S_MapModes_CreationMain : IProtoInitSystem
    {
        [DI] A_CoreMapMode coreMapMode_A;

        [DI] MainMapMode_Data mainMapMode_Data;

        public void Init(IProtoSystems systems)
        {
            //Создаём режимы карты по запросам
            MapModes_Creation();
        }

        void MapModes_Creation()
        {
            //Для каждого запроса создания режима карты
            foreach(ProtoEntity mapModeEntity in coreMapMode_A.mMC_Creation_SR_I)
            {
                //Берём запрос
                ref SR_MapModeCore_Creation rComp = ref coreMapMode_A.mMC_Creation_SR_P.Get(mapModeEntity);

                //Создаём режим карты
                MapMode_Creation(
                    mapModeEntity,
                    ref rComp);

                //ТЕСТ
                //Берём режим карты
                ref C_MapModeCore mapMode = ref coreMapMode_A.mMC_P.Get(mapModeEntity);

                //Если данный режим карты указан как стандартный
                if(rComp.defaultMapMode == true)
                {
                    //Сохраняем его как стандартный режим карты
                    mainMapMode_Data.DefaultMapModeEntity = mapModeEntity;
                }
                //ТЕСТ

                //Удаляем запрос
                coreMapMode_A.mMC_Creation_SR_P.Del(mapModeEntity);
            }
        }

        void MapMode_Creation(
            ProtoEntity mapModeEntity,
            ref SR_MapModeCore_Creation rComp)
        {
            //Назначаем сущности режима карты компонент режима карты
            ref C_MapModeCore mapMode = ref coreMapMode_A.mMC_P.Add(mapModeEntity);

            //Заполняем основные данные режима
            mapMode = new(
                rComp.name);
        }
    }
}
