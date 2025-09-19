
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapRender : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsFilterInject<Inc<CMapModeCore, CActiveMapMode, SRMapModeUpdate>> activeMapModeFilter = default;
        readonly EcsPoolInject<CMapModeCore> mapModePool = default;

        readonly EcsPoolInject<CProvinceRender> pRPool = default;
        readonly EcsFilterInject<Inc<CProvinceRender, SRUpdateThinEdges>> provinceUpdateThinEdgesSRFilter = default;
        readonly EcsFilterInject<Inc<CProvinceRender>, Exc<SRUpdateThinEdges>> provinceWithoutUpdateThinEdgesSRFilter = default;
        readonly EcsPoolInject<SRUpdateThinEdges> updateThinEdgesSRPool = default;
        readonly EcsFilterInject<Inc<CProvinceRender, SRUpdateThickEdges>> provinceUpdateThickEdgesSRFilter = default;
        readonly EcsFilterInject<Inc<CProvinceRender>, Exc<SRUpdateThickEdges>> provinceWithoutUpdateThickEdgesSRFilter = default;
        readonly EcsPoolInject<SRUpdateThickEdges> updateThickEdgesSRPool = default;

        readonly EcsPoolInject<RMapEdgesUpdate> mapEdgesUpdateRPool = default;
        readonly EcsPoolInject<RMapProvincesUpdate> mapProvincesUpdateRPool = default;
        
        public void Run(IEcsSystems systems)
        {
            //��������� ������ ������
            MapEdgesDataUpdate();

            //��� ������� ��������� ������ ����� � �������� ����������
            foreach(int activeMapModeEntity in activeMapModeFilter.Value)
            {
                //���� ����� �����
                ref CMapModeCore activeMapMode = ref mapModePool.Value.Get(activeMapModeEntity);

                //��������� ������ ������������ ���������
                MapProvinceRenderDataUpdate(
                    ref activeMapMode,
                    out bool isHeightUpdated,
                    out bool isColorUpdated);

                //����������� ���������� ������������ ��������� �����
                MapData.MapProvincesUpdateRequest(
                    world.Value,
                    mapProvincesUpdateRPool.Value,
                    false, isHeightUpdated, isColorUpdated);
            }
        }

        void MapEdgesDataUpdate()
        {
            //���������, ����� ����� ��������� ��������
            bool isThinUpdated = false;
            bool isThickUpdated = false;

            //���� ������ ���������� ������ ������ �� ����
            if (provinceUpdateThinEdgesSRFilter.Value.GetEntitiesCount() > 0)
            {
                //��������� ������ �����
                MapThinEdgesDataUpdate(out isThinUpdated);
            }

            //���� ������ ���������� ������� ������ �� ����
            if(provinceUpdateThickEdgesSRFilter.Value.GetEntitiesCount() > 0)
            {
                //��������� ������� �����
                MapThickEdgesDataUpdate(out isThickUpdated);
            }

            //���� �����-���� ����� ���� ���������
            if(isThinUpdated == true
                || isThickUpdated == true)
            {
                //����������� ���������� ������ �����
                MapData.MapEdgesUpdateRequest(
                    world.Value,
                    mapEdgesUpdateRPool.Value,
                    isThinUpdated, isThickUpdated);
            }
        }

        void MapThinEdgesDataUpdate(
            out bool isThinUpdated)
        {
            //������������� �������� �� ���������
            isThinUpdated = false;

            //��� ������ ��������� ��� ������� ���������� ������ ������
            foreach (int provinceEntity in provinceWithoutUpdateThinEdgesSRFilter.Value)
            {
                //���� ���������
                ref CProvinceRender pR = ref pRPool.Value.Get(provinceEntity);

                //���� ������ ������ ������ ��� �������
                if(MapModeData.UpdateProvinceThinEdgesIndex(
                    ref pR,
                    -1) == true)
                {
                    //��������, ��� ����� ���������
                    isThinUpdated = true;
                }
            }

            //���� ��� ��������� � ��������
            if (provinceUpdateThinEdgesSRFilter.Value.GetEntitiesCount() == 0)
            {
                //��������, ��� ��������� ���������� �����
                isThinUpdated = true;
            }

            //��� ������ ��������� � ��������
            foreach (int provinceEntity in provinceUpdateThinEdgesSRFilter.Value)
            {
                //���� ��������� � ������
                ref CProvinceRender pR = ref pRPool.Value.Get(provinceEntity);
                ref SRUpdateThinEdges requestComp = ref updateThinEdgesSRPool.Value.Get(provinceEntity);

                //���� ������ ������ ������ ��� �������
                if (MapModeData.UpdateProvinceThinEdgesIndex(
                    ref pR,
                    requestComp.edgeIndex) == true)
                {
                    //��������, ��� ����� ���������
                    isThinUpdated = true;
                }

                //������� ������
                updateThinEdgesSRPool.Value.Del(provinceEntity);
            }
        }

        void MapThickEdgesDataUpdate(
            out bool isThickUpdated)
        {
            //������������� �������� �� ���������
            isThickUpdated = false;

            //��� ������ ��������� ��� ������� ���������� ������� ������
            foreach (int provinceEntity in provinceWithoutUpdateThickEdgesSRFilter.Value)
            {
                //���� ���������
                ref CProvinceRender pR = ref pRPool.Value.Get(provinceEntity);

                //���� ������ ������� ������ ��� �������
                if (MapModeData.UpdateProvinceThickEdgesIndex(
                    ref pR,
                    -1) == true)
                {
                    //��������, ��� ����� ���������
                    isThickUpdated = true;
                }
            }

            //���� ��� ��������� � ��������
            if (provinceUpdateThickEdgesSRFilter.Value.GetEntitiesCount() == 0)
            {
                //��������, ��� ��������� ���������� �����
                isThickUpdated = true;
            }

            //��� ������ ��������� � ��������
            foreach (int provinceEntity in provinceUpdateThickEdgesSRFilter.Value)
            {
                //���� ��������� � ������
                ref CProvinceRender pR = ref pRPool.Value.Get(provinceEntity);
                ref SRUpdateThickEdges requestComp = ref updateThickEdgesSRPool.Value.Get(provinceEntity);

                //���� ������ ������� ������ ��� �������
                if (MapModeData.UpdateProvinceThickEdgesIndex(
                    ref pR,
                    requestComp.edgeIndex) == true)
                {
                    //��������, ��� ����� ���������
                    isThickUpdated = true;
                }

                //������� ������
                updateThickEdgesSRPool.Value.Del(provinceEntity);
            }
        }

        readonly EcsFilterInject<Inc<CProvinceRender, SRUpdateProvinceRender>> provinceUpdateProvinceRenderSRFilter = default;
        readonly EcsFilterInject<Inc<CProvinceRender>, Exc<SRUpdateProvinceRender>> provinceWithoutUpdateProvinceRenderSRFilter = default;
        readonly EcsPoolInject<SRUpdateProvinceRender> updateProvinceRenderSRPool = default;
        void MapProvinceRenderDataUpdate(
            ref CMapModeCore mapMode,
            out bool isHeightUpdated, out bool isColorUpdated)
        {
            //������������� �������� �� ���������
            isHeightUpdated = false;
            isColorUpdated = false;

            //��� ������ ��������� ��� ������� ���������� ������������
            foreach (int provinceEntity in provinceWithoutUpdateProvinceRenderSRFilter.Value)
            {
                //���� ���������
                ref CProvinceRender pR = ref pRPool.Value.Get(provinceEntity);

                //��������� ������������ ������ ���������
                MapModeData.UpdateProvinceDisplayedObject(
                    ref mapMode,
                    ref pR,
                    new());

                //�������� ��������� ������������ ���������
                //���� ������ ���� ���������
                if(MapModeData.UpdateProvinceHeight(
                    ref mapMode,
                    ref pR,
                    0.0f) == true)
                {
                    //��������, ��� ������ ���������
                    isHeightUpdated = true;
                }

                //���� ������ ����� ��� �������
                if(MapModeData.UpdateProvinceColorIndex(
                    ref mapMode,
                    ref pR,
                    -1) == true)
                {
                    //��������, ��� ���� �������
                    isColorUpdated = true;
                }
            }

            //���� ��� ��������� � ��������
            if(provinceUpdateProvinceRenderSRFilter.Value.GetEntitiesCount() == 0)
            {
                //��������, ��� ��������� ���������� �����
                isHeightUpdated = true;
                isColorUpdated = true;
            }

            //��� ������ ��������� � ��������
            foreach (int provinceEntity in provinceUpdateProvinceRenderSRFilter.Value)
            {
                //���� ��������� � ������
                ref CProvinceRender pR = ref pRPool.Value.Get(provinceEntity);
                ref SRUpdateProvinceRender requestComp = ref updateProvinceRenderSRPool.Value.Get(provinceEntity);

                //��������� ������������ ������ ���������
                MapModeData.UpdateProvinceDisplayedObject(
                    ref mapMode,
                    ref pR,
                    requestComp.displayedObjectPE);

                //�������� ��������� ������������ ���������
                //���� ������ ���� ���������
                if (MapModeData.UpdateProvinceHeight(
                    ref mapMode,
                    ref pR,
                    requestComp.height) == true)
                {
                    //��������, ��� ������ ���������
                    isHeightUpdated = true;
                }

                //���� ������ ����� ��� �������
                if (MapModeData.UpdateProvinceColorIndex(
                    ref mapMode,
                    ref pR,
                    requestComp.colorIndex) == true)
                {
                    //��������, ��� ���� �������
                    isColorUpdated = true;
                }

                //������� ������
                updateProvinceRenderSRPool.Value.Del(provinceEntity);
            }
        }
    }
}
