
using Leopotam.EcsProto.QoL;

namespace GBB.Input
{
    public readonly struct R_Mouse_MapPositionCheck
    {
        public R_Mouse_MapPositionCheck(
            ProtoPackedEntity currentProvincePE)
        {
            this.currentProvincePE = currentProvincePE;
        }

        public readonly ProtoPackedEntity currentProvincePE;
    }
}
