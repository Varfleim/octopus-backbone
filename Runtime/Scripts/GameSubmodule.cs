
using UnityEngine;

namespace GBB
{
    public class GameSubmodule : MonoBehaviour
    {
        private int submoduleIndex;

        public void Submodule_Initialization(
            int submoduleIndex)
        {
            this.submoduleIndex = submoduleIndex;
        }

        public virtual void Systems_Add(GameStartup startup)
        {

        }

        protected T System_New<T>(
            SystemWeight systemWeight) where T : VFSystem, new()
        {
            T system = new();

            system.systemWeight = systemWeight;
            system.fullName = submoduleIndex + " | " + (int)system.systemWeight + " | " + system.GetType().ToString();

            system.systemSubmodule = this;
            system.systemSubmoduleIndex = submoduleIndex;

            return system;
        }

        public virtual void Aspects_Add(
            GameStartup startup,
            A_Aspect parentAspect)
        {

        }

        public virtual void Data_Inject(GameStartup startup)
        {

        }

    }
}
