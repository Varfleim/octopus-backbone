
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map.Render
{
    public class S_MapMode_Update : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsCustomInject<MainMapMode_Data> mainMapMode_Data = default;

        public void Run(IEcsSystems systems)
        {
            //«апрашиваем обновление активного режима карты
            MapMode_Active_Update_Request();
        }

        readonly EcsPoolInject<SR_MapMode_Update> mMC_Update_SR_P = default;
        void MapMode_Active_Update_Request()
        {
            //≈сли есть активный режим карты
            if(mainMapMode_Data.Value.ActiveMapModePE.Unpack(world.Value, out int mapModeEntity))
            {
                //«апрашиваем обновление режима карты
                MainMapMode_Data.MapMode_Update_R(
                    mMC_Update_SR_P.Value,
                    mapModeEntity);
            }
        }
    }
}
