
using Leopotam.EcsLite;

namespace GBB.Input
{
    public readonly struct R_MouseMapPositionCheck
    {
        public R_MouseMapPositionCheck(
            EcsPackedEntity currentProvincePE)
        {
            this.currentProvincePE = currentProvincePE;
        }

        public readonly EcsPackedEntity currentProvincePE;
    }
}
