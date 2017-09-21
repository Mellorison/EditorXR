﻿#if UNITY_EDITOR
using System;
using UnityEditor.Experimental.EditorVR.Modules;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEditor.Experimental.EditorVR.Menus
{
	sealed class MainMenuButton : MonoBehaviour, ITooltip, IRayEnterHandler, IRayExitHandler, IPointerClickHandler
	{
		[SerializeField]
		Button m_Button;

		[SerializeField]
		Text m_ButtonDescription;

		[SerializeField]
		Text m_ButtonTitle;

		Color m_OriginalColor;

		public Button button { get { return m_Button; } }

		public string tooltipText { get { return tooltip != null ? tooltip.tooltipText : null; } }

		public ITooltip tooltip { private get; set; }

		public Type toolType { get; set; }

		public bool selected
		{
			set
			{
				if (value)
				{
					m_Button.transition = Selectable.Transition.None;
					m_Button.targetGraphic.color = m_Button.colors.highlightedColor;
				}
				else
				{
					m_Button.transition = Selectable.Transition.ColorTint;
					m_Button.targetGraphic.color = m_OriginalColor;
				}
			}
		}

		public event Action<Transform, Type, string> hovered;
		public event Action<Transform> clicked;

		void Awake()
		{
			m_OriginalColor = m_Button.targetGraphic.color;
		}

		public void SetData(string name, string description)
		{
			m_ButtonTitle.text = name;
			m_ButtonDescription.text = description;
		}

		public void OnRayEnter(RayEventData eventData)
		{
			if (hovered != null)
				hovered(eventData.rayOrigin, toolType, m_ButtonDescription.text);
		}

		public void OnRayExit(RayEventData eventData)
		{
			if (hovered != null)
				hovered(eventData.rayOrigin, null, null);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (clicked != null)
				clicked(null); // Pass null to perform the selection haptic pulse on both nodes
		}
	}
}
#endif
