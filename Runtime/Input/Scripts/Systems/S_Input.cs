
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;

namespace GBB.Input
{
    public class S_Input : VFSystem, IProtoRunSystem
    {
        [DI] A_Input input_A;

        [DI] Input_Data input_Data;

        public void Run()
        {
            //Обновляем положение курсора мыши по запросу из подмодуля взаимодействия с картой
            Mouse_MapPositionChange_Requests();
        }

        void Mouse_MapPositionChange_Requests()
        {
            //Для каждого запроса изменения положения курсора
            foreach (ProtoEntity rEntity in input_A.mouse_PositionChange_I)
            {
                //Берём запрос
                ref R_Mouse_PositionChange rComp = ref input_A.mouse_PositionChange_R_P.Get(rEntity);

                //Обновляем положение курсора мыши
                Mouse_MapPositionChange_Request(ref rComp);

                //Если курсор находится над картой
                if (input_Data.isMouseOverMap == true)
                {
                    //Запрашиваем проверку положения курсора на карте
                    input_A.Mouse_MapPositionCheck_Request(input_Data.lastHitProvincePE);

                    //Если клик левой или правой кнопкой мыши
                    if (input_Data.leftMouseButtonClick == true
                        || input_Data.rightMouseButtonClick == true)
                    {
                        //Запрашиваем проверку клика на карте
                        input_A.Mouse_MapClickCheck_Request(
                            input_Data.lastHitProvincePE,
                            input_Data.leftMouseButtonClick, input_Data.rightMouseButtonClick);
                    }
                }

                //Удаляем запрос
                input_A.mouse_PositionChange_R_P.Del(rEntity);
            }
        }

        void Mouse_MapPositionChange_Request(
            ref R_Mouse_PositionChange rComp)
        {
            //Переносим данные из запроса
            input_Data.isMouseOverMap = rComp.isMouseOverMap;
            input_Data.lastHitProvincePE = rComp.lastHitProvincePE;
        }
    }
}
