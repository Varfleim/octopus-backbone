
using UnityEngine;

namespace GBB
{
    public abstract class GameModule : MonoBehaviour
    {
        public GameSubmodule[] submodules;

        public void AddSubmodulesSystems(GameStartup startup)
        {
            //Для каждого подмодуля
            for (int a = 0; a < submodules.Length; a++)
            {
                //Добавляем системы
                submodules[a].AddSystems(startup);
            }
        }

        public void InjectSubmodulesData(GameStartup startup)
        {
            //Для каждого подмодуля
            for (int a = 0; a < submodules.Length; a++)
            {
                //Заносим данные
                submodules[a].InjectData(startup);
            }
        }
    }
}
