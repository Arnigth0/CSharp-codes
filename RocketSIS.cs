using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args) // Главный 
    {
        Console.WriteLine("Добро пожаловать на нашу ферму муравьев!" +
            "\nВы в роли королевы.\n");
        AntCreate antCreate = new AntCreate();//Вызываем паттерн Фактори, создаем объект класса AntCreate
        antCreate.CreateAnt();// Вызоваем метод CreateAnt()

        Console.WriteLine("Немного информации о королеве: ");
        Queen queen = Queen.QueenAnt; //Singlton, информация о королеве

        Console.WriteLine("\nПосмотрите как развивается муровей!" +
            "\nПросто замечтально!");
        AbstractAnt ant = new Egg();//Паттерн Декоратор /яйцо
        ant.EvolutionOfAnt();//вызываем объект класса Egg. Начало эволюции 

        ant = new Larva();// Личинка
        ant.EvolutionOfAnt();

        ant = new Doll();//Кукла
        ant.EvolutionOfAnt();

        ant = new FinishAnt();// Полноценный муровей
        ant.EvolutionOfAnt();
        Console.WriteLine("\nКстати, колонии нужна ваша помощь:\n" +
            "1.Разведчики нашли еду.\n" +
            "2.На колонию нападают.\n" +
            "3.Поблизости другие муравьи/ пища.\n" +
            "4.Королева хочет есть. \n" +
            "0.Не буду помогать!");
        int chose = 1;//Вводим чтобы цикл сразу не рухнул
        while (chose > 0 && chose < 5)
        {
            chose = Convert.ToInt32(Console.ReadLine());//вводим значение
            switch (chose)
            {
                case 1: // Паттерн Стратегии 
                    Gathering gathering = new Gathering();
                    gathering.Action();// Собирательсвто
                    break;
                case 2:
                    Defend defend = new Defend();
                    defend.Action();//Защита
                    break;
                case 3:
                    Attack attack = new Attack();
                    attack.Action();//Аттака
                    break;
                case 4:
                    FeadTheQueen fead = new FeadTheQueen();
                    fead.Action();//Кормление королевы
                    break;
                default:
                    Console.WriteLine("Cпасибо за подсказки.");
                    break;
            }
        }
        //Observer паттерн
        Console.WriteLine("\nТак же мы можем посмотреть на численность колонии. ");
        Ants ants = new Ants();//Вызов наблюдаемого обекта
        QueenObserver observer = new QueenObserver(ants);//Наблюдатель
        ants.CreateEggs();//Вызов метода создание яйц
    }
}

// Factory method/паттерн
public interface ICreate // Интерфейс для специи
{
    void Ant(int i);// Метод наследуемый наследникам
}

class WorkerAnt : ICreate//Класс специи
{
    public void Ant(int i) // имплементируя реализуем метод
    {
        Console.WriteLine($"Рабочих-муравьев было создано,{i} штук.");
    }
}

class WarriorAnt : ICreate// также как и классе рабочих муравьев
{
    public void Ant(int i)
    {
        Console.WriteLine($"Войнов-муравьев было создано, {i} штук.\n");
    }
}

public abstract class IAntCreate//абстрактный класс для фабрики
{
    public abstract ICreate CreateAnt();//метод для создания объекта из классов специй
}

class AntCreate : IAntCreate//Класс для фабрики
{
    public override ICreate CreateAnt()
    {
        ICreate ant = null; //созлдаем объект муравья
        Console.WriteLine("Каких муравьев хотите создать?" +// Сообщения
            "\nИ в каком количестве?" +
            "\n1. для создания Воина-Муровья." +
            "\n2. для создания Рабочих-Муровьев." +
            "\nЧто нибудь для выхода.");
        int chose = Convert.ToInt32(Console.ReadLine());//выбираем какой класс будем создовать
        switch (chose)
        {
            case 1:
                int i = Convert.ToInt32(Console.ReadLine());
                ant = new WarriorAnt();// переопределяем класс как WarriorAnt
                ant.Ant(i);//вызываем метод 
                break;

            case 2:
                int j = Convert.ToInt32(Console.ReadLine());
                ant = new WorkerAnt();// переопределяем класс как WorkerAnt
                ant.Ant(j);//вызываем метод 
                break;

            default: // default case 
                Console.WriteLine("Муравьи закончились...");
                break;
        }
        return ant;// возвращаем муравья
    }
}
// Singlton паттерн
sealed class Queen // sealed класс который не наследуется  
{
    private static Queen queen = new Queen();// вызываем объект класса в классе 
    private readonly string NameOfAnts = "Solenopsis invicta";//вид королевы

    private Queen()
    {
        Console.WriteLine($"Королева муравьев вида '{NameOfAnts}'.");// когда будет обявляться синглтон это сообщение аавтоматом вывведедться
    }

    public static Queen QueenAnt
    {
        get
        {
            Console.WriteLine("Королева единственная!"); // как и это, и при последующем создании нового объекта класса будет вовыдитьяс что "Королева единственная!"
            return queen;
        }
    }
}

// Паттерн стратегии
interface IAction // главный Интерфейс  
{
    void Action();// метод действие 
}

class Gathering : IAction// наслдует от IAction и реализует метод
{
    public void Action()// имплементированный метод от IAction
    {
        Console.WriteLine("Собирают еду.");
    }
}

class Defend : IAction//Дальше все повторяется, только метод меняется
{
    public void Action()
    {
        Console.WriteLine("Защишаются от врагов.");
    }
}

class FeadTheQueen : IAction
{
    public void Action()
    {
        Console.WriteLine("Кормять королеву.");
    }
}

class Attack : IAction
{
    public void Action()
    {
        Console.WriteLine("Аттакуют жертву.");
    }
}

//Decorator паттерн
abstract class AbstractAnt//  основной класс который не изменяется
{
    public abstract void EvolutionOfAnt();
}

class Egg : AbstractAnt //класс от которого создается объект.
{
    public override void EvolutionOfAnt()
    {
        Console.Write("Яйцо");
    }
}

abstract class AntDecorator : AbstractAnt //предок всех декораторов
{
    protected AbstractAnt ant;
    public void SetComponent(AbstractAnt ant)//указывает какой объект должен поменяться
    {
        this.ant = ant;
    }

    public override void EvolutionOfAnt()
    {
        if (ant != null)
        {
            ant.EvolutionOfAnt();
        }
    }
}

class Larva : AntDecorator // наследуется от декоратора
{
    public override void EvolutionOfAnt()//
    {
        base.EvolutionOfAnt();// базируемся от EvolutionOfAnt
        AddLarva();//добавляем сообщение => Личинка 
    }

    private void AddLarva()
    {
        Console.Write(" => Личинка");
    }
}

class Doll : AntDecorator// остальные двое также ничего не меняется
{
    public override void EvolutionOfAnt()
    {
        base.EvolutionOfAnt();
        AddDoll();
    }

    private void AddDoll()
    {
        Console.Write(" => Кукла");
    }
}

class FinishAnt : AntDecorator
{
    public override void EvolutionOfAnt()
    {
        base.EvolutionOfAnt();
        AddAnt();
    }

    private void AddAnt()
    {
        Console.Write(" => Муравей. \nМуравей готов! \n");
    }
}

interface IObserver // наблюдатель
{
    void Update(object o);//Метод Update
}

class QueenObserver : IObserver// Сам наблюдатель
{
    IObservable observable;//Свойсто, используется не один раз

    public QueenObserver(IObservable obs)// параметризованный конкструктор который приннимает одно значение
    {
        observable = obs;
        observable.RegisteredObserver(this);// регистрируемся
    }

    public void Update(object o)// реализвонный метод Update
    {
        NumberOfAntsInfo antsInfo = (NumberOfAntsInfo)o;// берется кол-во муравей из хранилища 
        if (antsInfo.Amount > 150)
        {
            Console.WriteLine($"Общее количество муравей: {antsInfo.Amount}, колония расцветает.");
        }
        else if (antsInfo.Amount < 150)
        {
            Console.WriteLine($"Общее количество муравей: {antsInfo.Amount}, колония погибает...");
        }
    }
}

class NumberOfAntsInfo // Хранище количество
{
    public int Amount { get; set; }//Здесь хранится колво
}

interface IObservable// Наблюдаемый 
{
    void CreateEggs();//метод для генерирования яйц
    void NotifyObservers();//метод для оповещения наблюдателей
    void RegisteredObserver(IObserver o);//метод для сохранения в список новых наблюдателей

}

class Ants : IObservable// Класс наблюдаемого // наследуем интерфейс 
{
    List<IObserver> observers;// нужное свойство
    NumberOfAntsInfo antsInfo;// объевляем класс NumberOfAntsInfo и берем из него колво

    public Ants()
    {
        observers = new List<IObserver>();
        antsInfo = new NumberOfAntsInfo();
    }

    public void CreateEggs()
    {
        Random random = new Random();// Обьевляем рандом 
        antsInfo.Amount += random.Next(100, 500);//Рандом числа от 100 до 500
        NotifyObservers();//вызываем  NotifyObservers()
    }

    public void NotifyObservers()// реализуем метод
    {
        foreach (IObserver observer in observers)
        {
            observer.Update(antsInfo);//Здесь вызываем метод Update из observer
        }
    }

    public void RegisteredObserver(IObserver o)
    {
        observers.Add(o);
    }
}