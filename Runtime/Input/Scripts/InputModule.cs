
using UnityEngine;

namespace GBB.Input
{
    internal class InputModule : GameSubmodule
    {
        [SerializeField]
        private InputData inputData;

        public override void AddSystems(GameStartup startup)
        {
            //Добавляем системы инициализации

            //Добавляем покадровые системы
            #region PreFrame
            //Обновляем переменные, требуемые для систем ввода - не зависящие от других модулей
            startup.AddPreFrameSystem(new SInputPreUpdate());
            //Обрабатываем нажатия клавиш на клавиатуре
            startup.AddPreFrameSystem(new SKeyboardInput());
            #endregion
            #region Frame
            //Обновляем переменные, требуемые для систем ввода - зависящие от других модулей
            startup.AddFrameSystem(new SInput());
            //Запрашиваем действия по нажатиям клавиш
            startup.AddFrameSystem(new SActionRequest());
            #endregion

            //Добавляем системы рендеринга

            //Добавляем потиковые системы

        }

        public override void InjectData(GameStartup startup)
        {
            //Вводим данные
            startup.InjectData(inputData);
        }
    }
}
