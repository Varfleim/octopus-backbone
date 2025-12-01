
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapModeUpdate : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsCustomInject<MapModeData> mapModeData = default;

        public void Run(IEcsSystems systems)
        {
            //Запрашиваем обновление активного режима карты
            MapModeActiveUpdate();
        }

        readonly EcsPoolInject<SR_MapModeUpdate> mapModeUpdateSRPool = default;
        void MapModeActiveUpdate()
        {
            //Берём сущность активного режима карты
            mapModeData.Value.activeMapModePE.Unpack(world.Value, out int activeMapModeEntity);
            //Запрашиваем обновление режима карты
            MapModeData.MapModeUpdateRequest(
                mapModeUpdateSRPool.Value,
                activeMapModeEntity);
        }
    }
}
