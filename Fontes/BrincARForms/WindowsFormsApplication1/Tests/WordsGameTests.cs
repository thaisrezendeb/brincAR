using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using brincAR.brincARCore;
using ServicesBrincAR;
using ServicesBrincAR.services.obj;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace BrincAR.Tests
{
    /// <summary>
    /// Classe de Testes da lógica do jogo "Gincana de Palavras"
    /// </summary>
    [TestFixture]
    class WordsGameTests
    {

        /// <summary>
        /// Teste da palavra BOLA com todas as letras detectadas
        /// </summary>
        [Test]
        public void BallTestOk()
        {
            Game game = new Game();
            PictureBox pbx = new PictureBox();
            Label lbl = new Label();
            DetectedMarker marker;

            //Objeto selecionado
            game.SelectedObject = "bola";

            //Lista de objetos detectados
            ArrayList listDetectedMarkers = new ArrayList();

            marker = new DetectedMarker();
            marker.markerID = 2; //b
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 15; //o
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 12; //l
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 1;  //a
            listDetectedMarkers.Add(marker);

            NyARWordsGameCore core = new NyARWordsGameCore(lbl, game, pbx);

            //TESTE - Compara as letras detectadas com o da palavra selecionada pelo jogo
            //A função retorna o total de letras encontradas. Para sucesso, deve ser igual ao comprimento da palavra.
            Assert.AreEqual(game.SelectedObject.Length, core.verificaTotalPalavraFormada(listDetectedMarkers, game.SelectedObject));

            //Encerrando a captura e desalocando o objeto do jogo
            core.stopCapture();
            core = null;
        }

        /// <summary>
        /// Teste da palavra BOLA com apenas 3 letras detectadas
        /// </summary>
        [Test]
        public void BallTest3Letters()
        {
            Game game = new Game();
            PictureBox pbx = new PictureBox();
            Label lbl = new Label();
            DetectedMarker marker;

            //Objeto selecionado
            game.SelectedObject = "bola";

            //Lista de objetos detectados
            ArrayList listDetectedMarkers = new ArrayList();

            marker = new DetectedMarker();
            marker.markerID = 2; //b
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 15; //o
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 12; //l
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 3;  //c
            listDetectedMarkers.Add(marker);

            NyARWordsGameCore core = new NyARWordsGameCore(lbl, game, pbx);

            //TESTE - Compara as letras detectadas com o da palavra selecionada pelo jogo
            //A função retorna o total de letras encontradas. Para sucesso, o resultado deve ser 3.
            Assert.AreEqual((game.SelectedObject.Length - 1), core.verificaTotalPalavraFormada(listDetectedMarkers, game.SelectedObject));

            //Encerrando a captura e desalocando o objeto do jogo
            core.stopCapture();
            core = null;
        }

        /// <summary>
        /// Teste da palavra BOLA com apenas 2 letras detectadas
        /// </summary>
        [Test]
        public void BallTest2Letters()
        {
            Game game = new Game();
            PictureBox pbx = new PictureBox();
            Label lbl = new Label();
            DetectedMarker marker;

            //Objeto selecionado
            game.SelectedObject = "bola";

            //Lista de objetos detectados
            ArrayList listDetectedMarkers = new ArrayList();

            marker = new DetectedMarker();
            marker.markerID = 2; //b
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 15; //o
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 13; //m
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 3;  //c
            listDetectedMarkers.Add(marker);

            NyARWordsGameCore core = new NyARWordsGameCore(lbl, game, pbx);

            //TESTE - Compara as letras detectadas com o da palavra selecionada pelo jogo
            //A função retorna o total de letras encontradas. Para sucesso, o resultado deve ser 2.
            Assert.AreEqual((game.SelectedObject.Length - 2), core.verificaTotalPalavraFormada(listDetectedMarkers, game.SelectedObject));

            //Encerrando a captura e desalocando o objeto do jogo
            core.stopCapture();
            core = null;
        }

        /// <summary>
        /// Teste da palavra BOLA com apenas uma letra detectada
        /// </summary>
        [Test]
        public void BallTest1Letter()
        {
            Game game = new Game();
            PictureBox pbx = new PictureBox();
            Label lbl = new Label();
            DetectedMarker marker;

            //Objeto selecionado
            game.SelectedObject = "bola";

            //Lista de objetos detectados
            ArrayList listDetectedMarkers = new ArrayList();

            marker = new DetectedMarker();
            marker.markerID = 2; //b
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 16; //p
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 13; //m
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 3;  //c
            listDetectedMarkers.Add(marker);

            NyARWordsGameCore core = new NyARWordsGameCore(lbl, game, pbx);

            //TESTE - Compara as letras detectadas com o da palavra selecionada pelo jogo
            //A função retorna o total de letras encontradas. Para sucesso, o resultado deve ser 1.
            Assert.AreEqual((game.SelectedObject.Length - 3), core.verificaTotalPalavraFormada(listDetectedMarkers, game.SelectedObject));

            //Encerrando a captura e desalocando o objeto do jogo
            core.stopCapture();
            core = null;
        }

        /// <summary>
        /// Teste da palavra BOLA com nenhuma letra detectada
        /// </summary>
        [Test]
        public void BallTestNoLetter()
        {
            Game game = new Game();
            PictureBox pbx = new PictureBox();
            Label lbl = new Label();
            DetectedMarker marker;

            //Objeto selecionado
            game.SelectedObject = "bola";

            //Lista de objetos detectados
            ArrayList listDetectedMarkers = new ArrayList();

            marker = new DetectedMarker();
            marker.markerID = 4; //d
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 16; //p
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 13; //m
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 3;  //c
            listDetectedMarkers.Add(marker);

            NyARWordsGameCore core = new NyARWordsGameCore(lbl, game, pbx);

            //TESTE - Compara as letras detectadas com o da palavra selecionada pelo jogo
            //A função retorna o total de letras encontradas. Para sucesso, o resultado deve ser 0.
            Assert.AreEqual(0, core.verificaTotalPalavraFormada(listDetectedMarkers, game.SelectedObject));

            //Encerrando a captura e desalocando o objeto do jogo
            core.stopCapture();
            core = null;
        }

        /// <summary>
        /// Teste da palavra FACA com todas as letras detectadas
        /// Este teste verifica a confiabilidade do algoritmo quando há mais de uma letra detectada
        /// </summary>
        [Test]
        public void SameLetterTest()
        {
            Game game = new Game();
            PictureBox pbx = new PictureBox();
            Label lbl = new Label();
            DetectedMarker marker;

            //Objeto selecionado
            game.SelectedObject = "faca";

            //Lista de objetos detectados
            ArrayList listDetectedMarkers = new ArrayList();

            marker = new DetectedMarker();
            marker.markerID = 6; //f
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 1; //a
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 3; //c
            listDetectedMarkers.Add(marker);

            marker = new DetectedMarker();
            marker.markerID = 1;  //a
            listDetectedMarkers.Add(marker);

            NyARWordsGameCore core = new NyARWordsGameCore(lbl, game, pbx);

            //TESTE - Compara as letras detectadas com o da palavra selecionada pelo jogo
            //A função retorna o total de letras encontradas. Para sucesso, deve ser igual ao comprimento da palavra.
            Assert.AreEqual(game.SelectedObject.Length, core.verificaTotalPalavraFormada(listDetectedMarkers, game.SelectedObject));

            //Encerrando a captura e desalocando o objeto do jogo
            core.stopCapture();
            core = null;
        }
    }


}
