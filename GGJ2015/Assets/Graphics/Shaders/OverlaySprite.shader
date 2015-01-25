Shader "Custom/OverlaySprite" {

   Properties {
	  _MainTex ("Base (RGB)", 2D) = "white" {}      
   }
   SubShader {
   
      Tags {"Queue" = "Transparent"}   
      Pass {	
         Tags { "LightMode" = "ForwardBase" } 
         
         ZWrite Off  
		 Blend DstColor SrcAlpha
		 ZTest Greater
		 Offset 100, 100		 
                
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         #include "UnityCG.cginc"
 
		 uniform sampler2D _MainTex; 
 
         struct vertexInput {
            float4 vertex : POSITION;
		    float4 texcoord : TEXCOORD0;
            fixed4 color : COLOR;
         };
         
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 col : TEXCOORD1;
			float2 texcoord : TEXCOORD0;                        
         };
 
         vertexOutput vert(vertexInput input) 
         {        
            vertexOutput output;
            
            // Pos
            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);         
            
            // Tex coords
            output.texcoord = input.texcoord;
                                                   
    		// Color
            output.col = input.color;
                                                        
            return output;
         }
 
         fixed4 frag(vertexOutput input) : COLOR
         {
            return input.col * tex2D(_MainTex, input.texcoord) ;	
         }
 
         ENDCG
      }
   }
   // The definition of a fallback shader should be commented out 
   // during development:
   // Fallback "Diffuse"
}