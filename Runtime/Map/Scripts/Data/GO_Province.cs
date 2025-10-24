
using System.Collections.Generic;

using UnityEngine;

namespace GBB.Map
{
    public class GO_Province : MonoBehaviour
    {
        public static GO_Province provinceGOPrefab;
        static List<GO_Province> cachedProvinceGOs = new();

        public static void CacheProvinceGO(
            ref C_ProvinceRender pR)
        {
            //���� GO
            GO_Province provinceGO = pR.ProvinceGO;

            //������� GO � ������ ������������
            cachedProvinceGOs.Add(provinceGO);

            //�������� GO � ������� ������ �� ������������ ������
            provinceGO.gameObject.SetActive(false);
            provinceGO.transform.SetParent(null);

            //������� ������ �� GO
            pR.SetGO(null);
        }

        public static void InstantiateProvinceGO(
            GameObject parentGO,
            ref C_ProvinceRender pR)
        {
            //������ ������ ���������� ��� GO
            GO_Province provinceGO;

            //���� ������ ������������ GO �� ����, �� ���� ������������
            if (cachedProvinceGOs.Count > 0)
            {
                //���� ��������� GO � ������ � ������� ��� �� ������
                provinceGO = cachedProvinceGOs[cachedProvinceGOs.Count - 1];
                cachedProvinceGOs.RemoveAt(cachedProvinceGOs.Count - 1);
            }
            //�����
            else
            {
                //������ ����� GO
                provinceGO = Instantiate(provinceGOPrefab);
            }

            //���������� GO � ������������ � ����������� ������������� GO
            provinceGO.gameObject.SetActive(true);
            provinceGO.gameObject.layer = parentGO.layer;
            provinceGO.transform.SetParent(parentGO.transform, false);
            provinceGO.transform.localPosition = Vector3.zero;
            provinceGO.transform.localScale = Vector3.one;
            provinceGO.transform.localRotation = Quaternion.Euler(0, 0, 0);

            //��� PR ������ �� GO
            pR.SetGO(provinceGO);
        }
    }
}
