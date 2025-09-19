
using Leopotam.EcsLite;

namespace GBB.Map
{
    public struct RMapModeActivation
    {
        public RMapModeActivation(
            EcsPackedEntity mapModePE)
        {
            this.mapModePE = mapModePE;
        }

        public readonly EcsPackedEntity mapModePE;
    }
}
