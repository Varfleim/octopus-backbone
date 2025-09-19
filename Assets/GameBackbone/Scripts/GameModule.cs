
using UnityEngine;

namespace GBB
{
    public abstract class GameModule : ScriptableObject
    {
        public abstract void AddSystems(GameStartup startup);

        public abstract void InjectData(GameStartup startup);
    }
}
