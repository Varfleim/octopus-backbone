
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;

namespace GBB.Map.Render
{
    public class S_MapMode_RenderEnd : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;

        public void Run(IEcsSystems systems)
        {
            //Выключаем системы визуализации режимов карты
            MapModes_RenderSystemsDeactivation();
        }

        readonly EcsFilterInject<Inc<C_MapModeCore, SR_MapMode_Update>> mMC_Updated_F = default;
        readonly EcsPoolInject<C_MapModeCore> mMC_P = default;
        readonly EcsPoolInject<SR_MapMode_Update> mMC_Update_SR_P = default;
        readonly EcsPoolInject<EcsGroupSystemState> ecsGroupSystemState_P = default;
        void MapModes_RenderSystemsDeactivation()
        {
            //Для каждого режима карты с запросом обновления
            foreach (int mapModeEntity in mMC_Updated_F.Value)
            {
                //Берём режим карты
                ref C_MapModeCore mapMode = ref mMC_P.Value.Get(mapModeEntity);

                //Создаём новую сущность и назначаем ей запрос переключения группы систем
                int requestEntity = world.Value.NewEntity();
                ref EcsGroupSystemState requestComp = ref ecsGroupSystemState_P.Value.Add(requestEntity);

                //Заполняем данные запроса
                requestComp.Name = mapMode.selfName;
                requestComp.State = false;

                //Удаляем запрос обновления режима карты
                mMC_Update_SR_P.Value.Del(mapModeEntity);
            }
        }
    }
}
