using System.ComponentModel;

namespace DiningPhilosophers;

public class Philosopher
{
    public int Id;
    private int _rightFork;
    private int _leftFork;

    //It makes sure the philosophers get the right forks
    public Philosopher(int id )
    {
        Id = id;
        if(id == 0)
        {
            _rightFork = 0;
            _leftFork = 4;
        }
        else 
        {
            _rightFork = Id;
            _leftFork = Id-1;
        }
    }
    // Runs the state of the philosophers
    public void Do(object callback)
    {
        while (true)
        {
            Think();
            Wait();
        }
    }

    // If there is no forks available, it will run this code and monitor how many cycles it goes through without eating
    private void Wait()
    {
        int tries = 0;
        bool ate = false;
        Console.WriteLine("Philosopher{0} is waiting", Id);
        while (!ate)
        {
            try
            {
                if (Monitor.TryEnter(Program.forks[_rightFork]))
                {
                    if (Monitor.TryEnter(Program.forks[_leftFork]))
                    {
                        Console.WriteLine("Philosopher{0} tried {1} times before eating", Id, tries);
                        tries = 0;
                        ate = true;
                        Eat();
                        Monitor.Exit(Program.forks[_leftFork]);
                    }
                    Monitor.Exit(Program.forks[_rightFork]);
                }
                tries++;
                
            }
            finally
            {

                if(tries == 100)
                {
                    Dead();
                }
                Thread.Sleep(100);
            }

        }
    }

    // Abort to kill a thread

    private void Dead()
    {
        Console.WriteLine("Philosopher{0} is DEAD", Id);
        while (true)
        {
            //Thread.Abort is deprecated
        }
    }

    // Eating with a randomized value
    private void Eat()
    {
        Console.WriteLine("Philosopher{0} is eating", Id);
        Random r = new Random();
        Thread.Sleep(r.Next(500, 2000));
    }
    // Think with a randomized value
    private void Think()
    {
        Console.WriteLine("Philosopher{0} is thinking", Id);
        Random r = new Random();
        Thread.Sleep(r.Next(500, 2000));

    }
}
