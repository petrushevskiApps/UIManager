using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TwoOneTwoGames.UIManager.Utilities
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class GridLayoutManager : MonoBehaviour
    {
        [SerializeField]
        private GridLayoutGroup _gridLayoutGroup;
        [SerializeField]
        private List<GridConstraint> _gridConstraintParameters;
        
        private int _childrenCount = 0;

        private void Update()
        {
            if (_childrenCount == transform.childCount)
            {
                return;
            }
            _childrenCount = transform.childCount;
            SetupGrid();
        }

        private void OnValidate()
        {
            SetupGrid();
        }

        private void SetupGrid()
        {
            try
            {
                GridConstraint constraint = _gridConstraintParameters
                    .First(parameter =>
                        parameter.ChildsCount == transform.Cast<Transform>().Count(t => t.gameObject.activeSelf));
                _gridLayoutGroup.constraintCount = constraint.GridCells;
            }
            catch (Exception)
            {
                if (_gridConstraintParameters.Count > 0)
                {
                    _gridLayoutGroup.constraintCount = _gridConstraintParameters.Last().GridCells;
                    return;
                }

                _gridLayoutGroup.constraintCount = 1;
            }
        }
    }

    [Serializable]
    public struct GridConstraint
    {
        public int ChildsCount;
        public int GridCells;
    }
}