
using System.Collections.Generic;

using UnityEngine;

using Leopotam.EcsLite;

namespace GBB.Map
{
    public class MapModeData : MonoBehaviour
    {
        internal EcsPackedEntity defaultMapModePE;

        public EcsPackedEntity activeMapModePE;

        public static void MapModeCreationRequest(
            EcsPool<SRMapModeCreation> requestPool,
            int mapModeEntity, string mapModeName,
            bool defaultMapMode)
        {
            //��������� �������� ������ �������� ������ �����
            ref SRMapModeCreation requestComp = ref requestPool.Add(mapModeEntity);

            //��������� ������ �������
            requestComp = new(
                mapModeName,
                defaultMapMode);
        }

        public static void MapModeUpdateColorsListFirstRequest(
            EcsWorld world,
            EcsPool<RMapModeUpdateColorsListFirst> requestPool,
            string coloredObjectType,
            List<Color> objectColors)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref RMapModeUpdateColorsListFirst requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                coloredObjectType,
                objectColors);
        }

        public static void MapModeUpdateColorsListSecondRequest(
            EcsWorld world,
            EcsPool<RMapModeUpdateColorsListSecond> requestPool,
            EcsPackedEntity mapModePE,
            List<Color> mapModeColors, Color defaultColor)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref RMapModeUpdateColorsListSecond requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                mapModePE,
                mapModeColors, defaultColor);
        }

        internal static void MapModeActivationRequest(
            EcsWorld world,
            EcsPool<RMapModeActivation> requestPool,
            EcsPackedEntity mapModePE)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref RMapModeActivation requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                mapModePE);
        }

        internal static void MapModeUpdateRequest(
            EcsPool<SRMapModeUpdate> requestPool,
            int mapModeEntity)
        {
            //��������� �������� ������ ����� ������ 
            ref SRMapModeUpdate requestComp = ref requestPool.Add(mapModeEntity);

            //��������� ������ �������
            requestComp = new(0);
        }

        public static void UpdateThinEdgesRequest(
            EcsPool<SRUpdateThinEdges> requestPool,
            int targetEntity,
            int edgeIndex)
        {
            //��������� �������� ������
            ref SRUpdateThinEdges requestComp = ref requestPool.Add(targetEntity);

            //��������� ������ �������
            requestComp = new(
                edgeIndex);
        }
        
        public static void UpdateThickEdgesRequest(
            EcsPool<SRUpdateThickEdges> requestPool,
            int targetEntity,
            int edgeIndex)
        {
            //��������� �������� ������
            ref SRUpdateThickEdges requestComp = ref requestPool.Add(targetEntity);

            //��������� ������ �������
            requestComp = new(
                edgeIndex);
        }

        public static void UpdateProvinceRenderRequestFull(
           EcsPool<SRUpdateProvinceRender> requestPool,
           //ref CMapModeCore mapMode,
           int targetEntity,
           EcsPackedEntity displayedObjectPE,
           float height,
           int colorIndex)
        {
            //��������� �������� ������
            ref SRUpdateProvinceRender requestComp = ref requestPool.Add(targetEntity);

            //��������� ������ �������
            requestComp = new(
                displayedObjectPE,
                height,
                colorIndex);
        }

        public static void UpdateProvinceRenderRequestCreation(
            EcsPool<SRUpdateProvinceRender> requestPool,
            int targetEntity)
        {
            //��������� �������� ������
            ref SRUpdateProvinceRender requestComp = ref requestPool.Add(targetEntity);
        }

        public static void UpdateProvinceRenderRequestUpdate(
            ref CMapModeCore mapMode,
            ref SRUpdateProvinceRender requestComp,
            EcsPackedEntity displayedObjectPE,
            float height,
            int colorIndex)
        {
            //��������� ������ �������
            requestComp = new(
                displayedObjectPE,
                height,
                colorIndex);
        }

        public static void ShowMapHoverHighlightRequest(
            EcsPool<SRShowMapHoverHighlight> requestPool,
            ref CMapModeCore mapMode,
            int targetEntity)
        {
            //��������� �������� ������
            ref SRShowMapHoverHighlight requestComp = ref requestPool.Add(targetEntity);

            //��������� ������ �������
            requestComp = new(0);
        }

        internal static void UpdateProvinceDisplayedObject(
            ref CMapModeCore mapMode,
            ref CProvinceRender pR,
            EcsPackedEntity displayedObjectPE)
        {
            //���� ������������ ������ ��������� �� ����� �����������
            if (pR.DisplayedObjectPE.EqualsTo(in displayedObjectPE) == false)
            {
                //��������� ���
                pR.SetDisplayedObject(displayedObjectPE);
            }
        }

        internal static bool UpdateProvinceThinEdgesIndex(
            ref CProvinceRender pR,
            int newThinEdgesIndex)
        {
            //���� ������ ������ ������ ��������� �� ����� �����������
            if(pR.ThinEdgesIndex != newThinEdgesIndex)
            {
                //��������� ���
                pR.SetThinEdgesIndex(newThinEdgesIndex);

                //����������, ��� ������ ��� �������
                return true;
            }
            //�����
            else
            {
                //����������, ��� ������ �� ��� �������
                return false;
            }
        }

        internal static bool UpdateProvinceThickEdgesIndex(
            ref CProvinceRender pR,
            int newThickEdgesIndex)
        {
            //���� ������ ������� ������ ��������� �� ����� �����������
            if (pR.ThickEdgesIndex != newThickEdgesIndex)
            {
                //��������� ���
                pR.SetThickEdgesIndex(newThickEdgesIndex);

                //����������, ��� ������ ��� �������
                return true;
            }
            //�����
            else
            {
                //����������, ��� ������ �� ��� �������
                return false;
            }
        }

        internal static bool UpdateProvinceHeight(
            ref CMapModeCore mapMode,
            ref CProvinceRender pR,
            float newProvinceHeight)
        {
            //���� ������ ��������� �� ����� ����������
            if (pR.ProvinceHeight != newProvinceHeight)
            {
                //��������� �
                pR.SetHeight(newProvinceHeight);

                //����������, ��� ������ ���� ���������
                return true;
            }
            //�����
            else
            {
                //����������, ��� ������ �� ���� ���������
                return false;
            }
        }

        internal static bool UpdateProvinceColorIndex(
            ref CMapModeCore mapMode,
            ref CProvinceRender pR,
            int newProvinceColorIndex)
        {
            //���� ������ ����� ��������� �� ����� �����������
            if (pR.ProvinceColorIndex != newProvinceColorIndex)
            {
                //��������� ���
                pR.SetColorIndex(newProvinceColorIndex);

                //����������, ��� ������ ����� ��� �������
                return true;
            }
            //�����
            else
            {
                //����������, ��� ������ ����� �� ��� �������
                return false;
            }
        }
    }
}
