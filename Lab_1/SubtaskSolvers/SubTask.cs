namespace Lab_1.SubtaskSolvers
{
    public abstract class SubTask<T> where T : struct
    {
        public abstract void Execute (T obj);
    }
}
