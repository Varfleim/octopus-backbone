
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map
{
    public class MapData : MonoBehaviour
    {
        public static void ProvinceCoreCreationRequest(
            EcsPool<SR_ProvinceCoreCreation> requestPool,
            int provinceEntity,
            EcsPackedEntity parentMapPE,
            List<EcsPackedEntity> neighbours)
        {
            //Назначаем сущности запрос
            ref SR_ProvinceCoreCreation requestComp = ref requestPool.Add(provinceEntity);

            //Заполняем данные запроса
            requestComp = new(
                parentMapPE,
                neighbours.ToArray());
        }

        /// <summary>
        /// Требуемый фильтр - Inc<SR_ProvinceCoreCreation>
        /// </summary>
        /// <param name="map"></param>
        /// <param name="pCCreationSRFilter"></param>
        public static void ProvincesCoreCreation(
            EcsWorld world,
            ref C_Map map,
            EcsFilter pCCreationSRFilter, EcsPool<SR_ProvinceCoreCreation> pCCreationSRPool,
            EcsPool<C_ProvinceCore> pCPool)
        {
            //Берём временный список из пула
            List<EcsPackedEntity> tempProvincePEs = ListPool<EcsPackedEntity>.Get();

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
                tempProvincePEs.Add(world.PackEntity(provinceEntity));

                //Удаляем запрос
                pCCreationSRPool.Del(provinceEntity);
            }

            //Сохраняем список как массив провинций карты
            map.provincePEs = tempProvincePEs.ToArray();

            //Возвращаем список в пул
            ListPool<EcsPackedEntity>.Add(tempProvincePEs);
        }

        private static void ProvinceCoreCreation(
            ref SR_ProvinceCoreCreation requestComp,
            int provinceEntity,
            EcsPool<C_ProvinceCore> pCPool)
        {
            //Назначаем сущности компонент PC
            ref C_ProvinceCore pC = ref pCPool.Add(provinceEntity);

            //Заполняем основные данные PC
            pC = new(
                requestComp.neighbourProvincePEs);
        }
    }
}
