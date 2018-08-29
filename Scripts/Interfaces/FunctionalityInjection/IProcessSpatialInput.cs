﻿#if UNITY_EDITOR
using System;
using UnityEditor.Experimental.EditorVR.Modules;
using UnityEngine;

namespace UnityEditor.Experimental.EditorVR
{
    /// <summary>
    /// Gives decorated class ability to detect the current spatial-input for a given node
    /// Spatial UI & UX can/should respond, based on a given node's spatial input type:
    /// (translation, single axis rotation, free rotation, etc)
    /// </summary>
    public interface IProcessSpatialInput : ICustomActionMap
    {
        /// <summary>
        /// The data defining a spatial scroll operation
        /// </summary>
        SpatialInputModule.SpatialScrollData spatialScrollData { get; set; }

        // Func that takes a node, and returns the current TEMPORAL spatial input type detected for that node
        //Func<Node, SpatialInputType> getSpatialInputTypeForNode { get; set; }

        bool pollingSpatialInputType { get; set; }
    }

    public static class IProcessSpatialInputTypeMethods
    {
        internal delegate SpatialInputModule.SpatialScrollData PerformLocalCardinallyConstrainedSpatialScrollDelegate(IProcessSpatialInput caller, SpatialInputModule.SpatialCardinalScrollDirection cardinalScrollDirection, Node node, Vector3 startingPosition,
            Vector3 currentPosition, float scrollLengthRange, SpatialInputModule.ScrollRepeatType scrollRepeatType, int scrollableItemCount, int maxItemCount = -1, bool centerVisuals = true);

        internal static PerformLocalCardinallyConstrainedSpatialScrollDelegate performLocalCardinallyConstrainedSpatialScroll { private get; set; }

        /// <summary>
        /// Perform a spatial scroll action
        /// </summary>
        /// "obj" : The object requesting the performance of a spatial scroll action
        /// <param name="node">The node on which to display & perform the spatial scroll</param>
        /// <param name="startingPosition">The initial position of the spatial scroll</param>
        /// <param name="currentPosition">The current/updated position of the spatial scroll</param>
        /// <param name="scrollLengthRange">The length at which a scroll action will return a repeating/looping value</param>
        /// <param name="scrollableItemCount">The number of items being scrolled through with this action</param>
        /// <param name="maxItemCount">The maximum number of items that can be scrolled through for this action</param>
        /// <param name="centerVisuals">If true, expand the scroll line visuals outward in both directions from the scroll start position</param>
        /// <returns>The spatial scroll data for a single scroll action, but an individual caller object</returns>
        public static SpatialInputModule.SpatialScrollData PerformLocalCardinallyConstrainedSpatialScroll(this IProcessSpatialInput obj, SpatialInputModule.SpatialCardinalScrollDirection cardinalScrollDirection, Node node,
            Vector3 startingPosition, Vector3 currentPosition, float scrollLengthRange, SpatialInputModule.ScrollRepeatType scrollRepeatType, int scrollableItemCount, int maxItemCount = -1, bool centerVisuals = true)
        {
            return performLocalCardinallyConstrainedSpatialScroll(obj, cardinalScrollDirection, node, startingPosition, currentPosition, scrollLengthRange, scrollRepeatType, scrollableItemCount, maxItemCount, centerVisuals);
        }

        // Below are previous implementations

        internal delegate SpatialInputModule.SpatialScrollData PerformOriginalSpatialScrollDelegate(IProcessSpatialInput caller, Node node, Vector3 startingPosition,
            Vector3 currentPosition, float repeatingScrollLengthRange, int scrollableItemCount, int maxItemCount = -1, bool centerVisuals = true);

        internal static PerformSpatialScrollDelegate performOriginalSpatialScroll { private get; set; }

        /// <summary>
        /// Perform a spatial scroll action
        /// </summary>
        /// "obj" : The object requesting the performance of a spatial scroll action
        /// <param name="node">The node on which to display & perform the spatial scroll</param>
        /// <param name="startingPosition">The initial position of the spatial scroll</param>
        /// <param name="currentPosition">The current/updated position of the spatial scroll</param>
        /// <param name="scrollLengthRange">The length at which a scroll action will return a repeating/looping value</param>
        /// <param name="scrollableItemCount">The number of items being scrolled through with this action</param>
        /// <param name="maxItemCount">The maximum number of items that can be scrolled through for this action</param>
        /// <param name="centerVisuals">If true, expand the scroll line visuals outward in both directions from the scroll start position</param>
        /// <returns>The spatial scroll data for a single scroll action, but an individual caller object</returns>
        public static SpatialInputModule.SpatialScrollData PerformOriginalSpatialScroll(this IProcessSpatialInput obj, Node node,
            Vector3 startingPosition, Vector3 currentPosition, float scrollLengthRange, SpatialInputModule.ScrollRepeatType scrollRepeatType, int scrollableItemCount, int maxItemCount = -1, bool centerVisuals = true)
        {
            return performOriginalSpatialScroll(obj, node, startingPosition, currentPosition, scrollLengthRange, scrollRepeatType, scrollableItemCount, maxItemCount, centerVisuals);
        }

        internal delegate SpatialInputModule.SpatialScrollData PerformSpatialScrollDelegate(IProcessSpatialInput caller, Node node, Vector3 startingPosition,
           Vector3 currentPosition, float scrollLengthRange, SpatialInputModule.ScrollRepeatType scrollRepeatType, int scrollableItemCount, int maxItemCount = -1, bool centerVisuals = true);

        internal static PerformSpatialScrollDelegate performSpatialScroll { private get; set; }

        /// <summary>
        /// Perform a spatial scroll action
        /// </summary>
        /// "obj" : The object requesting the performance of a spatial scroll action
        /// <param name="node">The node on which to display & perform the spatial scroll</param>
        /// <param name="startingPosition">The initial position of the spatial scroll</param>
        /// <param name="currentPosition">The current/updated position of the spatial scroll</param>
        /// <param name="scrollLengthRange">The length at which a scroll action will return a repeating/looping value</param>
        /// <param name="scrollableItemCount">The number of items being scrolled through with this action</param>
        /// <param name="maxItemCount">The maximum number of items that can be scrolled through for this action</param>
        /// <param name="centerVisuals">If true, expand the scroll line visuals outward in both directions from the scroll start position</param>
        /// <returns>The spatial scroll data for a single scroll action, but an individual caller object</returns>
        public static SpatialInputModule.SpatialScrollData PerformSpatialScroll(this IProcessSpatialInput obj, Node node,
            Vector3 startingPosition, Vector3 currentPosition, float scrollLengthRange, SpatialInputModule.ScrollRepeatType scrollRepeatType, int scrollableItemCount, int maxItemCount = -1, bool centerVisuals = true)
        {
            return performSpatialScroll(obj, node, startingPosition, currentPosition, scrollLengthRange, scrollRepeatType, scrollableItemCount, maxItemCount, centerVisuals);
        }

        internal delegate SpatialInputType GetSpatialInputTypeForNodeDelegate(IDetectSpatialInputType caller, Node node);
        internal static GetSpatialInputTypeForNodeDelegate getSpatialInputTypeForNode { private get; set; }

        internal static Action<IProcessSpatialInput> endSpatialScroll { private get; set; }
        /// <summary>
        /// End a spatial scrolling action for a given caller
        /// </summary>
        /// "obj" : The caller whose spatial scroll action will be ended
        public static void EndSpatialScroll(this IProcessSpatialInput obj)
        {
            endSpatialScroll(obj);
        }

        /// <summary>
        /// Detect the active/current spatial input type of a given node
        /// </summary>
        /// "obj" : The caller polling for a node's spatial input type
        /// "node" : The node whose spatial input type will be returned
        public static SpatialInputType GetSpatialInputTypeForNode(this IDetectSpatialInputType obj, Node node)
        {
            return getSpatialInputTypeForNode(obj, node);
        }
    }
}
#endif