using System.Diagnostics.Tracing;
using System.IO;
namespace Hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string chosenWord = chooseWord();

            if (chosenWord.Length > 0)
            {
                int countLives = 6;
                Hangman game = new Hangman(chosenWord, countLives); // Sélection d'un mot au hasard dans la liste de mot
                string userInput;
                bool isGameOver = false;

                do
                { // Jeu
                  // Affichage du jeu
                    Console.WriteLine("Voici le mot à deviner : {0}", game.hiddenWord);
                    Console.WriteLine("{0} vies restantes", game.countLives);
                    Console.WriteLine("Voici les lettres déjà utilisées : {0}", game.lettersUsed);

                    do
                    { // Demande d'une lettre au joueur
                        userInput = askForUserInput();
                    } while (userInput.Length != 1);


                    if (game.isAlreadyUsed(userInput) == -1 && game.isInWord(userInput) != -1)
                    { // Si la lettre n'a pas déja été utilisée et qu'elle est dans le mot
                        game.addLetterToHiddenWord(userInput);

                        if (game.isHiddenValid())
                        { // Si le joueur a gagné
                            isGameOver = true;
                            Console.WriteLine("Bravo ! Vous avez trouvé {0} !\nIl vous restait {1} vies", game.word, game.countLives);
                        }
                    }
                    else
                    { // Le joueur perd une vie
                        game.countLives--;
                        if (game.isLost(game.countLives))
                        {
                            isGameOver = true;
                            Console.WriteLine("Vous avez perdu ! Le mot à trouver était {0}", game.word);
                        }
                    }

                    if (game.isAlreadyUsed(userInput) == -1)
                    { // Ajout de la lettre à la liste de celles déjà utilisées
                        game.lettersUsed += userInput;
                        game.lettersUsed = sortString(game.lettersUsed);
                    }

                } while (!isGameOver);
            }

        }

        private static string sortString(string str)
        {
            char[] characters = str.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }

        private static string askForUserInput()
        {
            Console.Write("Entrez une lettre :");
            return Console.ReadLine().ToLower();
        }

        private static string chooseWord()
        {
            List<string> wordList = new List<string>();
            String line;
            string result = "";
            try
            {
                // Pass the file path and file name to the StreamReader constructor.
                // ./ pour rester dans le même dossier
                StreamReader sr = new StreamReader("./list.txt");
                // Read the first line of text
                line = sr.ReadLine();
                if (line == null)
                {
                    throw new ReadFileException();
                }
                while (line != null)
                { // Continue to read until you reach end of file
                    // Read the next line
                    line = sr.ReadLine();
                    wordList.Add(line);
                }
                // Close the file
                sr.Close();

                // Création d'un random pour chercher un mot aléatoire dans la list créée
                Random rnd = new Random();
                int iteration = rnd.Next(1, wordList.Count - 1);

                result = wordList.ElementAt(iteration).ToLower();

                Console.WriteLine("Un mot a été choisit au hasard. Bon jeu");
            }
            catch (ReadFileException r)
            {
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.Write("");
            }

            return result;
        }
    }
}
