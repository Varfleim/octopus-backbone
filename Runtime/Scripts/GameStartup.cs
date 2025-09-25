
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Unity.Ugui;

namespace GBB
{
    public class GameStartup : MonoBehaviour
    {
        EcsWorld world;
        EcsWorld uguiMapWorld;
        EcsWorld uguiUIWorld;

        EcsSystems preInitSystems;
        EcsSystems initSystems;
        EcsSystems postInitSystems;

        EcsSystems preFrameSystems;
        EcsSystems frameSystems;
        EcsSystems postFrameSystems;

        EcsSystems preRenderSystems;
        EcsSystems renderSystems;
        EcsSystems postRenderSystems;

        EcsSystems preTickSystems;
        EcsSystems tickSystems;
        EcsSystems postTickSystems;

        [SerializeField] EcsUguiEmitter uguiMapEmitter;
        [SerializeField] EcsUguiEmitter uguiUIEmitter;

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
            //�������������� ���� �������
            InitListPools();

            //�������������� ��� � ������ ������
            world = new EcsWorld();
            uguiMapWorld = new EcsWorld();
            uguiUIWorld = new EcsWorld();

            preInitSystems = new EcsSystems(world);
            initSystems = new EcsSystems(world);
            postInitSystems = new EcsSystems(world);

            preFrameSystems = new EcsSystems(world);
            frameSystems = new EcsSystems(world);
            postFrameSystems = new EcsSystems(world);

            preRenderSystems = new EcsSystems(world);
            renderSystems = new EcsSystems(world);
            postRenderSystems = new EcsSystems(world);

            preTickSystems = new EcsSystems(world);
            tickSystems = new EcsSystems(world);
            postTickSystems = new EcsSystems(world);

            //�������������� ������
            RuntimeData runtimeData = coreObject.AddComponent(typeof(RuntimeData)) as RuntimeData;

            //�������������� ������
            Random.InitState(0);

            //��� ������� ������ ��������� �������
            for (int a = 0; a < modules.Count; a++)
            {
                //��������� �������
                modules[a].AddSystems(this);
            }

            //��� ������� ������ ������ ������
            for (int a = 0; a < modules.Count; a++)
            {
                //������ ������
                modules[a].InjectData(this);
            }

            //������ ������
            InjectData(runtimeData);

            //��������� ������������� ������
            preInitSystems.Init();
            initSystems.Init();
            postInitSystems.Init();

            //preFrameSystems
            //    .AddWorld(uguiMapWorld, "uguiMapEventsWorld")
            //    .InjectUgui(uguiMapEmitter, "uguiMapEventsWorld")
            //    .AddWorld(uguiUIWorld, "uguiUIEventsWorld")
            //    .InjectUgui(uguiUIEmitter, "uguiUIEventsWorld");
            preFrameSystems.Init();
            frameSystems
                .AddWorld(uguiMapWorld, "uguiMapEventsWorld")
                .InjectUgui(uguiMapEmitter, "uguiMapEventsWorld")
                .AddWorld(uguiUIWorld, "uguiUIEventsWorld")
                .InjectUgui(uguiUIEmitter, "uguiUIEventsWorld");
            frameSystems.Init();
            //postFrameSystems
            //    .AddWorld(uguiMapWorld, "uguiMapEventsWorld")
            //    .InjectUgui(uguiMapEmitter, "uguiMapEventsWorld")
            //    .AddWorld(uguiUIWorld, "uguiUIEventsWorld")
            //    .InjectUgui(uguiUIEmitter, "uguiUIEventsWorld");
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
            //�������� ������ �������������
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

            //�������� ���������� ������
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

            //�������� ������ ����������
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

            //�������� ��������� ������
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

        public void InitListPools()
        {

        }

        public void AddPreInitSystem(IEcsSystem system)
        {
            preInitSystems.Add(system);
        }
        public void AddInitSystem(IEcsSystem system)
        {
            initSystems.Add(system);
        }
        public void AddPostInitSystem(IEcsSystem system)
        {
            postInitSystems.Add(system);
        }

        public void AddPreFrameSystem(IEcsSystem system)
        {
            preFrameSystems.Add(system);
        }
        public void AddFrameSystem(IEcsSystem system)
        {
            frameSystems.Add(system);
        }
        public void AddPostFrameSystem(IEcsSystem system)
        {
            postFrameSystems.Add(system);
        }

        public void AddPreRenderSystem(IEcsSystem system)
        {
            preRenderSystems.Add(system);
        }
        public void AddPreRenderSystemGroup(
            string groupName,
            bool defaultState,
            params IEcsSystem[] systems)
        {
            preRenderSystems.AddGroup(
                groupName,
                defaultState,
                null,
                systems);
        }
        public void AddRenderSystem(IEcsSystem system)
        {
            renderSystems.Add(system);
        }
        public void AddPostRenderSystem(IEcsSystem system)
        {
            postRenderSystems.Add(system);
        }

        public void AddPreTickSystem(IEcsSystem system)
        {
            preTickSystems.Add(system);
        }
        public void AddTickSystem(IEcsSystem system)
        {
            tickSystems.Add(system);
        }
        public void AddPostTickSystem(IEcsSystem system)
        {
            postTickSystems.Add(system);
        }

        public GameObject AddDataObject()
        {
            //������ ����� ������ ��� ���������� ������
            GameObject newDataObject = new GameObject();

            //����������� ��� � ��������� �������
            newDataObject.transform.SetParent(coreObject.transform);

            //���������� ������
            return newDataObject;
        }

        public void InjectData(params object[] injects)
        {
            preInitSystems.Inject(injects);
            initSystems.Inject(injects);
            postInitSystems.Inject(injects);

            preFrameSystems.Inject(injects);
            frameSystems.Inject(injects);
            postFrameSystems.Inject(injects);

            preRenderSystems.Inject(injects);
            renderSystems.Inject(injects);
            postRenderSystems.Inject(injects);

            preTickSystems.Inject(injects);
            tickSystems.Inject(injects);
            postTickSystems.Inject(injects);
        }
    }
}
