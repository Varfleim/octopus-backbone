
using UnityEngine;

namespace GBB.Input
{
    internal class Input_Submodule : GameSubmodule
    {
        [SerializeField]
        private Input_Data inputData;

        public override void Systems_Add(GameStartup startup)
        {
            //Добавляем системы инициализации

            //Добавляем покадровые системы
            #region Frame
            //Обновляем переменные, требуемые для систем ввода - не зависящие от других модулей
            startup.FrameSystem_Add(
                System_New<S_Input_PreUpdate>(SystemWeight.PreSystemWeight));
            //Обрабатываем нажатия клавиш на клавиатуре
            startup.FrameSystem_Add(
                System_New<S_Keyboard_Input>(SystemWeight.PreSystemWeight));

            //Обновляем переменные, требуемые для систем ввода - зависящие от других модулей
            startup.FrameSystem_Add(
                System_New<S_Input>(SystemWeight.SystemWeight));
            //Запрашиваем действия по нажатиям клавиш
            startup.FrameSystem_Add(
                System_New<S_ActionRequest>(SystemWeight.SystemWeight));
            #endregion

            //Добавляем системы рендеринга

            //Добавляем потиковые системы

        }

        public override void Aspects_Add(
            GameStartup startup,
            A_Aspect parentAspect)
        {
            //Создаём аспекты и присоединяем их к родительскому
            A_Input input_A = new();
            parentAspect.childrenAspects.Add(input_A);
        }

        public override void Data_Inject(GameStartup startup)
        {
            //Вводим данные
            startup.Data_Inject(inputData);
        }
    }
}
