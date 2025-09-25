
using UnityEngine;

namespace GBB.Map
{
    [CreateAssetMenu]
    internal class MapModule : GameModule
    {
        public float mapPanelAltitude;

        public GOProvince provinceGOPrefab;
        public GOProvinceHighlight provinceHighlightGOPrefab;
        public UnityEngine.UI.VerticalLayoutGroup provinceMapPanelGroupPrefab;

        public override void AddSystems(GameStartup startup)
        {
            //��������� ������� �������������
            #region PreInit
            //�������� ���� � �������� ���������
            startup.AddPreInitSystem(new SMapCreation());
            #endregion
            #region Init
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
            //������ ��������� ������ ����
            MapData mapData = startup.AddDataObject().AddComponent<MapData>();

            //������ ������
            startup.InjectData(mapData);

            //������ ��������� ������ ������� �����
            MapModeData mapModeData = startup.AddDataObject().AddComponent<MapModeData>();

            //������ ������
            startup.InjectData(mapModeData);

            //������ ��������� ������ ���������
            ProvinceData provinceData = startup.AddDataObject().AddComponent<ProvinceData>();

            //��������� � ���� ������
            provinceData.mapPanelAltitude = mapPanelAltitude;

            //������ ������
            startup.InjectData(provinceData);

            GOProvince.provinceGOPrefab = provinceGOPrefab;
            GOProvinceHighlight.provinceHighlightPrefab = provinceHighlightGOPrefab;
            CProvinceMapPanels.mapPanelGroupPrefab = provinceMapPanelGroupPrefab;
        }
    }
}
