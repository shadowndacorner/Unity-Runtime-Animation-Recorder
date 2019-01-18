using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(UnityAnimationRecorder))]
public class UnityAnimationRecorderEditor : Editor {

	// save file path
	SerializedProperty savePath;
	SerializedProperty fileName;

	SerializedProperty startRecordKey;
	SerializedProperty stopRecordKey;

	// options
	SerializedProperty showLogGUI;
	SerializedProperty recordLimitedFrames;
	SerializedProperty recordFrames;
	SerializedProperty recordBlendShape;
	SerializedProperty recordOnStart;

	SerializedProperty changeTimeScale;
	SerializedProperty timeScaleOnStart;
	SerializedProperty timeScaleOnRecord;

	SerializedProperty limitFramerate;
	SerializedProperty targetFramerate;
	SerializedProperty recordScale;

    void OnEnable() {
        savePath = serializedObject.FindProperty("savePath");
        fileName = serializedObject.FindProperty("fileName");

        startRecordKey = serializedObject.FindProperty("startRecordKey");
        stopRecordKey = serializedObject.FindProperty("stopRecordKey");

        showLogGUI = serializedObject.FindProperty("showLogGUI");
        recordLimitedFrames = serializedObject.FindProperty("recordLimitedFrames");
        recordFrames = serializedObject.FindProperty("recordFrames");
        recordBlendShape = serializedObject.FindProperty("recordBlendShape");
        recordOnStart = serializedObject.FindProperty("recordOnStart");

        changeTimeScale = serializedObject.FindProperty("changeTimeScale");
        timeScaleOnStart = serializedObject.FindProperty("timeScaleOnStart");
        timeScaleOnRecord = serializedObject.FindProperty("timeScaleOnRecord");

        limitFramerate = serializedObject.FindProperty("limitFramerate");
        targetFramerate = serializedObject.FindProperty("targetFramerate");
        recordScale = serializedObject.FindProperty("recordScale");
    }

	public override void OnInspectorGUI () {
		serializedObject.Update ();

		EditorGUILayout.LabelField ("== Path Settings ==");

		if (GUILayout.Button ("Set Save Path")) {
			string defaultName = serializedObject.targetObject.name + "-Animation";
			string targetPath = EditorUtility.SaveFilePanelInProject ("Save Anim File To ..", defaultName, "", "please select a folder and enter the file name");

			int lastIndex = targetPath.LastIndexOf ("/");
			savePath.stringValue = targetPath.Substring (0, lastIndex + 1);
			string toFileName = targetPath.Substring (lastIndex + 1);

			fileName.stringValue = toFileName;
		}
		EditorGUILayout.PropertyField (savePath);
		EditorGUILayout.PropertyField (fileName);


		EditorGUILayout.Space ();

		// keys setting
		EditorGUILayout.LabelField( "== Control Keys ==" );
		EditorGUILayout.PropertyField (startRecordKey);
		EditorGUILayout.PropertyField (stopRecordKey);

		EditorGUILayout.Space ();

		EditorGUILayout.LabelField( "== Recording Settings ==" );
        limitFramerate.boolValue = EditorGUILayout.Toggle ("Limit Framerate", limitFramerate.boolValue);
        if (limitFramerate.boolValue)
        {
            targetFramerate.floatValue = EditorGUILayout.FloatField("Limit Framerate", targetFramerate.floatValue);
            targetFramerate.floatValue = Mathf.Max(targetFramerate.floatValue, 1.0f);
        }

        recordScale.boolValue = EditorGUILayout.Toggle ("Record Scale", recordScale.boolValue);

        
        EditorGUILayout.Space ();

		// Other Settings
		EditorGUILayout.LabelField( "== Other Settings ==" );
        recordOnStart.boolValue = EditorGUILayout.Toggle ("Record On Start", recordOnStart.boolValue);
		recordBlendShape.boolValue = EditorGUILayout.Toggle ("Record BlendShapes", recordBlendShape.boolValue);
		bool timeScaleOption = EditorGUILayout.Toggle ( "Change Time Scale", changeTimeScale.boolValue);
		changeTimeScale.boolValue = timeScaleOption;

		if (timeScaleOption) {
			timeScaleOnStart.floatValue = EditorGUILayout.FloatField ("TimeScaleOnStart", timeScaleOnStart.floatValue);
			timeScaleOnRecord.floatValue = EditorGUILayout.FloatField ("TimeScaleOnRecord", timeScaleOnRecord.floatValue);
		}

		// gui log message
		showLogGUI.boolValue = EditorGUILayout.Toggle ("Show Debug On GUI", showLogGUI.boolValue);

		// recording frames setting
		recordLimitedFrames.boolValue = EditorGUILayout.Toggle( "Record Limited Frames", recordLimitedFrames.boolValue );

		if (recordLimitedFrames.boolValue)
			EditorGUILayout.PropertyField (recordFrames);

		serializedObject.ApplyModifiedProperties ();

		//DrawDefaultInspector ();
	}
}
