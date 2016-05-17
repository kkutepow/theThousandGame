using System;
using System.Collections.Generic;

namespace TheThousandGame
{
    /// <summary>
    /// Класс, описывающий колоду игральных карт.
    /// </summary>
    class Pack
    {
        #region Constructors
        /// <summary>
        /// Конструктор, описывающий колоду игральных карт.
        /// </summary>
        /// <param name="Full"> Полная или пустая игровая колода </param>
        public Pack(bool Full)
        {
            if (Full)
            {
                // Если Full, добавление всех карт в колоду Pack
                for (Card.Suits suit = Card.Suits.Пики; suit <= Card.Suits.Черви; suit++)
                {
                    for (Card.Ranks rank = Card.Ranks.Девять; rank <= Card.Ranks.Туз; rank++)
                    {
                        this.AddCard(new Card(rank, suit));
                    }
                }
            }
        }
        /// <summary>
        /// Конструктор, описывающий колоду игральных карт.
        /// </summary>
        /// <param name="cards">Карты, входящие в состав колоды</param>
        public Pack(params Card[] cards)
        {
            AddCards(cards);
        }
        #endregion

        #region Possibilities
        /// <summary>
        /// Добавить карту в колоду.
        /// </summary>
        /// <param name="card"> Добавляемая карта </param>
        public void AddCard(Card card)
        {
            _cards.Add(card);
        }
        /// <summary>
        /// Добавить несколько карт
        /// </summary>
        /// <param name="cards">Карты</param>
        public void AddCards(params Card[] cards)
        {
            foreach (Card card in cards)
            {
                this.AddCard(card);
            }
        }
        /// <summary>
        /// Добавить колоду карт
        /// </summary>
        /// <param name="pack">Колода</param>
        public void AddPack(Pack pack)
        {
            foreach (Card card in pack.Cards)
            {
                this.AddCard(card);
            }
        }
        /// <summary>
        /// Упорядочить карты в колоде
        /// </summary>
        public void ClassicSort()
        {
            List<Card> _tempPack = new List<Card>();
            for (int s = 3; s > -1; s--)
            {
                for (int p = 5; p > -1; p--)
                {
                    var _tempcard = new Card((Card.Ranks)p, (Card.Suits)s);
                    if (Contains(_tempcard))
                    {
                        _tempPack.Add(_tempcard);
                    }
                }
            }
            _cards = _tempPack;
        }
        /// <summary>
        /// Проверяет нахождение заданной карты в колоде.
        /// </summary>
        /// <param name="card">Искомая карта</param>
        /// <returns>True, если находится, и false, если не находится.</returns>
        public bool Contains(Card card)
        {
            foreach (Card _card in _cards)
            {
                if (_card.Suit == card.Suit && _card.Rank == card.Rank)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Скрыть карты
        /// </summary>
        public void Hide()
        {
            foreach (Card card in _cards)
            {
                card.Hide();
            }
        }
        /// <summary>
        /// Установить козырь
        /// </summary>
        /// <param name="Trump"></param>
        public void SetTrump(Card.Suits Trump)
        {
            _trump = Trump;
            foreach (Card card in _cards)
            {
                card.TrumpThis(Trump);
            }
        }
        /// <summary>
        /// Открыть карты
        /// </summary>
        public void Show()
        {
            foreach (Card card in _cards)
            {
                card.Show();
            }
        }
        /// <summary>
        /// Перетасовать колоду
        /// </summary>
        public void Shuffle()
        {
            List<Card> _tempPack = new List<Card>();
            Random R = new Random();
            for (; Count > 0;)
            {
                _tempPack.Add(TakeCard(R.Next(_cards.Count)));
            }
            _cards = _tempPack;
        }
        /// <summary>
        /// Упорядочить карты по убыванию стоимости
        /// </summary>
        public void Sort()
        {
            List<Card> _tempPack = new List<Card>();
            for (int p = 5; p > 0; p--)
            {
                var _tempcard = new Card((Card.Ranks)p, _trump);
                if (Contains(_tempcard))
                {
                    _tempPack.Add(_tempcard);
                }
            }
            for (int s = 0; s < 4; s++)
            {
                if ((Card.Suits)s != _trump)
                {
                    for (int p = 5; p > 0; p--)
                    {
                        var _tempcard = new Card((Card.Ranks)p, (Card.Suits)s);
                        if (Contains(_tempcard))
                        {
                            _tempPack.Add(_tempcard);
                        }
                    }
                }
            }
            _cards = _tempPack;
        }
        /// <summary>
        /// Взять все карты из колоды
        /// </summary>
        /// <returns>Пачка карт</returns>
        public Pack TakeAll()
        {
            Pack DestinationPack = new Pack(false);
            DestinationPack.AddCards(Cards);
            _cards = new List<Card>();
            return DestinationPack;
        }
        /// <summary>
        /// Взять указанную карту
        /// </summary>
        /// <param name="card">Карта</param>
        /// <returns>Вытаскивает карту из колоды</returns>
        public Card TakeCard(Card card)
        {
            if (this.Contains(card))
            {
                _cards.Remove(card);
                return card;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Взять указанную карту
        /// </summary>
        /// <param name="index">Порядковый номер карты в колоде</param>
        /// <returns>Вытаскивает карту из колоды</returns>
        public Card TakeCard(int index)
        {
            if (index < _cards.Count)
            {
                Card card = _cards[index];
                return TakeCard(card);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Взять несколько карт из колоды
        /// </summary>
        /// <param name="count">Необходимое количество карт</param>
        /// <returns>Пачка карт</returns>
        public Pack TakePack(int count)
        {
            Pack DestinationPack = new Pack(false);
            if (count > Count)
            {
                throw new Exception("Not enough cards in the pack");
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    DestinationPack.AddCard(TakeCard(0));
                }
            }
            return DestinationPack;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Сумма всех марьяжей, входящих в состав колоды
        /// </summary>
        /// <returns>Если они есть, то возвращается общая стоимость марьяжей, иначе 0. </returns>
        public int AllContainsMarriagePoints
        {
            get
            {
                int pts = 0;
                for (int i = 0; i < Marriages.Length; i++)
                {
                    pts += Card.MarriageCost(Marriages[i]);
                }
                return pts;
            }
        }
        /// <summary>
        /// Свойство, возвращающее список карт в виде массива.
        /// </summary>
        public Card[] Cards
        {
            get { return _cards.ToArray(); }
        }
        /// <summary>
        /// Содержит какой-либо марьяж
        /// </summary> 
        public bool ContainsAnyMarriage
        {
            get { return Marriages.Length > 0; }
        }
        /// <summary>
        /// Количество карт в колоде
        /// </summary>
        public int Count
        {
            get { return _cards.Count; }
        }
        /// <summary>
        /// Список мастей, марьяжи которых присутствуют в колоде
        /// </summary>
        public Card.Suits[] Marriages
        {
            get
            {
                List<Card.Suits> marriages = new List<Card.Suits>();
                bool containAllAces = true;
                for (int suit = 0; suit < 4; suit++)
                {
                    Card Ace = new Card(Card.Ranks.Туз, (Card.Suits)suit);
                    Card Queen = new Card(Card.Ranks.Дама, (Card.Suits)suit);
                    Card King = new Card(Card.Ranks.Король, (Card.Suits)suit);
                    if (Contains(Queen) && this.Contains(King))
                    {
                        marriages.Add((Card.Suits)suit);
                    }
                    containAllAces &= Contains(Ace);
                }
                if (containAllAces)
                {
                    marriages.Add(Card.Suits.Тузовая);
                }
                return marriages.ToArray();
            }

        }
        /// <summary>
        /// Сумма стоимостей всех карт в колоде.
        /// </summary>
        public int Points
        {
            get
            {
                int sum = 0;
                foreach (Card card in _cards)
                {
                    sum += Card.CardCost(card.Rank);
                }
                return sum;
            }
        }
        #endregion

        private List<Card> _cards = new List<Card>();
        Card.Suits _trump = Card.Suits.Нет;
    }
}
