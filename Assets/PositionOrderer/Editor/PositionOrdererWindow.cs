using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PositionOrder {

    public class PositionOrdererWinodw : EditorWindow {

        private enum OrderType { Line, Table, Cube }

        /* Position Orderer values */
        private PositionOrderer orderer = new PositionOrderer ();

        private OrderType orderType;
        private Axis2D axis2D;

        private LineAnchor lineAnchor;
        private TableAnchor tableAnchor;
        private CubeAnchor cubeAnchor;

        private int customIndex;
        private int colCount;
        private int rowCount;

        private int selectedIndex = -1; //Selected index of transform list
        private bool isUpdate = false; //Is position updating

        private static float minHeight = 336; //Winodw minimum height

        private Vector2 startPos = new Vector2 (0, 0); //Scroll view start position
        private float defaultContentHeight = 156; //Default content height without scrollview
        private float contentHeightWithoutList = 156; //Scroll view height fitter

        //Initialize
        [MenuItem ("Window/Position Orderer")]
        private static void Init () {
            PositionOrdererWinodw window = GetWindow (typeof (PositionOrdererWinodw), true, "Position Orderer") as PositionOrdererWinodw;
            window.minSize = new Vector2 (300, minHeight);
            window.Show ();
        }

        private void OnSelectionChange () {
            //Set custom index by selected object
            if (Selection.activeGameObject != null) {

                if (orderer.Transforms.Contains (Selection.activeGameObject.transform)) {
                    customIndex = orderer.Transforms.IndexOf (Selection.activeGameObject.transform);
                    selectedIndex = customIndex;
                    Repaint (); //UI Repaint
                }
            }
        }

        private void OnGUI () {

            #region Variables

            //Create default values
            float scrollSizeX = position.width - startPos.x;
            float scrollSizeY = position.height - contentHeightWithoutList;

            Color color_default = GUI.backgroundColor;
            Color color_selected = new Color (0.85f, 0.85f, 0.85f);

            //transform list item style
            GUIStyle itemStyle = new GUIStyle (GUI.skin.button);
            itemStyle.alignment = TextAnchor.MiddleLeft;
            itemStyle.active.background = itemStyle.normal.background;
            itemStyle.margin = new RectOffset (0, 0, 0, 0);

            //Bold font style
            GUIStyle boldStyle = new GUIStyle (GUI.skin.label);
            boldStyle.fontStyle = FontStyle.Bold;

            #endregion

            #region Object List Management

            //Draw transform list background
            GUI.DrawTexture (new Rect (0, 20, scrollSizeX, scrollSizeY), MakeTex ((int)scrollSizeX, (int)scrollSizeY, color_selected));

            //Draw transform list label
            GUIContent transformListLabel = new GUIContent ("Transform List", "Select objects and click Add button to add transforms into list");
            GUILayout.Label (transformListLabel, boldStyle);

            //Begin transform list
            startPos = EditorGUILayout.BeginScrollView (startPos, GUILayout.Width (scrollSizeX), GUILayout.Height (scrollSizeY));

            //Draw transform list items
            for (int i = 0; i < orderer.Transforms.Count; i++) {
                GUI.backgroundColor = (selectedIndex == i) ? color_selected : Color.clear;

                if (orderer.Transforms[i] != null) {
                    if (GUILayout.Button ($"[{i}] {orderer.Transforms[i].name}", itemStyle)) {
                        selectedIndex = i;
                        Selection.activeGameObject = orderer.Transforms[i].gameObject; //Select object in hierarchy
                    }

                } else {
                    orderer.Transforms.RemoveAt (i);
                }

                GUI.backgroundColor = color_default;
            }

            EditorGUILayout.EndScrollView ();

            //Draw transform list buttons
            EditorGUILayout.BeginHorizontal ();

            if (GUILayout.Button ("Add")) {
                isUpdate = false;

                //Sort transform list
                GameObject[] objs = Selection.gameObjects;
                Array.Sort (objs, new UnityTransformSort ());

                //Add transforms in to orderer list
                foreach (var obj in objs) {
                    if (!orderer.Transforms.Contains (obj.transform)) {
                        orderer.Transforms.Add (obj.transform);
                    }
                }
            }

            if (GUILayout.Button ("Remove")) { //Remove selected item
                if (selectedIndex >= 0 && selectedIndex < orderer.Transforms.Count) {
                    isUpdate = false;
                    orderer.Transforms.RemoveAt (selectedIndex);
                    selectedIndex -= 1;
                }
            }

            if (GUILayout.Button ("Clear")) { //Clear list
                isUpdate = false;
                orderer.Transforms.Clear ();
                selectedIndex = -1;
            }

            EditorGUILayout.EndHorizontal ();

            #endregion

            #region Draw Order Type Elements

            GUILayout.Space (18f);

            //Draw order type enum popup
            DrawLabelElement ("Type", 50, () => SetOrderType ((OrderType)EditorGUILayout.EnumPopup (orderType)));

            if (orderType == OrderType.Line) { //Draw line type anchor and element
                DrawLabelElement ("Anchor", 50, () => SetLineAnchor ((LineAnchor)EditorGUILayout.EnumPopup (lineAnchor)));

                if (lineAnchor == LineAnchor.Custom) { //Draw custom index int field
                    DrawLabelElement ("Index", 50, () => customIndex = EditorGUILayout.IntField (customIndex));
                }

            } else if (orderType == OrderType.Table) { //Draw table type anchor and element
                DrawLabelElement ("Anchor", 50, () => SetTableAnchor ((TableAnchor)EditorGUILayout.EnumPopup (tableAnchor)));
                DrawLabelElement ("Axis", 50, () => axis2D = (Axis2D)EditorGUILayout.EnumPopup (axis2D));

                if (tableAnchor == TableAnchor.Custom) { //Draw custom index int field
                    DrawLabelElement ("Index", 50, () => customIndex = EditorGUILayout.IntField (customIndex));
                }

                DrawLabelElement ("Column", 50, () => colCount = EditorGUILayout.IntField (colCount));

            } else if (orderType == OrderType.Cube) { //Draw cube type anchor and element
                DrawLabelElement ("Anchor", 50, () => SetCubeAnchor ((CubeAnchor)EditorGUILayout.EnumPopup (cubeAnchor)));

                if (cubeAnchor == CubeAnchor.Custom) { //Draw custom index int field
                    DrawLabelElement ("Index", 50, () => customIndex = EditorGUILayout.IntField (customIndex));
                }

                //Draw column and row int fields
                EditorGUILayout.BeginHorizontal ();

                DrawLabelElement ("Column", 50, () => colCount = EditorGUILayout.IntField (colCount));
                DrawLabelElement ("Row", 30, () => rowCount = EditorGUILayout.IntField (rowCount));

                EditorGUILayout.EndHorizontal ();
            }

            #endregion

            #region Apply Settings

            GUILayout.Space (18f);

            if (isUpdate) { //If it's update mode, draw float fields with drag hot zone
                if (orderType == OrderType.Line) {
                    DrawFloatFields (true, true, true);

                } else if (orderType == OrderType.Table) {
                    switch (axis2D) {
                        case Axis2D.XY:
                            DrawFloatFields (true, true, false);
                            break;
                        case Axis2D.XZ:
                            DrawFloatFields (true, false, true);
                            break;
                        case Axis2D.ZY:
                            DrawFloatFields (false, true, true);
                            break;
                    }

                } else if (orderType == OrderType.Cube) {
                    DrawFloatFields (true, true, true);
                }

            } else { //If it's not update mode, draw float fields with fixed label
                EditorGUILayout.BeginHorizontal ();

                if (orderType == OrderType.Line) {
                    DrawFloatFieldsWithFixedLabel (true, true, true);

                } else if (orderType == OrderType.Table) {
                    switch (axis2D) {
                        case Axis2D.XY:
                            DrawFloatFieldsWithFixedLabel (true, true, false);
                            break;
                        case Axis2D.XZ:
                            DrawFloatFieldsWithFixedLabel (true, false, true);
                            break;
                        case Axis2D.ZY:
                            DrawFloatFieldsWithFixedLabel (false, true, true);
                            break;
                    }

                } else if (orderType == OrderType.Cube) {
                    DrawFloatFieldsWithFixedLabel (true, true, true);
                }

                EditorGUILayout.EndHorizontal ();
            }

            //Draw apply buttons
            EditorGUILayout.BeginHorizontal ();

            if (GUILayout.Button ("Update")) { //Update button
                if (orderer.Transforms.Count >= PositionOrderer.MIN_COUNT) {
                    isUpdate = true;
                    SetContentHeight ();

                } else {
                    Debug.LogWarning ("Transform count in list is too small.");
                }
            }

            if (isUpdate) { //If it's update mode, draw stop button
                if (GUILayout.Button ("Stop")) {
                    isUpdate = false;
                    SetContentHeight ();
                }

            } else { //If it's not update mode, draw apply button
                if (GUILayout.Button ("Apply")) {
                    if (orderer.Transforms.Count >= PositionOrderer.MIN_COUNT) {
                        ApplyOrder ();

                    } else {
                        Debug.LogWarning ("Transform count in list is too small.");
                    }
                }
            }

            EditorGUILayout.EndHorizontal ();

            #endregion
        }

        #region Draw Element

        //Draw float fields with drag hot zone
        private void DrawFloatFields (bool x, bool y, bool z) {
            if (x)
                SetDistX (EditorGUILayout.FloatField ("X", orderer.Distance_X));
            if (y)
                SetDistY (EditorGUILayout.FloatField ("Y", orderer.Distance_Y));
            if (z)
                SetDistZ (EditorGUILayout.FloatField ("Z", orderer.Distance_Z));
        }

        //Draw float fields with fixed label
        private void DrawFloatFieldsWithFixedLabel (bool x, bool y, bool z) {
            if (x) {
                GUILayout.Label ("X", GUILayout.Width (15));
                SetDistX (EditorGUILayout.FloatField (orderer.Distance_X));
            }

            if (y) {
                GUILayout.Label ("Y", GUILayout.Width (15));
                SetDistY (EditorGUILayout.FloatField (orderer.Distance_Y));
            }

            if (z) {
                GUILayout.Label ("Z", GUILayout.Width (15));
                SetDistZ (EditorGUILayout.FloatField (orderer.Distance_Z));
            }
        }

        //Draw element with fixed label
        private void DrawLabelElement (string label, float width, Action draw) {
            if (draw == null)
                return;

            EditorGUILayout.BeginHorizontal ();
            GUILayout.Label (label, GUILayout.Width (width));
            draw ();
            EditorGUILayout.EndHorizontal ();
        }

        #endregion

        #region Setter

        //Set order type and update content height
        private void SetOrderType (OrderType type) {
            this.orderType = type;
            SetContentHeight ();
        }

        //Set line anchor type and update content height
        private void SetLineAnchor (LineAnchor anchor) {
            this.lineAnchor = anchor;
            SetContentHeight ();
        }

        //Set table anchor type and update content height
        private void SetTableAnchor (TableAnchor anchor) {
            this.tableAnchor = anchor;
            SetContentHeight ();
        }

        //Set cube anchor type and update content height
        private void SetCubeAnchor (CubeAnchor anchor) {
            this.cubeAnchor = anchor;
            SetContentHeight ();
        }

        //Set Distance_X and uptdate apply if it's update mode
        private void SetDistX (float value) {
            orderer.Distance_X = value;
            if (isUpdate) {
                ApplyOrder ();
            }
        }

        //Set Distance_Y and uptdate apply if it's update mode
        private void SetDistY (float value) {
            orderer.Distance_Y = value;
            if (isUpdate) {
                ApplyOrder ();
            }
        }

        //Set Distance_Z and uptdate apply if it's update mode
        private void SetDistZ (float value) {
            orderer.Distance_Z = value;
            if (isUpdate) {
                ApplyOrder ();
            }
        }

        //Set content height without scrollview and winode minimum height
        private void SetContentHeight () {
            float value = 0;

            if (!isUpdate) {
                if (orderType == OrderType.Line) {
                    if (lineAnchor == LineAnchor.Custom)
                        value += 18;

                } else if (orderType == OrderType.Table) {
                    value += 36;
                    if (tableAnchor == TableAnchor.Custom)
                        value += 18;

                } else if (orderType == OrderType.Cube) {
                    value += 18;
                    if (cubeAnchor == CubeAnchor.Custom)
                        value += 18;
                }

            } else {
                if (orderType == OrderType.Line) {
                    value += 36;
                    if (lineAnchor == LineAnchor.Custom)
                        value += 18;

                } else if (orderType == OrderType.Table) {
                    value += 54;
                    if (tableAnchor == TableAnchor.Custom)
                        value += 18;

                } else if (orderType == OrderType.Cube) {
                    value += 54;
                    if (cubeAnchor == CubeAnchor.Custom)
                        value += 18;
                }
            }

            contentHeightWithoutList = defaultContentHeight + value;
            minSize = new Vector2 (300, minHeight + value);
        }

        #endregion

        //Apply position order
        private void ApplyOrder () {
            if (orderType == OrderType.Line) { //Apply line order
                orderer.ApplyLineOrder (lineAnchor, customIndex);

            } else if (orderType == OrderType.Table) { //Apply table order
                orderer.ApplyTableOrder (tableAnchor, axis2D, colCount, customIndex);

            } else if (orderType == OrderType.Cube) { //Apply cube order
                orderer.ApplyCubeOrder (cubeAnchor, colCount, rowCount, customIndex);
            }
        }

        //Get background texture
        private Texture2D MakeTex (int width, int height, Color col) {
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;

            Texture2D result = new Texture2D (width, height);
            result.SetPixels (pix);
            result.Apply ();

            return result;
        }
    }

    //Transform list sorting object
    public class UnityTransformSort : IComparer<GameObject> {
        public int Compare (GameObject lhs, GameObject rhs) {
            if (lhs == rhs) 
                return 0;
            if (lhs == null) 
                return -1;
            if (rhs == null)
                return 1;
            return (lhs.transform.GetSiblingIndex () > rhs.transform.GetSiblingIndex ()) ? 1 : -1;
        }
    }
}
