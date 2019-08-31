﻿using System;
using System.Collections.Generic;
using System.Text;

namespace jp.nyatla.nyartoolkit.cs.core
{
    /**
     * 視錐台と、これを使った演算関数を定義します。
     * @author nyatla
     *
     */
    public class NyARFrustum
    {
	    /** frastum行列*/
	    protected NyARDoubleMatrix44 _frustum_rh=new NyARDoubleMatrix44();
	    /** frastum逆行列*/
	    protected NyARDoubleMatrix44 _inv_frustum_rh=new NyARDoubleMatrix44();	
	    protected NyARIntSize _screen_size=new NyARIntSize();
	    /**
	     * コンストラクタです。ARToolkitの射影変換行列から、インスタンスを作ります。
	     * @param i_projection
	     * @param i_width
	     * スクリーンサイズです。
	     * @param i_height
	     * スクリーンサイズです。
	     * @param i_near
	     * 近平面までの距離です。単位はmm
	     * @param i_far
	     * 遠平面までの距離です。単位はmm
	     */
	    public NyARFrustum(NyARPerspectiveProjectionMatrix i_projection,int i_width,int i_height,double i_near,double i_far)
	    {
		    this.setValue(i_projection, i_width, i_height, i_near, i_far);
	    }
	    /**
	     * ARToolKitスタイルの射影変換行列から、視錐台をセットします。
	     * @param i_projection
	     * @param i_width
	     * @param i_height
	     * @param i_near
	     * nearポイントをmm単位で指定します。
	     * @param i_far
	     * farポイントをmm単位で指定します。
	     */
	    public void setValue(NyARPerspectiveProjectionMatrix i_projection,int i_width,int i_height,double i_near,double i_far)
	    {
		    i_projection.makeCameraFrustumRH(i_width, i_height, i_near, i_far,this._frustum_rh);
		    this._inv_frustum_rh.inverse(this._frustum_rh);
		    this._screen_size.setValue(i_width,i_height);
	    }
	    /**
	     * 画像上の座標を、撮像点座標に変換します。
	     * この座標は、カメラ座標系です。
	     * @param ix
	     * 画像上の座標
	     * @param iy
	     * 画像上の座標
	     * @param o_point_on_screen
	     * 撮像点座標
	     * <p>
	     * この関数は、gluUnprojectのビューポートとモデルビュー行列を固定したものです。
	     * 公式は、以下の物使用。
	     * http://www.opengl.org/sdk/docs/man/xhtml/gluUnProject.xml
	     * ARToolKitの座標系に合せて計算するため、OpenGLのunProjectとはix,iyの与え方が違います。画面上の座標をそのまま与えてください。
	     * </p>
	     */
	    public void unProject(double ix,double iy,NyARDoublePoint3d o_point_on_screen)
	    {
		    double n=(this._frustum_rh.m23/(this._frustum_rh.m22-1));
		    NyARDoubleMatrix44 m44=this._inv_frustum_rh;
		    double v1=(this._screen_size.w-ix-1)*2/this._screen_size.w-1.0;//ARToolKitのFrustramに合せてる。
		    double v2=(this._screen_size.h-iy-1)*2/this._screen_size.h-1.0;
		    double v3=2*n-1.0;
		    double b=1/(m44.m30*v1+m44.m31*v2+m44.m32*v3+m44.m33);
		    o_point_on_screen.x=(m44.m00*v1+m44.m01*v2+m44.m02*v3+m44.m03)*b;
		    o_point_on_screen.y=(m44.m10*v1+m44.m11*v2+m44.m12*v3+m44.m13)*b;
		    o_point_on_screen.z=(m44.m20*v1+m44.m21*v2+m44.m22*v3+m44.m23)*b;
		    return;
	    }
	    /**
	     * 画面上の点と原点を結ぶ直線と任意姿勢の平面の交差点を、カメラの座標系で取得します。
	     * この座標は、カメラ座標系です。
	     * @param ix
	     * @param iy
	     * @param i_mat
	     * 平面の姿勢行列です。
	     * @param o_pos
	     */
	    public void unProjectOnCamera(double ix,double iy,NyARDoubleMatrix44 i_mat,NyARDoublePoint3d o_pos)
	    {
		    //画面→撮像点
		    this.unProject(ix,iy,o_pos);
		    //撮像点→カメラ座標系
		    double nx=i_mat.m02;
		    double ny=i_mat.m12;
		    double nz=i_mat.m22;
		    double mx=i_mat.m03;
		    double my=i_mat.m13;
		    double mz=i_mat.m23;
		    double t=(nx*mx+ny*my+nz*mz)/(nx*o_pos.x+ny*o_pos.y+nz*o_pos.z);
		    o_pos.x=t*o_pos.x;
		    o_pos.y=t*o_pos.y;
		    o_pos.z=t*o_pos.z;
	    }	
	    /**
	     * 画面上の点と原点を結ぶ直線と任意姿勢の平面の交差点を、平面の座標系で取得します。
	     * ARToolKitの本P175周辺の実装と同じです。
	     * @param ix
	     * @param iy
	     * @param i_mat
	     * 平面の姿勢行列です。
	     * @param o_pos
	     * @return
	     * <p>
	     * このAPIは繰り返し使用には最適化されていません。同一なi_matに繰り返しアクセスするときは、展開してください。
	     * </p>
	     */
	    public bool unProjectOnMatrix(double ix,double iy,NyARDoubleMatrix44 i_mat,NyARDoublePoint3d o_pos)
	    {
		    //交点をカメラ座標系で計算
		    unProjectOnCamera(ix,iy,i_mat,o_pos);
		    //座標系の変換
		    NyARDoubleMatrix44 m=new NyARDoubleMatrix44();
		    if(!m.inverse(i_mat)){
			    return false;
		    }
		    m.transform3d(o_pos, o_pos);
		    return true;
	    }
	    /**
	     * カメラ座標系を、画面座標へ変換します。
	     * @param i_x
	     * @param i_y
	     * @param i_z
	     * @param o_pos2d
	     */
	    public void project(double i_x,double i_y,double i_z,NyARDoublePoint2d o_pos2d)
	    {
		    NyARDoubleMatrix44 m=this._frustum_rh;
		    double v3_1=1/i_z*m.m32;
		    double w=this._screen_size.w;
		    double h=this._screen_size.h;
		    o_pos2d.x=w-(1+(i_x*m.m00+i_z*m.m02)*v3_1)*w/2;
		    o_pos2d.y=h-(1+(i_y*m.m11+i_z*m.m12)*v3_1)*h/2;
		    return;
	    }
	    /**
	     * 透視変換行列の参照値を返します。
	     * この値は読出し専用です。変更しないでください。
	     * @return
	     */
	    public NyARDoubleMatrix44 refMatrix()
	    {
		    return this._frustum_rh;
	    }
	    /**
	     * 透視変換行列の逆行列を返します。
	     * この値は読出し専用です。変更しないでください。
	     * @return
	     */
	    public NyARDoubleMatrix44 refInvMatrix()
	    {
		    return this._inv_frustum_rh;
	    }
    	
    }
}
