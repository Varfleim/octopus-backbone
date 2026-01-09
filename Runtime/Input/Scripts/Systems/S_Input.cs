
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Input
{
    public class S_Input : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<R_Mouse_MapPositionCheck> mouse_MapPositionCheck_R_P = default;

        readonly EcsPoolInject<R_Mouse_MapClickCheck> mouse_MapClickCheck_R_P = default;


        readonly EcsCustomInject<Input_Data> input_Data = default;

        public void Run(IEcsSystems systems)
        {
            //Обновляем положение курсора мыши по запросу из подмодуля взаимодействия с картой
            Mouse_MapPositionChange_Requests();
        }

        readonly EcsFilterInject<Inc<R_Mouse_PositionChange>> mouse_PositionChange_R_F = default;
        readonly EcsPoolInject<R_Mouse_PositionChange> mouse_PositionChange_R_P = default;
        void Mouse_MapPositionChange_Requests()
        {
            //Для каждого запроса изменения положения курсора
            foreach (int requestEntity in mouse_PositionChange_R_F.Value)
            {
                //Берём запрос
                ref R_Mouse_PositionChange requestComp = ref mouse_PositionChange_R_P.Value.Get(requestEntity);

                //Обновляем положение курсора мыши
                Mouse_MapPositionChange_Request(ref requestComp);

                //Если курсор находится над картой
                if (input_Data.Value.isMouseOverMap == true)
                {
                    //Запрашиваем проверку положения курсора на карте
                    Input_Data.Mouse_MapPositionCheck_Request(
                        world.Value,
                        mouse_MapPositionCheck_R_P.Value,
                        input_Data.Value.lastHitProvincePE);

                    //Если клик левой или правой кнопкой мыши
                    if (input_Data.Value.leftMouseButtonClick == true
                        || input_Data.Value.rightMouseButtonClick == true)
                    {
                        //Запрашиваем проверку клика на карте
                        Input_Data.Mouse_MapClickCheck_Request(
                            world.Value,
                            mouse_MapClickCheck_R_P.Value,
                            input_Data.Value.lastHitProvincePE,
                            input_Data.Value.leftMouseButtonClick, input_Data.Value.rightMouseButtonClick);
                    }
                }

                //Удаляем запрос
                mouse_PositionChange_R_P.Value.Del(requestEntity);
            }
        }

        void Mouse_MapPositionChange_Request(
            ref R_Mouse_PositionChange requestComp)
        {
            //Переносим данные из запроса
            input_Data.Value.isMouseOverMap = requestComp.isMouseOverMap;
            input_Data.Value.lastHitProvincePE = requestComp.lastHitProvincePE;
        }
    }
}
