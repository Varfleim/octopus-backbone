
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapModeUpdate : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            //«апрашиваем обновление активного режима карты
            MapModeActiveUpdate();
        }

        readonly EcsFilterInject<Inc<C_MapModeCore, CT_ActiveMapMode>> activeMapModeFilter = default;
        readonly EcsPoolInject<SR_MapModeUpdate> mapModeUpdateSRPool = default;
        void MapModeActiveUpdate()
        {
            //ƒл€ каждого активного режима карты
            foreach(int activeMapModeEntity in activeMapModeFilter.Value)
            {
                //«апрашиваем обновление режима карты
                MapModeData.MapModeUpdateRequest(
                    mapModeUpdateSRPool.Value,
                    activeMapModeEntity);
            }
        }
    }
}
