using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Entities;

namespace VM.Domain.Abstractions;
internal interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetByConditionAsync(
        Expression<Func<IEnumerable<Payment>, bool>> condition, CancellationToken cancellationToken);
    Task AddAsync(Payment payment, CancellationToken cancellationToken);
    void Update(Payment payment);
}
