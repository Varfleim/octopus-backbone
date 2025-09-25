
using Leopotam.EcsLite;

namespace GBB.Input
{
    public readonly struct RMouseMapPositionCheck
    {
        public RMouseMapPositionCheck(
            EcsPackedEntity currentProvincePE)
        {
            this.currentProvincePE = currentProvincePE;
        }

        public readonly EcsPackedEntity currentProvincePE;
    }
}
