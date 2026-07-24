
using Leopotam.EcsProto.QoL;

namespace GBB.Input
{
    public readonly struct R_Mouse_MapClickCheck
    {
        public R_Mouse_MapClickCheck(
            ProtoPackedEntity currentProvincePE,
            bool leftMouseButtonClick, bool rightMouseButtonClick)
        {
            this.currentProvincePE = currentProvincePE;

            this.leftMouseButtonClick = leftMouseButtonClick;
            this.rightMouseButtonClick = rightMouseButtonClick;
        }

        public readonly ProtoPackedEntity currentProvincePE;

        public readonly bool leftMouseButtonClick;
        public readonly bool rightMouseButtonClick;
    }
}
