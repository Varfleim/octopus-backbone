
using Leopotam.EcsLite;

namespace GBB.Input
{
    public readonly struct R_Mouse_MapPositionCheck
    {
        public R_Mouse_MapPositionCheck(
            EcsPackedEntity currentProvincePE)
        {
            this.currentProvincePE = currentProvincePE;
        }

        public readonly EcsPackedEntity currentProvincePE;
    }
}
