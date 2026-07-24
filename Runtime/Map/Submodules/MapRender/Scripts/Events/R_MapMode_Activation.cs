
using Leopotam.EcsProto;

namespace GBB.Map.Render
{
    public struct R_MapMode_Activation
    {
        public R_MapMode_Activation(
            ProtoEntity mMEntity)
        {
            this.mMEntity = mMEntity;
        }

        public readonly ProtoEntity mMEntity;
    }
}
