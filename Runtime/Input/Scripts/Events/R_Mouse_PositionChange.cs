
using Leopotam.EcsProto.QoL;

namespace GBB.Input
{
    public readonly struct R_Mouse_PositionChange
    {
        public R_Mouse_PositionChange(
            bool isMouseOverMap,
            ProtoPackedEntity lastHitProvincePE)
        {
            this.isMouseOverMap = isMouseOverMap;

            this.lastHitProvincePE = lastHitProvincePE;
        }

        public readonly bool isMouseOverMap;

        public readonly ProtoPackedEntity lastHitProvincePE;
    }
}
