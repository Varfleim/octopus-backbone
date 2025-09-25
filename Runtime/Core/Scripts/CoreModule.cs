
using UnityEngine;

namespace GBB.Core
{
    [CreateAssetMenu]
    public class CoreModule : GameModule
    {
        public int seed;

        public override void AddSystems(GameStartup startup)
        {
            //��������� ������� �������������
            #region PreInit
            //������������� RNG
            startup.AddPreInitSystem(new SRandom());
            #endregion
            #region Init

            #endregion
            #region PostInit
            //������� �������
            startup.AddPostInitSystem(new SEventsClear());
            #endregion

            //��������� ���������� �������

            //��������� ������� ����������

            //��������� ��������� �������

        }

        public override void InjectData(GameStartup startup)
        {
            //������ ��������� ������
            CoreData coreData = startup.AddDataObject().AddComponent<CoreData>();

            //��������� � ���� ������
            coreData.seed = seed;

            //������ ������
            startup.InjectData(coreData);
        }
    }
}
