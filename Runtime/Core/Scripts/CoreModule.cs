
using UnityEngine;

namespace GBB.Core
{
    internal class CoreModule : GameSubmodule
    {
        [SerializeField]
        private CoreData coreData;

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
            #region PostFrame
            //������� �������
            startup.AddPostFrameSystem(new SEventsClear());
            #endregion

            //��������� ������� ����������
            #region PostRender
            //������� �������
            startup.AddPostRenderSystem(new SEventsClear());
            #endregion

            //��������� ��������� �������
            #region PostTick
            //������� �������
            startup.AddPostTickSystem(new SEventsClear());
            #endregion
        }

        public override void InjectData(GameStartup startup)
        {
            //������ ������
            startup.InjectData(coreData);
        }
    }
}
