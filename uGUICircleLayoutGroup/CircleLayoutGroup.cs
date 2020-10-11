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

        [SerializeField]
        bool lookAtToPivot = false;
        [SerializeField]
        float offset = 0f;
        [SerializeField]
        float spacing = 10f;
        [SerializeField]
        float radius = 10f;
        [SerializeField]
        EMode mode = EMode.AverageFill;
        [SerializeField]
        bool clockwise = true;

        public float Radius
        {
            get { return radius; }
            set { radius = value; UpdateLayout(); }
        }

        public float Spacing
        {
            get { return spacing; }
            set { spacing = value; UpdateLayout(); }
        }

        public float Offset
        {
            get { return offset; }
            set { offset = value; UpdateLayout(); }
        }

        public bool LookAtToPivot
        {
            get { return lookAtToPivot; }
            set { lookAtToPivot = value; UpdateLayout(); }
        }

        public EMode Mode
        {
            get { return mode; }
            set { mode = value; UpdateLayout(); }
        }

        public bool Clockwise
        {
            get { return clockwise; }
            set { clockwise = value; UpdateLayout(); }
        }


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

            for (int i = 0, j = 0, iMax = rectChildren.Count; i < iMax; i++)
            {
                if (clockwise)
                    j = iMax - (i + 1);
                else
                    j = i;

                var quat = Quaternion.AngleAxis(i * finalSpacing + offset, Vector3.forward);
                var current = transform.localPosition + quat * Vector3.up * radius;

                rectChildren[j].localPosition = current;

                if (lookAtToPivot)
                    rectChildren[j].up = (transform.localPosition - rectChildren[j].localPosition).normalized;
            }
        }
    }
}
