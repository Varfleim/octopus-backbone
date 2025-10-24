
using UnityEngine;

namespace GBB.Map
{
    internal class MapModule : GameSubmodule
    {
        [SerializeField]
        private MapData mapData;
        [SerializeField]
        private MapModeData mapModeData;
        [SerializeField]
        private ProvinceData provinceData;

        public override void AddSystems(GameStartup startup)
        {
            //��������� ������� �������������
            #region PreInit

            #endregion
            #region Init
            //�������� ���� � �������� ���������
            startup.AddInitSystem(new SMapCreation());

            //�������� ������� ����������� ���������
            //startup.AddInitSystem(new SProvinceCoreCreation());

            //�������� ������� ����������� ������� �����
            startup.AddInitSystem(new SMainMapModesCreation());
            #endregion

            //��������� ���������� �������


            //��������� ������� ����������
            #region PreRender
            //���������� �������
            startup.AddPreRenderSystem(new SMapControl());

            //��������� ������� �����
            startup.AddPreRenderSystem(new SMapModesActivation());
            //��������� ������ ������ ������������ ������� �����
            startup.AddPreRenderSystem(new SMapModeRenderStart());
            #endregion
            #region Render
            //���������� ������ ������� �����
            startup.AddRenderSystem(new SMapModesUpdateColors());
            #endregion
            #region PostRender
            //��������� ���������� ������� �����
            startup.AddPostRenderSystem(new SMapRender());

            //���������� ������ ������ ������������ ������ �����
            startup.AddPostRenderSystem(new SMapModeRenderEnd());
            #endregion

            //��������� ��������� �������
            #region PostTick
            //������ ���������� ��������� ������ �����
            startup.AddPostTickSystem(new SMapModeUpdate());
            #endregion
        }

        public override void InjectData(GameStartup startup)
        {
            //������ ������
            startup.InjectData(mapData);

            //������ ������
            startup.InjectData(mapModeData);

            //������ ������
            startup.InjectData(provinceData);
        }
    }
}
