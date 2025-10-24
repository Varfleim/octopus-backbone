
using UnityEngine;

namespace GBB
{
    public abstract class GameSubmodule : MonoBehaviour
    {
        public abstract void AddSystems(GameStartup startup);

        public abstract void InjectData(GameStartup startup);
    }
}
