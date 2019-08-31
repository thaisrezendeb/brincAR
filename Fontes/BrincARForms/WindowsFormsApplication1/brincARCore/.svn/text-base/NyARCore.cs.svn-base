using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using jp.nyatla.nyartoolkit.cs;
using jp.nyatla.nyartoolkit.cs.core;
using jp.nyatla.nyartoolkit.cs.detector;
using NyARToolkitCSUtils.Capture;
using NyARToolkitCSUtils.Direct3d;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace brincAR.brincARCore
{

    //Classe que armazena os dados dos marcadores detectados
    public class DetectedMarker
    {
        private int _markerId;
        public int markerID
        {
            get
            {
                return _markerId;
            }
            set
            {
                _markerId = value;
            }
        }

        private NyARDoublePoint3d _point3D;
        public NyARDoublePoint3d point3D
        {
            get
            {
                return _point3D;
            }
            set
            {
                _point3D = value;
            }
        }
    }


    //Classe que ordena os marcadores
    public class OrdenacaoPorX : IComparer
    {
        int IComparer.Compare(Object x, Object y)
        {
            DetectedMarker objetoA = (DetectedMarker)x;
            DetectedMarker objetoB = (DetectedMarker)y;
            return objetoB.point3D.x.CompareTo(objetoA.point3D.x);
        }
    }

    class NyARCore : CaptureListener
    {

        //Objetos utilizados para renderização
        private ColorCube _cube;
        private Device _device = null;
        private NyARSurface_XRGB32 _surface;
        private Matrix _trans_mat;
        private NyARTransMatResult __OnBuffer_nyar_transmat = new NyARTransMatResult();

        //Objetos do Mesh
        private Mesh spacemesh;
        private Material[] spacemeshmaterials;
        private Texture[] spacemeshtextures;
        private float spacemeshradius;
        private float scaling = 0.0005f;
        //private float scaling = 25.0f;


        public CaptureDevice cap;
        public DsBGRX32Raster raster;



        /// <summary>
        /// Prepara os parâmetros da câmera
        /// </summary>
        /// <param name="cap"></param>
        public void PrepareCapture(CaptureDevice cap)
        {
            cap.SetCaptureListener(this);
            cap.PrepareCapture(320, 240, 30);  //320x240, 30 fps
        }

        /// <summary>
        /// Inicia a captura da câmera
        /// </summary>
        /// <param name="cap"></param>
        public void startCapture(CaptureDevice cap)
        {
            cap.StartCapture();
        }

        /// <summary>
        /// Encerra a captura da câmera
        /// </summary>
        /// <param name="cap"></param>
        public void StopCapture(CaptureDevice cap)
        {
            cap.StopCapture();
        }

        public DsBGRX32Raster InstanceRaster(int videowidth, int videoheight)
        {
            raster = new DsBGRX32Raster(videowidth, videoheight);
            return raster;
        }



        public NyARCore()
        {
            CaptureDeviceList cl = new CaptureDeviceList();
            try
            {
                cap = cl[0];
            }
            catch (Exception e)
            {
                MessageBox.Show("Câmera não encontrada. Verifique se ela está instalada e configurada corretamente.", "Erro!");
                Environment.Exit(0);
            }


        }




        //Método executado a cada frame de vídeo
        public void OnBuffer(CaptureDevice i_sender, double i_sample_time, IntPtr i_buffer, int i_buffer_len)
        {


        }



        //Prepara o dispositivo 3D para a renderização dos objetos.
        /* Direct3Dデバイスを準備する関数
         */
        public Device PrepareD3dDevice(Control i_window)
        {
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Flip;
            pp.BackBufferFormat = Format.X8R8G8B8;
            pp.BackBufferCount = 1;
            pp.EnableAutoDepthStencil = true;
            pp.AutoDepthStencilFormat = DepthFormat.D16;
            CreateFlags fl_base = CreateFlags.FpuPreserve;

            try
            {
                return new Device(0, DeviceType.Hardware, i_window.Handle, fl_base | CreateFlags.HardwareVertexProcessing, pp);
            }
            catch (Exception ex1)
            {
                Debug.WriteLine(ex1.ToString());
                try
                {
                    return new Device(0, DeviceType.Hardware, i_window.Handle, fl_base | CreateFlags.SoftwareVertexProcessing, pp);
                }
                catch (Exception ex2)
                {
                    // 作成に失敗
                    Debug.WriteLine(ex2.ToString());
                    try
                    {
                        return new Device(0, DeviceType.Reference, i_window.Handle, fl_base | CreateFlags.SoftwareVertexProcessing, pp);
                    }
                    catch (Exception ex3)
                    {
                        throw ex3;
                    }
                }
            }
        }

        /*-----------------------------------------------------------------------------------------------*/
        /*-----------------------------------------------------------------------------------------------*/
        /*-----------------------------------------------------------------------------------------------*/
        /*-------------------------- TESTES DE MESH -----------------------------------------------------*/
        //Carrega o mesh, com suas texturas e materiais.
        private void LoadMesh(string filename, ref Mesh mesh, ref Material[] meshmaterials, ref Texture[] meshtextures, ref float meshradius)
        {
            ExtendedMaterial[] materialarray;
            mesh = Mesh.FromFile(filename, MeshFlags.Managed, this._device, out materialarray);

            if ((materialarray != null) && (materialarray.Length > 0))
            {
                meshmaterials = new Material[materialarray.Length];
                meshtextures = new Texture[materialarray.Length];

                for (int i = 0; i < materialarray.Length; i++)
                {
                    meshmaterials[i] = materialarray[i].Material3D;
                    meshmaterials[i].Ambient = meshmaterials[i].Diffuse;

                    if ((materialarray[i].TextureFilename != null) && (materialarray[i].TextureFilename != string.Empty))
                    {
                        meshtextures[i] = TextureLoader.FromFile(this._device, materialarray[i].TextureFilename);
                    }
                }
            }

            mesh = mesh.Clone(mesh.Options.Value, CustomVertex.PositionNormalTextured.Format, this._device);
            mesh.ComputeNormals();

            VertexBuffer vertices = mesh.VertexBuffer;
            GraphicsStream stream = vertices.Lock(0, 0, LockFlags.None);
            Vector3 meshcenter;
            meshradius = Geometry.ComputeBoundingSphere(stream, mesh.NumberVertices, mesh.VertexFormat, out meshcenter) * scaling;
            vertices.Unlock();

        }

        //Carrega o mesh de acordo com o arquivo no disco
        public void LoadMeshes()
        {
            string file = "x/xwing.x";
            //string file = "x/BOLA.x";
            //string file = "x/01.x";
            //string file = "x/cubo.x";
            //string file = "x/boxman.x";
            LoadMesh(file, ref spacemesh, ref spacemeshmaterials, ref spacemeshtextures, ref spacemeshradius);
        }

        //Renderização do objeto Mesh
        private void DrawMesh(Mesh mesh, Material[] meshmaterials, Texture[] meshtextures)
        {
            for (int i = 0; i < meshmaterials.Length; i++)
            {
                this._device.Material = meshmaterials[i];
                this._device.SetTexture(0, meshtextures[i]);
                mesh.DrawSubset(i);
            }
        }
    }
}
