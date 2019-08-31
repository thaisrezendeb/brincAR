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
using ServicesBrincAR;
using ServicesBrincAR.services.obj;

namespace brincAR.brincARCore
{


    //Classe principal do trabalho da NyARToolkit e a Aplicação.
    class NyARWordsGameCore : CaptureListener
    {

        //Parâmetros da AR
        NyARCore Core = new NyARCore();

        //Objetos de visualização
        PictureBox pbxNyAR;
        Label lbNyAR;

        //Objetos do jogo
        Game gmNyAR;

        //Caminho do PATT marcador de todas as letras
        private const String AR_CODE_FILE_KANJI = "data/patt.kanji";
        private const String AR_CODE_FILE_A = "data/A.patt";
        private const String AR_CODE_FILE_B = "data/B.patt";
        private const String AR_CODE_FILE_C = "data/C.patt";
        private const String AR_CODE_FILE_D = "data/D.patt";
        private const String AR_CODE_FILE_E = "data/E.patt";
        private const String AR_CODE_FILE_F = "data/F.patt";
        private const String AR_CODE_FILE_G = "data/G.patt";
        private const String AR_CODE_FILE_H = "data/H.patt";
        private const String AR_CODE_FILE_I = "data/I.patt";
        private const String AR_CODE_FILE_J = "data/J.patt";
        private const String AR_CODE_FILE_K = "data/K.patt";
        private const String AR_CODE_FILE_L = "data/L.patt";
        private const String AR_CODE_FILE_M = "data/M.patt";
        private const String AR_CODE_FILE_N = "data/N.patt";
        private const String AR_CODE_FILE_O = "data/O.patt";
        private const String AR_CODE_FILE_P = "data/P.patt";
        private const String AR_CODE_FILE_Q = "data/Q.patt";
        private const String AR_CODE_FILE_R = "data/R.patt";
        private const String AR_CODE_FILE_S = "data/S.patt";
        private const String AR_CODE_FILE_T = "data/T.patt";
        private const String AR_CODE_FILE_U = "data/U.patt";
        private const String AR_CODE_FILE_V = "data/V.patt";
        private const String AR_CODE_FILE_W = "data/W.patt";
        private const String AR_CODE_FILE_X = "data/X.patt";
        private const String AR_CODE_FILE_Y = "data/Y.patt";
        private const String AR_CODE_FILE_Z = "data/Z.patt";
        private const String AR_CAMERA_FILE = "data/camera_para.dat";


        //Array de letras utilizado para formação de palavras
        private string[] arrayLetters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
                                         "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};

        //Detector das letras
        private NyARDetectMarker markerDetector;

        //Detector e controles do marcador Kanji
        private NyARSingleDetectMarker kanjiDetector;
        private bool blnKanji = false;
        private int kanjiCounter = 0;

        //Controle de refresh do detector
        int intRefresh = 0;

        private int i = 0;

        //Listas de matriz de resultado e detecção
        ArrayList detectedMarkersList;
        DetectedMarker detectedMarker;

        //Objetos utilizados para renderização
        private ColorCube _cube;
        private Device device = null;
        private NyARSurface_XRGB32 _surface;
        private Matrix _trans_mat;
        private NyARTransMatResult __OnBuffer_nyar_transmat = new NyARTransMatResult();

        //Lista de mesh
        private string meshBall = "x/bola.x";
        private string meshCat = "x/gato.x";
        private string meshFish = "x/peixe.x";
        private string meshFork = "x/garfo2.x";
        private string meshLion = "x/leao.x";
        private string meshPen = "x/caneta.x";
        private string meshPot = "x/panela.x";
        private string meshRadio = "x/radio.x";
        private string meshScissor = "x/tesoura.x";
        private string meshKnife = "x/faca.x";
        private string meshSword = "x/espada.x";
        private string meshTable = "x/mesa.x";
        private string meshDish = "x/pratook.x";
        

        //Lista de escalas dos Meshs (cada um precisa de uma escala específica para plotar em um padrão)
        private float[] scaleArray = { 0.5f, float.Parse("1.0"), 0.5f, 0.15f, 0.75f, 0.15f, float.Parse("1.0"), float.Parse("1.0"), float.Parse("4.0"), float.Parse("3.0"), 0.25f, 0.95f, float.Parse("2.0") };

        //private float scaling = 0.0005f;
        private float scaling = float.Parse("7.0");

        //Listas de Objetos Mesh
        private List<Mesh> listSpacemesh = new List<Mesh>();
        private List<Material[]> listSpacemeshmaterials = new List<Material[]>();
        private List<Texture[]> listSpacemeshtextures = new List<Texture[]>();
        private List<float> listSpacemeshradius = new List<float>();
        private List<string> listFilemesh = new List<string>();

        //Arquivos auxiliares para a lista de Meshs
        private Mesh tempSpacemesh;
        private Material[] tempSpacemeshmaterials;
        private Texture[] tempSpacemeshtextures;
        private float tempSpacemeshradius;

        public NyARWordsGameCore(Label lbl, Game gm, PictureBox pbx)
        {
            //Instanciando o PictureBox com o recebido do Form
            pbxNyAR = pbx;
            lbNyAR = lbl;
            gmNyAR = gm;
            gmNyAR.Points = "0";


            //Parâmetros da RA
            NyARParam ap = new NyARParam();
            ap.loadARParamFromFile(AR_CAMERA_FILE);   //Lendo arquivo .dat
            ap.changeScreenSize(640, 480);            //Setando tamanho da câmera.

            //Lendo padrão do marcador
            NyARCode codeKanji = new NyARCode(16, 16);
            NyARCode codeA = new NyARCode(16, 16);
            NyARCode codeB = new NyARCode(16, 16);
            NyARCode codeC = new NyARCode(16, 16);
            NyARCode codeD = new NyARCode(16, 16);
            NyARCode codeE = new NyARCode(16, 16);
            NyARCode codeF = new NyARCode(16, 16);
            NyARCode codeG = new NyARCode(16, 16);
            NyARCode codeH = new NyARCode(16, 16);
            NyARCode codeI = new NyARCode(16, 16);
            NyARCode codeJ = new NyARCode(16, 16);
            NyARCode codeK = new NyARCode(16, 16);
            NyARCode codeL = new NyARCode(16, 16);
            NyARCode codeM = new NyARCode(16, 16);
            NyARCode codeN = new NyARCode(16, 16);
            NyARCode codeO = new NyARCode(16, 16);
            NyARCode codeP = new NyARCode(16, 16);
            NyARCode codeQ = new NyARCode(16, 16);
            NyARCode codeR = new NyARCode(16, 16);
            NyARCode codeS = new NyARCode(16, 16);
            NyARCode codeT = new NyARCode(16, 16);
            NyARCode codeU = new NyARCode(16, 16);
            NyARCode codeV = new NyARCode(16, 16);
            NyARCode codeW = new NyARCode(16, 16);
            NyARCode codeX = new NyARCode(16, 16);
            NyARCode codeY = new NyARCode(16, 16);
            NyARCode codeZ = new NyARCode(16, 16);

            //Carregando os arquivos dos marcadores no NyARCode
            codeKanji.loadARPattFromFile(AR_CODE_FILE_KANJI);
            codeA.loadARPattFromFile(AR_CODE_FILE_A);
            codeB.loadARPattFromFile(AR_CODE_FILE_B);
            codeC.loadARPattFromFile(AR_CODE_FILE_C);
            codeD.loadARPattFromFile(AR_CODE_FILE_D);
            codeE.loadARPattFromFile(AR_CODE_FILE_E);
            codeF.loadARPattFromFile(AR_CODE_FILE_F);
            codeG.loadARPattFromFile(AR_CODE_FILE_G);
            codeH.loadARPattFromFile(AR_CODE_FILE_H);
            codeI.loadARPattFromFile(AR_CODE_FILE_I);
            codeJ.loadARPattFromFile(AR_CODE_FILE_J);
            codeK.loadARPattFromFile(AR_CODE_FILE_K);
            codeL.loadARPattFromFile(AR_CODE_FILE_L);
            codeM.loadARPattFromFile(AR_CODE_FILE_M);
            codeN.loadARPattFromFile(AR_CODE_FILE_N);
            codeO.loadARPattFromFile(AR_CODE_FILE_O);
            codeP.loadARPattFromFile(AR_CODE_FILE_P);
            codeQ.loadARPattFromFile(AR_CODE_FILE_Q);
            codeR.loadARPattFromFile(AR_CODE_FILE_R);
            codeS.loadARPattFromFile(AR_CODE_FILE_S);
            codeT.loadARPattFromFile(AR_CODE_FILE_T);
            codeU.loadARPattFromFile(AR_CODE_FILE_U);
            codeV.loadARPattFromFile(AR_CODE_FILE_V);
            codeW.loadARPattFromFile(AR_CODE_FILE_W);
            codeX.loadARPattFromFile(AR_CODE_FILE_X);
            codeY.loadARPattFromFile(AR_CODE_FILE_Y);
            codeZ.loadARPattFromFile(AR_CODE_FILE_Z);

            double[] widthArray = { 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0,
                                    80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0,
                                    80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0};

            NyARCode[] arrayCodes = { codeKanji, codeA, codeB, codeC, codeD, codeE, codeF, codeG, codeH, codeI, 
                                      codeJ, codeK, codeL, codeM, codeN, codeO, codeP, codeQ, codeR, codeS, 
                                      codeT, codeU, codeV, codeW, codeX, codeY, codeZ};

            //Parâmetros da câmera
            try
            {
                Core.cap.SetCaptureListener(this);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro ao estabelecer conexão com a câmera. Verifique se ela está instalada corretamente. Favor, reinicie a aplicação.","Erro!");
                Environment.Exit(0);
            }

            Core.cap.PrepareCapture(640, 480, 30);
            


            //Create a cluster - Instanciando o Raster, passando os parâmetros do vídeo encontrado no CaptureDevice
            Core.raster = Core.InstanceRaster(Core.cap.video_width, Core.cap.video_height);


            //Instanciando do detector único do marcador Kanji
            this.kanjiDetector = new NyARSingleDetectMarker(ap, codeKanji, 80.0, Core.raster.getBufferType());
            this.kanjiDetector.setContinueMode(true);

            //Instanciando o detector de todas as letras
            this.markerDetector = new NyARDetectMarker(ap, arrayCodes, widthArray, 27, Core.raster.getBufferType());
            this.markerDetector.setContinueMode(true);

            //Inicia a captura
            try
            {
                Core.cap.StartCapture();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no estabelecimento da conexão com a câmera. Verifique se ela está instalada corretamente. Favor, reinicie a aplicação.","Erro!");
                Environment.Exit(0);
            }


            //Abaixo, são as funções que preparam todo o dispositivo e cenário para renderização 3D
            //Necessário descobrir mais detalhes do funcionamento.

            //Preparing the 3D device
            this.device = Core.PrepareD3dDevice(pbx); //Prepara o dispositivo em cima do PictureBox
            this.device.RenderState.ZBufferEnable = true;
            this.device.RenderState.Lighting = false;

            //Projection Camera Settings
            Matrix tmp = new Matrix();
            NyARD3dUtil.toCameraFrustumRH(ap.getPerspectiveProjectionMatrix(), ap.getScreenSize(), 1, 10, 10000, ref tmp);
            this.device.Transform.Projection = tmp;

            // View conversion settings (set in the left-handed view matrix).
            //From 0,0,0, and facing Z +, Y-axis direction on
            this.device.Transform.View = Matrix.LookAtLH(
                new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 1.0f, 0.0f));
            Viewport vp = new Viewport();
            vp.X = 0;
            vp.Y = 0;
            vp.Height = ap.getScreenSize().h;
            vp.Width = ap.getScreenSize().w;
            vp.MaxZ = 1.0f;
            //Set viewport
            this.device.Viewport = vp;

            //Draw a color cube instance - Instância de um cubo colorido, padrão da biblioteca NyAR
            this._cube = new ColorCube(this.device, 40);

            //Create a background surface
            this._surface = new NyARSurface_XRGB32(this.device, 640, 480);

            //Carregando os meshs (objetos 3D)
            LoadMeshes();

        }

        public void startCapture()
        {
            Core.cap.SetCaptureListener(this);
            Core.cap.PrepareCapture(640, 480, 30);
            Core.cap.StartCapture();
        }

        public void stopCapture()
        {
            Core.cap.StopCapture();
        }



        //Método executado a cada frame de vídeo
        public void OnBuffer(CaptureDevice i_sender, double i_sample_time, IntPtr i_buffer, int i_buffer_len)
        {

            try
            {


                i++;
                int w = i_sender.video_width;
                int h = i_sender.video_height;
                int s = w * (i_sender.video_bit_count / 8);
                Matrix trans_matrix = new Matrix();
                NyARTransMatResult nyar_transmat = this.__OnBuffer_nyar_transmat;

                Bitmap b = new Bitmap(w, h, s, PixelFormat.Format32bppRgb, i_buffer);

                // If the image is upsidedown
                b.RotateFlip(RotateFlipType.RotateNoneFlipY);
                this.pbxNyAR.Image = b;

                //Calculation of the AR - Seta o frame do vídeo no objeto Raster
                Core.raster.setBuffer(i_buffer, i_sender.video_vertical_flip);

                //Instância dos objetos de detecção
                detectedMarkersList = new ArrayList();
                NyARTransMatResult transMatrix;
                NyARDoublePoint3d point3D;


                if (this.blnKanji == false)   //Controle Kanji. FALSE: Palavra não formada // TRUE: Palavra formada, pronto para plotar
                {

                    //Detecção
                    int totalMarkers = this.markerDetector.detectMarkerLite(Core.raster, 100);
                    int markerId = 0;
                    string palavraFormada = null;

                    NyARTransMatResult transMatKanji = new NyARTransMatResult();

                    //Caso encontrar marcadores
                    if (totalMarkers > 0)
                    {
                        for (int counter = 0; counter < totalMarkers; counter++)
                        {
                            markerId = this.markerDetector.getARCodeIndex(counter);

                            if (this.markerDetector.getConfidence(counter) > 0.60)
                            {

                                if (markerId == 0)
                                {
                                    this.markerDetector.getTransmationMatrix(markerId, transMatKanji);
                                }
                                else
                                {

                                    //Recupera o ângulo XYZ do marcador e salva na lista.
                                    transMatrix = new NyARTransMatResult();
                                    point3D = new NyARDoublePoint3d();
                                    this.markerDetector.getTransmationMatrix(counter, transMatrix);
                                    transMatrix.getZXYAngle(point3D);


                                    detectedMarker = new DetectedMarker();
                                    detectedMarker.markerID = markerId;
                                    detectedMarker.point3D = point3D;
                                    detectedMarkersList.Add(detectedMarker);
                                }

                            }
                        }
                    }


                    //Realiza os cálculos da RA caso encontrar algum marcador válido
                    if (detectedMarkersList.Count > 0)
                    {

                        //Ordena a lista de acordo com a posição
                        detectedMarkersList.Sort(new OrdenacaoPorX());

                        //Monta a palavra de acordo com os marcadores detectados
                        foreach (DetectedMarker item in detectedMarkersList)
                        {
                            palavraFormada += this.arrayLetters[item.markerID - 1];
                        }

                        if (detectedMarkersList.Count == 3)
                            totalMarkers = totalMarkers + 0;



                        //Teste - Verifica o número de letras correspondentes
                        string originalWord = gmNyAR.SelectedObject;
                        int total = verificaTotalPalavraFormada(detectedMarkersList, originalWord);
                        if (total == originalWord.Length)
                        {
                            this.blnKanji = true;
                            this.kanjiCounter = 0; //Refresh do contador
                            lbNyAR.Text = "Palavra correta!";
                        }
                        else
                        {
                            if (intRefresh == 0)
                            {
                                if (total == 1)
                                    lbNyAR.Text = "Você acertou " + total + " letra! ";
                                else
                                    lbNyAR.Text = "Você acertou " + total + " letras! ";
                            }

                        }

                        // Realiza a gravação da pontuação
                        int points = (total / originalWord.Length) * 100;

                        if (points > int.Parse(gmNyAR.Points))
                            gmNyAR.Points = points.ToString();

                        //Refresh da label que indica a quantidade de letras corretas.
                        intRefresh++;
                        if (intRefresh > 20)
                        {
                            intRefresh = 0;
                        }

                    }


                }
                else
                {
                    //Desenho do objeto 3D no marcador Kanji.
                    bool kanji = this.kanjiDetector.detectMarkerLite(Core.raster);
                    if (kanji)
                    {
                        NyARTransMatResult result = new NyARTransMatResult();
                        if (this.kanjiDetector.getConfidence() > 0.50)
                        {
                            this.kanjiDetector.getTransmationMatrix(result);
                            DrawWord(gmNyAR.SelectedObject, result);
                        }
                    }
                    this.kanjiCounter++;
                    if (this.kanjiCounter > 50)
                    {
                        this.blnKanji = false;
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro na verficação dos marcadores. Favor, reinicie a aplicação. Se o erro persistir, contate os desenvolvedores.","Erro!");
                Environment.Exit(0);
                
            }

        }


        public int verificaTotalPalavraFormada(ArrayList listDetectedMarkers, string palavraOriginal)
        {
            //Verificação da palavra bola
            int total = 0;


            char[] palavraSplit = palavraOriginal.ToCharArray();
            bool[] palavraBool = new bool[palavraSplit.Length];

            for (int i = 0; i < palavraBool.Length; i++)
            {
                palavraBool[i] = false;
            }

            //Contadores do número de letras de acordo com a palavra
            foreach (DetectedMarker item in listDetectedMarkers)
            {
                //foreach (char item2 in palavraSplit)
                for (int i = 0; i < palavraSplit.Length; i++)
                {
                    if ((this.arrayLetters[item.markerID - 1] == palavraSplit[i].ToString()) && (palavraBool[i] == false))
                    {
                        total++;
                        palavraBool[i] = true;
                        break;
                    }
                }

            }

            return total;
        }

        void DrawWord(string word, NyARTransMatResult result)
        {
            NyARD3dUtil.toD3dCameraView(result, 1f, ref this._trans_mat);
            this._surface.CopyFromXRGB32(Core.raster);

            lock (pbxNyAR)
            {
                //Ajustando a escala para cada desenho
                switch (word)
                {
                    case "bola":
                        scaling = scaleArray[0];
                        break;
                    case "gato":
                        scaling = scaleArray[1];
                        break;
                    case "peixe":
                        scaling = scaleArray[2];
                        break;
                    case "garfo":
                        scaling = scaleArray[3];
                        break;
                    case "leao":
                        scaling = scaleArray[4];
                        break;
                    case "caneta":
                        scaling = scaleArray[5];
                        break;
                    case "panela":
                        scaling = scaleArray[6];
                        break;
                    case "radio":
                        scaling = scaleArray[7];
                        break;
                    case "tesoura":
                        scaling = scaleArray[8];
                        break;
                    case "faca":
                        scaling = scaleArray[9];
                        break;
                    case "espada":
                        scaling = scaleArray[10];
                        break;
                    case "mesa":
                        scaling = scaleArray[11];
                        break;
                    case "prato":
                        scaling = scaleArray[12];
                        break;

                }
                // Background directly to the surface drawing
                Surface dest_surface = this.device.GetBackBuffer(0, 0, BackBufferType.Mono);
                Rectangle src_dest_rect = new Rectangle(0, 0, 543, 348);
                this.device.StretchRectangle(this._surface.d3d_surface, src_dest_rect, dest_surface, src_dest_rect, TextureFilter.None);

                // Drawing 3D objects from here
                this.device.BeginScene();
                this.device.Clear(ClearFlags.ZBuffer, Color.DarkBlue, 1.0f, 0);

                //MATRIZ QUE DEFINE O POSICIONAMENTO DO OBJETO 3D
                //On 20mm cube (top marker) to be shifted
                Matrix transform_mat2 = Matrix.Scaling(scaling, scaling, scaling);
                transform_mat2 *= Matrix.RotationX((float)Math.PI * 1 / 2);

                //Multiply the transformation matrix
                transform_mat2 *= this._trans_mat;
                // Coordinate transformation matrix was calculated
                this.device.SetTransform(TransformType.World, transform_mat2);

                // Rendering (drawing)
                //this._cube.draw(this._device);

                switch (word)
                {
                    case "bola":
                        DrawMesh(listSpacemesh[0], listSpacemeshmaterials[0], listSpacemeshtextures[0]);
                        break;
                    case "gato":
                        DrawMesh(listSpacemesh[1], listSpacemeshmaterials[1], listSpacemeshtextures[1]);
                        break;
                    case "peixe":
                        DrawMesh(listSpacemesh[2], listSpacemeshmaterials[2], listSpacemeshtextures[2]);
                        break;
                    case "garfo":
                        DrawMesh(listSpacemesh[3], listSpacemeshmaterials[3], listSpacemeshtextures[3]);
                        break;
                    case "leao":
                        DrawMesh(listSpacemesh[4], listSpacemeshmaterials[4], listSpacemeshtextures[4]);
                        break;
                    case "caneta":
                        DrawMesh(listSpacemesh[5], listSpacemeshmaterials[5], listSpacemeshtextures[5]);
                        break;
                    case "panela":
                        DrawMesh(listSpacemesh[6], listSpacemeshmaterials[6], listSpacemeshtextures[6]);
                        break;
                    case "radio":
                        DrawMesh(listSpacemesh[7], listSpacemeshmaterials[7], listSpacemeshtextures[7]);
                        break;
                    case "tesoura":
                        DrawMesh(listSpacemesh[8], listSpacemeshmaterials[8], listSpacemeshtextures[8]);
                        break;
                    case "faca":
                        DrawMesh(listSpacemesh[9], listSpacemeshmaterials[9], listSpacemeshtextures[9]);
                        break;
                    case "espada":
                        DrawMesh(listSpacemesh[10], listSpacemeshmaterials[10], listSpacemeshtextures[10]);
                        break;
                    case "mesa":
                        DrawMesh(listSpacemesh[11], listSpacemeshmaterials[11], listSpacemeshtextures[11]);
                        break;
                    case "prato":
                        DrawMesh(listSpacemesh[12], listSpacemeshmaterials[12], listSpacemeshtextures[12]);
                        break;
                }

                //Plota a imagem
                //DrawMesh(spacemesh, spacemeshmaterials, spacemeshtextures);

                // Drawing up here
                this.device.EndScene();

                // Drawing on the actual display
                this.device.Present();
            }
        }


        //Funções de desenho de Meshs
        private void LoadMesh(string filename, ref Mesh mesh, ref Material[] meshmaterials, ref Texture[] meshtextures, ref float meshradius)
        {
            ExtendedMaterial[] materialarray;
            mesh = Mesh.FromFile(filename, MeshFlags.Managed, this.device, out materialarray);

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
                        meshtextures[i] = TextureLoader.FromFile(this.device, @"x\" + materialarray[i].TextureFilename);
                    }
                }
            }

            mesh = mesh.Clone(mesh.Options.Value, CustomVertex.PositionNormalTextured.Format, this.device);
            mesh.ComputeNormals();

            VertexBuffer vertices = mesh.VertexBuffer;
            GraphicsStream stream = vertices.Lock(0, 0, LockFlags.None);
            Vector3 meshcenter;
            meshradius = Geometry.ComputeBoundingSphere(stream, mesh.NumberVertices, mesh.VertexFormat, out meshcenter) * scaling;
            vertices.Unlock();

        }

        //Carrega o mesh de acordo com o arquivo no disco
        //private void LoadMeshes(int control)
        private void LoadMeshes()
        {


            listFilemesh.Add(meshBall);
            listFilemesh.Add(meshCat);
            listFilemesh.Add(meshFish);
            listFilemesh.Add(meshFork);
            listFilemesh.Add(meshLion);
            listFilemesh.Add(meshPen);
            listFilemesh.Add(meshPot);
            listFilemesh.Add(meshRadio);
            listFilemesh.Add(meshScissor);
            //listFilemesh.Add(meshShoes);
            listFilemesh.Add(meshKnife);
            listFilemesh.Add(meshSword);
            listFilemesh.Add(meshTable);
            listFilemesh.Add(meshDish);


            for (int i = 0; i < listFilemesh.Count; i++)
            {
                LoadMesh(listFilemesh[i], ref tempSpacemesh, ref tempSpacemeshmaterials, ref tempSpacemeshtextures, ref tempSpacemeshradius);

                listSpacemesh.Add(tempSpacemesh);
                listSpacemeshmaterials.Add(tempSpacemeshmaterials);
                listSpacemeshtextures.Add(tempSpacemeshtextures);
                listSpacemeshradius.Add(tempSpacemeshradius);

            }

            //LoadMesh(file, ref spacemesh, ref spacemeshmaterials, ref spacemeshtextures, ref spacemeshradius);
        }

        //Renderização do objeto Mesh
        private void DrawMesh(Mesh mesh, Material[] meshmaterials, Texture[] meshtextures)
        {
            for (int i = 0; i < meshmaterials.Length; i++)
            {
                this.device.Material = meshmaterials[i];
                this.device.SetTexture(0, meshtextures[i]);
                mesh.DrawSubset(i);
            }
        }
    }
}
