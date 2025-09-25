
using UnityEngine;

namespace GBB.Input
{
    [CreateAssetMenu]
    public class InputModule : GameModule
    {
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
            //Создаём объект для данных ввода и назначаем ему их компонент
            InputData inputData = startup.AddDataObject().AddComponent<InputData>();

            //Вводим данные
            startup.InjectData(inputData);
        }
    }
}
