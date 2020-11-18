using System.Collections.Generic;
using UnityEngine;

namespace PositionOrder {

    public enum Axis2D { XY, XZ, ZY }

    public enum LineAnchor { Start, Center, End, Custom }
    public enum TableAnchor { TopLeft, TopMiddle, TopRight, MiddleLeft, Center, MiddleRight, BottomLeft, BottomMiddle, BottomRight, Custom }
    public enum CubeAnchor { Up, Forward, Left, Center, Right, Back, Down, Custom }

    public class PositionOrderer {

        private enum WarningRequest { TransformOnly, Column, ColumnAndRow }

        //Distance properties
        public float Distance_X { get; set; }
        public float Distance_Y { get; set; }
        public float Distance_Z { get; set; }

        public List<Transform> Transforms { get; set; } //Transform list

        public const int MIN_COUNT = 2; //Minimum count of objects, column and row

        //Initialize
        public PositionOrderer () {
            Transforms = new List<Transform> ();
        }

        #region Apply

        //Apply line order
        public void ApplyLineOrder (LineAnchor anchor, int customIndex = 0) {

            //Stop if count is unsafe
            if (!IsCountSafe (WarningRequest.TransformOnly)) {
                return;
            }

            int idx = 0;
            if (anchor == LineAnchor.Custom) { //Set index to custom index
                if (!TrySetCustomIndex (customIndex, out idx)) {
                    return;
                }

            } else { //Get index form line anchor
                idx = GetStandardIndexFromLineAnchor (anchor);
            }

            int count = Transforms.Count;

            Vector3 startPos = Transforms[idx].position; //Standard position
            Vector3 dist = Vector3.zero; //Distance

            //Set transform positions
            for (int i = 0; i < count; i++) {
                dist.Set ((i - idx) * Distance_X, (i - idx) * Distance_Y, (i - idx) * Distance_Z);
                Transforms[i].position = startPos + dist;
            }
        }

        //Apply table order
        public void ApplyTableOrder (TableAnchor anchor, Axis2D axis, int col, int customIndex = 0) {

            //Stop if count is unsafe
            if (!IsCountSafe (WarningRequest.Column, col)) {
                return;
            }

            int idx = 0;
            if (anchor == TableAnchor.Custom) { //Set index to custom index
                if (!TrySetCustomIndex (customIndex, out idx)) {
                    return;
                }

            } else { //Get index from line anchor
                idx = GetStandardIndexFromTableAnchor (anchor, col);
            }

            //Default settings
            int start_col = idx % col;
            int start_row = idx / col;
            int curr_col, curr_row;

            Vector3 startPos = Transforms[idx].position; //Standard position
            Vector3 dist = Vector3.zero; //Distance      

            //Set transform positions
            for (int i = 0; i < Transforms.Count; i++) {
                curr_col = i % col;
                curr_row = i / col;
                SetDistanceByAxis2D (ref dist, axis, curr_col - start_col, start_row - curr_row);
                Transforms[i].position = startPos + dist;
            }
        }

        //Set distance vector3 by axis
        private void SetDistanceByAxis2D (ref Vector3 vector, Axis2D axis, float col, float row) {
            switch (axis) {
                case Axis2D.XY:
                    vector.Set (col * Distance_X, row * Distance_Y, 0);
                    break;
                case Axis2D.XZ:
                    vector.Set (col * Distance_X, 0, row * Distance_Z);
                    break;
                case Axis2D.ZY:
                    vector.Set (0, row * Distance_Y, col * Distance_Z);
                    break;
            }
        }

        //Apply cube order
        public void ApplyCubeOrder (CubeAnchor anchor, int col, int row, int customIndex = 0) {

            //Stop if count is unsafe
            if (!IsCountSafe (WarningRequest.ColumnAndRow, col, row)) {
                return;
            }

            int idx = 0;
            if (anchor == CubeAnchor.Custom) { //Set index to custom index
                if (!TrySetCustomIndex (customIndex, out idx)) {
                    return;
                }

            } else { //Get index from line anchor
                idx = GetStandardIndexFromCubeAnchor (anchor, col, row);
            }

            //Default settings
            int floor_count = col * row;
            int start_height = idx / floor_count;
            int start_col = idx % col;
            int start_row = (idx % floor_count) / col;
            int curr_col, curr_row, curr_height;

            Vector3 startPos = Transforms[idx].position; //Standard position
            Vector3 dist = Vector3.zero; //Distance      

            //Set transform positions
            for (int i = 0; i < Transforms.Count; i++) {
                curr_height = i / floor_count;
                curr_col = i % col;
                curr_row = (i % floor_count) / col;
                dist.Set ((curr_col - start_col) * Distance_X, (start_height - curr_height) * Distance_Y, (start_row - curr_row) * Distance_Z);
                Transforms[i].position = startPos + dist;
            }
        }

        #endregion

        #region Common

        private bool IsCountSafe (WarningRequest warning, int col = 0, int row = 0) {

            //Return false if transform list count is less than minimum count
            if (Transforms.Count < MIN_COUNT) {
                Debug.LogWarning ("Transform count in list is too small.");
                return false;
            }

            if (warning == WarningRequest.Column || warning == WarningRequest.ColumnAndRow) {

                //Return false if column is less than minimum count
                if (col < MIN_COUNT) {
                    Debug.LogWarning ("Column count is too small.");
                    return false;
                }

                if (warning == WarningRequest.ColumnAndRow) {
                    //Return false if row is less than minimum count
                    if (row < MIN_COUNT) {
                        Debug.LogWarning ("Row count is too small.");
                        return false;
                    }
                }
            }

            return true; //This count is safe
        }

        private bool TrySetCustomIndex (int customIndex, out int result) {
            if (customIndex >= 0 && customIndex < Transforms.Count) { //Return true and result if index is safe
                result = customIndex;
                return true;

            } else { //Return false if index is out of range
                Debug.LogError ("Custom index out of range: " + customIndex);
                result = -1;
                return false;
            }
        }

        #endregion

        #region Get Standard Index

        //Get index from line anchor
        private int GetStandardIndexFromLineAnchor (LineAnchor anchor) {
            int count = Transforms.Count;

            switch (anchor) {
                case LineAnchor.Start:
                    return 0;
                case LineAnchor.Center:
                    return count / 2;
                case LineAnchor.End:
                    return count - 1;
                default:
                    return -1;
            }
        }

        //Get index from table anchor
        private int GetStandardIndexFromTableAnchor (TableAnchor anchor, int col) {
            int count = Transforms.Count;
            int row = Mathf.CeilToInt ((float)count / col);

            switch (anchor) {
                case TableAnchor.TopLeft:
                    return 0;
                case TableAnchor.TopMiddle:
                    return col / 2;
                case TableAnchor.TopRight:
                    return col - 1;
                case TableAnchor.MiddleLeft:
                    return col * (row / 2);
                case TableAnchor.Center:
                    return count / 2;
                case TableAnchor.MiddleRight:
                    return (col * (row / 2)) + (col - 1);
                case TableAnchor.BottomLeft:
                    return row * (col - 1);
                case TableAnchor.BottomMiddle:
                    return (row * (col - 1)) + (row / 2);
                case TableAnchor.BottomRight:
                    return count - 1;
                default:
                    return -1;
            }
        }

        //Get index from cube anchor
        private int GetStandardIndexFromCubeAnchor (CubeAnchor anchor, int col, int row) {
            int count = Transforms.Count;
            int floor_count = col * row;
            int mid_height_idx = floor_count * (Mathf.CeilToInt ((float)count / floor_count) / 2);
            int mid_row_idx = floor_count / col;

            switch (anchor) {
                case CubeAnchor.Up:
                    return floor_count / 2;
                case CubeAnchor.Forward:
                    return mid_height_idx + (col / 2);
                case CubeAnchor.Left:
                    return mid_height_idx + mid_row_idx;
                case CubeAnchor.Center:
                    return count / 2;
                case CubeAnchor.Right:
                    return mid_height_idx + mid_row_idx + (col - 1);
                case CubeAnchor.Back:
                    return mid_height_idx + (floor_count - ((col / 2) + 1));
                case CubeAnchor.Down:
                    return (count - 1) - (floor_count / 2);
                default:
                    return -1;
            }
        }

        #endregion
    }
}
