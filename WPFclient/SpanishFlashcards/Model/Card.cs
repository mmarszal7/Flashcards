using Newtonsoft.Json;

namespace SpanishFlashcards.Model
{
    public class Card : BaseViewModel
    {
        [JsonProperty("sp")]
        public string SpanishWord { get; set; }
        [JsonProperty("pl")]
        public string PolishWord { get; set; }
        [JsonProperty("tag")]
        public string Tag { get; set; }
        [JsonProperty("repeat")]
        public bool Repeat { get; set; }
        [JsonProperty("count")]
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                if (value <= 2  && value >= 0)
                {
                    number = value;

                    switch (number)
                    {
                        case 3:
                            {
                                Color = "Red";
                                break;
                            }
                        case 2:
                            {
                                Color = "Orange";
                                break;
                            }
                        case 1:
                            {
                                Color = "Yellow";
                                break;
                            }
                        case 0:
                            {
                                Color = "White";
                                break;
                            }
                    }
                }
                RaisePropertyChanged();
            }
        }

        private int number;
        public bool ShowPolish { get; set; } = true;
        public string Color { get; set; } = "White";
        public string Text { get { return ShowPolish ? this.PolishWord : this.SpanishWord; } }

        public RelayCommand SwapLanguageCommand { get; set; }
        public RelayCommand PlusCommand { get; set; }
        public RelayCommand MinusCommand { get; set; }

        public Card()
        {
            SwapLanguageCommand = new RelayCommand(() => { ShowPolish = !ShowPolish; RaisePropertyChanged(); });
            PlusCommand = new RelayCommand(() => Number = Number + 1);
            MinusCommand = new RelayCommand(() => Number = Number - 1);
        }
    }
}
