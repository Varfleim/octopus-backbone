
using Leopotam.EcsLite;

namespace GBB.Map.Render
{
    public readonly struct R_Map_Activation
    {
        public R_Map_Activation(
            EcsPackedEntity mapPE)
        {
            this.mapPE = mapPE;
        }

        public readonly EcsPackedEntity mapPE;
    }
}
