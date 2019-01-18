using UnityEngine;
using System.Collections.Generic;

public class UnityObjectAnimation {
    public bool RecordScale = false;
	public UnityCurveContainer[] curves;
	public Transform observeGameObject;
	public string pathName = "";

	public UnityObjectAnimation( string hierarchyPath, Transform observeObj, bool recordScale ) {
        RecordScale = recordScale;
		pathName = hierarchyPath;
		observeGameObject = observeObj;

		curves = new UnityCurveContainer[RecordScale ? 10 : 7];

		curves [0] = new UnityCurveContainer( "localPosition.x" );
		curves [1] = new UnityCurveContainer( "localPosition.y" );
		curves [2] = new UnityCurveContainer( "localPosition.z" );

		curves [3] = new UnityCurveContainer( "localRotation.x" );
		curves [4] = new UnityCurveContainer( "localRotation.y" );
		curves [5] = new UnityCurveContainer( "localRotation.z" );
		curves [6] = new UnityCurveContainer( "localRotation.w" );

        if (RecordScale)
        {
            curves[7] = new UnityCurveContainer("localScale.x");
            curves[8] = new UnityCurveContainer("localScale.y");
            curves[9] = new UnityCurveContainer("localScale.z");
        }
	}

    public void Finish()
    {
        if (skippedLastFrame)
        {
            curves[0].AddValue(prevTime, lastPosition.x);
            curves[1].AddValue(prevTime, lastPosition.y);
            curves[2].AddValue(prevTime, lastPosition.z);

            curves[3].AddValue(prevTime, lastRotation.x);
            curves[4].AddValue(prevTime, lastRotation.y);
            curves[5].AddValue(prevTime, lastRotation.z);
            curves[6].AddValue(prevTime, lastRotation.w);

            if (RecordScale)
            {
                curves[7].AddValue(prevTime, lastScale.x);
                curves[8].AddValue(prevTime, lastScale.y);
                curves[9].AddValue(prevTime, lastScale.z);
            }
            skippedLastFrame = false;
        }
    }

    bool isFirstFrame = true;
    Vector3 lastPosition = new Vector3();
    Quaternion lastRotation = new Quaternion();
    Vector3 lastScale = new Vector3();
    float prevTime = 0;
    bool skippedLastFrame = false;

	public void AddFrame ( float time ) {
        if (isFirstFrame)
        {
            lastPosition = observeGameObject.localPosition;
            lastRotation = observeGameObject.localRotation;
            lastScale = observeGameObject.localScale;
            prevTime = time;
            isFirstFrame = false;
        }
        else
        {
            if (!(
                (lastPosition - observeGameObject.localPosition).sqrMagnitude > float.Epsilon || 
                Quaternion.Dot(lastRotation, observeGameObject.localRotation) < (1.0f - float.Epsilon) ||
                (lastScale - observeGameObject.localScale).sqrMagnitude > float.Epsilon
                ))
            {
                skippedLastFrame = true;
                prevTime = time;
                return;
            }

            if (skippedLastFrame)
            {
                curves[0].AddValue(prevTime, lastPosition.x);
                curves[1].AddValue(prevTime, lastPosition.y);
                curves[2].AddValue(prevTime, lastPosition.z);

                curves[3].AddValue(prevTime, lastRotation.x);
                curves[4].AddValue(prevTime, lastRotation.y);
                curves[5].AddValue(prevTime, lastRotation.z);
                curves[6].AddValue(prevTime, lastRotation.w);

                if (RecordScale)
                {
                    curves[7].AddValue(prevTime, lastScale.x);
                    curves[8].AddValue(prevTime, lastScale.y);
                    curves[9].AddValue(prevTime, lastScale.z);
                }
                skippedLastFrame = false;
            }
        }

        prevTime = time;
        lastPosition = observeGameObject.localPosition;
        lastRotation = observeGameObject.localRotation;
        lastScale = observeGameObject.localScale;

        curves [0].AddValue (time, observeGameObject.localPosition.x);
		curves [1].AddValue (time, observeGameObject.localPosition.y);
		curves [2].AddValue (time, observeGameObject.localPosition.z);

		curves [3].AddValue (time, observeGameObject.localRotation.x);
		curves [4].AddValue (time, observeGameObject.localRotation.y);
		curves [5].AddValue (time, observeGameObject.localRotation.z);
		curves [6].AddValue (time, observeGameObject.localRotation.w);

        if (RecordScale)
        {
            curves[7].AddValue(time, observeGameObject.localScale.x);
            curves[8].AddValue(time, observeGameObject.localScale.y);
            curves[9].AddValue(time, observeGameObject.localScale.z);
        }
	}
}
