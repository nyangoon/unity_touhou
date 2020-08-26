using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;
using System;

class WebGLBuilder
{
	static void build()
	{
		string[] scenes = {
			"Assets/Sprites/Alice01/Scenes/SampleScene.unity"
		};

		UnityEditor.BuildPipeline.BuildPlayer(scenes, "WebGL-Dist", UnityEditor.BuildTarget.WebGL, UnityEditor.BuildOptions.Development);
	}
}
