using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hont
{
    public class CircleLayoutGroup : LayoutGroup
    {
        public enum ModeEnum { AverageFill, FixedStep }

        const float ONE_CIRCLE = 360f;

        public bool lookAtToPivot;
        public float offset;
        public float spacing;
        public float radius;
        public ModeEnum mode;


        public void ManualUpdateLayout()
        {
            Execute();
        }

        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
        }

        public override void SetLayoutVertical()
        {
        }

        protected override void OnTransformChildrenChanged()
        {
            base.OnTransformChildrenChanged();

            Execute();
        }

        protected override void OnDidApplyAnimationProperties()
        {
            base.OnDidApplyAnimationProperties();

            Execute();
        }

        void Execute()
        {
            var finalSpacing = spacing;

            if (mode == ModeEnum.AverageFill)
                finalSpacing = ONE_CIRCLE / rectChildren.Count;

            for (int i = 0, iMax = rectChildren.Count; i < iMax; i++)
            {
                var quat = Quaternion.AngleAxis(i * finalSpacing + offset, Vector3.forward);
                var current = transform.position + quat * Vector3.up * radius;

                rectChildren[i].position = current;

                if (lookAtToPivot)
                    rectChildren[i].up = (transform.position - rectChildren[i].position).normalized;
            }
        }
    }
}
