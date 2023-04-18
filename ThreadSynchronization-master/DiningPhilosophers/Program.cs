using DiningPhilosophers;

public class Program
{
    // Makes a list as an objective that holds objectives.
    public static Fork[] forks = new Fork[5] { new Fork(), new Fork(), new Fork(), new Fork(),  new Fork() };
    // Making Philosopher object
    static Philosopher[] Philosophers = new Philosopher[5];
    public static void Main()
    {
        // Creats the Pilosophers in a loop and create threats with 1 philosopher each
        for (int i = 0; i < 5; i++)
        {
            Philosopher phil = new Philosopher(i);
            Philosophers[i] = phil;
        }
        for (int i = 0; i < Philosophers.Length; i++)
        {
            ThreadPool.QueueUserWorkItem(Philosophers[i].Do);
        }
        Console.Read();
    }
}
