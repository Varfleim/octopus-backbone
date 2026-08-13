
using System;
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Ugui;
using Leopotam.EcsProto.ConditionalSystems;

namespace GBB
{
    public enum SystemWeight
    {
        StartSystemWeight = 100,
        PreSystemWeight = 200,
        SystemWeight = 300,
        PostSystemWeight = 400,
        EndSystemWeight = 500
    }

    [Serializable]
    public class VFSystem : IProtoSystem, IComparable<VFSystem>
    {
        [SerializeField]
        internal string fullName;
        [SerializeField]
        internal SystemWeight systemWeight;

        [SerializeField]
        internal GameSubmodule systemSubmodule;
        [SerializeField]
        internal int systemSubmoduleIndex;

        protected void Time_Start_Print()
        {
            Debug.LogWarning(
                fullName
                + "\nSystem Start " + DateTime.Now.ToString("hh:mm:ss:fff"));
        }
        
        protected void Time_End_Print()
        {
            Debug.LogWarning(
                fullName
                + "\nSystem End " + DateTime.Now.ToString("hh:mm:ss:fff"));
        }

        public int CompareTo(VFSystem other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                if (systemWeight > other.systemWeight)
                {
                    return 1;
                }
                else if (systemWeight < other.systemWeight)
                {
                    return -1;
                }
                else
                {
                    return systemSubmoduleIndex.CompareTo(other.systemSubmoduleIndex);
                }
            }
        }
    }

    [Serializable]
    internal class VFSystem_List
    {
        [SerializeField]
        internal List<VFSystem> systems = new();
    }

    public class GameStartup : MonoBehaviour
    {
        ProtoWorld world;

        [SerializeField]
        List<VFSystem_List> initSystemsLists;
        IProtoSystems initSystems;

        [SerializeField]
        List<VFSystem_List> frameSystemsLists;
        IProtoSystems frameSystems;

        [SerializeField]
        List<VFSystem_List> renderSystemsLists;
        IProtoSystems renderSystems;

        [SerializeField]
        List<VFSystem_List> tickSystemsLists;
        IProtoSystems tickSystems;

        A_Core coreAspect;

        #region Map
        public GameObject coreObject;
        public GameObject mapObject;
        public Collider mapCollider;
        #endregion

        #region Camera
        public Transform mapCamera;
        public Transform swiwel;
        public Transform stick;
        public new Camera camera;
        #endregion

        public GameModule[] modules;

        void Start()
        {
            //Инициализируем пулы списков
            ListPools_Init();

            //Инициализируем модули
            for(int a = 0; a < modules.Length; a++)
            {
                modules[a].Initialization();
            }

            //Инициализируем корневой аспект, мир и группы систем
            coreAspect = new();
            //Добавляем аспект Ugui
            coreAspect.childrenAspects.Add(new UnityUguiAspect());
            //Для каждого модуля добавляем аспекты
            for (int a = 0; a < modules.Length; a++)
            {
                //Добавляем аспекты
                modules[a].Submodules_Aspects_Add(this);
                coreAspect.childrenAspects.Add(modules[a].mainAspect);
            }
            world = new(coreAspect);

            initSystems = new ProtoSystems(world);
            initSystems.AddModule(new AutoInjectModule());

            frameSystems = new ProtoSystems(world);
            frameSystems.AddModule(new AutoInjectModule());
            frameSystems.AddModule(new UnityModule());
            frameSystems.AddModule(new UnityUguiModule(default, 100000));

            renderSystems = new ProtoSystems(world);
            renderSystems.AddModule(new AutoInjectModule());

            tickSystems = new ProtoSystems(world);
            tickSystems.AddModule(new AutoInjectModule());

            //Инициализируем данные
            RuntimeData runtimeData = coreObject.AddComponent(typeof(RuntimeData)) as RuntimeData;

            //Инициализируем семена
            UnityEngine.Random.InitState(0);

            //Создаём счётчик подмодулей
            int submodulesCount = 0;

            //Для каждого модуля добавляем системы
            for (int a = 0; a < modules.Length; a++)
            {
                //Добавляем системы
                submodulesCount = modules[a].Submodules_AddSystems(
                    this,
                    submodulesCount);
            }

            //Для каждого модуля вводим данные
            for (int a = 0; a < modules.Length; a++)
            {
                //Вводим данные
                modules[a].Submodules_InjectData(this);
            }

            //Вводим данные
            Data_Inject(runtimeData);

            //Инициализируем системы
            initSystems.Init();
            frameSystems.Init();
            renderSystems.Init();
            tickSystems.Init();

            TimeTickSystem.Create();

            TimeTickSystem.OnTick += delegate (object sender, TimeTickSystem.OnTickEventArgs e)
            {
                if (runtimeData.isGameActive == true)
                {
                    Debug.Log("Tick Start " + DateTime.Now.ToString("hh:mm:ss:fff"));
                    tickSystems?.Run();
                    Debug.Log("Tick End " + DateTime.Now.ToString("hh:mm:ss:fff"));
                }
            };
        }

        void Update()
        {
            Debug.Log("Frame Start " + DateTime.Now.ToString("hh:mm:ss:fff"));

            frameSystems?.Run();

            Debug.Log("Frame End, Render Start " + DateTime.Now.ToString("hh:mm:ss:fff"));

            renderSystems?.Run();

            Debug.Log("Render End " + DateTime.Now.ToString("hh:mm:ss:fff"));
        }

        void OnDestroy()
        {
            if (initSystems != null)
            {
                initSystems.Destroy();
                initSystems = null;
            }

            if (frameSystems != null)
            {
                frameSystems.Destroy();
                frameSystems = null;
            }

            if (renderSystems != null)
            {
                renderSystems.Destroy();
                renderSystems = null;
            }

            if (tickSystems != null)
            {
                tickSystems.Destroy();
                tickSystems = null;
            }

            if (world != null)
            {
                world.Destroy();
                world = null;
            }
        }

        public void ListPools_Init()
        {

        }

        void System_Add(
            IProtoSystems systems,
            IProtoSystem system, SystemWeight weight)
        {
            systems.AddSystem(
                system, (int)weight);
        }

        void System_AddToList(
            List<VFSystem_List> systemLists,
            VFSystem system)
        {
            systemLists[(int)system.systemWeight / 100 - 1].systems.Add(system);
        }

        public void InitSystem_Add(
            VFSystem system)
        {
            System_Add(
                initSystems,
                system, system.systemWeight);
            System_AddToList(initSystemsLists, system);
        }

        public void FrameSystem_Add(
            VFSystem system)
        {
            System_Add(
                frameSystems,
                system, system.systemWeight);
            System_AddToList(frameSystemsLists, system);
        }

        public void RenderSystem_Add(
            VFSystem system)
        {
            System_Add(
                renderSystems,
                system, system.systemWeight);
            System_AddToList(renderSystemsLists, system);
        }
        public void RenderSystem_AddGroup(
            IConditionalSystemSolver groupSolver,
            params VFSystem[] groupSystems)
        {
            System_Add(
                renderSystems,
                new ConditionalSystem(
                    groupSolver,
                    true,
                    groupSystems),
                groupSystems[0].systemWeight);
            for (int a = 0; a < groupSystems.Length; a++)
            {
                System_AddToList(renderSystemsLists, groupSystems[a]);
            }
        }

        public void TickSystem_Add(
            VFSystem system)
        {
            System_Add(
                tickSystems,
                system, system.systemWeight);
            System_AddToList(tickSystemsLists, system);
        }

        public GameObject DataObject_Add()
        {
            //Создаём новый объект для компонента данных
            GameObject newDataObject = new GameObject();

            //Прикрепляем его к корневому объекту
            newDataObject.transform.SetParent(coreObject.transform);

            //Возвращаем объект
            return newDataObject;
        }

        public void Data_Inject(object inject)
        {
            initSystems.AddService(inject);
            
            frameSystems.AddService(inject);
            
            renderSystems.AddService(inject);
            
            tickSystems.AddService(inject);
        }
    }
}
