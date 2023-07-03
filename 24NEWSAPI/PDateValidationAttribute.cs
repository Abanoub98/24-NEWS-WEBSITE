namespace _24NEWSAPI
{
    public class PDateValidationAttribute : ValidationAttribute
    {
       
            public PDateValidationAttribute(int week)
            {
                this.WeekSpan = week;
            }

            public int WeekSpan { get; private set; }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    var date = (DateTime)value;
                    var now = DateTime.Now;

                TimeSpan time = new TimeSpan(0, 23, 59, 59);
                DateTime combined = now.Subtract(time);

                var futureDate = now.AddDays(this.WeekSpan);

                    if (combined <= date && date < futureDate)
                    {
                        return null;
                    }
                }

                return new ValidationResult(this.FormatErrorMessage(this.ErrorMessage));         
        }
    }
}
