﻿/* 
 * PROJECT: NyARToolkitCS
 * --------------------------------------------------------------------------------
 * This work is based on the original ARToolKit developed by
 *   Hirokazu Kato
 *   Mark Billinghurst
 *   HITLab, University of Washington, Seattle
 * http://www.hitl.washington.edu/artoolkit/
 *
 * The NyARToolkitCS is C# edition ARToolKit class library.
 * Copyright (C)2008-2009 Ryo Iizuka
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * For further information please contact.
 *	http://nyatla.jp/nyatoolkit/
 *	<airmail(at)ebony.plala.or.jp> or <nyatla(at)nyatla.jp>
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace jp.nyatla.nyartoolkit.cs.core
{
    /**
     * INyARMatchPattのRGBColor差分データを格納するクラスです。
     *
     */
    public class NyARMatchPattDeviationColorData
    {
	    private int[] _data;
	    private double _pow;
	    private NyARIntSize _size;
	    //
	    private int _optimize_for_mod;
	    public int[] refData()
	    {
		    return this._data;
	    }
	    public double getPow()
	    {
		    return this._pow;
	    }
    	                  
	    public NyARMatchPattDeviationColorData(int i_width,int i_height)
	    {
		    this._size=new NyARIntSize(i_width,i_height);
		    int number_of_pix=this._size.w*this._size.h;
		    this._data=new int[number_of_pix*3];
		    this._optimize_for_mod=number_of_pix-(number_of_pix%8);	
		    return;
	    }

    	
	    /**
	     * NyARRasterからパターンデータをセットします。
	     * この関数は、データを元に所有するデータ領域を更新します。
	     * @param i_buffer
	     * @throws NyARException 
	     */
	    public void setRaster(INyARRgbRaster i_raster)
	    {
		    Debug.Assert(i_raster.getSize().isEqualSize(this._size));
		    switch(i_raster.getBufferType())
		    {
		    case NyARBufferType.INT1D_X8R8G8B8_32:
			    this._pow=setRaster_INT1D_X8R8G8B8_32((int[])i_raster.getBuffer(),this._size.w*this._size.h,this._optimize_for_mod,this._data);
			    break;
		    default:
			    this._pow=setRaster_ANY(i_raster.getRgbPixelReader(),this._size,this._size.w*this._size.h,this._data);
			    break;
		    }
		    return;
	    }
	    /**
	     * 回転方向を指定してラスタをセットします。
	     * @param i_reader
	     * @param i_direction
	     * 右上の位置です。0=1象限、1=2象限、、2=3象限、、3=4象限の位置に対応します。
	     * @throws NyARException
	     */
	    public void setRaster(INyARRgbRaster i_raster,int i_direction)
	    {
		    int width=this._size.w;
		    int height=this._size.h;
		    int i_number_of_pix=width*height;
		    INyARRgbPixelReader reader=i_raster.getRgbPixelReader();
		    int[] rgb=new int[3];
		    int[] dout=this._data;
		    int ave;//<PV/>
		    //<平均値計算>
		    ave = 0;
		    for(int y=height-1;y>=0;y--){
			    for(int x=width-1;x>=0;x--){
				    reader.getPixel(x,y,rgb);
				    ave += rgb[0]+rgb[1]+rgb[2];
			    }
		    }
		    //<平均値計算>
		    ave=i_number_of_pix*255*3-ave;
		    ave =255-(ave/ (i_number_of_pix * 3));//(255-R)-ave を分解するための事前計算

		    int sum = 0,w_sum;
		    int input_ptr=i_number_of_pix*3-1;
		    switch(i_direction)
		    {
		    case 0:
			    for(int y=height-1;y>=0;y--){
				    for(int x=width-1;x>=0;x--){
					    reader.getPixel(x,y,rgb);
					    w_sum = (ave - rgb[2]) ;dout[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
					    w_sum = (ave - rgb[1]) ;dout[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
					    w_sum = (ave - rgb[0]) ;dout[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
				    }
			    }
			    break;
		    case 1:
			    for(int x=0;x<width;x++){
				    for(int y=height-1;y>=0;y--){
					    reader.getPixel(x,y,rgb);
					    w_sum = (ave - rgb[2]) ;dout[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
					    w_sum = (ave - rgb[1]) ;dout[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
					    w_sum = (ave - rgb[0]) ;dout[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
				    }
			    }
			    break;
		    case 2:
			    for(int y=0;y<height;y++){
				    for(int x=0;x<width;x++){
					    reader.getPixel(x,y,rgb);
					    w_sum = (ave - rgb[2]) ;dout[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
					    w_sum = (ave - rgb[1]) ;dout[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
					    w_sum = (ave - rgb[0]) ;dout[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
				    }
			    }
			    break;
		    case 3:
			    for(int x=width-1;x>=0;x--){
				    for(int y=0;y<height;y++){
					    reader.getPixel(x,y,rgb);
					    w_sum = (ave - rgb[2]) ;dout[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
					    w_sum = (ave - rgb[1]) ;dout[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
					    w_sum = (ave - rgb[0]) ;dout[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
				    }
			    }
			    break;
    			
		    }
		    //<差分値計算>
		    //<差分値計算(FORの1/8展開)/>
		    double p=Math.Sqrt((double) sum);
		    this._pow=(p!=0.0?p:0.0000001);
	    }	
    	
    	
    	
    	
    	
	    /**
	     * INT1D_X8R8G8B8_32形式の入力ドライバ。
	     * @param i_buf
	     * @param i_number_of_pix
	     * @param i_for_mod
	     * @param o_out
	     * pow値
	     * @return
	     */
	    private static double setRaster_INT1D_X8R8G8B8_32(int[] i_buf,int i_number_of_pix,int i_for_mod,int[] o_out)
	    {
		    //i_buffer[XRGB]→差分[R,G,B]変換			
		    int i;
		    int ave;//<PV/>
		    int rgb;//<PV/>
		    //<平均値計算(FORの1/8展開)>
		    ave = 0;
		    for(i=i_number_of_pix-1;i>=i_for_mod;i--){
			    rgb = i_buf[i];ave += ((rgb >> 16) & 0xff) + ((rgb >> 8) & 0xff) + (rgb & 0xff);
		    }
		    for (;i>=0;) {
			    rgb = i_buf[i];ave += ((rgb >> 16) & 0xff) + ((rgb >> 8) & 0xff) + (rgb & 0xff);i--;
			    rgb = i_buf[i];ave += ((rgb >> 16) & 0xff) + ((rgb >> 8) & 0xff) + (rgb & 0xff);i--;
			    rgb = i_buf[i];ave += ((rgb >> 16) & 0xff) + ((rgb >> 8) & 0xff) + (rgb & 0xff);i--;
			    rgb = i_buf[i];ave += ((rgb >> 16) & 0xff) + ((rgb >> 8) & 0xff) + (rgb & 0xff);i--;
			    rgb = i_buf[i];ave += ((rgb >> 16) & 0xff) + ((rgb >> 8) & 0xff) + (rgb & 0xff);i--;
			    rgb = i_buf[i];ave += ((rgb >> 16) & 0xff) + ((rgb >> 8) & 0xff) + (rgb & 0xff);i--;
			    rgb = i_buf[i];ave += ((rgb >> 16) & 0xff) + ((rgb >> 8) & 0xff) + (rgb & 0xff);i--;
			    rgb = i_buf[i];ave += ((rgb >> 16) & 0xff) + ((rgb >> 8) & 0xff) + (rgb & 0xff);i--;
		    }
		    //<平均値計算(FORの1/8展開)/>
		    ave=i_number_of_pix*255*3-ave;
		    ave =255-(ave/ (i_number_of_pix * 3));//(255-R)-ave を分解するための事前計算

		    int sum = 0,w_sum;
		    int input_ptr=i_number_of_pix*3-1;
		    //<差分値計算(FORの1/8展開)>
		    for (i = i_number_of_pix-1; i >= i_for_mod;i--) {
			    rgb = i_buf[i];
			    w_sum = (ave - (rgb & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
			    w_sum = (ave - ((rgb >> 8) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
			    w_sum = (ave - ((rgb >> 16) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
		    }
		    for (; i >=0;) {
			    rgb = i_buf[i];i--;
			    w_sum = (ave - (rgb & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
			    w_sum = (ave - ((rgb >> 8) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
			    w_sum = (ave - ((rgb >> 16) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
			    rgb = i_buf[i];i--;
			    w_sum = (ave - (rgb & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
			    w_sum = (ave - ((rgb >> 8) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
			    w_sum = (ave - ((rgb >> 16) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
			    rgb = i_buf[i];i--;
			    w_sum = (ave - (rgb & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
			    w_sum = (ave - ((rgb >> 8) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
			    w_sum = (ave - ((rgb >> 16) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
			    rgb = i_buf[i];i--;
			    w_sum = (ave - (rgb & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
			    w_sum = (ave - ((rgb >> 8) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
			    w_sum = (ave - ((rgb >> 16) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
			    rgb = i_buf[i];i--;
			    w_sum = (ave - (rgb & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
			    w_sum = (ave - ((rgb >> 8) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
			    w_sum = (ave - ((rgb >> 16) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
			    rgb = i_buf[i];i--;
			    w_sum = (ave - (rgb & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
			    w_sum = (ave - ((rgb >> 8) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
			    w_sum = (ave - ((rgb >> 16) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
			    rgb = i_buf[i];i--;
			    w_sum = (ave - (rgb & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
			    w_sum = (ave - ((rgb >> 8) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
			    w_sum = (ave - ((rgb >> 16) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
			    rgb = i_buf[i];i--;
			    w_sum = (ave - (rgb & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
			    w_sum = (ave - ((rgb >> 8) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
			    w_sum = (ave - ((rgb >> 16) & 0xff)) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
		    }
		    //<差分値計算(FORの1/8展開)/>
		    double p=Math.Sqrt((double) sum);
		    return p!=0.0?p:0.0000001;
	    }
	    /**
	     * ANY形式の入力ドライバ。
	     * @param i_buf
	     * @param i_number_of_pix
	     * @param i_for_mod
	     * @param o_out
	     * pow値
	     * @return
	     * @throws NyARException 
	     */
	    private static double setRaster_ANY(INyARRgbPixelReader i_reader,NyARIntSize i_size,int i_number_of_pix,int[] o_out)
	    {
		    int width=i_size.w;
		    int[] rgb=new int[3];
		    int ave;//<PV/>
		    //<平均値計算>
		    ave = 0;
		    for(int y=i_size.h-1;y>=0;y--){
			    for(int x=width-1;x>=0;x--){
				    i_reader.getPixel(x,y,rgb);
				    ave += rgb[0]+rgb[1]+rgb[2];
			    }
		    }
		    //<平均値計算>
		    ave=i_number_of_pix*255*3-ave;
		    ave =255-(ave/ (i_number_of_pix * 3));//(255-R)-ave を分解するための事前計算

		    int sum = 0,w_sum;
		    int input_ptr=i_number_of_pix*3-1;
		    //<差分値計算>
		    for(int y=i_size.h-1;y>=0;y--){
			    for(int x=width-1;x>=0;x--){
				    i_reader.getPixel(x,y,rgb);
				    w_sum = (ave - rgb[2]) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//B
				    w_sum = (ave - rgb[1]) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//G
				    w_sum = (ave - rgb[0]) ;o_out[input_ptr--] = w_sum;sum += w_sum * w_sum;//R
			    }
		    }
		    //<差分値計算(FORの1/8展開)/>
		    double p=Math.Sqrt((double) sum);
		    return p!=0.0?p:0.0000001;
	    }	
    }
}


