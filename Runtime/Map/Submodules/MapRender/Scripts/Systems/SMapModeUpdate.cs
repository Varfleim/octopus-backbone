
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
            //«апрашиваем обновление активного режима карты
            MapModeActiveUpdate();
        }

        readonly EcsPoolInject<SR_MapModeUpdate> mapModeUpdateSRPool = default;
        void MapModeActiveUpdate()
        {
            //≈сли есть активный режим карты
            if(mainMapModeData.Value.ActiveMapModePE.Unpack(world.Value, out int activeMapModeEntity))
            {
                //«апрашиваем обновление режима карты
                MainMapModeData.MapModeUpdateRequest(
                    mapModeUpdateSRPool.Value,
                    activeMapModeEntity);
            }
        }
    }
}
