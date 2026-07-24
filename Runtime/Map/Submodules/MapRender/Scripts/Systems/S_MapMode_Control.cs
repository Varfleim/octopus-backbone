
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Map.Render
{
    public class S_MapMode_Control : IProtoRunSystem
    {
        [DI] A_MapRender mapRender_A;
        [DI] A_CoreMapMode coreMapMode_A;

        public void Run()
        {
            //Активируем режим карты по запросу
            MapModes_Activation();
        }

        void MapModes_Activation()
        {
            //Для каждого запроса активации режима карты
            foreach(ProtoEntity rEntity in coreMapMode_A.mM_Activation_R_I)
            {
                //Берём запрос
                ref R_MapMode_Activation rComp = ref coreMapMode_A.mM_Activation_R_P.Get(rEntity);

                //Если текущий активный режим карты был деактивирован
                if(MapMode_DeactivationCheck(ref rComp))
                {
                    //Активируем режим карты
                    MapMode_Activation(ref rComp);
                }

                //Удаляем запрос
                coreMapMode_A.mM_Activation_R_P.Del(rEntity);
            }
        }

        /// <summary>
        /// Возвращает False, если запрошенный режим карты уже активен, то есть его не требуется деактивировать
        /// </summary>
        /// <param name="rComp"></param>
        /// <returns></returns>
        bool MapMode_DeactivationCheck(
            ref R_MapMode_Activation rComp)
        {
            //Для каждого активного режима карты
            foreach(ProtoEntity activeMMEntity in coreMapMode_A.activeMM_I)
            {
                //Если это не запрошенный режим карты
                if(activeMMEntity.Equals(rComp.mMEntity) == false)
                {
                    //Деактивируем его, удаляя временный компонент
                    coreMapMode_A.activeMM_P.Del(activeMMEntity);

                    //Возвращаем, что режим деактивирован
                    return true;
                }
                //Иначе возвращаем, что режим карты уже активен
                else
                {
                    return false;
                }
            }

            return true;
        }

        void MapMode_Activation(
            ref R_MapMode_Activation rComp)
        {
            //Берём запрошенный режим карты и назначаем ему компонент активного
            ref C_MapModeCore mapMode = ref coreMapMode_A.mMC_P.Get(rComp.mMEntity);
            ref CT_ActiveMapMode activeMM = ref coreMapMode_A.activeMM_P.Add(rComp.mMEntity);

            //Отменяем все запросы обновления режимов карты
            MapMode_UpdatesCancel();

            //Запрашиваем обновление режима карты
            coreMapMode_A.MapMode_Update_SR(rComp.mMEntity);

            //Запрашиваем обновление провинций карты
            mapRender_A.Map_UpdateProvincesRender_R(
                true, false, false);

            UnityEngine.Debug.LogWarning(mapMode.selfName);
        }

        /// <summary>
        /// Удаление всех существующих запросов обновления режима карты
        /// </summary>
        void MapMode_UpdatesCancel()
        {
            //Для каждого запроса обновления режима карты
            foreach(ProtoEntity mMEntity in coreMapMode_A.mM_Update_SR_I)
            {
                //Удаляем запрос
                coreMapMode_A.mM_Update_SR_P.Del(mMEntity);
            }
        }
    }
}
