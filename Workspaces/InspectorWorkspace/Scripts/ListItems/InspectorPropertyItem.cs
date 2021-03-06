using System;
using TMPro;
using UnityEditor.Experimental.EditorVR.Data;
using UnityEngine;

namespace UnityEditor.Experimental.EditorVR.Workspaces
{
    abstract class InspectorPropertyItem : InspectorListItem
    {
        [SerializeField]
        TextMeshProUGUI m_Label;

        public Transform tooltipTarget
        {
            get { return m_TooltipTarget; }
        }

        [SerializeField]
        Transform m_TooltipTarget;

        public Transform tooltipSource
        {
            get { return m_TooltipSource; }
        }

        [SerializeField]
        Transform m_TooltipSource;

        public TextAlignment tooltipAlignment
        {
            get { return TextAlignment.Right; }
        }

        public Action<ITooltip> showTooltip { get; set; }
        public Action<ITooltip> hideTooltip { get; set; }

        public string tooltipText
        {
#if UNITY_EDITOR
            get { return m_SerializedProperty.tooltip; }
#else
            get { return string.Empty; }
#endif
        }

        protected SerializedProperty m_SerializedProperty;

        public override void Setup(InspectorData data)
        {
            base.Setup(data);

            m_SerializedProperty = ((PropertyData)data).property;

#if UNITY_EDITOR
            m_Label.text = m_SerializedProperty.displayName;
#endif
        }

        public override void OnObjectModified()
        {
            base.OnObjectModified();

#if UNITY_EDITOR
            m_SerializedProperty = data.serializedObject.FindProperty(m_SerializedProperty.propertyPath);
#endif
        }

        protected void FinalizeModifications()
        {
#if UNITY_EDITOR
            Undo.IncrementCurrentGroup();
            data.serializedObject.ApplyModifiedProperties();
#endif
        }
    }
}
