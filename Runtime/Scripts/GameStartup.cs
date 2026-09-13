
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
        StartSystemWeight = 1,
        PreSystemWeight = 2,
        SystemWeight = 3,
        PostSystemWeight = 4,
        EndSystemWeight = 5
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
        internal List<VFSystem> systems;
    }

    public class GameStartup : MonoBehaviour
    {
        ProtoWorld world;

        [SerializeField]
        VFSystem_List[] initSystemsLists;
        IProtoSystems initSystems;

        [SerializeField]
        VFSystem_List[] frameSystemsLists;
        IProtoSystems frameSystems;

        [SerializeField]
        VFSystem_List[] renderSystemsLists;
        IProtoSystems renderSystems;

        [SerializeField]
        VFSystem_List[] tickSystemsLists;
        IProtoSystems tickSystems;

        [SerializeField] 
        VFSystem_List[] tickRenderSystemsLists;
        IProtoSystems tickRenderSystems;

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

            tickRenderSystems = new ProtoSystems(world);
            tickRenderSystems.AddModule(new AutoInjectModule());

            //Инициализируем данные
            RuntimeData runtimeData = coreObject.AddComponent(typeof(RuntimeData)) as RuntimeData;

            //Инициализируем семена
            UnityEngine.Random.InitState(0);

            //Создаём счётчик подмодулей
            int submodulesCount = 0;

            //Инициализируем списки систем
            SystemLists_Init(ref initSystemsLists);
            SystemLists_Init(ref frameSystemsLists);
            SystemLists_Init(ref renderSystemsLists);
            SystemLists_Init(ref tickSystemsLists);
            SystemLists_Init(ref tickRenderSystemsLists);

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
            tickRenderSystems.Init();

            TimeTickSystem.Create();

            TimeTickSystem.OnTick += delegate (object sender, TimeTickSystem.OnTickEventArgs e)
            {
                if (runtimeData.isGameActive == true)
                {
                    Debug.Log("Tick Start " + DateTime.Now.ToString("hh:mm:ss:fff"));
                    tickSystems?.Run();
                    Debug.Log("Tick End, Tick Render Start " + DateTime.Now.ToString("hh:mm:ss:fff"));
                    tickRenderSystems?.Run();
                    Debug.Log("Tick Render End " + DateTime.Now.ToString("hh:mm:ss:fff"));
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

            if (tickRenderSystems != null)
            {
                tickRenderSystems.Destroy();
                tickRenderSystems = null;
            }

            if (world != null)
            {
                world.Destroy();
                world = null;
            }
        }

        void ListPools_Init()
        {

        }

        void SystemLists_Init(
            ref VFSystem_List[] systemLists)
        {
            systemLists = new VFSystem_List[(int)SystemWeight.EndSystemWeight];
            for(int a = 0; a < systemLists.Length; a++)
            {
                systemLists[a] = new();
                systemLists[a].systems = new();
            }
        }

        void Systems_Add(
            IProtoSystems systemsGroup,
            SystemWeight weight, params IProtoSystem[] systems)
        {
            for (int a = 0; a < systems.Length; a++)
            {
                systemsGroup.AddSystem(
                    systems[a], (int)weight);
            }
        }

        void Systems_AddToList(
            VFSystem_List[] systemLists,
            params VFSystem[] systems)
        {
            for (int a = 0; a < systems.Length; a++)
            {
                systemLists[(int)systems[0].systemWeight - 1].systems.Add(systems[a]);
            }
        }

        public void InitSystems_Add(
            params VFSystem[] systems)
        {
            Systems_Add(
                initSystems,
                systems[0].systemWeight, systems);
            Systems_AddToList(initSystemsLists, systems);
        }

        public void FrameSystems_Add(
            params VFSystem[] systems)
        {
            Systems_Add(
                frameSystems,
                systems[0].systemWeight, systems);
            Systems_AddToList(frameSystemsLists, systems);
        }

        public void RenderSystems_Add(
            params VFSystem[] systems)
        {
            Systems_Add(
                renderSystems,
                systems[0].systemWeight, systems);
            Systems_AddToList(renderSystemsLists, systems);
        }
        public void RenderGroupSystem_Add(
            IConditionalSystemSolver groupSolver,
            params VFSystem[] groupSystems)
        {
            Systems_Add(
                renderSystems,
                groupSystems[0].systemWeight,
                new ConditionalSystem(
                    groupSolver,
                    true,
                    groupSystems));
            for (int a = 0; a < groupSystems.Length; a++)
            {
                Systems_AddToList(renderSystemsLists, groupSystems[a]);
            }
        }

        public void TickSystems_Add(
            params VFSystem[] systems)
        {
            Systems_Add(
                tickSystems,
                systems[0].systemWeight, systems);
            Systems_AddToList(tickSystemsLists, systems);
        }

        public void TickRenderSystems_Add(
            params VFSystem[] systems)
        {
            Systems_Add(
                tickRenderSystems,
                systems[0].systemWeight, systems);
            Systems_AddToList(tickRenderSystemsLists, systems);
        }
        public void TickRenderGroupSystem_Add(
            IConditionalSystemSolver groupSolver,
            params VFSystem[] groupSystems)
        {
            Systems_Add(
                tickRenderSystems,
                groupSystems[0].systemWeight,
                new ConditionalSystem(
                    groupSolver,
                    true,
                    groupSystems));
            for (int a = 0; a < groupSystems.Length; a++)
            {
                Systems_AddToList(tickRenderSystemsLists, groupSystems[a]);
            }
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

            tickRenderSystems.AddService(inject);
        }
    }
}
