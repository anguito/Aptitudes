using FluentValidation;

namespace Aptitudes
{
    public class AptitudesValidation : AbstractValidator<Aptitud>
    {
        public AptitudesValidation()
        {
            var properties = typeof(Aptitud).GetProperties();

            foreach (var property in properties)
            {
                // Para todos los tipos (genérico)
                RuleFor(x => property.GetValue(x))
                    .NotEmpty()
                    .WithMessage($"'{property.Name}' esta vacio");
            }
        }
    }
}