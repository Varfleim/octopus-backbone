
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace GBB.Map
{
    public class SMapRender : IEcsRunSystem
    {
        readonly EcsWorldInject world = default;


        readonly EcsPoolInject<C_ProvinceRender> pRPool = default;

        readonly EcsPoolInject<R_MapProvincesUpdate> mapProvincesUpdateRPool = default;
        
        public void Run(IEcsSystems systems)
        {
            //��������� ������ ������
            MapEdgesDataUpdate();

            //��������� ������ ������� �����
            MapModesDataUpdate();
        }

        readonly EcsPoolInject<R_MapEdgesUpdate> mapEdgesUpdateRPool = default;
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

        readonly EcsFilterInject<Inc<C_ProvinceRender, SR_UpdateThinEdges>> provinceUpdateThinEdgesSRFilter = default;
        readonly EcsFilterInject<Inc<C_ProvinceRender>, Exc<SR_UpdateThinEdges>> provinceWithoutUpdateThinEdgesSRFilter = default;
        readonly EcsPoolInject<SR_UpdateThinEdges> updateThinEdgesSRPool = default;
        void MapThinEdgesDataUpdate(
            out bool isThinUpdated)
        {
            //������������� �������� �� ���������
            isThinUpdated = false;

            //��� ������ ��������� ��� ������� ���������� ������ ������
            foreach (int provinceEntity in provinceWithoutUpdateThinEdgesSRFilter.Value)
            {
                //���� ���������
                ref C_ProvinceRender pR = ref pRPool.Value.Get(provinceEntity);

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
                ref C_ProvinceRender pR = ref pRPool.Value.Get(provinceEntity);
                ref SR_UpdateThinEdges requestComp = ref updateThinEdgesSRPool.Value.Get(provinceEntity);

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

        readonly EcsFilterInject<Inc<C_ProvinceRender, SR_UpdateThickEdges>> provinceUpdateThickEdgesSRFilter = default;
        readonly EcsFilterInject<Inc<C_ProvinceRender>, Exc<SR_UpdateThickEdges>> provinceWithoutUpdateThickEdgesSRFilter = default;
        readonly EcsPoolInject<SR_UpdateThickEdges> updateThickEdgesSRPool = default;
        void MapThickEdgesDataUpdate(
            out bool isThickUpdated)
        {
            //������������� �������� �� ���������
            isThickUpdated = false;

            //��� ������ ��������� ��� ������� ���������� ������� ������
            foreach (int provinceEntity in provinceWithoutUpdateThickEdgesSRFilter.Value)
            {
                //���� ���������
                ref C_ProvinceRender pR = ref pRPool.Value.Get(provinceEntity);

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
                ref C_ProvinceRender pR = ref pRPool.Value.Get(provinceEntity);
                ref SR_UpdateThickEdges requestComp = ref updateThickEdgesSRPool.Value.Get(provinceEntity);

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

        readonly EcsFilterInject<Inc<C_MapModeCore, CT_ActiveMapMode, SR_MapModeUpdate>> activeMapModeFilter = default;
        readonly EcsPoolInject<C_MapModeCore> mapModePool = default;
        void MapModesDataUpdate()
        {
            //��� ������� ��������� ������ ����� � �������� ����������
            foreach (int activeMapModeEntity in activeMapModeFilter.Value)
            {
                //���� ����� �����
                ref C_MapModeCore activeMapMode = ref mapModePool.Value.Get(activeMapModeEntity);

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

        readonly EcsFilterInject<Inc<C_ProvinceRender, SR_UpdateProvinceRender>> provinceUpdateProvinceRenderSRFilter = default;
        readonly EcsFilterInject<Inc<C_ProvinceRender>, Exc<SR_UpdateProvinceRender>> provinceWithoutUpdateProvinceRenderSRFilter = default;
        readonly EcsPoolInject<SR_UpdateProvinceRender> updateProvinceRenderSRPool = default;
        void MapProvinceRenderDataUpdate(
            ref C_MapModeCore mapMode,
            out bool isHeightUpdated, out bool isColorUpdated)
        {
            //������������� �������� �� ���������
            isHeightUpdated = false;
            isColorUpdated = false;

            //��� ������ ��������� ��� ������� ���������� ������������
            foreach (int provinceEntity in provinceWithoutUpdateProvinceRenderSRFilter.Value)
            {
                //���� ���������
                ref C_ProvinceRender pR = ref pRPool.Value.Get(provinceEntity);

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
                ref C_ProvinceRender pR = ref pRPool.Value.Get(provinceEntity);
                ref SR_UpdateProvinceRender requestComp = ref updateProvinceRenderSRPool.Value.Get(provinceEntity);

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
