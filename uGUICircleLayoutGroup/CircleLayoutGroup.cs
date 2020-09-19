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


        public void ManualUpdateLayout()
        {
            UpdateLayout();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            UpdateLayout();
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
            var finalSpacing = spacing;

            if (mode == EMode.AverageFill)
                finalSpacing = ONE_CIRCLE / rectChildren.Count;

            for (int i = 0, iMax = rectChildren.Count; i < iMax; i++)
            {
                var quat = Quaternion.AngleAxis(i * finalSpacing + offset, Vector3.forward);
                var current = transform.localPosition + quat * Vector3.up * radius;

                rectChildren[i].localPosition = current;

                if (lookAtToPivot)
                    rectChildren[i].up = (transform.localPosition - rectChildren[i].localPosition).normalized;
            }
        }
    }
}
