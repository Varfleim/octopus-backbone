
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
            EcsPool<SR_MapModeCreation> requestPool,
            int mapModeEntity, string mapModeName,
            bool defaultMapMode)
        {
            //��������� �������� ������ �������� ������ �����
            ref SR_MapModeCreation requestComp = ref requestPool.Add(mapModeEntity);

            //��������� ������ �������
            requestComp = new(
                mapModeName,
                defaultMapMode);
        }

        public static void MapModeUpdateColorsListFirstRequest(
            EcsWorld world,
            EcsPool<R_MapModeUpdateColorsListFirst> requestPool,
            string coloredObjectType,
            List<Color> objectColors)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref R_MapModeUpdateColorsListFirst requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                coloredObjectType,
                objectColors);
        }

        public static void MapModeUpdateColorsListSecondRequest(
            EcsWorld world,
            EcsPool<R_MapModeUpdateColorsListSecond> requestPool,
            EcsPackedEntity mapModePE,
            List<Color> mapModeColors, Color defaultColor)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref R_MapModeUpdateColorsListSecond requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                mapModePE,
                mapModeColors, defaultColor);
        }

        internal static void MapModeActivationRequest(
            EcsWorld world,
            EcsPool<R_MapModeActivation> requestPool,
            EcsPackedEntity mapModePE)
        {
            //������ ����� �������� � ��������� �� ������
            int requestEntity = world.NewEntity();
            ref R_MapModeActivation requestComp = ref requestPool.Add(requestEntity);

            //��������� ������ �������
            requestComp = new(
                mapModePE);
        }

        internal static void MapModeUpdateRequest(
            EcsPool<SR_MapModeUpdate> requestPool,
            int mapModeEntity)
        {
            //��������� �������� ������ ����� ������ 
            ref SR_MapModeUpdate requestComp = ref requestPool.Add(mapModeEntity);

            //��������� ������ �������
            requestComp = new(0);
        }

        public static void UpdateThinEdgesRequest(
            EcsPool<SR_UpdateThinEdges> requestPool,
            int targetEntity,
            int edgeIndex)
        {
            //��������� �������� ������
            ref SR_UpdateThinEdges requestComp = ref requestPool.Add(targetEntity);

            //��������� ������ �������
            requestComp = new(
                edgeIndex);
        }
        
        public static void UpdateThickEdgesRequest(
            EcsPool<SR_UpdateThickEdges> requestPool,
            int targetEntity,
            int edgeIndex)
        {
            //��������� �������� ������
            ref SR_UpdateThickEdges requestComp = ref requestPool.Add(targetEntity);

            //��������� ������ �������
            requestComp = new(
                edgeIndex);
        }

        public static void UpdateProvinceRenderRequestFull(
           EcsPool<SR_UpdateProvinceRender> requestPool,
           //ref CMapModeCore mapMode,
           int targetEntity,
           EcsPackedEntity displayedObjectPE,
           float height,
           int colorIndex)
        {
            //��������� �������� ������
            ref SR_UpdateProvinceRender requestComp = ref requestPool.Add(targetEntity);

            //��������� ������ �������
            requestComp = new(
                displayedObjectPE,
                height,
                colorIndex);
        }

        public static void UpdateProvinceRenderRequestCreation(
            EcsPool<SR_UpdateProvinceRender> requestPool,
            int targetEntity)
        {
            //��������� �������� ������
            ref SR_UpdateProvinceRender requestComp = ref requestPool.Add(targetEntity);
        }

        public static void UpdateProvinceRenderRequestUpdate(
            ref C_MapModeCore mapMode,
            ref SR_UpdateProvinceRender requestComp,
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
            EcsPool<SR_ShowMapHoverHighlight> requestPool,
            ref C_MapModeCore mapMode,
            int targetEntity)
        {
            //��������� �������� ������
            ref SR_ShowMapHoverHighlight requestComp = ref requestPool.Add(targetEntity);

            //��������� ������ �������
            requestComp = new(0);
        }

        internal static void UpdateProvinceDisplayedObject(
            ref C_MapModeCore mapMode,
            ref C_ProvinceRender pR,
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
            ref C_ProvinceRender pR,
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
            ref C_ProvinceRender pR,
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
            ref C_MapModeCore mapMode,
            ref C_ProvinceRender pR,
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
            ref C_MapModeCore mapMode,
            ref C_ProvinceRender pR,
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
