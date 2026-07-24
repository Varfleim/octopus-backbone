
using Leopotam.EcsProto;

namespace GBB.Map.Render
{
    public readonly struct SR_ProvinceRender_Update
    {
        public SR_ProvinceRender_Update(
            ProtoEntity displayedObjectEntity,
            float height, 
            int colorIndex)
        {
            this.displayedObjectEntity = displayedObjectEntity;

            this.height = height;
            
            this.colorIndex = colorIndex;
        }

        public readonly ProtoEntity displayedObjectEntity;

        public readonly float height;

        public readonly int colorIndex;
    }
}
