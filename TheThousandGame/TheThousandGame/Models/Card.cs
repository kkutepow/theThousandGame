using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TheThousandGame
{
    /// <summary>
    /// Класс, описывающий игральную карту, имеющую свою масть и достоинство.
    /// </summary>
    class Card : IComparable
    {
        /// <summary>
        /// Конструктор, описывающий карту с заданной мастью (suit) и достоинством (priority)
        /// </summary>
        /// <param name="priority"> Достоинство </param>
        /// <param name="suit"> Масть </param>
        public Card(Priorities priority, Suits suit)
        {
            this._priority = priority;
            this._suit = suit;
            this.isTrump = false;
            this.isVisible = true;
        }

        #region Properties
        /// <summary>
        /// Свойство, возвращающее истину, если карта козырной масти.
        /// </summary>
        public bool IsTrump
        {
            get { return isTrump; }
        }
        /// <summary>
        /// Изображение карты
        /// </summary>
        public Image Picture
        {
            get
            {
                Image img = Properties.Resources.Рубашка;
                switch (this.Suit)
                {
                    case Suits.Буби:
                        {
                            switch (this.Priority)
                            {
                                case Priorities.Девять: img = Properties.Resources.Девять_Буби; break;
                                case Priorities.Десять: img = Properties.Resources.Десять_Буби; break;
                                case Priorities.Валет: img = Properties.Resources.Валет_Буби; break;
                                case Priorities.Дама: img = Properties.Resources.Дама_Буби; break;
                                case Priorities.Король: img = Properties.Resources.Король_Буби; break;
                                case Priorities.Туз: img = Properties.Resources.Туз_Буби; break;
                                default: img = Properties.Resources.Туз_Буби; break;
                            }
                            break;
                        }
                    case Suits.Черви:
                        {
                            switch (this.Priority)
                            {
                                case Priorities.Девять: img = Properties.Resources.Девять_Черви; break;
                                case Priorities.Десять: img = Properties.Resources.Десять_Черви; break;
                                case Priorities.Валет: img = Properties.Resources.Валет_Черви; break;
                                case Priorities.Дама: img = Properties.Resources.Дама_Черви; break;
                                case Priorities.Король: img = Properties.Resources.Король_Черви; break;
                                case Priorities.Туз: img = Properties.Resources.Туз_Черви; break;
                                default: img = Properties.Resources.Туз_Черви; break;
                            }
                            break;
                        }
                    case Suits.Пики:
                        {
                            switch (this.Priority)
                            {
                                case Priorities.Девять: img = Properties.Resources.Девять_Пики; break;
                                case Priorities.Десять: img = Properties.Resources.Десять_Пики; break;
                                case Priorities.Валет: img = Properties.Resources.Валет_Пики; break;
                                case Priorities.Дама: img = Properties.Resources.Дама_Пики; break;
                                case Priorities.Король: img = Properties.Resources.Король_Пики; break;
                                case Priorities.Туз: img = Properties.Resources.Туз_Пики; break;
                                default: img = Properties.Resources.Туз_Пики; break;
                            }
                            break;
                        }
                    case Suits.Крести:
                        {
                            switch (this.Priority)
                            {
                                case Priorities.Девять: img = Properties.Resources.Девять_Крести; break;
                                case Priorities.Десять: img = Properties.Resources.Десять_Крести; break;
                                case Priorities.Валет: img = Properties.Resources.Валет_Крести; break;
                                case Priorities.Дама: img = Properties.Resources.Дама_Крести; break;
                                case Priorities.Король: img = Properties.Resources.Король_Крести; break;
                                case Priorities.Туз: img = Properties.Resources.Туз_Крести; break;
                                default: img = Properties.Resources.Туз_Крести; break;
                            }
                            break;
                        }
                }
                return img;
            }
        }
        /// <summary>
        /// Свойство, возвращающее достоинство игральной карты.
        /// </summary>
        public Priorities Priority
        {
            get { return _priority; }
        }
        /// <summary>
        /// Свойство, возвращающее масть игральной карты.
        /// </summary>
        public Suits Suit
        {
            get { return _suit; }
        }
        #endregion

        #region Abilities
        /// <summary>
        /// Метод сравнения двух карт
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> Возвращает 1, если карта старше, или -1, если карта младше </returns>
        public int CompareTo(object obj)
        {
            Card Other = (Card)obj;

            // Подсчет весомости главной карты
            int thisCost = Card.CardCost(this.Priority) + (this.isTrump ? 40 : 0);
            if (this.Suit != Other.Suit) thisCost += 20;

            // Подсчет весомости сравниваемой карты, относительно главной
            int otherCost = Card.CardCost(Other.Priority) + (Other.IsTrump ? 40 : 0);

            // Сравниваем весомости карт
            return thisCost.CompareTo(otherCost);
        }
        /// <summary>
        /// Скрыть карту
        /// </summary>
        public void Hide()
        {
            isVisible = false;
        }
        /// <summary>
        /// Скрыть карту
        /// </summary>
        public void Show()
        {
            isVisible = true;
        }
        /// <summary>
        /// Строковое описание карты
        /// </summary>
        /// <returns> Описание карты </returns>
        public override string ToString()
        {
            return isVisible ? this._priority.ToString() + " " + this._suit.ToString() : "рубашка";
        }
        /// <summary>
        /// Объявление козыря
        /// </summary>
        /// <param name="trumpSuit"> Козырь </param>
        public void TrumpThis(Suits trumpSuit)
        {
            this.isTrump = this.Suit == trumpSuit;
        }
        #endregion

        #region Fields
        public static string PathToCardImage = @"C:\Users\KKAProduct\YandexDisk\Учеба\2 курс\Projects\TheThousandGame\TheThousandGame\bin\debug\img\cards\";
        private bool isTrump;
        private bool isVisible;
        private Priorities _priority;
        private Suits _suit;
        private static Dictionary<Priorities, int> _cardCostDictionary = new Dictionary<Priorities, int>
            {
                {Priorities.Туз, 11},
                {Priorities.Десять, 10},
                {Priorities.Король, 4},
                {Priorities.Дама, 3},
                {Priorities.Валет, 2},
                {Priorities.Девять, 0},
            };
        private static Dictionary<Suits, int> _marriageCost = new Dictionary<Suits, int>
            {
                {Suits.Пики, 40},
                {Suits.Крести, 60},
                {Suits.Буби, 80},
                {Suits.Черви, 100},
                {Suits.Тузовая, 200},
            };
        #endregion

        #region Suits&Priorities
        /// <summary>
        /// Список всех доступных достоинств
        /// </summary>
        public enum Priorities
        {
            Девять,
            Валет,
            Дама,
            Король,
            Десять,
            Туз
        }
        /// <summary>
        /// Список всех доступных мастей
        /// </summary>
        public enum Suits
        {
            Пики,
            Крести,
            Буби,
            Черви,
            Тузовая,
            Нет
        }
        /// <summary>
        /// Игровая "стоимость" игральной карты.
        /// </summary>
        public static int CardCost(Priorities Priority)
        {
            int cost;
            return _cardCostDictionary.TryGetValue(Priority, out cost) ? cost : 0;
        }
        /// <summary>
        /// Стоимость марьяжа заданной масти
        /// </summary>
        /// <param name="Suit">Масть</param>
        /// <returns>Стоимость</returns>
        public static int MarriageCost(Suits Suit)
        {
            int cost;
            return _marriageCost.TryGetValue(Suit, out cost) ? cost : 0;
        }
        #endregion

    }
}