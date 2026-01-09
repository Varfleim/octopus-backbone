
using UnityEngine;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Core
{
    public class S_Random_Test : IEcsInitSystem
    {
        readonly EcsCustomInject<Core_Data> core_Data = default;

        public void Init(IEcsSystems systems)
        {
            Random.InitState(core_Data.Value.Seed);
        }
    }
}
