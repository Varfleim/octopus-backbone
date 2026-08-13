
using UnityEngine;

using Leopotam.EcsProto;

namespace GBB
{
    public abstract class GameModule : MonoBehaviour
    {
        public GameSubmodule[] submodules;

        public A_Aspect mainAspect;

        public abstract void Initialization();

        public int Submodules_AddSystems(
            GameStartup startup,
            int submodulesCount)
        {
            //Для каждого подмодуля
            for (int a = 0; a < submodules.Length; a++)
            {
                //Инициализируем подмодуль
                submodules[a].Submodule_Initialization(submodulesCount);

                //Добавляем системы
                submodules[a].Systems_Add(startup);

                //Увеличиваем счётчик подмодулей
                submodulesCount++;
            }

            //Возвращаем увеличенный счётчик
            return submodulesCount;
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
