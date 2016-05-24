using System;
using System.Linq;
using System.ComponentModel;
using System.Drawing;

namespace TheThousandGame
{
    /// <summary>
    /// Класс, описывающий игрока
    /// </summary>
    class Player
    {
        /// <summary>
        /// Конструктор, описывающий игрока по его Имени и Фото.
        /// </summary>
        /// <param name="Name">Имя игрока</param>
        /// <param name="Photo">Фото игрока</param>
        public Player(string Name, Image Photo)
        {
            this._name = Name;
            this._photo = Photo;
            this.PrepareToGame();
        }

        #region Behavior
        /// <summary>
        /// Добавить карту в колоду.
        /// </summary>
        /// <param name="card"> Добавляемая карта </param>
        public void AddCard(Card card)
        {
            _pack.AddCard(card);
            _pack.ClassicSort();
        }
        /// <summary>
        /// Добавить несколько карт
        /// </summary>
        /// <param name="cards">Карты</param>
        public void AddCards(params Card[] cards)
        {
            foreach (Card card in cards)
            {
                AddCard(card);
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
                AddCard(card);
            }
        }
        /// <summary>
        /// Проверить себя на болт
        /// </summary>
        protected void CheckBoltPenalty()
        {
            if (CurrentPts(false) == 0)
            {
                _bolts++;
                if (Bolts == 3)
                {
                    _pts -= 120;
                    _bolts = 0;
                }
            }
        }
        /// <summary>
        /// Проверить себя на бочку
        /// </summary>
        protected void CheckBarrelPenalty()
        {
            // Если очков больше 880
            if (OnBarrel)
            {
                // Если очков больше или равно 1000
                if (_pts >= 1000)
                {
                    // то округлить до 1000
                    _pts = 1000;
                }
                else
                {
                    // если очков от 880 до 1000,
                    // проверяем количество попыток набрать 1000
                    switch (++_attempts)
                    {
                        case 1:
                            // значит влез на бочку
                            _barrels++;
                            _pts = 880;
                            break;
                        case 5:
                            // значит ничего не предпринял
                            if (_barrels == 3)
                            {
                                // если это 3яя бочка была, то на 0
                                _pts = 0;
                                _barrels = 0;
                            }
                            else
                            {
                                // если не 3яя, то на 760 + болты
                                _pts = 760;
                                CheckBoltPenalty();
                            }
                            // и сбрасываем попытки
                            _attempts = 0;
                            break;
                        default:
                            // иначе просто округляем до 880
                            _pts = 880;
                            CheckBoltPenalty();
                            if (!OnBarrel) CheckBarrelPenalty();
                            break;
                    }
                }
            }
            else
            {
                // Если очков меньше 880, а до этого он был на бочке трижды
                if (_attempts > 0 && _barrels == 3)
                {
                    _pts = 0;
                    _barrels = 0;
                    _attempts = 0;
                }
                else
                {
                    CheckBoltPenalty();
                }
                _attempts = 0;
            }
        }
        /// <summary>
        /// Проверить на самосвал
        /// </summary>
        protected void CheckDumperPenalty()
        {
            if (_pts == 555)
            {
                _pts = 0;
            }
        }
        /// <summary>
        /// Проверить себя на штрафы
        /// </summary>
        protected void CheckPenalty()
        {
            CheckBarrelPenalty();
            CheckDumperPenalty();
        }
        /// <summary>
        /// Сравнить с другим игроком
        /// </summary>
        /// <param name="obj">Игрок</param>
        /// <returns>1 (если >), 0 (если =), -1 (если меньше)</returns>
        public int CompareTo(object obj)
        {
            return _name.CompareTo(((Player)obj).Name);
        }
        /// <summary>
        /// Подсчет набранных очков
        /// </summary>
        /// <param name="Rounded">Округлять или нет</param>
        /// <returns>Кол-во набранных очков</returns>
        protected int CurrentPts(bool Rounded)
        {
            int Pts = _marriagepts + _harvestpack.Points;
            if (Rounded)
            {
                Pts += (Pts % 5 > 2 ? 5 : 0) - Pts % 5;
            }
            return Pts;
        }
        /// <summary>
        /// Объявит ли игрок этот картой козырь
        /// </summary>
        /// <param name="card">Карта</param>
        /// <returns> Объявит или нет </returns>
        protected bool DeclaredMarriage(Card card)
        {
            if (_pack.Count == 8) return false;
            switch (card.Rank)
            {
                case Card.Ranks.Дама:
                case Card.Ranks.Король: return _pack.Marriages.Contains(card.Suit);
                case Card.Ranks.Туз: return _pack.Marriages.Contains(Card.Suits.Тузовая);
                default: return false;
            }
        }
        /// <summary>
        /// Получить прикуп
        /// </summary>
        /// <param name="Bundle">Прикуп</param>
        public void GetBundle(Pack Bundle)
        {
            this.IsAgree = true;
            this.IsCroupier = true;
            this._harvestpack = Bundle;
            this._marriagepts += Bundle.AllContainsMarriagePoints;
            ToThink("Я в прикупе!");
        }
        /// <summary>
        /// Забрать карты со стола
        /// </summary>
        /// <param name="TablePack">Стол</param>
        public void Harvest(Pack pack)
        {
            ToThink("Моё");
            this._harvestpack.AddPack(pack);
        }
        /// <summary>
        /// Подведение итогов раунда
        /// </summary>
        public void Ingathering(bool doublepts)
        {
            int roundpts = 0;
            if (_promised)
            {
                if (CurrentPts(false) >= _promisedpts)
                {
                    roundpts += _promisedpts;
                    ToThink("Есть!");
                }
                else
                {
                    roundpts -= _promisedpts;
                    ToThink("Эх...");
                }
            }
            else
            {
                roundpts += CurrentPts(true);
            }
            roundpts *= doublepts ? 2 : 1;
            _pts += roundpts;
            CheckPenalty();
        }
        /// <summary>
        /// Подготовка игрока перед игрой
        /// </summary>
        public void PrepareToGame()
        {
            this._pts = 0;
            this.PrepareToRound();
            ToThink("Удачной игры!");
        }
        /// <summary>
        /// Подготовка игрока перед новым коном
        /// </summary>
        public void PrepareToRound()
        {
            this._isAgree = false;
            this._isCroupier = false;
            this._promisedpts = 0;
            this._marriagepts = 0;
            this._pack = new Pack(false);
            this._harvestpack = new Pack(false);
            ToThink("Хм...");
        }
        /// <summary>
        /// Пообещать ставку
        /// </summary>
        /// <param name="PromisedPts">Ставка</param>
        public void Promise(int PromisedPts)
        {
            this._promised = PromisedPts > 0;
            this._promisedpts = PromisedPts;
            ToThink(_promised ? "Попробую взять " + PromisedPts : "");
        }
        /// <summary>
        /// Взять все карты из колоды
        /// </summary>
        /// <returns>Пачка карт</returns>
        public Pack TakeAllCards()
        {
            return _pack.TakeAll();
        }
        /// <summary>
        /// Взять карту у игрока
        /// </summary>
        /// <param name="index">Порядковый номер карты</param>
        /// <returns>Карта</returns>
        public Card TakeCard(int index)
        {
            ToThink("Это вам...");
            //OnPropertyChanged(new PropertyChangedEventArgs("PlayerPack"));
            return _pack.TakeCard(index);
        }
        /// <summary>
        /// Подумать
        /// </summary>
        /// <param name="Thought">Мысль</param>
        protected void ToThink(string Thought)
        {
            _thoughts = Thought;
        }
        /// <summary>
        /// Штраф "двое на бочке"
        /// </summary>
        public void TwoOnBarrelPenalty()
        {
            _pts = 760;
            _attempts = 0;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Количество попыток взять 1000 на текущей бочке
        /// </summary>
        public int Attempts
        {
            get { return _attempts; }
        }
        /// <summary>
        /// Количество бочек
        /// </summary>
        public int Barrels
        {
            get { return _barrels; }
        }
        /// <summary>
        /// Количество болтов
        /// </summary>
        public int Bolts
        {
            get { return _bolts; }
        }
        /// <summary>
        /// Согласен этот игрок со ставкой или нет
        /// </summary>
        public bool IsAgree
        {
            get { return _isAgree; }
            set { _isAgree = value; }
        }
        /// <summary>
        /// Виртуален этот игрок или нет
        /// </summary>
        public bool IsAIPlayer
        {
            get { return _isAIPlayer; }
        }
        /// <summary>
        /// В прикупе этот игрок или нет
        /// </summary>
        public bool IsCroupier
        {
            get { return _isCroupier; }
            set { _isCroupier = value; }
        }
        /// <summary>
        /// Максимальное количество очков, которое может пообещать игрок
        /// </summary>
        public int MaxPointsForPromise
        {
            get { return (new Pack(true)).Points + _pack.AllContainsMarriagePoints; }
        }
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string Name
        {
            get { return _name; }
        }
        /// <summary>
        /// Находится ли игрок на бочке
        /// </summary>
        public bool OnBarrel
        {
            get { return _pts >= 880; }
        }
        /// <summary>
        /// Фото игрока
        /// </summary>
        public Image Photo
        {
            get { return _photo; }
        }
        /// <summary>
        /// Карты игрока
        /// </summary>
        public Pack PlayerPack
        {
            get { return _pack; }
        }
        /// <summary>
        /// Очки игрока
        /// </summary>
        public int Pts
        {
            get { return _pts; }
        }
        /// <summary>
        /// Количество очков этого раунда
        /// </summary>
        public int RoundPts
        {
            get { return CurrentPts(false); }
        }
        /// <summary>
        /// Мысли игрока
        /// </summary>
        public string Thought
        {
            get { return _thoughts; }
        }
        #endregion

        #region Fields
        protected string _name;
        protected string _thoughts;
        protected bool _isAgree;
        protected bool _isAIPlayer;
        protected bool _isCroupier;
        protected bool _promised;
        protected int _attempts;
        protected int _barrels;
        protected int _bolts;
        protected int _marriagepts;
        protected int _promisedpts;
        protected int _pts;
        protected Pack _pack, _harvestpack;
        protected Image _photo;
        #endregion
    }
}
