
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
            //Совершаем общие действия
            CacheProvinceHighlight(pHoverHighlight.highlight);

            //Удаляем ссылку на подсветку
            pHoverHighlight.highlight = null;
        }

        static void CacheProvinceHighlight(
            GO_ProvinceHighlight provinceHighlight)
        {
            //Скрываем подсветку и удаляем ссылку на родительский объект
            provinceHighlight.gameObject.SetActive(false);
            provinceHighlight.transform.SetParent(null);

            //Заносим подсветку в список кэшированных
            cachedProvinceHighlights.Add(provinceHighlight);
        }

        public static void InstantiateProvinceHighlight(
            ref C_ProvinceRender pR, ref C_ProvinceHoverHighlight pHoverHighlight,
            Material highlightMaterial)
        {
            //Совершаем общие действия
            GO_ProvinceHighlight provinceHighlight = InstantiateProvinceHighlight(
                ref pR, 
                highlightMaterial);

            //Присоединяем подсветку к указанному родителю
            pHoverHighlight.highlight = provinceHighlight;
        }

        static GO_ProvinceHighlight InstantiateProvinceHighlight(
            ref C_ProvinceRender pR,
            Material highlightMaterial)
        {
            //Создаём пустую переменную для подсветки
            GO_ProvinceHighlight provinceHighlight;

            //Если список кэшированных подсветок не пуст, то берём кэшированную
            if (cachedProvinceHighlights.Count > 0)
            {
                //Берём последнюю подсветку в списке и удаляем её из списка
                provinceHighlight = cachedProvinceHighlights[cachedProvinceHighlights.Count - 1];
                cachedProvinceHighlights.RemoveAt(cachedProvinceHighlights.Count - 1);
            }
            //Иначе
            else
            {
                //Создаём новую подсветку
                provinceHighlight = Instantiate(provinceHighlightPrefab);
            }

            //Отображаем подсветку и присоединяем её к GO провинции
            provinceHighlight.gameObject.SetActive(true);
            provinceHighlight.transform.SetParent(pR.ProvinceGO.transform);
            provinceHighlight.transform.localScale = Vector3.one;


            //Устанавливаем материал подсветки
            provinceHighlight.meshRenderer.material = highlightMaterial;

            //Возвращаем рендерер
            return provinceHighlight;
        }

        public static void UpdateProvinceHighlight(
            ref C_ProvinceRender pR,
            GO_ProvinceHighlight provinceHighlight,
            Material highlightMaterial)
        {
            //Обновляем материал подсветки
            provinceHighlight.meshRenderer.material = highlightMaterial;
        }
    }
}
