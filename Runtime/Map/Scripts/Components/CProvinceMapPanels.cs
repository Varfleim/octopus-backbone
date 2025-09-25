
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace GBB.Map
{
    public struct CProvinceMapPanels
    {
        public CProvinceMapPanels(
            int a)
        {
            mapPanelGroup = null;
        }

        public static VerticalLayoutGroup mapPanelGroupPrefab;
        static List<VerticalLayoutGroup> cachedMapPanelGroups = new();

        public VerticalLayoutGroup mapPanelGroup;

        public static void CacheMapPanelGroup(
            ref CProvinceMapPanels pMP)
        {
            //������� ������ � ������ ������������
            cachedMapPanelGroups.Add(pMP.mapPanelGroup);

            //�������� ������ � �������� ������������ ������
            pMP.mapPanelGroup.gameObject.SetActive(false);
            pMP.mapPanelGroup.transform.SetParent(null);

            //������� ������ �� ������
            pMP.mapPanelGroup = null;
        }

        public static void InstantiateMapPanelGroup(
            ref CProvinceRender pR, ref CProvinceMapPanels pMP,
            Vector3 provinceCenter,
            float mapPanelAltitude)
        {
            //������ ������ ���������� ��� ������ �������
            VerticalLayoutGroup mapPanelGroup;

            //���� ������ ������������ ����� �� ����, �� ���� ������������
            if(cachedMapPanelGroups.Count > 0)
            {
                //���� ��������� ������ � ������ � ������� � �� ������
                mapPanelGroup = cachedMapPanelGroups[cachedMapPanelGroups.Count - 1];
                cachedMapPanelGroups.RemoveAt(cachedMapPanelGroups.Count - 1);
            }
            //�����
            else
            {
                //������ ����� ������
                mapPanelGroup = Object.Instantiate(mapPanelGroupPrefab);
            }

            //���������� ������ � ������������ � � ���������� ���������
            mapPanelGroup.gameObject.SetActive(true);
            mapPanelGroup.transform.SetParent(pR.ProvinceGO.transform);
            pMP.mapPanelGroup = mapPanelGroup;

            //����� ��������� ������
            CalculateMapPanelGroupPosition(
                ref pMP,
                provinceCenter,
                mapPanelAltitude);
        }

        public static void CalculateMapPanelGroupPosition(
            ref CProvinceMapPanels pMP,
            Vector3 provinceCenter,
            float mapPanelAltitude)
        {
            Vector3 direction = provinceCenter.normalized * mapPanelAltitude;

            pMP.mapPanelGroup.transform.position = provinceCenter + direction;
        }
    }
}
