
using Leopotam.EcsLite;

namespace GBB.Map
{
    internal readonly struct R_MapActivation
    {
        public R_MapActivation(
            EcsPackedEntity mapPE)
        {
            this.mapPE = mapPE;
        }

        public readonly EcsPackedEntity mapPE;
    }
}
