
using Leopotam.EcsLite;

namespace GBB.Map.Render
{
    public struct R_MapMode_Activation
    {
        public R_MapMode_Activation(
            EcsPackedEntity mapModePE)
        {
            this.mapModePE = mapModePE;
        }

        public readonly EcsPackedEntity mapModePE;
    }
}
