Shader "Custom/SimpleTexture" {

   Properties {
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
 
         uniform sampler2D _MainTex;
   
         struct vertexInput {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD;
         };
         
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;

            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            output.uv = input.texcoord;
            
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
         	fixed4 col;
         	col.rgba = tex2D(_MainTex, input.uv);//fixed4(255, 0, 0, 255);
            return col;
         }
 
         ENDCG
      }
   }
   // The definition of a fallback shader should be commented out 
   // during development:
   // Fallback "Diffuse"
}