
using System.Collections.Generic;

using UnityEngine;

namespace GBB.Map
{
    public class GO_ProvinceHighlight : MonoBehaviour
    {
        public static GO_ProvinceHighlight provinceHighlightPrefab;
        static List<GO_ProvinceHighlight> cachedProvinceHighlights = new();

        public MeshRenderer meshRenderer;
        public MeshFilter meshFilter;

        public static void CacheProvinceHighlight(
            ref C_ProvinceHoverHighlight pHoverHighlight)
        {
            //��������� ����� ��������
            CacheProvinceHighlight(pHoverHighlight.highlight);

            //������� ������ �� ���������
            pHoverHighlight.highlight = null;
        }

        static void CacheProvinceHighlight(
            GO_ProvinceHighlight provinceHighlight)
        {
            //�������� ��������� � ������� ������ �� ������������ ������
            provinceHighlight.gameObject.SetActive(false);
            provinceHighlight.transform.SetParent(null);

            //������� ��������� � ������ ������������
            cachedProvinceHighlights.Add(provinceHighlight);
        }

        public static void InstantiateProvinceHighlight(
            ref C_ProvinceRender pR, ref C_ProvinceHoverHighlight pHoverHighlight,
            Material highlightMaterial)
        {
            //��������� ����� ��������
            GO_ProvinceHighlight provinceHighlight = InstantiateProvinceHighlight(
                ref pR, 
                highlightMaterial);

            //������������ ��������� � ���������� ��������
            pHoverHighlight.highlight = provinceHighlight;
        }

        static GO_ProvinceHighlight InstantiateProvinceHighlight(
            ref C_ProvinceRender pR,
            Material highlightMaterial)
        {
            //������ ������ ���������� ��� ���������
            GO_ProvinceHighlight provinceHighlight;

            //���� ������ ������������ ��������� �� ����, �� ���� ������������
            if (cachedProvinceHighlights.Count > 0)
            {
                //���� ��������� ��������� � ������ � ������� � �� ������
                provinceHighlight = cachedProvinceHighlights[cachedProvinceHighlights.Count - 1];
                cachedProvinceHighlights.RemoveAt(cachedProvinceHighlights.Count - 1);
            }
            //�����
            else
            {
                //������ ����� ���������
                provinceHighlight = Instantiate(provinceHighlightPrefab);
            }

            //���������� ��������� � ������������ � � GO ���������
            provinceHighlight.gameObject.SetActive(true);
            provinceHighlight.transform.SetParent(pR.ProvinceGO.transform);
            provinceHighlight.transform.localScale = Vector3.one;


            //������������� �������� ���������
            provinceHighlight.meshRenderer.material = highlightMaterial;

            //���������� ��������
            return provinceHighlight;
        }

        public static void UpdateProvinceHighlight(
            ref C_ProvinceRender pR,
            GO_ProvinceHighlight provinceHighlight,
            Material highlightMaterial)
        {
            //��������� �������� ���������
            provinceHighlight.meshRenderer.material = highlightMaterial;
        }
    }
}
