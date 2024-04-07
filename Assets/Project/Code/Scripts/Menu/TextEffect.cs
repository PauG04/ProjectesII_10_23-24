using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class TextEffect : MonoBehaviour
{
    private TMP_Text textMesh;

    private void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    private void Update()
    { 
        textMesh.ForceMeshUpdate();
        TMP_TextInfo textInfo = textMesh.textInfo;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; ++j)
            {
                var origin = vertices[charInfo.vertexIndex + j];
                vertices[charInfo.vertexIndex + j] = origin + Wobble(Time.time, origin);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            TMP_MeshInfo meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textMesh.UpdateGeometry(meshInfo.mesh, i);
        }
    }

    private Vector3 Wobble(float time, Vector3 orig)
    {
        return new Vector3(0, Mathf.Sin(time * 2f + orig.x * 0.01f) * 10f, 0);
    }
}
