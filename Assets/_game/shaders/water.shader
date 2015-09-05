// Shader created with Shader Forge v1.18 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.18;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:32797,y:32708,varname:node_3138,prsc:2|emission-677-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32403,y:32597,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.08965492,c3:1,c4:1;n:type:ShaderForge.SFN_Sin,id:5537,x:31893,y:33033,varname:node_5537,prsc:2|IN-4924-XYZ;n:type:ShaderForge.SFN_FragmentPosition,id:4924,x:31721,y:33033,varname:node_4924,prsc:2;n:type:ShaderForge.SFN_RemapRange,id:677,x:32238,y:33030,varname:node_677,prsc:2,frmn:-1,frmx:1,tomn:-1,tomx:1|IN-5638-OUT;n:type:ShaderForge.SFN_Time,id:7923,x:32008,y:32766,varname:node_7923,prsc:2;n:type:ShaderForge.SFN_Tangent,id:9841,x:31965,y:32610,varname:node_9841,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5638,x:32057,y:33030,varname:node_5638,prsc:2|A-5537-OUT,B-176-OUT;n:type:ShaderForge.SFN_Vector1,id:176,x:31880,y:33195,varname:node_176,prsc:2,v1:0.1;n:type:ShaderForge.SFN_TexCoord,id:1052,x:32008,y:32883,varname:node_1052,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:7347,x:32436,y:32844,varname:node_7347,prsc:2;proporder:7241;pass:END;sub:END;*/

Shader "Shader Forge/water" {
    Properties {
        _Color ("Color", Color) = (0,0.08965492,1,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float3 node_677 = ((sin(i.posWorld.rgb)*0.1)*1.0+0.0);
                float3 emissive = node_677;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
