
using UnityEngine;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Core
{
    public class S_Random_Test : VFSystem, IProtoInitSystem
    {
        [DI] Core_Data core_Data;

        public void Init(IProtoSystems systems)
        {
            Random.InitState(core_Data.Seed);
        }
    }
}
