using System.Collections.Generic;
using System.Xml.Serialization;

namespace HeritageEtInterface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DisplayMainMenu();
        }

        private static void DisplayMainMenu()
        {
            // Création de médias
            Book harryPotter = new Book("Harry Potter", "Un orphelin paranoïaque est dans un asile", 3, 325);
            DVD lordOfTheRings = new DVD("Le seigneur des anneaux", "Une bande de potes bourrés part en vacances", 1, 300);

            //Création des utilisateurs
            Admin antoine = new Admin("Deleplanque", "Antoine", 30, "2020-01-01");
            Customer xavier = new Customer("Boucher", "Xavier", 26, "2024-10-01");

            // Création de listes
            List<MediaObject> mediaList = new List<MediaObject>();
            List<User> userList = new List<User>();

            // Ajout dans les listes
            mediaList.Add(harryPotter);
            mediaList.Add(lordOfTheRings);
            userList.Add(antoine);
            userList.Add(xavier);

            string userInput = AskUserInputMenu("Bienvenue dans la médiathèque ! Qui êtes vous ?\n" +
                "1. Admin\n" +
                "2. Client\n" +
                "3. Quitter le programme", "3", "1", "2");

            switch (userInput)
            {
                case "1":
                    DisplayAdminMenu(mediaList);
                    break;
                case "2":
                    List<User> customerList = new List<User>();
                    foreach (User user in userList)
                    {
                        Customer fictiveCustomer = user as Customer;
                        if (fictiveCustomer != null)
                        {
                            customerList.Add(fictiveCustomer);
                        }
                    }
                    User fictiveUser = AskUserInputUser("Veuillez choisir votre profil :", customerList);
                    Customer customer = fictiveUser as Customer;
                    if (customer != null)
                    {
                        DisplayCustomerMenu(mediaList, customer);
                    }
                    break;
                case "3":
                    Console.WriteLine("Au revoir.");
                    break;
            }
        }

        private static void DisplayCustomerMenu(List<MediaObject> mediaList, Customer customer)
        {
            string userInput = AskUserInputMenu("Bonjour " + customer.FirstName + " ! Que souhaitez-vous faire ?\n" +
                "1. Emprunter un média\n" +
                "2. Rendre un média\n" +
                "3. Retour au menu principal", "3", "1", "2");
            switch (userInput)
            {
                case "1":
                    DisplayBorrow(customer, mediaList);
                    DisplayCustomerMenu(mediaList, customer);
                    break;
                case "2":
                    DisplayGiveBack(customer);
                    DisplayCustomerMenu(mediaList, customer);
                    break;
                case "3":
                    DisplayMainMenu();
                    break;
            }
        }

        private static void DisplayGiveBack(Customer customer)
        {
            if (customer.MediaList.Count > 0) {
                MediaObject media = AskUserInputMedia("Quel media voulez-vous rendre ?", customer.MediaList);
                customer.RemoveMedia(media);
            }
            else
            {
                Console.WriteLine("Vous n'avez plus rien à rendre");
            }
        }

        private static void DisplayBorrow(Customer customer, List<MediaObject> mediaList)
        {
            MediaObject customerMedia = AskUserInputMedia("Quel média souhaitez-vous emprunter ?", mediaList);
            customer.AddMedia(customerMedia);
            Console.WriteLine("Merci d'avoir emprunté {0}, {1} !", customerMedia.Name, customer.FirstName);
        }

        private static void DisplayAdminMenu(List<MediaObject> mediaList)
        {
            string userInput = AskUserInputMenu("Bonjour admin ! Que souhaitez vous faire ?\n" +
                "1. Ajouter un média\n" +
                "2. Supprimer un média\n" +
                "3. Lister les médias existants\n" +
                "4. Retour au menu principal", "4", "1", "2", "3");
            switch (userInput)
            {
                case "1":
                    AddMedia(mediaList);
                    DisplayAdminMenu(mediaList);
                    break;
                case "2":
                    DeleteMedia(mediaList);
                    DisplayAdminMenu(mediaList);
                    break;
                case "3":
                    listMedias(mediaList);
                    DisplayAdminMenu(mediaList);
                    break;
                case "4":
                    DisplayMainMenu();
                    break;
            }
        }

        private static void listMedias(List<MediaObject> mediaList)
        {
            Console.WriteLine("Voici la liste de médias disponibles");
            foreach (MediaObject media in mediaList)
            {
                if (media.Stock > 0)
                {
                    Console.WriteLine(media.Name);
                }
            }
        }

        private static void DeleteMedia(List<MediaObject> mediaList)
        {
            MediaObject mediaToRemove = AskUserInputMedia("Lequel voulez-vous supprimer ?", mediaList);
            Console.WriteLine("Combien voulez-vous en supprimer ?");
            string userInputAdd = Console.ReadLine();
            int nbAdd = 1;
            Int32.TryParse(userInputAdd, out nbAdd);
            for (int i = 0; i < nbAdd; i++)
            {
                mediaToRemove.Borrow();
            }
            Console.WriteLine("{0} a été ajouté {1} fois", mediaToRemove.Name, nbAdd);
        }

        private static void AddMedia(List<MediaObject> mediaList)
        {
            string userInput = AskUserInputString("Voulez-vous ajouter un média déjà existant ? O/N", "O", "N");
            if(userInput == "O")
            { // Média existant
                MediaObject mediaToAdd = AskUserInputMedia("Lequel voulez-vous ajouter ?", mediaList);
                Console.WriteLine("Combien voulez-vous en rajouter ?");
                string userInputAdd = Console.ReadLine();
                int nbAdd = 1;
                Int32.TryParse(userInputAdd, out nbAdd);
                for (int i = 0; i < nbAdd; i++)
                {
                    mediaToAdd.GiveBack();
                }
                Console.WriteLine("{0} a été ajouté {1} fois", mediaToAdd.Name, nbAdd);
            }
            else if (userInput == "N")
            { // Nouveau média
                string userInputType = AskUserInputString("Quel type de média est-ce ?\n1 : Livre\n2 : DVD", "1", "2");
                string userInputName = AskUserInputString("Quel est le titre ?");
                string userInputSynopsis = AskUserInputString("Quel est le synopsis ?");
                int userInputStock = AskUserInputInt("Combien voulez-vous en rajouter ?");

                if (userInputType == "1")
                { // Ajout d'un livre
                    int userInputNbPages = AskUserInputInt("Quel est le nombre de pages de ce livre ?");
                    Book newBook = new Book(userInputName, userInputSynopsis, userInputStock, userInputNbPages);
                    mediaList.Add(newBook);
                }
                else
                { // Ajout d'un DVD
                    int userInputDuration = AskUserInputInt("Quel est la durée de ce DVD ?");
                    DVD newDVD = new DVD(userInputName, userInputSynopsis, userInputStock, userInputDuration);
                    mediaList.Add(newDVD);
                }
            }
        }

        public static string AskUserInputMenu(string question, string exitValue, params object[] choices)
        { // Demande à l'utilisateur une chaîne de caractère et vérifie s'il correspond aux choix donnés
            string userInput;
            bool isValidAnswer = false;
            Console.WriteLine(question);
            do
            {
                userInput = Console.ReadLine();
                foreach (string choice in choices)
                {
                    if (userInput == choice)
                    {
                        isValidAnswer = true;
                    }
                }
            } while (!isValidAnswer && userInput != exitValue);
            return userInput;
        }

        public static string AskUserInputString(string question, params object[] choices)
        { // Demande à l'utilisateur une chaîne de caractère et vérifie s'il correspond aux choix donnés
            string userInput;
            bool isValidAnswer = false;
            Console.WriteLine(question);
            do
            {
                userInput = Console.ReadLine();
                if (choices.Length > 0)
                {
                    foreach (string choice in choices)
                    {
                        if (userInput == choice)
                        {
                            isValidAnswer = true;
                        }
                    }
                }
                else
                {
                    isValidAnswer = true;
                }
            } while (!isValidAnswer);
            return userInput;
        }

        public static int AskUserInputInt(string question, params object[] choices)
        { // Demande à l'utilisateur un entier
            string input;
            int userInput;
            bool isValidAnswer = false;
            Console.WriteLine(question);
            do
            {
                input = Console.ReadLine();
                if (choices.Length > 0)
                {
                    foreach (string choice in choices)
                    {
                        if (input == choice)
                        {
                            isValidAnswer = true;
                        }
                    }
                }
                else
                {
                    isValidAnswer = true;
                }
            } while (!isValidAnswer);
            Int32.TryParse(input, out userInput);
            return userInput;
        }

        public static MediaObject AskUserInputMedia(string question, List<MediaObject> mediaList)
        { // Demande à l'utilisateur un choix dans la liste de media
            string userInput;
            int cpt = 0;
            Dictionary<string, MediaObject> myCustomDictionary = new Dictionary<string, MediaObject>();
            foreach (MediaObject media in mediaList)
            { // Ajout des valeurs au dictionnaire
                myCustomDictionary.Add(cpt.ToString(), media);
                cpt++;
            }

            foreach (KeyValuePair<string, MediaObject> media in myCustomDictionary)
            { // Affichage des valeurs
                Console.WriteLine("{0} : {1}", media.Key.ToString(), media.Value.Name);
            }

            // Affichage question et vérification valeur de retour
            Console.WriteLine(question);
            do
            {
                userInput = Console.ReadLine();
            } while (!myCustomDictionary.ContainsKey(userInput));
            int value = 0;
            try
            {
                Int32.TryParse(userInput, out value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return myCustomDictionary.ElementAt(value).Value;

        }

        public static User AskUserInputUser(string question, List<User> userList)
        { // Demande à l'utilisateur un choix dans la liste de media
            string userInput;
            int cpt = 0;
            Dictionary<string, User> myCustomDictionary = new Dictionary<string, User>();
            foreach (User user in userList)
            { // Ajout des valeurs au dictionnaire
                myCustomDictionary.Add(cpt.ToString(), user);
                cpt++;
            }

            foreach (KeyValuePair<string, User> media in myCustomDictionary)
            { // Affichage des valeurs
                Console.WriteLine("{0} : {1} {2}", media.Key, media.Value.FirstName, media.Value.LastName);
            }

            // Affichage question et vérification valeur de retour
            Console.WriteLine(question);
            do
            {
                userInput = Console.ReadLine();
            } while (!myCustomDictionary.ContainsKey(userInput));
            int value = 0;
            Int32.TryParse(userInput, out value);

            return myCustomDictionary.ElementAt(value).Value;

        }
    }
}
