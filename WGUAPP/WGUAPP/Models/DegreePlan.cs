using System.Collections.ObjectModel;

namespace WGUAPP.Models
{
    public static class DegreePlan
    {
        private static int lastId = 0;
        public static ObservableCollection<Term> Terms { get; set; } = [];

        public static void AddTerm(Term term)
        {
            lastId++;
            //term.Id = lastId;

            Terms.Add(term);
        }

        public static void RemoveTerm(Term term)
        {
            Terms.Remove(term);
        }
    }
}
