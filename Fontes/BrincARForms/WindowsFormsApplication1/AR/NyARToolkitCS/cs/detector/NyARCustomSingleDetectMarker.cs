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
using jp.nyatla.nyartoolkit.cs.core;

namespace jp.nyatla.nyartoolkit.cs.detector
{
    /**
     * 画像からARCodeに最も一致するマーカーを1個検出し、その変換行列を計算するクラスです。
     * 変換行列を求めるには、detectMarkerLite関数にラスタイメージを入力して、計算対象の矩形を特定します。
     * detectMarkerLiteが成功すると、getTransmationMatrix等の関数が使用可能な状態になり、変換行列を求めることができます。
     * 
     * 
     */
    public class NyARCustomSingleDetectMarker
    {
	    /** 一致率*/
	    private double _confidence;
	    private NyARSquare _square=new NyARSquare();
    	
	    //参照インスタンス
	    private INyARRgbRaster _ref_raster;
	    //所有インスタンス
	    private INyARColorPatt _inst_patt;
	    private NyARMatchPattDeviationColorData _deviation_data;
	    private NyARMatchPatt_Color_WITHOUT_PCA _match_patt;
	    private NyARMatchPattResult __detectMarkerLite_mr=new NyARMatchPattResult();
	    private NyARCoord2Linear _coordline;
    	

	    private NyARIntPoint2d[] __ref_vertex=new NyARIntPoint2d[4];

	    /**
	     * 矩形が見付かるたびに呼び出されます。
	     * 発見した矩形のパターンを検査して、方位を考慮した頂点データを確保します。
	     */
	    public void updateSquareInfo(NyARIntCoordinates i_coord,int[] i_vertex_index)
	    {
		    NyARMatchPattResult mr=this.__detectMarkerLite_mr;
		    //輪郭座標から頂点リストに変換
		    NyARIntPoint2d[] vertex=this.__ref_vertex;	//C言語ならポインタ扱いで実装
		    vertex[0]=i_coord.items[i_vertex_index[0]];
		    vertex[1]=i_coord.items[i_vertex_index[1]];
		    vertex[2]=i_coord.items[i_vertex_index[2]];
		    vertex[3]=i_coord.items[i_vertex_index[3]];
    	
		    //画像を取得
		    if (!this._inst_patt.pickFromRaster(this._ref_raster,vertex)){
			    return;
		    }
		    //取得パターンをカラー差分データに変換して評価する。
		    this._deviation_data.setRaster(this._inst_patt);
		    if(!this._match_patt.evaluate(this._deviation_data,mr)){
			    return;
		    }
		    //現在の一致率より低ければ終了
		    if (this._confidence > mr.confidence){
			    return;
		    }
		    //一致率の高い矩形があれば、方位を考慮して頂点情報を作成
		    NyARSquare sq=this._square;
		    this._confidence = mr.confidence;
		    //directionを考慮して、squareを更新する。
		    for(int i=0;i<4;i++){
			    int idx=(i+4 - mr.direction) % 4;
			    this._coordline.coord2Line(i_vertex_index[idx],i_vertex_index[(idx+1)%4],i_coord,sq.line[i]);
		    }
		    //ちょっと、ひっくり返してみようか。
		    for (int i = 0; i < 4; i++) {
			    //直線同士の交点計算
			    if(!sq.line[i].crossPos(sq.line[(i + 3) % 4],sq.sqvertex[i])){
				    throw new NyARException();//ここのエラー復帰するならダブルバッファにすればOK
			    }
		    }
	    }


        private bool _is_continue = false;
	    private NyARSquareContourDetector _square_detect;
	    protected INyARTransMat _transmat;
	    //画処理用
	    private NyARBinRaster _bin_raster;
	    protected INyARRasterFilter_Rgb2Bin _tobin_filter;

	    private NyARRectOffset _offset; 


	    protected NyARCustomSingleDetectMarker()
	    {
		    return;
	    }
	    protected void initInstance(
		    INyARColorPatt i_patt_inst,
		    NyARSquareContourDetector i_sqdetect_inst,
		    INyARTransMat i_transmat_inst,
		    INyARRasterFilter_Rgb2Bin i_filter,
		    NyARParam	i_ref_param,
		    NyARCode	i_ref_code,
		    double		i_marker_width)
	    {
		    NyARIntSize scr_size=i_ref_param.getScreenSize();		
		    // 解析オブジェクトを作る
		    this._square_detect = i_sqdetect_inst;
		    this._transmat = i_transmat_inst;
		    this._tobin_filter=i_filter;
		    //２値画像バッファを作る
		    this._bin_raster=new NyARBinRaster(scr_size.w,scr_size.h);
		    //パターンの一致検索処理用
		    this._inst_patt=i_patt_inst;
		    this._deviation_data=new NyARMatchPattDeviationColorData(i_ref_code.getWidth(),i_ref_code.getHeight());
		    this._coordline=new NyARCoord2Linear(i_ref_param.getScreenSize(),i_ref_param.getDistortionFactor());
		    this._match_patt=new NyARMatchPatt_Color_WITHOUT_PCA(i_ref_code);
		    //オフセットを作成
		    this._offset=new NyARRectOffset();
		    this._offset.setSquare(i_marker_width);
		    return;
    		
	    }

    	

    	
	    /**
	     * i_imageにマーカー検出処理を実行し、結果を記録します。
	     * 
	     * @param i_raster
	     * マーカーを検出するイメージを指定します。イメージサイズは、カメラパラメータ
	     * と一致していなければなりません。
	     * @return マーカーが検出できたかを真偽値で返します。
	     * @throws NyARException
	     */
	    public bool detectMarkerLite(INyARRgbRaster i_raster)
	    {
		    //サイズチェック
		    if(!this._bin_raster.getSize().isEqualSize(i_raster.getSize())){
			    throw new NyARException();
		    }

		    //ラスタを２値イメージに変換する.
		    this._tobin_filter.doFilter(i_raster,this._bin_raster);

		    //コールバックハンドラの準備
		    this._confidence=0;
		    this._ref_raster=i_raster;

		    //矩形を探す(戻り値はコールバック関数で受け取る。)
		    this._square_detect.detectMarker(this._bin_raster);
		    if(this._confidence==0){
			    return false;
		    }
		    return true;
	    }
	    /**
	     * 検出したマーカーの変換行列を計算して、o_resultへ値を返します。
	     * 直前に実行したdetectMarkerLiteが成功していないと使えません。
	     * 
	     * @param o_result
	     * 変換行列を受け取るオブジェクトを指定します。
	     * @throws NyARException
	     */
	    public void getTransmationMatrix(NyARTransMatResult o_result)
	    {
		    // 一番一致したマーカーの位置とかその辺を計算
		    if (this._is_continue) {
			    this._transmat.transMatContinue(this._square,this._offset,o_result, o_result);
		    } else {
			    this._transmat.transMat(this._square,this._offset, o_result);
		    }
		    return;
	    }
	    /**
	     * 現在の矩形を返します。
	     * @return
	     */
	    public NyARSquare refSquare()
	    {
		    return this._square;
	    }
	    /**
	     * 検出したマーカーの一致度を返します。
	     * 
	     * @return マーカーの一致度を返します。0～1までの値をとります。 一致度が低い場合には、誤認識の可能性が高くなります。
	     * @throws NyARException
	     */
	    public double getConfidence()
	    {
		    return this._confidence;
	    }
	    /**
	     * getTransmationMatrixの計算モードを設定します。 初期値はTRUEです。
	     * 
	     * @param i_is_continue
	     * TRUEなら、transMatCont互換の計算をします。 FALSEなら、transMat互換の計算をします。
	     */
        public void setContinueMode(bool i_is_continue)
	    {
		    this._is_continue = i_is_continue;
	    }
	    /**
	     * プローブ関数
	     * @return
	     */
	    public Object[] _getProbe()
	    {
		    Object[] r=new Object[1];
		    r[0]=this._inst_patt;
		    return r;
	    }	
    	
    	
    	
    }

}
