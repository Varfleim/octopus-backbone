
using UnityEngine;

namespace GBB
{
    public abstract class GameSubmodule : MonoBehaviour
    {
        public abstract void Systems_Add(GameStartup startup);

        public abstract void Data_Inject(GameStartup startup);
    }
}
