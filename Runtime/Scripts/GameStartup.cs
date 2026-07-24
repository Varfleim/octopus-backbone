
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Ugui;
using Leopotam.EcsProto.ConditionalSystems;

namespace GBB
{
    public class GameStartup : MonoBehaviour
    {
        ProtoWorld world;

        IProtoSystems preInitSystems;
        IProtoSystems initSystems;
        IProtoSystems postInitSystems;

        IProtoSystems preFrameSystems;
        IProtoSystems frameSystems;
        IProtoSystems postFrameSystems;

        IProtoSystems preRenderSystems;
        IProtoSystems renderSystems;
        IProtoSystems postRenderSystems;

        IProtoSystems preTickSystems;
        IProtoSystems tickSystems;
        IProtoSystems postTickSystems;

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

        public List<GameModule> modules = new List<GameModule>();

        void Start()
        {
            //Инициализируем пулы списков
            ListPools_Init();

            //Инициализируем модули
            for(int a = 0; a < modules.Count; a++)
            {
                modules[a].Initialization();
            }

            //Инициализируем корневой аспект, мир и группы систем
            coreAspect = new();
            //Добавляем аспект Ugui
            coreAspect.childrenAspects.Add(new UnityUguiAspect());
            //Для каждого модуля добавляем аспекты
            for (int a = 0; a < modules.Count; a++)
            {
                //Добавляем аспекты
                modules[a].Submodules_Aspects_Add(this);
                coreAspect.childrenAspects.Add(modules[a].mainAspect);
            }
            world = new(coreAspect);

            preInitSystems = new ProtoSystems(world);
            preInitSystems.AddModule(new AutoInjectModule());
            initSystems = new ProtoSystems(world);
            initSystems.AddModule(new AutoInjectModule());
            postInitSystems = new ProtoSystems(world);
            postInitSystems.AddModule(new AutoInjectModule());

            preFrameSystems = new ProtoSystems(world);
            preFrameSystems.AddModule(new AutoInjectModule());
            frameSystems = new ProtoSystems(world);
            frameSystems.AddModule(new AutoInjectModule());
            frameSystems.AddModule(new UnityModule());
            frameSystems.AddModule(new UnityUguiModule(default, 100));
            postFrameSystems = new ProtoSystems(world);
            postFrameSystems.AddModule(new AutoInjectModule());

            preRenderSystems = new ProtoSystems(world);
            preRenderSystems.AddModule(new AutoInjectModule());
            renderSystems = new ProtoSystems(world);
            renderSystems.AddModule(new AutoInjectModule());
            postRenderSystems = new ProtoSystems(world);
            postRenderSystems.AddModule(new AutoInjectModule());

            preTickSystems = new ProtoSystems(world);
            preTickSystems.AddModule(new AutoInjectModule());
            tickSystems = new ProtoSystems(world);
            tickSystems.AddModule(new AutoInjectModule());
            postTickSystems = new ProtoSystems(world);
            postTickSystems.AddModule(new AutoInjectModule());

            //Инициализируем данные
            RuntimeData runtimeData = coreObject.AddComponent(typeof(RuntimeData)) as RuntimeData;

            //Инициализируем семена
            Random.InitState(0);

            //Для каждого модуля добавляем системы
            for (int a = 0; a < modules.Count; a++)
            {
                //Добавляем системы
                modules[a].Submodules_AddSystems(this);
            }

            

            //Для каждого модуля вводим данные
            for (int a = 0; a < modules.Count; a++)
            {
                //Вводим данные
                modules[a].Submodules_InjectData(this);
            }

            //Вводим данные
            Data_Inject(runtimeData);

            //Выполняем инициализацию систем
            preInitSystems.Init();
            initSystems.Init();
            postInitSystems.Init();

            preFrameSystems.Init();
            frameSystems.Init();
            postFrameSystems.Init();

            preRenderSystems.Init();
            renderSystems.Init();
            postRenderSystems.Init();

            preTickSystems.Init();
            tickSystems.Init();
            postTickSystems.Init();

            TimeTickSystem.Create();

            TimeTickSystem.OnTick += delegate (object sender, TimeTickSystem.OnTickEventArgs e)
            {
                if (runtimeData.isGameActive == true)
                {
                    Debug.Log("Tick Start " + System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss:fff"));
                    preTickSystems?.Run();
                    tickSystems?.Run();
                    postTickSystems?.Run();
                    Debug.Log("Tick End " + System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss:fff"));
                }
            };
        }

        void Update()
        {
            preFrameSystems?.Run();
            frameSystems?.Run();
            postFrameSystems?.Run();

            preRenderSystems?.Run();
            renderSystems?.Run();
            postRenderSystems?.Run();
        }

        void OnDestroy()
        {
            //Удаление систем инициализации
            if (preInitSystems != null)
            {
                preInitSystems.Destroy();
                preInitSystems = null;
            }
            if (initSystems != null)
            {
                initSystems.Destroy();
                initSystems = null;
            }
            if (postInitSystems != null)
            {
                postInitSystems.Destroy();
                postInitSystems = null;
            }

            //Удаление покадровых систем
            if (preFrameSystems != null)
            {
                preFrameSystems.Destroy();
                preFrameSystems = null;
            }
            if (frameSystems != null)
            {
                frameSystems.Destroy();
                frameSystems = null;
            }
            if (postFrameSystems != null)
            {
                postFrameSystems.Destroy();
                postFrameSystems = null;
            }

            //Удаление систем рендеринга
            if (preRenderSystems != null)
            {
                preRenderSystems.Destroy();
                preRenderSystems = null;
            }
            if (renderSystems != null)
            {
                renderSystems.Destroy();
                renderSystems = null;
            }
            if (postRenderSystems != null)
            {
                postRenderSystems.Destroy();
                postRenderSystems = null;
            }

            //Удаление потиковых систем
            if (preTickSystems != null)
            {
                preTickSystems.Destroy();
                preTickSystems = null;
            }
            if (tickSystems != null)
            {
                tickSystems.Destroy();
                tickSystems = null;
            }
            if (postTickSystems != null)
            {
                postTickSystems.Destroy();
                postTickSystems = null;
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

        public void PreInitSystem_Add(IProtoSystem system)
        {
            preInitSystems.AddSystem(system);
        }
        public void InitSystem_Add(IProtoSystem system)
        {
            initSystems.AddSystem(system);
        }
        public void PostInitSystem_Add(IProtoSystem system)
        {
            postInitSystems.AddSystem(system);
        }

        public void PreFrameSystem_Add(IProtoSystem system)
        {
            preFrameSystems.AddSystem(system);
        }
        public void FrameSystem_Add(IProtoSystem system)
        {
            frameSystems.AddSystem(system);
        }
        public void PostFrameSystem_Add(IProtoSystem system)
        {
            postFrameSystems.AddSystem(system);
        }

        public void PreRenderSystem_Add(IProtoSystem system)
        {
            preRenderSystems.AddSystem(system);
        }
        public void PreRenderSystem_AddGroup(
            IConditionalSystemSolver groupSolver,
            params IProtoSystem[] groupSystems)
        {
            preRenderSystems.AddSystem(
                new ConditionalSystem(
                    groupSolver,
                    true,
                    groupSystems));
        }
        public void RenderSystem_Add(IProtoSystem system)
        {
            renderSystems.AddSystem(system);
        }
        public void PostRenderSystem_Add(IProtoSystem system)
        {
            postRenderSystems.AddSystem(system);
        }

        public void PreTickSystem_Add(IProtoSystem system)
        {
            preTickSystems.AddSystem(system);
        }
        public void TickSystem_Add(IProtoSystem system)
        {
            tickSystems.AddSystem(system);
        }
        public void PostTickSystem_Add(IProtoSystem system)
        {
            postTickSystems.AddSystem(system);
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
            preInitSystems.AddService(inject);
            initSystems.AddService(inject);
            postInitSystems.AddService(inject);

            preFrameSystems.AddService(inject);
            frameSystems.AddService(inject);
            postFrameSystems.AddService(inject);

            preRenderSystems.AddService(inject);
            renderSystems.AddService(inject);
            postRenderSystems.AddService(inject);

            preTickSystems.AddService(inject);
            tickSystems.AddService(inject);
            postTickSystems.AddService(inject);
        }
    }
}
