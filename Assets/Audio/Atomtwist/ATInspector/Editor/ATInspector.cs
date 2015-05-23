using UnityEngine;
using System.Collections;
using UnityEditor;
using Atomtwist;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Atomtwist {

	public class ATInspector : Editor {
		
		
		GUIStyle rich;
		Texture2D headerLogo;
		
		public virtual void OnEnable()
		{
			rich = new GUIStyle();
			rich.richText = true;
			headerLogo = Resources.Load("Textures/AtomtwistLogo", typeof(Texture2D)) as Texture2D;
		}


		public void DrawPsychoColors(Action[] actions) {


			GUI.color = Color.cyan;
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.BeginVertical("Box");
			GUI.color = Color.yellow;
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.BeginVertical("Box");
			EditorGUILayout.BeginVertical("Box");
			GUI.color = Color.white;
			EditorGUILayout.BeginVertical("Box");
			
			actions.ToList ().ForEach(a => a());
			
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
		}

		public void DrawPsychoHeader(string headerText)
		{
			DrawPsychoColors(new Action[] {
				() => DrawHeader(headerText + "™")
			});
		}
		
		void Header(string text, int length)
		{
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent(headerLogo), GUILayout.Height(58), GUILayout.Width(58));
			GUILayout.BeginVertical();
			EditorGUILayout.Space();EditorGUILayout.Space();EditorGUILayout.Space();EditorGUILayout.Space();
			EditorGUILayout.LabelField(text, GUIStyle.none, GUILayout.Width(length));
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		void Text(string text, int length)
		{
			EditorGUILayout.Space();
			EditorGUI.indentLevel =1;
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
			EditorGUILayout.LabelField(text, GUIStyle.none, GUILayout.Width(length));
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			EditorGUI.indentLevel =0;
			EditorGUILayout.Space();
			
		}
		
		
		public void DrawHeader(string headerText)
		{
			Header("<size=13><b><color=#FFFFF>" + headerText + "</color></b></size>",22);
		}

		public void DrawHeading(string headingText, float textSize, Color color)
		{
			Text("<size="+textSize+ "><b><color=" + ATColorConverter.ColorToHex(color) + ">" + headingText + "</color></b></size>",55);
		}

		
	}
	
	
}
