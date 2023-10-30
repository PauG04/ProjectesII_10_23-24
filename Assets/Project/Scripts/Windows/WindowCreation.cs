using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Windows
{
    public class WindowCreation : MonoBehaviour
    {
        public WindowNode node;

        [Header("Offset")]
        [SerializeField] private float offsetWidth = 0.02f;
        [SerializeField] private float offsetHeight = 0.08f;

        [Header("Child Components")]
        [SerializeField] private Transform windowControl;
        private SpriteRenderer spriteRendererChild;

        private void Awake()
        {
            #region GetComponents
            spriteRendererChild = GetComponentInChildren<SpriteRenderer>();
            #endregion

            #region Start Functions
            RenameObject();
            CreatePrefabInsideWindow();
            ResizeWindowToPrefab();
            SetWindowControlPosition();
            #endregion
        }
        private void RenameObject()
        {
            gameObject.name = node.GetWindowID();
        }
        private void CreatePrefabInsideWindow()
        {
            GameObject prefabInstance = Instantiate(node.GetPrefabChild());
            prefabInstance.transform.parent = transform;
        }
        void ResizeWindowToPrefab()
        {
            node.GetPrefabChild().transform.position = new Vector3(
                node.GetPrefabChild().transform.position.x, 
                node.GetPrefabChild().transform.position.y, 
                transform.position.z
            );

            transform.position = node.GetPrefabChild().transform.position;

            Vector2 newWindowSize = new Vector2(
                node.GetPrefabChild().transform.localScale.x + offsetWidth,
                node.GetPrefabChild().transform.localScale.y + offsetHeight
            );

            spriteRendererChild.size = newWindowSize;
        }
        void SetWindowControlPosition()
        {
            float correctXPos = 0.01f;
            float correctYPos = 0.03f;

            Vector2 newWindowControlPos = new Vector2(
                (spriteRendererChild.size.x / 2) - correctXPos,
                (spriteRendererChild.size.y / 2) + correctYPos
            );

            windowControl.position = newWindowControlPos;
        }
    }
}