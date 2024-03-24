using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class Transaction
    {

        [Key]
        public int TransactionId { get; set; }

        // CategoryId (foreign key in range [0, +inf]) + link to Category obj
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId {  get; set; }
        public Category? Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0.")]
        public int Amount { get; set; }

        // Create column in sql table, which can be null or have up to 75 chars
        [Column(TypeName = "nvarchar(75)")]
        public string? Note { get; set; }

        public DateTime Date {  get; set; } = DateTime.Now;

        // Specify that this field should not be mapped to a column in db; but rather just a string to be used anytime
        [NotMapped]
        public string? CategoryTitleWithIcon
        {
            get
            {
                return Category == null ? "" : Category.Icon + " " + Category.Title;
            }
        }

        // Also an string for $ amount of category, to be used anytime & not included in db
        [NotMapped]
        public string? FormattedAmount
        {
            get
            {
                return ((Category == null || Category.Type == "Expense") ? "- " : "+ ") + Amount.ToString("C0");
            }
        }
    }
}
