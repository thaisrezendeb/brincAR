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
    /// Classe de Testes da lógica do jogo "Bingo das Nomes"
    /// </summary>
    [TestFixture]
    class WordsBingoTests
    {
        [Test]
        public void BallTestOk()
        {
            Game game = new Game();
            PictureBox pbx = new PictureBox();
            Label lbl = new Label();

            game.SelectedObject = "bola";

            //Lista de objetos detectados
            ArrayList raffledLetters = new ArrayList();

            raffledLetters.Add(char.Parse("b"));
            raffledLetters.Add(char.Parse("o"));
            raffledLetters.Add(char.Parse("l"));
            raffledLetters.Add(char.Parse("a"));

            NyARWordsBingoCore core = new NyARWordsBingoCore(lbl, game, pbx);

            Assert.AreEqual(game.SelectedObject.Length, core.checkTotalLetters(raffledLetters, game.SelectedObject));

            //Encerrando a captura e desalocando o objeto do jogo
            core.stopCapture();
            core = null;

        }

        [Test]
        public void BallTest3Letters()
        {
            Game game = new Game();
            PictureBox pbx = new PictureBox();
            Label lbl = new Label();

            game.SelectedObject = "bola";

            //Lista de objetos detectados
            ArrayList raffledLetters = new ArrayList();

            raffledLetters.Add(char.Parse("c"));
            raffledLetters.Add(char.Parse("o"));
            raffledLetters.Add(char.Parse("l"));
            raffledLetters.Add(char.Parse("a"));

            NyARWordsBingoCore core = new NyARWordsBingoCore(lbl, game, pbx);

            Assert.AreEqual((game.SelectedObject.Length - 1), core.checkTotalLetters(raffledLetters, game.SelectedObject));

            //Encerrando a captura e desalocando o objeto do jogo
            core.stopCapture();
            core = null;

        }

        [Test]
        public void BallTest2Letters()
        {
            Game game = new Game();
            PictureBox pbx = new PictureBox();
            Label lbl = new Label();

            game.SelectedObject = "bola";

            //Lista de objetos detectados
            ArrayList raffledLetters = new ArrayList();

            raffledLetters.Add(char.Parse("c"));
            raffledLetters.Add(char.Parse("p"));
            raffledLetters.Add(char.Parse("l"));
            raffledLetters.Add(char.Parse("a"));

            NyARWordsBingoCore core = new NyARWordsBingoCore(lbl, game, pbx);

            Assert.AreEqual((game.SelectedObject.Length - 2), core.checkTotalLetters(raffledLetters, game.SelectedObject));

            //Encerrando a captura e desalocando o objeto do jogo
            core.stopCapture();
            core = null;

        }

        [Test]
        public void BallTest1Letter()
        {
            Game game = new Game();
            PictureBox pbx = new PictureBox();
            Label lbl = new Label();

            game.SelectedObject = "bola";

            //Lista de objetos detectados
            ArrayList raffledLetters = new ArrayList();

            raffledLetters.Add(char.Parse("c"));
            raffledLetters.Add(char.Parse("p"));
            raffledLetters.Add(char.Parse("m"));
            raffledLetters.Add(char.Parse("a"));

            NyARWordsBingoCore core = new NyARWordsBingoCore(lbl, game, pbx);

            Assert.AreEqual((game.SelectedObject.Length - 3), core.checkTotalLetters(raffledLetters, game.SelectedObject));

            //Encerrando a captura e desalocando o objeto do jogo
            core.stopCapture();
            core = null;
        }

        [Test]
        public void BallTestNoLetter()
        {
            Game game = new Game();
            PictureBox pbx = new PictureBox();
            Label lbl = new Label();

            game.SelectedObject = "bola";

            //Lista de objetos detectados
            ArrayList raffledLetters = new ArrayList();

            raffledLetters.Add(char.Parse("c"));
            raffledLetters.Add(char.Parse("p"));
            raffledLetters.Add(char.Parse("m"));
            raffledLetters.Add(char.Parse("d"));

            NyARWordsBingoCore core = new NyARWordsBingoCore(lbl, game, pbx);

            Assert.AreEqual(0, core.checkTotalLetters(raffledLetters, game.SelectedObject));

            //Encerrando a captura e desalocando o objeto do jogo
            core.stopCapture();
            core = null;
        }

        [Test]
        public void SameLetterTest()
        {
            Game game = new Game();
            PictureBox pbx = new PictureBox();
            Label lbl = new Label();

            game.SelectedObject = "peixe";

            //Lista de objetos detectados
            ArrayList raffledLetters = new ArrayList();

            raffledLetters.Add(char.Parse("p"));
            raffledLetters.Add(char.Parse("e"));
            raffledLetters.Add(char.Parse("i"));
            raffledLetters.Add(char.Parse("x"));

            NyARWordsBingoCore core = new NyARWordsBingoCore(lbl, game, pbx);

            Assert.AreEqual(game.SelectedObject.Length, core.checkTotalLetters(raffledLetters, game.SelectedObject));

            //Encerrando a captura e desalocando o objeto do jogo
            core.stopCapture();
            core = null;
        }
    }
}
