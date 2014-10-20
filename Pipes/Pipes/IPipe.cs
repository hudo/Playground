namespace Pipes
{
    public interface IPipe<I, O>
    {
        I Input { set; }
        O Output { set; }

        void Execute();
    }
}