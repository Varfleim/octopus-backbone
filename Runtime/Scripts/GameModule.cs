
using UnityEngine;

namespace GBB
{
    public abstract class GameModule : MonoBehaviour
    {
        public GameSubmodule[] submodules;

        public void AddSubmodulesSystems(GameStartup startup)
        {
            //��� ������� ���������
            for (int a = 0; a < submodules.Length; a++)
            {
                //��������� �������
                submodules[a].AddSystems(startup);
            }
        }

        public void InjectSubmodulesData(GameStartup startup)
        {
            //��� ������� ���������
            for (int a = 0; a < submodules.Length; a++)
            {
                //������� ������
                submodules[a].InjectData(startup);
            }
        }
    }
}
