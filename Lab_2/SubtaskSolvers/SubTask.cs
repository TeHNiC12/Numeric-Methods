namespace Lab_2.SubtaskSolvers
{
    public abstract class SubTask<T> where T : struct
    {
        public abstract void Execute(T obj);
    }
}
