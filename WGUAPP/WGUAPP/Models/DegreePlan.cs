using System.Collections.ObjectModel;

namespace WGUAPP.Models
{
    public class DegreePlan
    {
        public static ObservableCollection<Term> Terms { get; set; } = [];

        public static void AddTerm(Term term)
        {
            Terms.Add(term);
        }
    }
}
