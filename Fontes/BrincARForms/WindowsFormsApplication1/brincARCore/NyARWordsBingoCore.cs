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
    class NyARWordsBingoCore : CaptureListener
    {
        //Parâmetros da AR
        NyARCore Core = new NyARCore();

        //Objetos de visualização
        PictureBox pbxNyAR;
        Form frmNyAR;
        Label lbNyAR;

        //Objetos do jogo
        Game gmNyAR;
        int maxPoints = 0;

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

        //Caminho dps PATTs da cartela
        private const String AR_CODE_FILE_BALL = "data/patt_bingo/bola.patt";
        //private const String AR_CODE_FILE_BALL = "data/patt.bola";
        private const String AR_CODE_FILE_SWORD = "data/patt_bingo/espada.patt";
        private const String AR_CODE_FILE_CAR = "data/patt_bingo/carro2.patt";
        private const String AR_CODE_FILE_BRIGADIER = "data/patt_bingo/brigadeiro.patt";
        private const String AR_CODE_FILE_FORK = "data/patt_bingo/garfo.patt";
        private const String AR_CODE_FILE_PEN = "data/patt_bingo/caneta2.patt";
        private const String AR_CODE_FILE_FISH = "data/patt_bingo/peixe.patt";


        //Array de letras utilizado para formação de palavras
        private string[] arrayLetters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
                                         "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};

        //Detector das letras
        private NyARDetectMarker markerDetectorLetters;
        private NyARDetectMarker markerDetectorImages;

        //Detector único de cada marcador para facilitar o desenho 3D
        private NyARSingleDetectMarker ballMarkerDetector;
        private NyARSingleDetectMarker swordMarkerDetector;
        private NyARSingleDetectMarker carMarkerDetector;
        private NyARSingleDetectMarker brigadierlMarkerDetector;
        private NyARSingleDetectMarker forkMarkerDetector;
        private NyARSingleDetectMarker penMarkerDetector;
        private NyARSingleDetectMarker fishMarkerDetector;


        //Controles de Refresh
        private int intRefresh = 0;
        private int intDetected = 0;

        private int i = 0;

        //Listas de matriz de resultado e detecção
        ArrayList raffledLetters;
        ArrayList detectedMarkersListLetters;
        ArrayList detectedMarkersListImages;
        DetectedMarker detectedMarker;

        //Objetos utilizados para renderização
        private ColorCube _cube;
        private Device _device = null;
        private NyARSurface_XRGB32 _surface;
        private Matrix _trans_mat;
        private NyARTransMatResult __OnBuffer_nyar_transmat = new NyARTransMatResult();

        //Lista de mesh
        //private string meshFrog = "x/SAPO.x";
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
        //private string meshShoes = "x/sapato.x";
        private string meshSword = "x/espada.x";
        private string meshTable = "x/mesa.x";
        private string meshDish = "x/pratook.x";

        //Lista de escalas dos Meshs (cada um precisa de uma escala específica para plotar em um padrão)
        private float[] scaleArray = { 0.5f, float.Parse("1.0"), 0.5f, 0.15f, 0.75f, 0.15f, float.Parse("1.0"), float.Parse("1.0"), float.Parse("4.0"), float.Parse("3.0"), 0.25f, 0.95f, float.Parse("2.0") };

        ////Objetos do Mesh
        //private Mesh spacemesh;
        //private Material[] spacemeshmaterials;
        //private Texture[] spacemeshtextures;
        //private float spacemeshradius;
        private float scaling = float.Parse("0.5");

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

        public NyARWordsBingoCore(Label lbl, Game currentGame, PictureBox pbx)
        {
            //Instanciando o PictureBox com o recebido do Form
            pbxNyAR = pbx;
            lbNyAR = lbl;
            gmNyAR = currentGame;


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

            NyARCode codeBall = new NyARCode(16, 16);
            NyARCode codeSword = new NyARCode(16, 16);
            NyARCode codeCar = new NyARCode(16, 16);
            NyARCode codeBrigadier = new NyARCode(16, 16);
            NyARCode codeFork = new NyARCode(16, 16);
            NyARCode codePen = new NyARCode(16, 16);
            NyARCode codeFish = new NyARCode(16, 16);


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

            codeBall.loadARPattFromFile(AR_CODE_FILE_BALL);
            codeSword.loadARPattFromFile(AR_CODE_FILE_SWORD);
            codeCar.loadARPattFromFile(AR_CODE_FILE_CAR);
            codeBrigadier.loadARPattFromFile(AR_CODE_FILE_BRIGADIER);
            codeFork.loadARPattFromFile(AR_CODE_FILE_FORK);
            codePen.loadARPattFromFile(AR_CODE_FILE_PEN);
            codeFish.loadARPattFromFile(AR_CODE_FILE_FISH);



            double[] WidthArray = { 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0,
                                    80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0,
                                    80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0};

            NyARCode[] arrayCodes = { codeKanji, codeA, codeB, codeC, codeD, codeE, codeF, codeG, codeH, codeI, 
                                      codeJ, codeK, codeL, codeM, codeN, codeO, codeP, codeQ, codeR, codeS, 
                                      codeT, codeU, codeV, codeW, codeX, codeY, codeZ};

            double[] WidthArrayImages = { 80.0, 80.0, 80.0, 80.0, 80.0, 80.0, 80.0 };
            NyARCode[] arrayCodesImage = { codeBall, codeSword, codeCar, codeBrigadier, codeFork, codePen, codeFish };


            //Seta propriedades da câmera
            try
            {
                Core.cap.SetCaptureListener(this);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no estabelecimento da conexão com a câmera. Verifique se ela está instalada corretamente. Favor, reinicie a aplicação.","Erro!");
                Environment.Exit(0);
            }

            Core.cap.PrepareCapture(640, 480, 30);


            //Create a cluster - Instanciando o Raster, passando os parâmetros do vídeo encontrado no CaptureDevice
            Core.raster = Core.InstanceRaster(Core.cap.video_width, Core.cap.video_height);


            //Instanciando os detectores únicos
            this.ballMarkerDetector = new NyARSingleDetectMarker(ap, codeBall, 80.0, Core.raster.getBufferType());
            this.ballMarkerDetector.setContinueMode(true);

            this.brigadierlMarkerDetector = new NyARSingleDetectMarker(ap, codeBrigadier, 80.0, Core.raster.getBufferType());
            this.brigadierlMarkerDetector.setContinueMode(true);

            this.carMarkerDetector = new NyARSingleDetectMarker(ap, codeCar, 80.0, Core.raster.getBufferType());
            this.carMarkerDetector.setContinueMode(true);

            this.forkMarkerDetector = new NyARSingleDetectMarker(ap, codeFork, 80.0, Core.raster.getBufferType());
            this.forkMarkerDetector.setContinueMode(true);

            this.penMarkerDetector = new NyARSingleDetectMarker(ap, codePen, 80.0, Core.raster.getBufferType());
            this.penMarkerDetector.setContinueMode(true);

            this.swordMarkerDetector = new NyARSingleDetectMarker(ap, codeSword, 80.0, Core.raster.getBufferType());
            this.swordMarkerDetector.setContinueMode(true);

            this.fishMarkerDetector = new NyARSingleDetectMarker(ap, codeFish, 80.0, Core.raster.getBufferType());
            this.fishMarkerDetector.setContinueMode(true);



            //Instanciando o detector de todas as letras
            this.markerDetectorLetters = new NyARDetectMarker(ap, arrayCodes, WidthArray, 27, Core.raster.getBufferType());
            this.markerDetectorLetters.setContinueMode(true);

            //Instanciando o detector das imagens que representam a palavra na cartela
            this.markerDetectorImages = new NyARDetectMarker(ap, arrayCodesImage, WidthArrayImages, 7, Core.raster.getBufferType());
            this.markerDetectorImages.setContinueMode(true);


            //Inicia a captura
            //Inicia a captura
            try
            {
                Core.cap.StartCapture();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro no estabelecimento da conexão com a câmera. Verifique se ela está instalada corretamente.");
                Environment.Exit(0);
            }


            //Abaixo, são as funções que preparam todo o dispositivo e cenário para renderização 3D
            //Necessário descobrir mais detalhes do funcionamento.

            //Preparing the 3D device
            this._device = Core.PrepareD3dDevice(pbx); //Prepara o dispositivo em cima do PictureBox
            this._device.RenderState.ZBufferEnable = true;
            this._device.RenderState.Lighting = false;

            //Projection Camera Settings
            Matrix tmp = new Matrix();
            NyARD3dUtil.toCameraFrustumRH(ap.getPerspectiveProjectionMatrix(), ap.getScreenSize(), 1, 10, 10000, ref tmp);
            this._device.Transform.Projection = tmp;

            // View conversion settings (set in the left-handed view matrix).
            //From 0,0,0, and facing Z +, Y-axis direction on
            this._device.Transform.View = Matrix.LookAtLH(
                new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 1.0f, 0.0f));
            Viewport vp = new Viewport();
            vp.X = 0;
            vp.Y = 0;
            vp.Height = ap.getScreenSize().h;
            vp.Width = ap.getScreenSize().w;
            vp.MaxZ = 1.0f;
            //Set viewport
            this._device.Viewport = vp;

            //Draw a color cube instance - Instância de um cubo colorido, padrão da biblioteca NyAR
            this._cube = new ColorCube(this._device, 40);

            //Create a background surface
            this._surface = new NyARSurface_XRGB32(this._device, 640, 480);

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

        public void updateGame(Game gm)
        {
            gmNyAR = gm;
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
                detectedMarkersListLetters = new ArrayList();
                detectedMarkersListImages = new ArrayList();
                NyARTransMatResult transMatrix;
                NyARDoublePoint3d point3D;



                //Aqui vem a lógica do Bingo
                //1 - Detectar o marcador da cartela
                //2 - Verificar a relação de letras sorteadas e a palavra da cartela
                //3 - Se a palavra tiver todas as letras sorteadas, plotar o desenho específico
                //  - Senão, informa de algum jeito o total das letras, e qual falta, sei lá.

                //Verficar a palavra relacionada à cartela
                string originalWord = string.Empty;

                //Adiciona as letras sorteadas no ArrayList para comparação
                raffledLetters = new ArrayList();
                foreach (char item in gmNyAR.RaffleLetters)
                {
                    raffledLetters.Add(item);
                }



                if (intRefresh == 0)
                {

                    //Detecção das LETRAS
                    int totalMarkersLetter = this.markerDetectorLetters.detectMarkerLite(Core.raster, 100);
                    int markerLetterId = 0;
                    NyARTransMatResult transMatKanji = new NyARTransMatResult();

                    //Caso encontrar marcadores
                    if (totalMarkersLetter > 0)
                    {
                        for (int counter = 0; counter < totalMarkersLetter; counter++)
                        {
                            markerLetterId = this.markerDetectorLetters.getARCodeIndex(counter);

                            if (this.markerDetectorLetters.getConfidence(counter) > 0.60)
                            {

                                if (markerLetterId == 0)
                                {
                                    this.markerDetectorLetters.getTransmationMatrix(markerLetterId, transMatKanji);
                                }
                                else
                                {

                                    //Recupera o ângulo XYZ do marcador e salva na lista.
                                    transMatrix = new NyARTransMatResult();
                                    point3D = new NyARDoublePoint3d();
                                    this.markerDetectorLetters.getTransmationMatrix(counter, transMatrix);
                                    transMatrix.getZXYAngle(point3D);

                                    detectedMarker = new DetectedMarker();
                                    detectedMarker.markerID = markerLetterId;
                                    detectedMarker.point3D = point3D;
                                    detectedMarkersListLetters.Add(detectedMarker);

                                }

                            }
                        }
                    }

                    //Detecção dos marcadores específicos
                    int totalMarkerImages = this.markerDetectorImages.detectMarkerLite(Core.raster, 100);
                    int markerImageId = 0;

                    if (totalMarkerImages > 0)
                    {
                        for (int counter = 0; counter < totalMarkerImages; counter++)
                        {
                            markerImageId = this.markerDetectorImages.getARCodeIndex(counter);

                            if (this.markerDetectorImages.getConfidence(counter) > 0.50)
                            {

                                //Recupera o ângulo XYZ do marcador e salva na lista.
                                transMatrix = new NyARTransMatResult();
                                point3D = new NyARDoublePoint3d();
                                this.markerDetectorImages.getTransmationMatrix(counter, transMatrix);
                                transMatrix.getZXYAngle(point3D);

                                detectedMarker = new DetectedMarker();
                                detectedMarker.markerID = markerImageId;
                                detectedMarker.point3D = point3D;
                                detectedMarkersListImages.Add(detectedMarker);

                            }
                        }
                    }



                    if ((detectedMarkersListLetters.Count + detectedMarkersListImages.Count) > 0)
                    {
                        if ((detectedMarkersListLetters.Count + detectedMarkersListImages.Count) > 1)
                        {
                            lbNyAR.Text = "Há letras que não foram preenchidas na cartela";
                            //Fazer algo para avisar que nem todas as letras foram preenchidas.
                        }
                        else
                        {
                            if (detectedMarkersListImages.Count > 0)
                            {

                                //switch das palavras correspondentes ao marcador encontrado.
                                switch (markerImageId)
                                {
                                    case 0: originalWord = "BOLA";
                                        break;
                                    case 1: originalWord = "ESPADA";
                                        break;
                                    case 2: originalWord = "CARRO";
                                        break;
                                    case 3: originalWord = "BRIGADEIRO";
                                        break;
                                    case 4: originalWord = "GARFO";
                                        break;
                                    case 5: originalWord = "CANETA";
                                        break;
                                    case 6: originalWord = "PEIXE";
                                        break;
                                }

                                int total = checkTotalLetters(raffledLetters, originalWord);

                                //Se todas as letras da palavra tiverem sido sorteadas, plota a imagem
                                if (total == originalWord.Length)
                                {
                                    lbNyAR.Text = "BINGO! Palavra: " + originalWord;
                                    intRefresh++;
                                    intDetected = markerImageId;
                                }
                                else
                                {
                                    lbNyAR.Text = "Algumas letras ainda não foram sorteadas";
                                    //Verificar o que vai fazer... pode ser uma mensagem na tela.
                                }
                            }
                        }
                    }


                }
                else
                {
                    bool detected;
                    switch (intDetected)
                    {
                        case 0: detected = this.ballMarkerDetector.detectMarkerLite(Core.raster);
                            if (detected)
                            {
                                NyARTransMatResult result = new NyARTransMatResult();
                                if (this.ballMarkerDetector.getConfidence() > 0.50)
                                {
                                    this.ballMarkerDetector.getTransmationMatrix(result);
                                    //DrawWord("bola", result);
                                }
                            }
                            break;

                        case 1: detected = this.swordMarkerDetector.detectMarkerLite(Core.raster);
                            if (detected)
                            {
                                NyARTransMatResult result = new NyARTransMatResult();
                                if (this.swordMarkerDetector.getConfidence() > 0.50)
                                {
                                    this.swordMarkerDetector.getTransmationMatrix(result);
                                    //DrawWord("espada", result);
                                }
                            }
                            break;

                        case 3: detected = this.carMarkerDetector.detectMarkerLite(Core.raster);
                            if (detected)
                            {
                                NyARTransMatResult result = new NyARTransMatResult();
                                if (this.carMarkerDetector.getConfidence() > 0.50)
                                {
                                    this.carMarkerDetector.getTransmationMatrix(result);
                                    //DrawWord("carro", result);
                                }
                            }
                            break;

                        case 4: detected = this.forkMarkerDetector.detectMarkerLite(Core.raster);
                            if (detected)
                            {
                                NyARTransMatResult result = new NyARTransMatResult();
                                if (this.forkMarkerDetector.getConfidence() > 0.50)
                                {
                                    this.forkMarkerDetector.getTransmationMatrix(result);
                                    DrawWord("garfo", result);
                                }
                            }
                            break;

                        case 5: detected = this.penMarkerDetector.detectMarkerLite(Core.raster);
                            if (detected)
                            {
                                NyARTransMatResult result = new NyARTransMatResult();
                                if (this.penMarkerDetector.getConfidence() > 0.50)
                                {
                                    this.penMarkerDetector.getTransmationMatrix(result);
                                    //DrawWord("caneta", result);
                                }
                            }
                            break;

                        case 6: detected = this.fishMarkerDetector.detectMarkerLite(Core.raster);
                            if (detected)
                            {
                                NyARTransMatResult result = new NyARTransMatResult();
                                if (this.fishMarkerDetector.getConfidence() > 0.50)
                                {
                                    this.fishMarkerDetector.getTransmationMatrix(result);
                                    DrawWord("peixe", result);
                                }
                            }
                            break;
                    }


                    //Refresh do detector
                    intRefresh++;
                    if (intRefresh > 20)
                    {
                        intRefresh = 0;
                        intDetected = -1;
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Ocorreu um erro na verficação dos marcadores. Favor, reinicie a aplicação. Se o erro persistir, contate os desenvolvedores.","Erro!");
                Environment.Exit(0);
            }
        }


        public int checkTotalLetters(ArrayList listDetectedMarkers, string palavraOriginal)
        {
            //Verificação da palavra bola
            int total = 0;


            char[] palavraSplit = palavraOriginal.ToCharArray();
            bool[] palavraBool = new bool[palavraSplit.Length];

            for (int i = 0; i < palavraBool.Length; i++)
            {
                palavraBool[i] = false;
            }

            //Contadores do número de letras da palavra BOLA (exemplo)
            foreach (char item in listDetectedMarkers)
            {
                //foreach (char item2 in palavraSplit)
                for (int i = 0; i < palavraSplit.Length; i++)
                {
                    if ((item == palavraSplit[i]) && (palavraBool[i] == false))
                    {
                        total++;
                        palavraBool[i] = true;
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
                }

                // Background directly to the surface drawing
                Surface dest_surface = this._device.GetBackBuffer(0, 0, BackBufferType.Mono);
                Rectangle src_dest_rect = new Rectangle(0, 0, 543, 348);
                this._device.StretchRectangle(this._surface.d3d_surface, src_dest_rect, dest_surface, src_dest_rect, TextureFilter.None);

                // Drawing 3D objects from here
                this._device.BeginScene();
                this._device.Clear(ClearFlags.ZBuffer, Color.DarkBlue, 1.0f, 0);

                //MATRIZ QUE DEFINE O POSICIONAMENTO DO OBJETO 3D
                //On 20mm cube (top marker) to be shifted
                Matrix transform_mat2 = Matrix.Scaling(scaling, scaling, scaling);
                transform_mat2 *= Matrix.RotationX((float)Math.PI * 1 / 2);

                //Multiply the transformation matrix
                transform_mat2 *= this._trans_mat;
                // Coordinate transformation matrix was calculated
                this._device.SetTransform(TransformType.World, transform_mat2);

                // Rendering (drawing)
                //this._cube.draw(this._device);

                //switch (word)
                //{
                //    case "sapo":
                //        DrawMesh(listSpacemesh[0], listSpacemeshmaterials[0], listSpacemeshtextures[0]);
                //        break;
                //    case "bola":
                //        DrawMesh(listSpacemesh[1], listSpacemeshmaterials[1], listSpacemeshtextures[1]);
                //        break;
                //}

                //Plota a imagem
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
                }
                //DrawMesh(spacemesh, spacemeshmaterials, spacemeshtextures);
                //DrawMesh(listSpacemesh[2], listSpacemeshmaterials[2], listSpacemeshtextures[2]);

                // Drawing up here
                this._device.EndScene();

                // Drawing on the actual display
                this._device.Present();
            }
        }

        //Funções de desenho de Meshs
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
                        meshtextures[i] = TextureLoader.FromFile(this._device, @"x\" + materialarray[i].TextureFilename);
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
            listFilemesh.Add(meshKnife);
            listFilemesh.Add(meshSword);
            listFilemesh.Add(meshTable);

            for (int i = 0; i < listFilemesh.Count; i++)
            {
                LoadMesh(listFilemesh[i], ref tempSpacemesh, ref tempSpacemeshmaterials, ref tempSpacemeshtextures, ref tempSpacemeshradius);

                listSpacemesh.Add(tempSpacemesh);
                listSpacemeshmaterials.Add(tempSpacemeshmaterials);
                listSpacemeshtextures.Add(tempSpacemeshtextures);
                listSpacemeshradius.Add(tempSpacemeshradius);

            }

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
