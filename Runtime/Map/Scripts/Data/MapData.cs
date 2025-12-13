
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map
{
    public class MapData : MonoBehaviour
    {
        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля представления карты
        /// </summary>
        /// <param name="requestPool"></param>
        /// <param name="provinceEntity"></param>
        /// <param name="parentMapPE"></param>
        /// <param name="neighbours"></param>
        public static void ProvinceCoreCreationRequest(
            EcsPool<SR_ProvinceCoreCreation> requestPool,
            int provinceEntity,
            int parentMapEntity,
            List<int> neighbours)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceCoreCreation requestComp = ref requestPool.Add(provinceEntity);

            //Заполняем данные запроса
            requestComp = new(
                parentMapEntity,
                neighbours.ToArray());
        }

        /// <summary>
        /// Публичная функция, поскольку запрашивается из модуля представления карты
        /// Требуемый фильтр - Inc<SR_ProvinceCoreCreation>
        /// </summary>
        /// <param name="map"></param>
        /// <param name="pCCreationSRFilter"></param>
        public static void ProvincesCoreCreation(
            ref C_Map map,
            EcsFilter pCCreationSRFilter, EcsPool<SR_ProvinceCoreCreation> pCCreationSRPool,
            EcsPool<C_ProvinceCore> pCPool)
        {
            //Берём временный список из пула
            List<int> tempProvinceEntities = ListPool<int>.Get();

            //Для каждой провинции с запросом создания PC
            foreach (int provinceEntity in pCCreationSRFilter)
            {
                //Берём запрос
                ref SR_ProvinceCoreCreation requestComp = ref pCCreationSRPool.Get(provinceEntity);

                //Создаём PC по запросу
                ProvinceCoreCreation(
                    ref requestComp,
                    provinceEntity,
                    pCPool);

                //Заносим провинцию во временный список
                tempProvinceEntities.Add(provinceEntity);

                //Удаляем запрос
                pCCreationSRPool.Del(provinceEntity);
            }

            //Сохраняем список как массив провинций карты
            map.provinceEntities = tempProvinceEntities.ToArray();

            //Возвращаем список в пул
            ListPool<int>.Add(tempProvinceEntities);
        }

        /// <summary>
        /// Закрытая функция, поскольку запрашивается только косвенно
        /// </summary>
        /// <param name="requestComp"></param>
        /// <param name="provinceEntity"></param>
        /// <param name="pCPool"></param>
        private static void ProvinceCoreCreation(
            ref SR_ProvinceCoreCreation requestComp,
            int provinceEntity,
            EcsPool<C_ProvinceCore> pCPool)
        {
            //Назначаем сущности компонент PC
            ref C_ProvinceCore pC = ref pCPool.Add(provinceEntity);

            //Заполняем основные данные PC
            pC = new(
                requestComp.neighbourProvinceEntities);
        }
    }
}
