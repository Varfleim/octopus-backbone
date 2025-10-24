
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
            //Берём GO
            GO_Province provinceGO = pR.ProvinceGO;

            //Заносим GO в список кэшированных
            cachedProvinceGOs.Add(provinceGO);

            //Скрываем GO и удаляем ссылку на родительский объект
            provinceGO.gameObject.SetActive(false);
            provinceGO.transform.SetParent(null);

            //Удаляем ссылку на GO
            pR.SetGO(null);
        }

        public static void InstantiateProvinceGO(
            GameObject parentGO,
            ref C_ProvinceRender pR)
        {
            //Создаём пустую переменную для GO
            GO_Province provinceGO;

            //Если список кэшированных GO не пуст, то берём кэшированный
            if (cachedProvinceGOs.Count > 0)
            {
                //Берём последний GO в списке и удаляем его из списка
                provinceGO = cachedProvinceGOs[cachedProvinceGOs.Count - 1];
                cachedProvinceGOs.RemoveAt(cachedProvinceGOs.Count - 1);
            }
            //Иначе
            else
            {
                //Создаём новый GO
                provinceGO = Instantiate(provinceGOPrefab);
            }

            //Отображаем GO и присоединяем к переданному родительскому GO
            provinceGO.gameObject.SetActive(true);
            provinceGO.gameObject.layer = parentGO.layer;
            provinceGO.transform.SetParent(parentGO.transform, false);
            provinceGO.transform.localPosition = Vector3.zero;
            provinceGO.transform.localScale = Vector3.one;
            provinceGO.transform.localRotation = Quaternion.Euler(0, 0, 0);

            //Даём PR ссылку на GO
            pR.SetGO(provinceGO);
        }
    }
}
