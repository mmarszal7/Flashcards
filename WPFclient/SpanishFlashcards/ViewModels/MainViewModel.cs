using System.Collections.Generic;
using SpanishFlashcards.Model;
using FireSharp.Config;
using FireSharp;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using NLog;
using System.ComponentModel;
using System.Windows.Forms;

namespace SpanishFlashcards
{
    class MainViewModel : BaseViewModel
    {
        #region Fields, Properties and Constructor

        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public ObservableCollection<Card> Cards { get; set; }
        private List<Card> allCards = new List<Card>();
        private int listIterator = 0;
        Random rnd = new Random();

        public RelayCommand DrawCommand { get; set; }

        public MainViewModel()
        {
            Cards = new ObservableCollection<Card>();
            DrawCommand = new RelayCommand(Draw);
            GetCards();
        }

        #endregion

        private async void GetCards()
        {
            try
            {
                IFirebaseClient client = new FirebaseClient(new FirebaseConfig { BasePath = "https://spannishwords.firebaseio.com/" });
                FirebaseResponse response = await client.GetAsync("words");
                allCards = response.ResultAs<List<Card>>().Where(c => c.Repeat == true).OrderBy(x => rnd.Next()).ToList();
                Cards.Clear();
                allCards.Take(5).ToList().ForEach(c => Cards.Add(c));

                RaisePropertyChanged();
            }
            catch (Exception e)
            {
                Logger.Error(e, "Problem with Firebase connection");
                DialogResult result = MessageBox.Show("Problem with Firebase connection.", "Connection Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                    System.Environment.Exit(1);
            }
        }

        private void Draw()
        {
            listIterator += 5;
            if (listIterator > allCards.Count)
                listIterator = 0;

            Cards.Clear();
            allCards.Skip(listIterator).Take(5).ToList().ForEach(c => Cards.Add(c));
            foreach (var c in Cards)
                c.ShowPolish = true;
        }
    }
}
