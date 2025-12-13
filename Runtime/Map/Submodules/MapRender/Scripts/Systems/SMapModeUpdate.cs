
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map.Render
{
    public class SMapModeUpdate : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsCustomInject<MainMapModeData> mainMapModeData = default;

        public void Run(IEcsSystems systems)
        {
            //Запрашиваем обновление активного режима карты
            MapModeActiveUpdate();
        }

        readonly EcsPoolInject<SR_MapModeUpdate> mapModeUpdateSRPool = default;
        void MapModeActiveUpdate()
        {
            //Берём сущность активного режима карты
            mainMapModeData.Value.ActiveMapModePE.Unpack(world.Value, out int activeMapModeEntity);

            //Запрашиваем обновление режима карты
            MainMapModeData.MapModeUpdateRequest(
                mapModeUpdateSRPool.Value,
                activeMapModeEntity);
        }
    }
}
