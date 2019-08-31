﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
namespace jp.nyatla.nyartoolkit.cs.core
{
    /**
     * 遠近法を使ったパースペクティブ補正をかけて、ラスタ上の四角形から
     * 任意解像度の矩形パターンを作成します。
     * 
     * 出力ラスタ形式が、INT1D_X8R8G8B8_32の物については、単体サンプリングモードの時は高速化済み。マルチサンプリングモードのときは高速化なし。
     * 入力ラスタ形式がINT1D_X8R8G8B8_32,BYTE1D_B8G8R8_24,BYTE1D_R8G8B8_24については、高速化済み。
     * 他の形式のラスタでは、PixelReaderを介した低速転送で対応します。
     * <p>メモ-
     * この関数は、1倍の時はNyARColorPatt_Perspective,
     * n倍の時はNyARColorPatt_Perspective_O2の関数を元に作ってます。
     * </p>
     *
     */
    public class NyARPerspectiveRasterReader
    {
	    protected NyARPerspectiveParamGenerator _perspective_gen;
	    private const int LOCAL_LT=1;
	    protected double[] __pickFromRaster_cpara=new double[8];
	    private IPickupRasterImpl _picker;	
    	
	    private void initializeInstance(int i_buffer_type)
	    {
		    //新しいモードに対応したら書いてね。
		    switch(i_buffer_type){
		    case NyARBufferType.BYTE1D_B8G8R8X8_32:
			    this._picker=new PPickup_Impl_BYTE1D_B8G8R8X8_32();
			    break;
		    case NyARBufferType.BYTE1D_B8G8R8_24:
			    this._picker=new PPickup_Impl_BYTE1D_B8G8R8_24();
			    break;
		    case NyARBufferType.BYTE1D_R8G8B8_24:
			    this._picker=new PPickup_Impl_BYTE1D_R8G8B8_24();
			    break;
		    default:
			    this._picker=new PPickup_Impl_AnyRaster();
			    //低速インタフェイス警告。必要に応じて、高速取得系を実装してね
    //			System.out.println("NyARToolKit Warning:"+this.getClass().getName()+":Low speed interface.");
			    break;
		    }
		    this._perspective_gen=new NyARPerspectiveParamGenerator_O1(LOCAL_LT,LOCAL_LT);
		    return;		
	    }
	    /**
	     * コンストラクタです。このコンストラクタで作成したインスタンスは、入力ラスタタイプに依存しませんが低速です。
	     * 入力画像のラスタの形式が既知の場合は、もう一方のコンストラクタを使用してください。
	     */
	    public NyARPerspectiveRasterReader()
	    {
		    initializeInstance(NyARBufferType.NULL_ALLZERO);
		    return;
	    }
	    /**
	     * コンストラクタです。入力ラスタの形式を制限してインスタンスを作成します。
	     * @param i_input_raster_type
	     */
	    public NyARPerspectiveRasterReader(int i_input_raster_type)
	    {
		    //入力制限
		    this.initializeInstance(i_input_raster_type);
		    return;
	    }

	    /**
	     * i_in_rasterから4頂点i_vertexsでかこまれた領域の画像を射影変換して、o_outへ格納します。
	     * @param i_in_raster
	     * このラスタの形式は、コンストラクタで制限したものと一致している必要があります。
	     * @param i_vertex
	     * 4頂点を格納した配列です。
	     * @param i_edge_x
	     * X方向のエッジ割合です。0-99の数値を指定します。
	     * @param i_edge_y
	     * Y方向のエッジ割合です。0-99の数値を指定します。
	     * @param i_resolution
	     * 出力の1ピクセルあたりのサンプリング数を指定します。例えば2を指定すると、出力1ピクセルあたり4ピクセルをサンプリングします。
	     * @param o_out
	     * 出力先のラスタです。
	     * @return
	     * @throws NyARException
	     */
        public bool read4Point(INyARRgbRaster i_in_raster, NyARDoublePoint2d[] i_vertex, int i_edge_x, int i_edge_y, int i_resolution, INyARRgbRaster o_out)
	    {
		    NyARIntSize out_size=o_out.getSize();
		    int xe=out_size.w*i_edge_x/50;
		    int ye=out_size.h*i_edge_y/50;

		    //サンプリング解像度で分岐
		    if(i_resolution==1){
			    if (!this._perspective_gen.getParam((xe*2+out_size.w),(ye*2+out_size.h),i_vertex, this.__pickFromRaster_cpara)) {
				    return false;
			    }
			    this._picker.onePixel(xe+LOCAL_LT,ye+LOCAL_LT,this.__pickFromRaster_cpara,i_in_raster,o_out);
		    }else{
			    if (!this._perspective_gen.getParam((xe*2+out_size.w)*i_resolution,(ye*2+out_size.h)*i_resolution,i_vertex, this.__pickFromRaster_cpara)) {
				    return false;
			    }
			    this._picker.multiPixel(xe*i_resolution+LOCAL_LT,ye*i_resolution+LOCAL_LT,this.__pickFromRaster_cpara,i_resolution,i_in_raster,o_out);
		    }
		    return true;
	    }
	    /**
	     * read4Pointの入力型違いです。
	     */
        public bool read4Point(INyARRgbRaster i_in_raster, NyARIntPoint2d[] i_vertex, int i_edge_x, int i_edge_y, int i_resolution, INyARRgbRaster o_out)
	    {
		    NyARIntSize out_size=o_out.getSize();
		    int xe=out_size.w*i_edge_x/50;
		    int ye=out_size.h*i_edge_y/50;

		    //サンプリング解像度で分岐
		    if(i_resolution==1){
			    if (!this._perspective_gen.getParam((xe*2+out_size.w),(ye*2+out_size.h),i_vertex, this.__pickFromRaster_cpara)) {
				    return false;
			    }
			    this._picker.onePixel(xe+LOCAL_LT,ye+LOCAL_LT,this.__pickFromRaster_cpara,i_in_raster,o_out);
		    }else{
			    if (!this._perspective_gen.getParam((xe*2+out_size.w)*i_resolution,(ye*2+out_size.h)*i_resolution,i_vertex, this.__pickFromRaster_cpara)) {
				    return false;
			    }
			    this._picker.multiPixel(xe*i_resolution+LOCAL_LT,ye*i_resolution+LOCAL_LT,this.__pickFromRaster_cpara,i_resolution,i_in_raster,o_out);
		    }
		    return true;
	    }
	    /**
	     * read4Pointの入力型違いです。
	     */	
	    public bool read4Point(INyARRgbRaster i_in_raster,double i_x1,double i_y1,double i_x2,double i_y2,double i_x3,double i_y3,double i_x4,double i_y4,int i_edge_x,int i_edge_y,int i_resolution,INyARRgbRaster o_out)
	    {
		    NyARIntSize out_size=o_out.getSize();
		    int xe=out_size.w*i_edge_x/50;
		    int ye=out_size.h*i_edge_y/50;

		    //サンプリング解像度で分岐
		    if(i_resolution==1){
			    if (!this._perspective_gen.getParam((xe*2+out_size.w),(ye*2+out_size.h),i_x1,i_y1,i_x2,i_y2,i_x3,i_y3,i_x4,i_y4, this.__pickFromRaster_cpara)) {
				    return false;
			    }
			    this._picker.onePixel(xe+LOCAL_LT,ye+LOCAL_LT,this.__pickFromRaster_cpara,i_in_raster,o_out);
		    }else{
			    if (!this._perspective_gen.getParam((xe*2+out_size.w)*i_resolution,(ye*2+out_size.h)*i_resolution,i_x1,i_y1,i_x2,i_y2,i_x3,i_y3,i_x4,i_y4, this.__pickFromRaster_cpara)) {
				    return false;
			    }
			    this._picker.multiPixel(xe*i_resolution+LOCAL_LT,ye*i_resolution+LOCAL_LT,this.__pickFromRaster_cpara,i_resolution,i_in_raster,o_out);
		    }
		    return true;
	    }	
    }


    //
    //ここから先は入力画像毎のラスタドライバ
    //

    interface IPickupRasterImpl
    {
	    void onePixel(int pk_l,int pk_t,double[] cpara,INyARRgbRaster i_in_raster,INyARRgbRaster o_out);
	    void multiPixel(int pk_l,int pk_t,double[] cpara,int i_resolution,INyARRgbRaster i_in_raster,INyARRgbRaster o_out);
    }

    sealed class PPickup_Impl_BYTE1D_R8G8B8_24 : IPickupRasterImpl
    {
	    public void onePixel(int pk_l,int pk_t,double[] cpara,INyARRgbRaster i_in_raster,INyARRgbRaster o_out)
	    {
		    Debug.Assert(i_in_raster.isEqualBufferType(NyARBufferType.BYTE1D_R8G8B8_24));
		    //出力形式による分岐
		    switch(o_out.getBufferType())
		    {
		    case NyARBufferType.INT1D_X8R8G8B8_32:
			    onePixel_INT1D_X8R8G8B8_32(pk_l,pk_t,i_in_raster.getWidth(),i_in_raster.getHeight(),cpara,(byte[])i_in_raster.getBuffer(),o_out);
			    break;
		    default:
			    onePixel_ANY(pk_l,pk_t,i_in_raster.getWidth(),i_in_raster.getHeight(),cpara,(byte[])i_in_raster.getBuffer(),o_out);
			    break;
		    }
		    return;
	    }
	    public void multiPixel(int pk_l,int pk_t,double[] cpara,int i_resolution,INyARRgbRaster i_in_raster,INyARRgbRaster o_out)
	    {
		    Debug.Assert(i_in_raster.isEqualBufferType(NyARBufferType.BYTE1D_R8G8B8_24));
		    //出力形式による分岐(分解能が高い時は大した差が出ないから、ANYだけ。)
		    multiPixel_ANY(pk_l,pk_t,i_in_raster.getWidth(),i_in_raster.getHeight(),i_resolution,cpara,(byte[])i_in_raster.getBuffer(),o_out);
		    return;		
	    }
    	
	    private void onePixel_INT1D_X8R8G8B8_32(int pk_l,int pk_t,int in_w,int in_h,double[] cpara,byte[] i_in_buf,INyARRgbRaster o_out)
	    {
		    Debug.Assert(o_out.isEqualBufferType(NyARBufferType.INT1D_X8R8G8B8_32));
		    int[] pat_data=(int[])o_out.getBuffer();
		    //ピクセルリーダーを取得
		    double cp0=cpara[0];
		    double cp3=cpara[3];
		    double cp6=cpara[6];
		    double cp1=cpara[1];
		    double cp4=cpara[4];
		    double cp7=cpara[7];
    		
		    int out_w=o_out.getWidth();
		    int out_h=o_out.getHeight();
		    double cp7_cy_1  =cp7*pk_t+1.0+cp6*pk_l;
		    double cp1_cy_cp2=cp1*pk_t+cpara[2]+cp0*pk_l;
		    double cp4_cy_cp5=cp4*pk_t+cpara[5]+cp3*pk_l;
		    int r,g,b;
		    int p=0;
		    for(int iy=0;iy<out_h;iy++){
			    //解像度分の点を取る。
			    double cp7_cy_1_cp6_cx  =cp7_cy_1;
			    double cp1_cy_cp2_cp0_cx=cp1_cy_cp2;
			    double cp4_cy_cp5_cp3_cx=cp4_cy_cp5;
    			
			    for(int ix=0;ix<out_w;ix++){
				    //1ピクセルを作成
				    double d=1/(cp7_cy_1_cp6_cx);
				    int x=(int)((cp1_cy_cp2_cp0_cx)*d);
				    int y=(int)((cp4_cy_cp5_cp3_cx)*d);
				    if(x<0){x=0;}else if(x>=in_w){x=in_w-1;}
				    if(y<0){y=0;}else if(y>=in_h){y=in_h-1;}
    						
				    int bp = (x + y * in_w) * 3;
				    r=(i_in_buf[bp + 0] & 0xff);
				    g=(i_in_buf[bp + 1] & 0xff);
				    b=(i_in_buf[bp + 2] & 0xff);
				    cp7_cy_1_cp6_cx+=cp6;
				    cp1_cy_cp2_cp0_cx+=cp0;
				    cp4_cy_cp5_cp3_cx+=cp3;
				    pat_data[p]=(r<<16)|(g<<8)|((b&0xff));
				    p++;
			    }
			    cp7_cy_1+=cp7;
			    cp1_cy_cp2+=cp1;
			    cp4_cy_cp5+=cp4;
		    }
		    return;
	    }
	    private void onePixel_ANY(int pk_l,int pk_t,int in_w,int in_h,double[] cpara,byte[] i_in_buf,INyARRgbRaster o_out)
	    {
		    INyARRgbPixelReader out_reader=o_out.getRgbPixelReader();

		    //ピクセルリーダーを取得
		    double cp0=cpara[0];
		    double cp3=cpara[3];
		    double cp6=cpara[6];
		    double cp1=cpara[1];
		    double cp4=cpara[4];
		    double cp7=cpara[7];
    		
		    int out_w=o_out.getWidth();
		    int out_h=o_out.getHeight();
		    double cp7_cy_1  =cp7*pk_t+1.0+cp6*pk_l;
		    double cp1_cy_cp2=cp1*pk_t+cpara[2]+cp0*pk_l;
		    double cp4_cy_cp5=cp4*pk_t+cpara[5]+cp3*pk_l;
		    int r,g,b;
		    for(int iy=0;iy<out_h;iy++){
			    //解像度分の点を取る。
			    double cp7_cy_1_cp6_cx  =cp7_cy_1;
			    double cp1_cy_cp2_cp0_cx=cp1_cy_cp2;
			    double cp4_cy_cp5_cp3_cx=cp4_cy_cp5;
    			
			    for(int ix=0;ix<out_w;ix++){
				    //1ピクセルを作成
				    double d=1/(cp7_cy_1_cp6_cx);
				    int x=(int)((cp1_cy_cp2_cp0_cx)*d);
				    int y=(int)((cp4_cy_cp5_cp3_cx)*d);
				    if(x<0){x=0;}else if(x>=in_w){x=in_w-1;}
				    if(y<0){y=0;}else if(y>=in_h){y=in_h-1;}
    						
				    int bp = (x + y * in_w) * 3;
				    r=(i_in_buf[bp + 0] & 0xff);
				    g=(i_in_buf[bp + 1] & 0xff);
				    b=(i_in_buf[bp + 2] & 0xff);
				    cp7_cy_1_cp6_cx+=cp6;
				    cp1_cy_cp2_cp0_cx+=cp0;
				    cp4_cy_cp5_cp3_cx+=cp3;

				    out_reader.setPixel(ix,iy,r,g,b);
			    }
			    cp7_cy_1+=cp7;
			    cp1_cy_cp2+=cp1;
			    cp4_cy_cp5+=cp4;
		    }
		    return;
	    }
	    private void multiPixel_ANY(int pk_l,int pk_t,int in_w,int in_h,int i_resolution,double[] cpara,byte[] i_in_buf,INyARRgbRaster o_out)
	    {
		    int res_pix=i_resolution*i_resolution;

		    INyARRgbPixelReader out_reader=o_out.getRgbPixelReader();

		    //ピクセルリーダーを取得
		    double cp0=cpara[0];
		    double cp3=cpara[3];
		    double cp6=cpara[6];
		    double cp1=cpara[1];
		    double cp4=cpara[4];
		    double cp7=cpara[7];
		    double cp2=cpara[2];
		    double cp5=cpara[5];
    		
		    int out_w=o_out.getWidth();
		    int out_h=o_out.getHeight();
		    for(int iy=out_h-1;iy>=0;iy--){
			    //解像度分の点を取る。
			    for(int ix=out_w-1;ix>=0;ix--){
				    int r,g,b;
				    r=g=b=0;
				    int cy=pk_t+iy*i_resolution;
				    int cx=pk_l+ix*i_resolution;
				    double cp7_cy_1_cp6_cx_b  =cp7*cy+1.0+cp6*cx;
				    double cp1_cy_cp2_cp0_cx_b=cp1*cy+cp2+cp0*cx;
				    double cp4_cy_cp5_cp3_cx_b=cp4*cy+cp5+cp3*cx;
				    for(int i2y=i_resolution-1;i2y>=0;i2y--){
					    double cp7_cy_1_cp6_cx  =cp7_cy_1_cp6_cx_b;
					    double cp1_cy_cp2_cp0_cx=cp1_cy_cp2_cp0_cx_b;
					    double cp4_cy_cp5_cp3_cx=cp4_cy_cp5_cp3_cx_b;
					    for(int i2x=i_resolution-1;i2x>=0;i2x--){
						    //1ピクセルを作成
						    double d=1/(cp7_cy_1_cp6_cx);
						    int x=(int)((cp1_cy_cp2_cp0_cx)*d);
						    int y=(int)((cp4_cy_cp5_cp3_cx)*d);
						    if(x<0){x=0;}else if(x>=in_w){x=in_w-1;}
						    if(y<0){y=0;}else if(y>=in_h){y=in_h-1;}
    						
						    int bp = (x + y * in_w) * 3;
						    r+=(i_in_buf[bp + 0] & 0xff);
						    g+=(i_in_buf[bp + 1] & 0xff);
						    b+=(i_in_buf[bp + 2] & 0xff);
						    cp7_cy_1_cp6_cx+=cp6;
						    cp1_cy_cp2_cp0_cx+=cp0;
						    cp4_cy_cp5_cp3_cx+=cp3;
					    }
					    cp7_cy_1_cp6_cx_b+=cp7;
					    cp1_cy_cp2_cp0_cx_b+=cp1;
					    cp4_cy_cp5_cp3_cx_b+=cp4;
				    }
				    out_reader.setPixel(ix,iy,r/res_pix,g/res_pix,b/res_pix);
			    }
		    }
		    return;
	    }
    }






    sealed class PPickup_Impl_BYTE1D_B8G8R8_24 : IPickupRasterImpl
    {
	    public void onePixel(int pk_l,int pk_t,double[] cpara,INyARRgbRaster i_in_raster,INyARRgbRaster o_out)
	    {
		    Debug.Assert(i_in_raster.isEqualBufferType(NyARBufferType.BYTE1D_B8G8R8_24));
		    //出力形式による分岐
		    switch(o_out.getBufferType())
		    {
		    case NyARBufferType.INT1D_X8R8G8B8_32:
			    onePixel_INT1D_X8R8G8B8_32(pk_l,pk_t,i_in_raster.getWidth(),i_in_raster.getHeight(),cpara,(byte[])i_in_raster.getBuffer(),o_out);
			    break;
		    default:
			    onePixel_ANY(pk_l,pk_t,i_in_raster.getWidth(),i_in_raster.getHeight(),cpara,(byte[])i_in_raster.getBuffer(),o_out);
			    break;
		    }
		    return;
	    }
	    public void multiPixel(int pk_l,int pk_t,double[] cpara,int i_resolution,INyARRgbRaster i_in_raster,INyARRgbRaster o_out)
	    {
		    Debug.Assert(i_in_raster.isEqualBufferType(NyARBufferType.BYTE1D_B8G8R8_24));
		    //出力形式による分岐(分解能が高い時は大した差が出ないから、ANYだけ。)
		    multiPixel_ANY(pk_l,pk_t,i_in_raster.getWidth(),i_in_raster.getHeight(),i_resolution,cpara,(byte[])i_in_raster.getBuffer(),o_out);
		    return;		
	    }
    	
	    private void onePixel_INT1D_X8R8G8B8_32(int pk_l,int pk_t,int in_w,int in_h,double[] cpara,byte[] i_in_buf,INyARRgbRaster o_out)
	    {
		    Debug.Assert(o_out.isEqualBufferType(NyARBufferType.INT1D_X8R8G8B8_32));
		    int[] pat_data=(int[])o_out.getBuffer();
		    //ピクセルリーダーを取得
		    double cp0=cpara[0];
		    double cp3=cpara[3];
		    double cp6=cpara[6];
		    double cp1=cpara[1];
		    double cp4=cpara[4];
		    double cp7=cpara[7];
    		
		    int out_w=o_out.getWidth();
		    int out_h=o_out.getHeight();
		    double cp7_cy_1  =cp7*pk_t+1.0+cp6*pk_l;
		    double cp1_cy_cp2=cp1*pk_t+cpara[2]+cp0*pk_l;
		    double cp4_cy_cp5=cp4*pk_t+cpara[5]+cp3*pk_l;
		    int r,g,b;
		    int p=0;
		    for(int iy=0;iy<out_h;iy++){
			    //解像度分の点を取る。
			    double cp7_cy_1_cp6_cx  =cp7_cy_1;
			    double cp1_cy_cp2_cp0_cx=cp1_cy_cp2;
			    double cp4_cy_cp5_cp3_cx=cp4_cy_cp5;
    			
			    for(int ix=0;ix<out_w;ix++){
				    //1ピクセルを作成
				    double d=1/(cp7_cy_1_cp6_cx);
				    int x=(int)((cp1_cy_cp2_cp0_cx)*d);
				    int y=(int)((cp4_cy_cp5_cp3_cx)*d);
				    if(x<0){x=0;}else if(x>=in_w){x=in_w-1;}
				    if(y<0){y=0;}else if(y>=in_h){y=in_h-1;}
    						
				    int bp = (x + y * in_w) * 3;
				    r=(i_in_buf[bp + 2] & 0xff);
				    g=(i_in_buf[bp + 1] & 0xff);
				    b=(i_in_buf[bp + 0] & 0xff);
				    cp7_cy_1_cp6_cx+=cp6;
				    cp1_cy_cp2_cp0_cx+=cp0;
				    cp4_cy_cp5_cp3_cx+=cp3;
				    pat_data[p]=(r<<16)|(g<<8)|((b&0xff));
				    p++;
			    }
			    cp7_cy_1+=cp7;
			    cp1_cy_cp2+=cp1;
			    cp4_cy_cp5+=cp4;
		    }
		    return;
	    }
	    private void onePixel_ANY(int pk_l,int pk_t,int in_w,int in_h,double[] cpara,byte[] i_in_buf,INyARRgbRaster o_out)
	    {
		    INyARRgbPixelReader out_reader=o_out.getRgbPixelReader();

		    //ピクセルリーダーを取得
		    double cp0=cpara[0];
		    double cp3=cpara[3];
		    double cp6=cpara[6];
		    double cp1=cpara[1];
		    double cp4=cpara[4];
		    double cp7=cpara[7];
    		
		    int out_w=o_out.getWidth();
		    int out_h=o_out.getHeight();
		    double cp7_cy_1  =cp7*pk_t+1.0+cp6*pk_l;
		    double cp1_cy_cp2=cp1*pk_t+cpara[2]+cp0*pk_l;
		    double cp4_cy_cp5=cp4*pk_t+cpara[5]+cp3*pk_l;
		    int r,g,b;
		    for(int iy=0;iy<out_h;iy++){
			    //解像度分の点を取る。
			    double cp7_cy_1_cp6_cx  =cp7_cy_1;
			    double cp1_cy_cp2_cp0_cx=cp1_cy_cp2;
			    double cp4_cy_cp5_cp3_cx=cp4_cy_cp5;
    			
			    for(int ix=0;ix<out_w;ix++){
				    //1ピクセルを作成
				    double d=1/(cp7_cy_1_cp6_cx);
				    int x=(int)((cp1_cy_cp2_cp0_cx)*d);
				    int y=(int)((cp4_cy_cp5_cp3_cx)*d);
				    if(x<0){x=0;}else if(x>=in_w){x=in_w-1;}
				    if(y<0){y=0;}else if(y>=in_h){y=in_h-1;}
    						
				    int bp = (x + y * in_w) * 3;
				    r=(i_in_buf[bp + 2] & 0xff);
				    g=(i_in_buf[bp + 1] & 0xff);
				    b=(i_in_buf[bp + 0] & 0xff);
				    cp7_cy_1_cp6_cx+=cp6;
				    cp1_cy_cp2_cp0_cx+=cp0;
				    cp4_cy_cp5_cp3_cx+=cp3;

				    out_reader.setPixel(ix,iy,r,g,b);
			    }
			    cp7_cy_1+=cp7;
			    cp1_cy_cp2+=cp1;
			    cp4_cy_cp5+=cp4;
		    }
		    return;
	    }
	    private void multiPixel_ANY(int pk_l,int pk_t,int in_w,int in_h,int i_resolution,double[] cpara,byte[] i_in_buf,INyARRgbRaster o_out)
	    {
		    int res_pix=i_resolution*i_resolution;

		    INyARRgbPixelReader out_reader=o_out.getRgbPixelReader();

		    //ピクセルリーダーを取得
		    double cp0=cpara[0];
		    double cp3=cpara[3];
		    double cp6=cpara[6];
		    double cp1=cpara[1];
		    double cp4=cpara[4];
		    double cp7=cpara[7];
		    double cp2=cpara[2];
		    double cp5=cpara[5];
    		
		    int out_w=o_out.getWidth();
		    int out_h=o_out.getHeight();
		    for(int iy=out_h-1;iy>=0;iy--){
			    //解像度分の点を取る。
			    for(int ix=out_w-1;ix>=0;ix--){
				    int r,g,b;
				    r=g=b=0;
				    int cy=pk_t+iy*i_resolution;
				    int cx=pk_l+ix*i_resolution;
				    double cp7_cy_1_cp6_cx_b  =cp7*cy+1.0+cp6*cx;
				    double cp1_cy_cp2_cp0_cx_b=cp1*cy+cp2+cp0*cx;
				    double cp4_cy_cp5_cp3_cx_b=cp4*cy+cp5+cp3*cx;
				    for(int i2y=i_resolution-1;i2y>=0;i2y--){
					    double cp7_cy_1_cp6_cx  =cp7_cy_1_cp6_cx_b;
					    double cp1_cy_cp2_cp0_cx=cp1_cy_cp2_cp0_cx_b;
					    double cp4_cy_cp5_cp3_cx=cp4_cy_cp5_cp3_cx_b;
					    for(int i2x=i_resolution-1;i2x>=0;i2x--){
						    //1ピクセルを作成
						    double d=1/(cp7_cy_1_cp6_cx);
						    int x=(int)((cp1_cy_cp2_cp0_cx)*d);
						    int y=(int)((cp4_cy_cp5_cp3_cx)*d);
						    if(x<0){x=0;}else if(x>=in_w){x=in_w-1;}
						    if(y<0){y=0;}else if(y>=in_h){y=in_h-1;}
    						
						    int bp = (x + y * in_w) * 3;
						    r+=(i_in_buf[bp + 2] & 0xff);
						    g+=(i_in_buf[bp + 1] & 0xff);
						    b+=(i_in_buf[bp + 0] & 0xff);
						    cp7_cy_1_cp6_cx+=cp6;
						    cp1_cy_cp2_cp0_cx+=cp0;
						    cp4_cy_cp5_cp3_cx+=cp3;
					    }
					    cp7_cy_1_cp6_cx_b+=cp7;
					    cp1_cy_cp2_cp0_cx_b+=cp1;
					    cp4_cy_cp5_cp3_cx_b+=cp4;
				    }
				    out_reader.setPixel(ix,iy,r/res_pix,g/res_pix,b/res_pix);
			    }
		    }
		    return;
	    }
    }




    sealed class PPickup_Impl_BYTE1D_B8G8R8X8_32 : IPickupRasterImpl
    {
	    public void onePixel(int pk_l,int pk_t,double[] cpara,INyARRgbRaster i_in_raster,INyARRgbRaster o_out)
	    {
		    Debug.Assert(i_in_raster.isEqualBufferType(NyARBufferType.BYTE1D_B8G8R8X8_32));
		    //出力形式による分岐
		    switch(o_out.getBufferType())
		    {
		    case NyARBufferType.INT1D_X8R8G8B8_32:
			    onePixel_INT1D_X8R8G8B8_32(pk_l,pk_t,i_in_raster.getWidth(),i_in_raster.getHeight(),cpara,(byte[])i_in_raster.getBuffer(),o_out);
			    break;
		    default:
			    onePixel_ANY(pk_l,pk_t,i_in_raster.getWidth(),i_in_raster.getHeight(),cpara,(byte[])i_in_raster.getBuffer(),o_out);
			    break;
		    }
		    return;
	    }
	    public void multiPixel(int pk_l,int pk_t,double[] cpara,int i_resolution,INyARRgbRaster i_in_raster,INyARRgbRaster o_out)
	    {
		    Debug.Assert(i_in_raster.isEqualBufferType(NyARBufferType.BYTE1D_B8G8R8X8_32));
		    //出力形式による分岐(分解能が高い時は大した差が出ないから、ANYだけ。)
		    multiPixel_ANY(pk_l,pk_t,i_in_raster.getWidth(),i_in_raster.getHeight(),i_resolution,cpara,(byte[])i_in_raster.getBuffer(),o_out);
		    return;		
	    }
    	
	    private void onePixel_INT1D_X8R8G8B8_32(int pk_l,int pk_t,int in_w,int in_h,double[] cpara,byte[] i_in_buf,INyARRgbRaster o_out)
	    {
		    Debug.Assert(o_out.isEqualBufferType(NyARBufferType.INT1D_X8R8G8B8_32));
		    int[] pat_data=(int[])o_out.getBuffer();
		    //ピクセルリーダーを取得
		    double cp0=cpara[0];
		    double cp3=cpara[3];
		    double cp6=cpara[6];
		    double cp1=cpara[1];
		    double cp4=cpara[4];
		    double cp7=cpara[7];
    		
		    int out_w=o_out.getWidth();
		    int out_h=o_out.getHeight();
		    double cp7_cy_1  =cp7*pk_t+1.0+cp6*pk_l;
		    double cp1_cy_cp2=cp1*pk_t+cpara[2]+cp0*pk_l;
		    double cp4_cy_cp5=cp4*pk_t+cpara[5]+cp3*pk_l;
		    int r,g,b;
		    int p=0;
		    for(int iy=0;iy<out_h;iy++){
			    //解像度分の点を取る。
			    double cp7_cy_1_cp6_cx  =cp7_cy_1;
			    double cp1_cy_cp2_cp0_cx=cp1_cy_cp2;
			    double cp4_cy_cp5_cp3_cx=cp4_cy_cp5;
    			
			    for(int ix=0;ix<out_w;ix++){
				    //1ピクセルを作成
				    double d=1/(cp7_cy_1_cp6_cx);
				    int x=(int)((cp1_cy_cp2_cp0_cx)*d);
				    int y=(int)((cp4_cy_cp5_cp3_cx)*d);
				    if(x<0){x=0;}else if(x>=in_w){x=in_w-1;}
				    if(y<0){y=0;}else if(y>=in_h){y=in_h-1;}
    						
				    int bp = (x + y * in_w) * 4;
				    r=(i_in_buf[bp + 2] & 0xff);
				    g=(i_in_buf[bp + 1] & 0xff);
				    b=(i_in_buf[bp + 0] & 0xff);
				    cp7_cy_1_cp6_cx+=cp6;
				    cp1_cy_cp2_cp0_cx+=cp0;
				    cp4_cy_cp5_cp3_cx+=cp3;
				    pat_data[p]=(r<<16)|(g<<8)|((b&0xff));
				    p++;
			    }
			    cp7_cy_1+=cp7;
			    cp1_cy_cp2+=cp1;
			    cp4_cy_cp5+=cp4;
		    }
		    return;
	    }
	    private void onePixel_ANY(int pk_l,int pk_t,int in_w,int in_h,double[] cpara,byte[] i_in_buf,INyARRgbRaster o_out)
	    {
		    INyARRgbPixelReader out_reader=o_out.getRgbPixelReader();

		    //ピクセルリーダーを取得
		    double cp0=cpara[0];
		    double cp3=cpara[3];
		    double cp6=cpara[6];
		    double cp1=cpara[1];
		    double cp4=cpara[4];
		    double cp7=cpara[7];
    		
		    int out_w=o_out.getWidth();
		    int out_h=o_out.getHeight();
		    double cp7_cy_1  =cp7*pk_t+1.0+cp6*pk_l;
		    double cp1_cy_cp2=cp1*pk_t+cpara[2]+cp0*pk_l;
		    double cp4_cy_cp5=cp4*pk_t+cpara[5]+cp3*pk_l;
		    int r,g,b;
		    for(int iy=0;iy<out_h;iy++){
			    //解像度分の点を取る。
			    double cp7_cy_1_cp6_cx  =cp7_cy_1;
			    double cp1_cy_cp2_cp0_cx=cp1_cy_cp2;
			    double cp4_cy_cp5_cp3_cx=cp4_cy_cp5;
    			
			    for(int ix=0;ix<out_w;ix++){
				    //1ピクセルを作成
				    double d=1/(cp7_cy_1_cp6_cx);
				    int x=(int)((cp1_cy_cp2_cp0_cx)*d);
				    int y=(int)((cp4_cy_cp5_cp3_cx)*d);
				    if(x<0){x=0;}else if(x>=in_w){x=in_w-1;}
				    if(y<0){y=0;}else if(y>=in_h){y=in_h-1;}
    						
				    int bp = (x + y * in_w) * 4;
				    r=(i_in_buf[bp + 2] & 0xff);
				    g=(i_in_buf[bp + 1] & 0xff);
				    b=(i_in_buf[bp + 0] & 0xff);
				    cp7_cy_1_cp6_cx+=cp6;
				    cp1_cy_cp2_cp0_cx+=cp0;
				    cp4_cy_cp5_cp3_cx+=cp3;

				    out_reader.setPixel(ix,iy,r,g,b);
			    }
			    cp7_cy_1+=cp7;
			    cp1_cy_cp2+=cp1;
			    cp4_cy_cp5+=cp4;
		    }
		    return;
	    }

	    private void multiPixel_ANY(int pk_l,int pk_t,int in_w,int in_h,int i_resolution,double[] cpara,byte[] i_in_buf,INyARRgbRaster o_out)
	    {
		    int res_pix=i_resolution*i_resolution;

		    INyARRgbPixelReader out_reader=o_out.getRgbPixelReader();

		    //ピクセルリーダーを取得
		    double cp0=cpara[0];
		    double cp3=cpara[3];
		    double cp6=cpara[6];
		    double cp1=cpara[1];
		    double cp4=cpara[4];
		    double cp7=cpara[7];
		    double cp2=cpara[2];
		    double cp5=cpara[5];
    		
		    int out_w=o_out.getWidth();
		    int out_h=o_out.getHeight();
		    for(int iy=out_h-1;iy>=0;iy--){
			    //解像度分の点を取る。
			    for(int ix=out_w-1;ix>=0;ix--){
				    int r,g,b;
				    r=g=b=0;
				    int cy=pk_t+iy*i_resolution;
				    int cx=pk_l+ix*i_resolution;
				    double cp7_cy_1_cp6_cx_b  =cp7*cy+1.0+cp6*cx;
				    double cp1_cy_cp2_cp0_cx_b=cp1*cy+cp2+cp0*cx;
				    double cp4_cy_cp5_cp3_cx_b=cp4*cy+cp5+cp3*cx;
				    for(int i2y=i_resolution-1;i2y>=0;i2y--){
					    double cp7_cy_1_cp6_cx  =cp7_cy_1_cp6_cx_b;
					    double cp1_cy_cp2_cp0_cx=cp1_cy_cp2_cp0_cx_b;
					    double cp4_cy_cp5_cp3_cx=cp4_cy_cp5_cp3_cx_b;
					    for(int i2x=i_resolution-1;i2x>=0;i2x--){
						    //1ピクセルを作成
						    double d=1/(cp7_cy_1_cp6_cx);
						    int x=(int)((cp1_cy_cp2_cp0_cx)*d);
						    int y=(int)((cp4_cy_cp5_cp3_cx)*d);
						    if(x<0){x=0;}else if(x>=in_w){x=in_w-1;}
						    if(y<0){y=0;}else if(y>=in_h){y=in_h-1;}
    						
						    int bp = (x + y * in_w) * 4;
						    r+=(i_in_buf[bp + 2] & 0xff);
						    g+=(i_in_buf[bp + 1] & 0xff);
						    b+=(i_in_buf[bp + 0] & 0xff);
						    cp7_cy_1_cp6_cx+=cp6;
						    cp1_cy_cp2_cp0_cx+=cp0;
						    cp4_cy_cp5_cp3_cx+=cp3;
					    }
					    cp7_cy_1_cp6_cx_b+=cp7;
					    cp1_cy_cp2_cp0_cx_b+=cp1;
					    cp4_cy_cp5_cp3_cx_b+=cp4;
				    }
				    out_reader.setPixel(ix,iy,r/res_pix,g/res_pix,b/res_pix);
			    }
		    }
		    return;
	    }
    }

    /**
     * 全種類のNyARRasterを入力できるクラス
     */
    sealed class PPickup_Impl_AnyRaster : IPickupRasterImpl
    {
	    private int[] __pickFromRaster_rgb_tmp = new int[3];
	    public void onePixel(int pk_l,int pk_t,double[] cpara,INyARRgbRaster i_in_raster,INyARRgbRaster o_out)
	    {
		    //出力形式による分岐
		    switch(o_out.getBufferType())
		    {
		    case NyARBufferType.INT1D_X8R8G8B8_32:
			    onePixel_INT1D_X8R8G8B8_32(pk_l,pk_t,i_in_raster.getWidth(),i_in_raster.getHeight(),cpara,i_in_raster.getRgbPixelReader(),o_out);
			    break;
		    default:
			    onePixel_ANY(pk_l,pk_t,i_in_raster.getWidth(),i_in_raster.getHeight(),cpara,i_in_raster.getRgbPixelReader(),o_out);
			    break;
		    }
		    return;
	    }
	    public void multiPixel(int pk_l,int pk_t,double[] cpara,int i_resolution,INyARRgbRaster i_in_raster,INyARRgbRaster o_out)
	    {
		    //出力形式による分岐
		    switch(o_out.getBufferType())
		    {
		    case NyARBufferType.INT1D_X8R8G8B8_32:
			    multiPixel_INT1D_X8R8G8B8_32(pk_l,pk_t,i_in_raster.getWidth(),i_in_raster.getHeight(),i_resolution,cpara,i_in_raster.getRgbPixelReader(),o_out);
			    break;
		    default:
			    multiPixel_ANY(pk_l,pk_t,i_in_raster.getWidth(),i_in_raster.getHeight(),i_resolution,cpara,i_in_raster.getRgbPixelReader(),o_out);
			    break;
		    }
		    return;		
	    }
    	
	    private void onePixel_INT1D_X8R8G8B8_32(int pk_l,int pk_t,int in_w,int in_h,double[] cpara,INyARRgbPixelReader i_in_reader,INyARRgbRaster o_out)
	    {
		    Debug.Assert(o_out.isEqualBufferType(NyARBufferType.INT1D_X8R8G8B8_32));
		    int[] rgb_tmp = this.__pickFromRaster_rgb_tmp;
		    int[] pat_data=(int[])o_out.getBuffer();

		    //ピクセルリーダーを取得
		    double cp0=cpara[0];
		    double cp3=cpara[3];
		    double cp6=cpara[6];
		    double cp1=cpara[1];
		    double cp4=cpara[4];
		    double cp7=cpara[7];
    		
		    int out_w=o_out.getWidth();
		    int out_h=o_out.getHeight();
		    double cp7_cy_1  =cp7*pk_t+1.0+cp6*pk_l;
		    double cp1_cy_cp2=cp1*pk_t+cpara[2]+cp0*pk_l;
		    double cp4_cy_cp5=cp4*pk_t+cpara[5]+cp3*pk_l;
		    int p=0;
		    for(int iy=out_h-1;iy>=0;iy--){
			    //解像度分の点を取る。
			    double cp7_cy_1_cp6_cx  =cp7_cy_1;
			    double cp1_cy_cp2_cp0_cx=cp1_cy_cp2;
			    double cp4_cy_cp5_cp3_cx=cp4_cy_cp5;
    			
			    for(int ix=out_w-1;ix>=0;ix--){
				    //1ピクセルを作成
				    double d=1/(cp7_cy_1_cp6_cx);
				    int x=(int)((cp1_cy_cp2_cp0_cx)*d);
				    int y=(int)((cp4_cy_cp5_cp3_cx)*d);
				    if(x<0){x=0;}else if(x>=in_w){x=in_w-1;}
				    if(y<0){y=0;}else if(y>=in_h){y=in_h-1;}
    						
				    i_in_reader.getPixel(x, y, rgb_tmp);
				    cp7_cy_1_cp6_cx+=cp6;
				    cp1_cy_cp2_cp0_cx+=cp0;
				    cp4_cy_cp5_cp3_cx+=cp3;

				    pat_data[p]=(rgb_tmp[0]<<16)|(rgb_tmp[1]<<8)|((rgb_tmp[2]&0xff));
				    p++;
			    }
			    cp7_cy_1+=cp7;
			    cp1_cy_cp2+=cp1;
			    cp4_cy_cp5+=cp4;
		    }
		    return;
	    }
	    private void onePixel_ANY(int pk_l,int pk_t,int in_w,int in_h,double[] cpara,INyARRgbPixelReader i_in_reader,INyARRgbRaster o_out)
	    {
		    int[] rgb_tmp = this.__pickFromRaster_rgb_tmp;
		    INyARRgbPixelReader out_reader=o_out.getRgbPixelReader();

		    //ピクセルリーダーを取得
		    double cp0=cpara[0];
		    double cp3=cpara[3];
		    double cp6=cpara[6];
		    double cp1=cpara[1];
		    double cp4=cpara[4];
		    double cp7=cpara[7];
    		
		    int out_w=o_out.getWidth();
		    int out_h=o_out.getHeight();
		    double cp7_cy_1  =cp7*pk_t+1.0+cp6*pk_l;
		    double cp1_cy_cp2=cp1*pk_t+cpara[2]+cp0*pk_l;
		    double cp4_cy_cp5=cp4*pk_t+cpara[5]+cp3*pk_l;
    		
		    for(int iy=0;iy<out_h;iy++){
			    //解像度分の点を取る。
			    double cp7_cy_1_cp6_cx  =cp7_cy_1;
			    double cp1_cy_cp2_cp0_cx=cp1_cy_cp2;
			    double cp4_cy_cp5_cp3_cx=cp4_cy_cp5;
    			
			    for(int ix=0;ix<out_w;ix++){
				    //1ピクセルを作成
				    double d=1/(cp7_cy_1_cp6_cx);
				    int x=(int)((cp1_cy_cp2_cp0_cx)*d);
				    int y=(int)((cp4_cy_cp5_cp3_cx)*d);
				    if(x<0){x=0;}else if(x>=in_w){x=in_w-1;}
				    if(y<0){y=0;}else if(y>=in_h){y=in_h-1;}
    						
				    i_in_reader.getPixel(x, y, rgb_tmp);
				    cp7_cy_1_cp6_cx+=cp6;
				    cp1_cy_cp2_cp0_cx+=cp0;
				    cp4_cy_cp5_cp3_cx+=cp3;

				    out_reader.setPixel(ix,iy,rgb_tmp);
			    }
			    cp7_cy_1+=cp7;
			    cp1_cy_cp2+=cp1;
			    cp4_cy_cp5+=cp4;
		    }
		    return;
	    }

	    private void multiPixel_INT1D_X8R8G8B8_32(int pk_l,int pk_t,int in_w,int in_h,int i_resolution,double[] cpara,INyARRgbPixelReader i_in_reader,INyARRgbRaster o_out)
	    {
		    Debug.Assert(o_out.isEqualBufferType(NyARBufferType.INT1D_X8R8G8B8_32));
		    int res_pix=i_resolution*i_resolution;

		    int[] rgb_tmp = this.__pickFromRaster_rgb_tmp;
		    int[] pat_data=(int[])o_out.getBuffer();

		    //ピクセルリーダーを取得
		    double cp0=cpara[0];
		    double cp3=cpara[3];
		    double cp6=cpara[6];
		    double cp1=cpara[1];
		    double cp4=cpara[4];
		    double cp7=cpara[7];
		    double cp2=cpara[2];
		    double cp5=cpara[5];
    		
		    int out_w=o_out.getWidth();
		    int out_h=o_out.getHeight();
		    int p=(out_w*out_h-1);
		    for(int iy=out_h-1;iy>=0;iy--){
			    //解像度分の点を取る。
			    for(int ix=out_w-1;ix>=0;ix--){
				    int r,g,b;
				    r=g=b=0;
				    int cy=pk_t+iy*i_resolution;
				    int cx=pk_l+ix*i_resolution;
				    double cp7_cy_1_cp6_cx_b  =cp7*cy+1.0+cp6*cx;
				    double cp1_cy_cp2_cp0_cx_b=cp1*cy+cp2+cp0*cx;
				    double cp4_cy_cp5_cp3_cx_b=cp4*cy+cp5+cp3*cx;
				    for(int i2y=i_resolution-1;i2y>=0;i2y--){
					    double cp7_cy_1_cp6_cx  =cp7_cy_1_cp6_cx_b;
					    double cp1_cy_cp2_cp0_cx=cp1_cy_cp2_cp0_cx_b;
					    double cp4_cy_cp5_cp3_cx=cp4_cy_cp5_cp3_cx_b;
					    for(int i2x=i_resolution-1;i2x>=0;i2x--){
						    //1ピクセルを作成
						    double d=1/(cp7_cy_1_cp6_cx);
						    int x=(int)((cp1_cy_cp2_cp0_cx)*d);
						    int y=(int)((cp4_cy_cp5_cp3_cx)*d);
						    if(x<0){x=0;}else if(x>=in_w){x=in_w-1;}
						    if(y<0){y=0;}else if(y>=in_h){y=in_h-1;}
    						
						    i_in_reader.getPixel(x, y, rgb_tmp);
						    r+=rgb_tmp[0];
						    g+=rgb_tmp[1];
						    b+=rgb_tmp[2];
						    cp7_cy_1_cp6_cx+=cp6;
						    cp1_cy_cp2_cp0_cx+=cp0;
						    cp4_cy_cp5_cp3_cx+=cp3;
					    }
					    cp7_cy_1_cp6_cx_b+=cp7;
					    cp1_cy_cp2_cp0_cx_b+=cp1;
					    cp4_cy_cp5_cp3_cx_b+=cp4;
				    }
				    r/=res_pix;
				    g/=res_pix;
				    b/=res_pix;
				    pat_data[p]=((r&0xff)<<16)|((g&0xff)<<8)|((b&0xff));
				    p--;
			    }
		    }
		    return;
	    }
	    private void multiPixel_ANY(int pk_l,int pk_t,int in_w,int in_h,int i_resolution,double[] cpara,INyARRgbPixelReader i_in_reader,INyARRgbRaster o_out)
	    {
		    int res_pix=i_resolution*i_resolution;

		    int[] rgb_tmp = this.__pickFromRaster_rgb_tmp;
		    INyARRgbPixelReader out_reader=o_out.getRgbPixelReader();

		    //ピクセルリーダーを取得
		    double cp0=cpara[0];
		    double cp3=cpara[3];
		    double cp6=cpara[6];
		    double cp1=cpara[1];
		    double cp4=cpara[4];
		    double cp7=cpara[7];
		    double cp2=cpara[2];
		    double cp5=cpara[5];
    		
		    int out_w=o_out.getWidth();
		    int out_h=o_out.getHeight();
		    for(int iy=out_h-1;iy>=0;iy--){
			    //解像度分の点を取る。
			    for(int ix=out_w-1;ix>=0;ix--){
				    int r,g,b;
				    r=g=b=0;
				    int cy=pk_t+iy*i_resolution;
				    int cx=pk_l+ix*i_resolution;
				    double cp7_cy_1_cp6_cx_b  =cp7*cy+1.0+cp6*cx;
				    double cp1_cy_cp2_cp0_cx_b=cp1*cy+cp2+cp0*cx;
				    double cp4_cy_cp5_cp3_cx_b=cp4*cy+cp5+cp3*cx;
				    for(int i2y=i_resolution-1;i2y>=0;i2y--){
					    double cp7_cy_1_cp6_cx  =cp7_cy_1_cp6_cx_b;
					    double cp1_cy_cp2_cp0_cx=cp1_cy_cp2_cp0_cx_b;
					    double cp4_cy_cp5_cp3_cx=cp4_cy_cp5_cp3_cx_b;
					    for(int i2x=i_resolution-1;i2x>=0;i2x--){
						    //1ピクセルを作成
						    double d=1/(cp7_cy_1_cp6_cx);
						    int x=(int)((cp1_cy_cp2_cp0_cx)*d);
						    int y=(int)((cp4_cy_cp5_cp3_cx)*d);
						    if(x<0){x=0;}else if(x>=in_w){x=in_w-1;}
						    if(y<0){y=0;}else if(y>=in_h){y=in_h-1;}
    						
						    i_in_reader.getPixel(x, y, rgb_tmp);
						    r+=rgb_tmp[0];
						    g+=rgb_tmp[1];
						    b+=rgb_tmp[2];
						    cp7_cy_1_cp6_cx+=cp6;
						    cp1_cy_cp2_cp0_cx+=cp0;
						    cp4_cy_cp5_cp3_cx+=cp3;
					    }
					    cp7_cy_1_cp6_cx_b+=cp7;
					    cp1_cy_cp2_cp0_cx_b+=cp1;
					    cp4_cy_cp5_cp3_cx_b+=cp4;
				    }
				    out_reader.setPixel(ix,iy,r/res_pix,g/res_pix,b/res_pix);
			    }
		    }
		    return;
	    }	
    }



}
