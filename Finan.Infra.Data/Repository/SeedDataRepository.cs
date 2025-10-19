using Finan.Domain.Entities;
using Finan.Domain.Enums;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Finan.Infra.Data.Repository
{
    public class SeedDataRepository : ISeedDataRepository
    {
        private readonly BaseContext _context;

        public SeedDataRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task SeedDefaultDataAsync(Guid tenantId)
        {
            // Inserir grupos se não existirem
            if (!await _context.Group.AnyAsync(g => g.TenantId == tenantId))
            {
                var groups = new[]
                {
                    new Group { Description = "Despesas Fixas", Nature = NatureGroup.Debit, TenantId = tenantId },
                    new Group { Description = "Despesas Variáveis", Nature = NatureGroup.Debit, TenantId = tenantId },
                    new Group { Description = "Receitas de Trabalho", Nature = NatureGroup.Credit, TenantId = tenantId },
                    new Group { Description = "Receitas de Investimentos", Nature = NatureGroup.Credit, TenantId = tenantId },
                    new Group { Description = "Receitas Eventuais ou Extraordinárias", Nature = NatureGroup.Credit, TenantId = tenantId },
                };

                await _context.Group.AddRangeAsync(groups);
                await _context.SaveChangesAsync();
            }

            // Garantir mapa de grupos (description -> id) para usar como FK nas classificações.
            var groupsMap = await _context.Group
                .Where(g => g.TenantId == tenantId)
                .ToDictionaryAsync(g => g.Description ?? string.Empty, g => g.Id);

            // Inserir classificações sem popular navigation properties (somente GroupId) para evitar ciclos.
            if (!await _context.Classification.AnyAsync(c => c.TenantId == tenantId))
            {
                int GetGroupId(string desc)
                {
                    groupsMap.TryGetValue(desc, out var id);
                    return id;
                }

                var classifications = new[]
                {
                    new Classification { Description = "Moradia", GroupId = GetGroupId("Despesas Fixas"), TenantId = tenantId  },
                    new Classification { Description = "Alimentação", GroupId = GetGroupId("Despesas Variáveis"), TenantId = tenantId  },
                    new Classification { Description = "Transporte", GroupId = GetGroupId("Despesas Variáveis"), TenantId = tenantId  },
                    new Classification { Description = "Lazer", GroupId = GetGroupId("Despesas Variáveis"), TenantId = tenantId  },
                    new Classification { Description = "Viagens", GroupId = GetGroupId("Despesas Variáveis"), TenantId = tenantId  },
                    new Classification { Description = "Salário", GroupId = GetGroupId("Receitas de Trabalho"), TenantId = tenantId },
                    new Classification { Description = "Gratificações e Bônus", GroupId = GetGroupId("Receitas de Trabalho"), TenantId = tenantId  },
                    new Classification { Description = "Rendimentos de Poupança", GroupId = GetGroupId("Receitas de Investimentos"), TenantId = tenantId  },
                    new Classification { Description = "Dividendos", GroupId = GetGroupId("Receitas de Investimentos"), TenantId = tenantId },
                    new Classification { Description = "Juros de Investimentos", GroupId = GetGroupId("Receitas de Investimentos"), TenantId = tenantId  },
                    new Classification { Description = "Aluguel de Imóveis", GroupId = GetGroupId("Receitas de Investimentos"), TenantId = tenantId  },
                    new Classification { Description = "Presentes", GroupId = GetGroupId("Receitas Eventuais ou Extraordinárias"), TenantId = tenantId  },
                    new Classification { Description = "Vendas de Bens", GroupId = GetGroupId("Receitas Eventuais ou Extraordinárias"), TenantId = tenantId  },
                    new Classification { Description = "Educação e Cursos", GroupId = GetGroupId("Despesas Variáveis"), TenantId = tenantId  },
                    new Classification { Description = "Cartão de Crédito", GroupId = GetGroupId("Despesas Variáveis"), TenantId = tenantId  },
                    new Classification { Description = "Serviços de Internet, Gás e Energia", GroupId = GetGroupId("Despesas Variáveis"), TenantId = tenantId  },
                    new Classification { Description = "Pagamento de Impostos", GroupId = GetGroupId("Despesas Variáveis"), TenantId = tenantId },
                    new Classification { Description = "Restituição de Impostos", GroupId = GetGroupId("Receitas Eventuais ou Extraordinárias"), TenantId = tenantId },
                };

                await _context.Classification.AddRangeAsync(classifications);
                await _context.SaveChangesAsync();
            }

            // Currencies
            if (!await _context.Currency.AnyAsync(c => c.TenantId == tenantId))
            {
                var currencies = new[]
                {
                    new Currency { Name = "Real", Code = "BRL", Symbol = "R$", TenantId = tenantId  },
                    new Currency { Name = "Dólar", Code = "USD", Symbol = "$", TenantId = tenantId  }
                };

                await _context.Currency.AddRangeAsync(currencies);
                await _context.SaveChangesAsync();
            }

            // CostCenters
            if (!await _context.CostCenter.AnyAsync(cc => cc.TenantId == tenantId))
            {
                var costCenters = new[]
                {
                    new CostCenter { Description = "Casa", TenantId = tenantId  },
                    new CostCenter { Description = "Transporte", TenantId = tenantId  },
                    new CostCenter { Description = "Saúde", TenantId = tenantId  },
                    new CostCenter { Description = "Educação", TenantId = tenantId  },
                    new CostCenter { Description = "Lazer", TenantId = tenantId  },
                    new CostCenter { Description = "Investimentos", TenantId = tenantId  },
                    new CostCenter { Description = "Alimentação", TenantId = tenantId  },
                    new CostCenter { Description = "Cartão de Crédito", TenantId = tenantId  },
                    new CostCenter { Description = "Doações e Presentes", TenantId = tenantId  },
                    new CostCenter { Description = "Trabalho Formal", TenantId = tenantId  },
                    new CostCenter { Description = "Trabalho Autônomo", TenantId = tenantId  },
                    new CostCenter { Description = "Aluguéis", TenantId = tenantId  },
                    new CostCenter { Description = "Outras Receitas", TenantId = tenantId  }
                };

                await _context.CostCenter.AddRangeAsync(costCenters);
                await _context.SaveChangesAsync();
            }

            // Banks
            if (!await _context.Bank.AnyAsync(b => b.TenantId == tenantId))
            {
                var banks = new[]
                {
                    new Bank { Name = "Banco do Brasil S.A.", Code = "001", TenantId = tenantId  },
                    new Bank { Name = "Banco Santander (Brasil) S.A.", Code = "033", TenantId = tenantId  },
                    new Bank { Name = "Caixa Econômica Federal", Code = "104", TenantId = tenantId  },
                    new Bank { Name = "Banco Bradesco S.A.", Code = "237", TenantId = tenantId  },
                    new Bank { Name = "Itaú Unibanco S.A.", Code = "341", TenantId = tenantId  },
                    new Bank { Name = "Nu Pagamentos S.A.", Code = "260", TenantId = tenantId  },
                    new Bank { Name = "Banco C6 S.A.", Code = "336", TenantId = tenantId  },
                    new Bank { Name = "Banco Inter S.A.", Code = "077", TenantId = tenantId  }
                };

                await _context.Bank.AddRangeAsync(banks);
                await _context.SaveChangesAsync();
            }
        }
    }
}
