using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class FbxObjectsManager {

	List<FbxObjectNode> objNodes;

	public FbxObjectsManager () {
		objNodes = new List<FbxObjectNode> ();
	}

	// insert new objs into file
	public void EditTargetFile (string targetFilePath) {

		string sourceData = File.ReadAllText (targetFilePath);
		string newData = "";

		// find start of the Objects
		int startIndex = sourceData.IndexOf ("Objects:  {\n");
		startIndex += ("Objects:  {\n").Length;

		// copy data into new
		newData = sourceData.Substring(0, startIndex);



		StringReader reader = new StringReader (sourceData);

		// skip to start index
		for (int i = 0; i < startIndex; i++)
			reader.Read ();


		// find the end of the Objects {}
		int bracketBalancer = 1;
		int readCounter = 0;

		while (true) {
			char temp = (char)reader.Read ();
			++readCounter;

			if (temp == '{')
				bracketBalancer += 1;
			else if (temp == '}') {
				bracketBalancer -= 1;
				if (bracketBalancer == 0)
					break;
			}
		}

		// write other data in new
		newData += sourceData.Substring(startIndex, readCounter-1);

		// write custom datas
		for (int i = 0; i < objNodes.Count; i++)
			newData += objNodes [i].GetResultString ();


		// end the file
		newData += sourceData.Substring(startIndex + readCounter-1);

		File.WriteAllText (targetFilePath, newData);
	}

	public void AddObject (string nodeType, string nodeId, string nodeName, string subType) {
		FbxObjectNode obj = new FbxObjectNode (nodeType, nodeId, nodeName, subType);
	}

	public void AddAnimationCurveNode (string inputId, FbxAnimationCurveNodeType animCurveType, Vector3 initData ) {
		string nodeName = "";

		if (animCurveType == FbxAnimationCurveNodeType.Translation)
			nodeName = "AnimCurveNode::T";
		else if( animCurveType == FbxAnimationCurveNodeType.Rotation)
			nodeName = "AnimCurveNode::R";
		else if( animCurveType == FbxAnimationCurveNodeType.Scale)
			nodeName = "AnimCurveNode::S";
		else if( animCurveType == FbxAnimationCurveNodeType.Visibility)
			nodeName = "AnimCurveNode::Visibility";
		
		FbxObjectNode obj = new FbxObjectNode ("AnimationCurveNode", inputId, nodeName, "");

		string pName = "Properties70";
		string pData = " {\n";
		pData += "\t\t\tP: \"d|X\", \"Number\", \"\", \"A\"," + initData.x.ToString () + "\n";
		pData += "\t\t\tP: \"d|Y\", \"Number\", \"\", \"A\"," + initData.y.ToString () + "\n";
		pData += "\t\t\tP: \"d|Z\", \"Number\", \"\", \"A\"," + initData.z.ToString () + "\n";
		pData += "\t\t}\n";

		obj.AddSubnode (pName, pData);

		objNodes.Add (obj);
	}

	public void AddAnimationCurve (string inputId, float[] curveData) {
		FbxObjectNode obj = new FbxObjectNode ("AnimationCurve", inputId, "AnimCurve::", "");

		// prepare some data
		string[] timeArray = new string[curveData.Length];
		int[] keyAttrFlagDatas = new int[curveData.Length];

		for (int i = 0; i < timeArray.Length; i++) {
			timeArray [i] = FbxHelper.getFbxSeconds(i, 60);
			keyAttrFlagDatas [i] = 24840;
		}

		// add properties
		obj.AddSubnode ("Default", "0");
		obj.AddSubnode ("KeyVer", "4008");
		obj.AddSubnode ("KeyTime", timeArray);
		obj.AddSubnode ("KeyValueFloat", curveData);
		obj.AddSubnode (";KeyAttrFlags", "Cubic|TangeantAuto|GenericTimeIndependent|GenericClampProgressive");
		obj.AddSubnode ("KeyAttrFlags", new int[]{24840});
		//obj.AddSubnode (";KeyAttrDataFloat", "RightAuto:0, NextLeftAuto:61.3648; RightAuto:0, NextLeftAuto:0; RightAuto:0, NextLeftAuto:0");
		obj.AddSubnode ("KeyAttrRefCount", new int[]{timeArray.Length});

		objNodes.Add (obj);
	}
}

public enum FbxAnimationCurveNodeType {
	Translation,
	Rotation,
	Scale,
	Visibility
}
