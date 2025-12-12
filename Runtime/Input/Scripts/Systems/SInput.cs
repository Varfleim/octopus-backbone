
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Input
{
    public class SInput : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;



        readonly EcsPoolInject<R_MouseMapPositionCheck> mouseMapPositionCheckRPool = default;

        readonly EcsPoolInject<R_MouseMapClickCheck> mouseMapClickCheckRPool = default;


        readonly EcsCustomInject<InputData> inputData = default;

        public void Run(IEcsSystems systems)
        {
            //Обновляем положение курсора мыши по запросу из подмодуля взаимодействия с картой
            MousePositionChangeRequests();
        }

        readonly EcsFilterInject<Inc<R_MousePositionChange>> mousePositionChangeRFilter = default;
        readonly EcsPoolInject<R_MousePositionChange> mousePositionChangeRPool = default;
        void MousePositionChangeRequests()
        {
            //Для каждого запроса изменения положения курсора
            foreach (int requestEntity in mousePositionChangeRFilter.Value)
            {
                //Берём запрос
                ref R_MousePositionChange requestComp = ref mousePositionChangeRPool.Value.Get(requestEntity);

                //Обновляем положение курсора мыши
                MousePositionChangeRequest(ref requestComp);

                //Если курсор находится над картой
                if (inputData.Value.isMouseOverMap == true)
                {
                    //Запрашиваем проверку положения курсора на карте
                    InputData.MouseMapPositionCheckRequest(
                        world.Value,
                        mouseMapPositionCheckRPool.Value,
                        inputData.Value.lastHitProvincePE);

                    //Если клик левой или правой кнопкой мыши
                    if (inputData.Value.leftMouseButtonClick == true
                        || inputData.Value.rightMouseButtonClick == true)
                    {
                        //Запрашиваем проверку клика на карте
                        InputData.MouseMapClickCheckRequest(
                            world.Value,
                            mouseMapClickCheckRPool.Value,
                            inputData.Value.lastHitProvincePE,
                            inputData.Value.leftMouseButtonClick, inputData.Value.rightMouseButtonClick);
                    }
                }

                //Удаляем запрос
                mousePositionChangeRPool.Value.Del(requestEntity);
            }
        }

        void MousePositionChangeRequest(
            ref R_MousePositionChange requestComp)
        {
            //Переносим данные из запроса
            inputData.Value.isMouseOverMap = requestComp.isMouseOverMap;
            inputData.Value.lastHitProvincePE = requestComp.lastHitProvincePE;
        }
    }
}
