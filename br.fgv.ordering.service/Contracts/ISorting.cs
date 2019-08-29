namespace br.fgv.ordering.service.Service
{
    public interface ISorting
    {
        string ColumnName { get; set; }
        bool Ascending { get; set; }
    }

    public class Sorting : ISorting
    {
        public string ColumnName { get; set; }
        public bool Ascending { get; set; }

        public Sorting()
        {
            Ascending = true;
        }
    }
}