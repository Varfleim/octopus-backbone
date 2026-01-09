
using Leopotam.EcsLite;

namespace GBB.Input
{
    public readonly struct R_Mouse_MapClickCheck
    {
        public R_Mouse_MapClickCheck(
            EcsPackedEntity currentProvincePE,
            bool leftMouseButtonClick, bool rightMouseButtonClick)
        {
            this.currentProvincePE = currentProvincePE;

            this.leftMouseButtonClick = leftMouseButtonClick;
            this.rightMouseButtonClick = rightMouseButtonClick;
        }

        public readonly EcsPackedEntity currentProvincePE;

        public readonly bool leftMouseButtonClick;
        public readonly bool rightMouseButtonClick;
    }
}
