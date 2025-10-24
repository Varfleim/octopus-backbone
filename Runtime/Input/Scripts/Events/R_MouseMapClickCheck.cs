
using Leopotam.EcsLite;

namespace GBB.Input
{
    public readonly struct R_MouseMapClickCheck
    {
        public R_MouseMapClickCheck(
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
