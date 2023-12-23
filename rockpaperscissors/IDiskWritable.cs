namespace rockpaperscissors
{
    internal interface IDiskWritable<T>
    {
        public T ReadFile(string path);
        public void WriteFile(string path);
    }
}
