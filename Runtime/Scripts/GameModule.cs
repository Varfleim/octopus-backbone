
using UnityEngine;

namespace GBB
{
    public abstract class GameModule : MonoBehaviour
    {
        public GameSubmodule[] submodules;

        public void Submodules_AddSystems(GameStartup startup)
        {
            //Для каждого подмодуля
            for (int a = 0; a < submodules.Length; a++)
            {
                //Добавляем системы
                submodules[a].Systems_Add(startup);
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
