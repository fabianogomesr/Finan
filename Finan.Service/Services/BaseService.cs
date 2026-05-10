using Finan.Contracts.Enums;
using Finan.Contracts.Messages;
using Finan.Domain.Interfaces;
using FluentValidation;

namespace Finan.Application.Services
{
    public class BaseService : IBaseService
    {
        public MessageCollection Messages { get; set; } = new();

        public bool Validate<TEntity>(TEntity obj, IValidator<TEntity> validator)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "Registros não detectados!");
            if (validator == null)
                throw new ArgumentNullException(nameof(validator), "Validador não pode ser nulo!");

            // Limpa mensagens de validação anteriores
            Messages.Clear();

            var result = validator.Validate(obj);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                    Messages.Add(MessageRecord.Create(MessageType.Error, error.ErrorMessage));
            }

            return result.IsValid;
        }
    }
}
