
using Leopotam.EcsLite;

namespace GBB.Map
{
    internal readonly struct RMapActivation
    {
        public RMapActivation(
            EcsPackedEntity mapPE)
        {
            this.mapPE = mapPE;
        }

        public readonly EcsPackedEntity mapPE;
    }
}
