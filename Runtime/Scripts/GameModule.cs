
using UnityEngine;

using Leopotam.EcsProto;

namespace GBB
{
    public abstract class GameModule : MonoBehaviour
    {
        public GameSubmodule[] submodules;

        public A_Aspect mainAspect;

        public abstract void Initialization();

        public void Submodules_AddSystems(GameStartup startup)
        {
            //Для каждого подмодуля
            for (int a = 0; a < submodules.Length; a++)
            {
                //Добавляем системы
                submodules[a].Systems_Add(startup);
            }
        }

        public void Submodules_Aspects_Add(GameStartup startup)
        {
            //Для каждого подмодуля
            for(int a = 0; a < submodules.Length; a++)
            {
                //Добавляем аспекты
                submodules[a].Aspects_Add(
                    startup,
                    mainAspect);
            }
        }

        public void Submodules_InjectData(GameStartup startup)
        {
            //Для каждого подмодуля
            for (int a = 0; a < submodules.Length; a++)
            {
                //Заносим данные
                submodules[a].Data_Inject(startup);
            }
        }
    }
}
