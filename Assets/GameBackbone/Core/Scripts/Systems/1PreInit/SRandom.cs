
using UnityEngine;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Core
{
    public class SRandom : IEcsInitSystem
    {
        readonly EcsCustomInject<CoreData> coreData = default;

        public void Init(IEcsSystems systems)
        {
            Random.InitState(coreData.Value.seed);
        }
    }
}
