using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Control
{
	public class CursorController : MonoBehaviour
	{
		[System.Serializable]
		struct CursorMapping
		{
			public CursorType type;
			public Texture2D texture;
			public Vector2 hotspot;
		}

		[SerializeField] CursorMapping[] cursorMappings = null;
		
		private void SetCursor(CursorType type)
		{
			CursorMapping mapping = GetCursorMapping(type);
			Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
		}
		
		private CursorMapping GetCursorMapping(CursorType type)
		{
			foreach (CursorMapping mapping in cursorMappings)
			{
				if (mapping.type == type)
				{
					return mapping;
				}
			}
			return cursorMappings[0];
		}
		private static Ray GetMouseRay()
		{
			return Camera.main.ScreenPointToRay(Input.mousePosition);
		}
	}

}
