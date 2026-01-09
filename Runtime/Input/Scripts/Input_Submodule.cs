
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
            #region PreFrame
            //Обновляем переменные, требуемые для систем ввода - не зависящие от других модулей
            startup.PreFrameSystem_Add(new S_Input_PreUpdate());
            //Обрабатываем нажатия клавиш на клавиатуре
            startup.PreFrameSystem_Add(new S_Keyboard_Input());
            #endregion
            #region Frame
            //Обновляем переменные, требуемые для систем ввода - зависящие от других модулей
            startup.FrameSystem_Add(new S_Input());
            //Запрашиваем действия по нажатиям клавиш
            startup.FrameSystem_Add(new S_ActionRequest());
            #endregion

            //Добавляем системы рендеринга

            //Добавляем потиковые системы

        }

        public override void Data_Inject(GameStartup startup)
        {
            //Вводим данные
            startup.Data_Inject(inputData);
        }
    }
}
