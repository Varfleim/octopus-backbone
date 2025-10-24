
using Leopotam.EcsLite;

namespace GBB.Map
{
    public struct R_MapModeActivation
    {
        public R_MapModeActivation(
            EcsPackedEntity mapModePE)
        {
            this.mapModePE = mapModePE;
        }

        public readonly EcsPackedEntity mapModePE;
    }
}
