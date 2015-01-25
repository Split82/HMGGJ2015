Shader "Custom/Color" {

   Properties {
      _Color ("Diffuse Material Color", Color) = (1,1,1,1) 
	  _MainTex ("Base (RGB)", 2D) = "white" {}         
   }
   SubShader {
   
      Tags {"Queue" = "Transparent"}   
      Pass {	
         Tags { "LightMode" = "ForwardBase" } 
         
         ZWrite Off  
         Blend SrcAlpha OneMinusSrcAlpha  
         Cull Off
           
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         #include "UnityCG.cginc"
 
         uniform float4 _Color;
         uniform sampler2D _MainTex;
   
         struct vertexInput {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD;
         };
         
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
            half4 col : COLOR;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;

            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            output.col = _Color;
            output.uv = input.texcoord;
            
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
         	fixed4 col;
         	col.rgb = input.col.rgb;
         	col.a = tex2D(_MainTex, input.uv).a;
            return col;
         }
 
         ENDCG
      }
   }
   // The definition of a fallback shader should be commented out 
   // during development:
   // Fallback "Diffuse"
}