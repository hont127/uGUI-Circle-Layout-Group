using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hont
{
    [AddComponentMenu("Layout / Circle Layout Group(Extra)", 151)]
    public class CircleLayoutGroup : LayoutGroup
    {
        public enum EMode { AverageFill, FixedStep }

        const float ONE_CIRCLE = 360f;

        public bool lookAtToPivot;
        public float offset;
        public float spacing;
        public float radius;
        public EMode mode;

        [SerializeField]
        float referenceResolution_x;

        Camera mCacheMainCamera;


        public void ManualUpdateLayout()
        {
            UpdateLayout();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            UpdateLayout();

            mCacheMainCamera = Camera.main;
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            UpdateLayout();
        }
#endif

        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
        }

        public override void SetLayoutVertical()
        {
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();

            UpdateLayout();
        }

        protected override void OnTransformChildrenChanged()
        {
            base.OnTransformChildrenChanged();

            UpdateLayout();
        }

        protected override void OnDidApplyAnimationProperties()
        {
            base.OnDidApplyAnimationProperties();

            UpdateLayout();
        }

        void UpdateLayout()
        {
            var scaleFactor = 1f;
            if (!Application.isPlaying)
            {
#if UNITY_EDITOR

                var dstCamera = Camera.main;
                if (dstCamera == null)
                    dstCamera = Camera.current;

                if (dstCamera != null)
                {
                    referenceResolution_x = dstCamera.pixelWidth;
                }
#endif
            }
            else
            {
                if (referenceResolution_x > 0f && mCacheMainCamera != null)
                {
                    scaleFactor = mCacheMainCamera.pixelWidth / referenceResolution_x;
                }
            }

            var finalSpacing = spacing * scaleFactor;

            if (mode == EMode.AverageFill)
                finalSpacing = ONE_CIRCLE / rectChildren.Count;

            for (int i = 0, iMax = rectChildren.Count; i < iMax; i++)
            {
                var quat = Quaternion.AngleAxis(i * finalSpacing + offset * scaleFactor, Vector3.forward);
                var current = transform.position + quat * Vector3.up * radius * scaleFactor;

                rectChildren[i].position = current;

                if (lookAtToPivot)
                    rectChildren[i].up = (transform.position - rectChildren[i].position).normalized;
            }
        }
    }
}
