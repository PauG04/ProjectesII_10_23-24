Shader "Custom/CutShader2D" 
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ClipMask ("Clip Mask", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _ClipMask;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 color = tex2D(_MainTex, i.uv);
                half4 clip = tex2D(_ClipMask, i.uv);

                clip.a = 1.0 - clip.a; // Invert clip mask alpha
                clip.rgb *= clip.a; // Set the clip color to transparent
                
                color *= clip.a; // Apply the clip to the color

                return color;
            }
            ENDCG
        }
    }
}