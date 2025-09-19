
using System.Collections.Generic;

using UnityEngine;

namespace GBB.Map
{
    public class GOProvinceHighlight : MonoBehaviour
    {
        public static GOProvinceHighlight provinceHighlightPrefab;
        static List<GOProvinceHighlight> cachedProvinceHighlights = new();

        public MeshRenderer meshRenderer;
        public MeshFilter meshFilter;

        public static void CacheProvinceHighlight(
            ref CProvinceHoverHighlight pHoverHighlight)
        {
            //��������� ����� ��������
            CacheProvinceHighlight(pHoverHighlight.highlight);

            //������� ������ �� ���������
            pHoverHighlight.highlight = null;
        }

        static void CacheProvinceHighlight(
            GOProvinceHighlight provinceHighlight)
        {
            //�������� ��������� � ������� ������ �� ������������ ������
            provinceHighlight.gameObject.SetActive(false);
            provinceHighlight.transform.SetParent(null);

            //������� ��������� � ������ ������������
            cachedProvinceHighlights.Add(provinceHighlight);
        }

        public static void InstantiateProvinceHighlight(
            ref CProvinceRender pR, ref CProvinceHoverHighlight pHoverHighlight,
            Material highlightMaterial)
        {
            //��������� ����� ��������
            GOProvinceHighlight provinceHighlight = InstantiateProvinceHighlight(
                ref pR, 
                highlightMaterial);

            //������������ ��������� � ���������� ��������
            pHoverHighlight.highlight = provinceHighlight;
        }

        static GOProvinceHighlight InstantiateProvinceHighlight(
            ref CProvinceRender pR,
            Material highlightMaterial)
        {
            //������ ������ ���������� ��� ���������
            GOProvinceHighlight provinceHighlight;

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
            ref CProvinceRender pR,
            GOProvinceHighlight provinceHighlight,
            Material highlightMaterial)
        {
            //��������� �������� ���������
            provinceHighlight.meshRenderer.material = highlightMaterial;
        }
    }
}
