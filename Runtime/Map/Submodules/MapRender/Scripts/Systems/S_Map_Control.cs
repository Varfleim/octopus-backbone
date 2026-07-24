
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Map.Render
{
    public class S_Map_Control : IProtoRunSystem
    {
        [DI] A_Map map_A;
        [DI] A_MapRender mapRender_A;
        [DI] A_CoreMapMode coreMapMode_A;

        [DI] MainMapMode_Data mainMapMode_Data;

        public void Run()
        {
            //Активируем карту по запросу
            Maps_Activation();
        }

        void Maps_Activation()
        {
            //Для каждого запроса активации карты
            foreach(ProtoEntity mEntity in mapRender_A.map_Activation_SR_I)
            {
                //Берём запрос
                ref SR_Map_Activation rComp = ref mapRender_A.map_Activation_SR_P.Get(mEntity);

                //Если запрошенная карта не была активна
                if(Map_DeactivationCheck(mEntity))
                {
                    //Активируем запрошенную карту
                    Map_Activation(mEntity);
                }

                //Удаляем запрос
                mapRender_A.map_Activation_SR_P.Del(mEntity);
            }
        }

        /// <summary>
        /// Возвращает False, если запрошенная карта уже активна, то есть её не требуется деактивировать
        /// </summary>
        /// <param name="mEntity"></param>
        /// <returns></returns>
        bool Map_DeactivationCheck(
            ProtoEntity mEntity)
        {
            //Для каждой активной карты
            foreach(ProtoEntity activeMEntity in mapRender_A.activeMap_I)
            {
                //Если это не запрошенная карта
                if (activeMEntity.Equals(mEntity) == false)
                {
                    //Берём её
                    ref C_Map activeM = ref map_A.map_P.Get(activeMEntity);

                    //Деактивируем её, удаляя временный компонент
                    mapRender_A.activeMap_P.Del(activeMEntity);

                    //Для каждой провинции карты
                    for (int a = 0; a < activeM.provinceEntities.Length; a++)
                    {
                        //Удаляем компонент PR
                        mapRender_A.pR_P.Del(activeM.provinceEntities[a]);

                        //Удаляем компонент PR_Update
                        mapRender_A.pR_Update_P.Del(activeM.provinceEntities[a]);
                    }

                    //Возвращаем, что карта деактивирована
                    return true;
                }
                //Иначе возвращаем, что карта уже активна
                else
                {
                    return false;
                }
            }

            return true;
        }

        void Map_Activation(
            ProtoEntity mEntity)
        {
            //Берём запрошенную карту и назначаем ей компонент активной
            ref C_Map map = ref map_A.map_P.Get(mEntity);
            ref CT_ActiveMap activeM = ref mapRender_A.activeMap_P.Add(mEntity);

            //Для каждой провинции карты
            for (int a = 0; a < map.provinceEntities.Length; a++)
            {
                //Берём сущность провинции и назначаем ей компонент PR
                ref C_ProvinceRender pR = ref mapRender_A.pR_P.Add(map.provinceEntities[a]);

                //Заполняем данные PR
                pR = new(0);
            }

            //Запрашиваем инициализацию рендера карты
            Map_RenderInitialization_Request(mEntity);

            //ТЕСТ
            //Запрашиваем активацию стандартного режима карты
            coreMapMode_A.MapMode_Activation_R(mainMapMode_Data.DefaultMapModeEntity);
            //ТЕСТ
        }

        void Map_RenderInitialization_Request(
            ProtoEntity mEntity)
        {
            //Назначаем запрос инициализации рендера, если его ещё нет
            ref SR_Map_RenderInitialization rComp = ref mapRender_A.map_RenderInitialization_SR_P.Add(mEntity);
            rComp = new(0);
        }
    }
}
