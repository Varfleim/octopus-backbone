
using Leopotam.EcsLite;

namespace GBB.Map.Render
{
    public readonly struct SR_UpdateProvinceRender
    {
        public SR_UpdateProvinceRender(
            EcsPackedEntity displayedObjectPE,
            float height, 
            int colorIndex)
        {
            this.displayedObjectPE = displayedObjectPE;

            this.height = height;
            
            this.colorIndex = colorIndex;
        }

        public readonly EcsPackedEntity displayedObjectPE;

        public readonly float height;

        public readonly int colorIndex;
    }
}
