using Finan.Domain.Entities;
using Finan.Domain.Enums;
using Finan.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Finan.Infra.Data.Initializer
{
	public class DatabaseInitializer
	{
		public static async Task SeedAsync(IServiceProvider serviceProvider)
		{
			using (var scope = serviceProvider.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<BaseContext>();
				var logger = scope.ServiceProvider.GetRequiredService<ILogger<DatabaseInitializer>>();

				using (var transaction = await context.Database.BeginTransactionAsync())
				{
					try
					{
						logger.LogInformation("Verificando se o banco de dados precisa de migrações...");
						await context.Database.MigrateAsync(); // Aplica as migrations automaticamente

						logger.LogInformation("Verificando se há registros iniciais...");
						if (!context.FinancialGroup.Any()) // Evita duplicação de dados
						{
							logger.LogInformation("Inserindo registros iniciais...");

							context.FinancialGroup.AddRange(new[]
							{
								new FinancialGroup { Description = "Despesas Fixas" },
								new FinancialGroup { Description = "Despesas Variáveis" },
								new FinancialGroup { Description = "Receitas de Trabalho" },
								new FinancialGroup { Description = "Receitas de Investimentos" },
								new FinancialGroup { Description = "Receitas Eventuais ou Extraordinárias" },
                                new FinancialGroup { Description = "Impostos" }
                            });

							await context.SaveChangesAsync();

							context.FinancialClassification.AddRange(new[]
							{
								new FinancialClassification { Description = "Moradia", FinancialGroupId = 1, Type = ClassificationType.Expense },
								new FinancialClassification { Description = "Alimentação", FinancialGroupId = 2, Type = ClassificationType.Expense },
								new FinancialClassification { Description = "Transporte", FinancialGroupId = 2, Type = ClassificationType.Expense },
								new FinancialClassification { Description = "Lazer", FinancialGroupId = 2, Type = ClassificationType.Expense },
								new FinancialClassification { Description = "Viagens", FinancialGroupId = 2, Type = ClassificationType.Expense },
								new FinancialClassification { Description = "Salário", FinancialGroupId = 3, Type = ClassificationType.Income },
								new FinancialClassification { Description = "Gratificações e Bônus", FinancialGroupId = 3, Type = ClassificationType.Income },
								new FinancialClassification { Description = "Rendimentos de Poupança", FinancialGroupId = 4, Type = ClassificationType.Income },
								new FinancialClassification { Description = "Dividendos", FinancialGroupId = 4, Type = ClassificationType.Income },
								new FinancialClassification { Description = "Juros de Investimentos", FinancialGroupId = 4, Type = ClassificationType.Income },
								new FinancialClassification { Description = "Aluguel de Imóveis", FinancialGroupId = 4, Type = ClassificationType.Income },
								new FinancialClassification { Description = "Presentes", FinancialGroupId = 5, Type = ClassificationType.Income },
								new FinancialClassification { Description = "Vendas de Bens", FinancialGroupId = 5, Type = ClassificationType.Income },
								new FinancialClassification { Description = "Educação e Cursos", FinancialGroupId = 2, Type = ClassificationType.Expense },
								new FinancialClassification { Description = "Cartão de Crédito", FinancialGroupId = 2, Type = ClassificationType.Expense },
                                new FinancialClassification { Description = "Serviços de Internet, Gás e Energia", FinancialGroupId = 2, Type = ClassificationType.Expense },
                                new FinancialClassification { Description = "Pagamento e Restituição de Impostos", FinancialGroupId = 2, Type = ClassificationType.Expense }
                            });

							context.CostCenter.AddRange(new[]
							{
								new CostCenter { Description = "Casa" },
								new CostCenter { Description = "Transporte" },
								new CostCenter { Description = "Saúde" },
								new CostCenter { Description = "Educação" },
								new CostCenter { Description = "Lazer" },
								new CostCenter { Description = "Investimentos" },
                                new CostCenter { Description = "Alimentação" },
                                new CostCenter { Description = "Cartão de Crédito" },
                                new CostCenter { Description = "Doações e Presentes" },
                                new CostCenter { Description = "Trabalho Formal" },
                                new CostCenter { Description = "Trabalho Autônomo" },
                                new CostCenter { Description = "Aluguéis" },
                                new CostCenter { Description = "Outras Receitas" }
                            });

							context.Payer.AddRange(new[]
							{
								new Payer { Name = "Pix" },
								new Payer { Name = "Boleto" },
								new Payer { Name = "Transferência Bancária" },
								new Payer { Name = "Débito Automático" }
							});

							context.Currency.AddRange(new[]
							{
								new Currency { Name = "Real", Code = "BRL", Symbol = "R$" },
								new Currency { Name = "Dólar", Code = "USD", Symbol = "$" }
							});

							context.Bank.AddRange(new[]
							{
								new Bank { Name = "Banco do Brasil S.A.", Code = "001"},
								new Bank { Name = "Banco Santander (Brasil) S.A.", Code = "033" },
								new Bank { Name = "Caixa Econômica Federal", Code = "104" },
								new Bank { Name = "Banco Bradesco S.A.", Code = "237" },
								new Bank { Name = "Itaú Unibanco S.A.", Code = "341" },
								new Bank { Name = "Nu Pagamentos S.A.", Code = "260" },
								new Bank { Name = "Banco C6 S.A.", Code = "336" },
								new Bank { Name = "Banco Inter S.A.", Code = "077" }
							});

							context.User.AddRange(new[]
							{
								new User { UserName = "Finan", Password = "Finan@1234", Email = "dev.fabianorocha@gmail.com", Role = "Maganer" }
							});

							await context.SaveChangesAsync();
							await transaction.CommitAsync();
							logger.LogInformation("Registros iniciais inseridos com sucesso!");
						}
						else
						{
							logger.LogInformation("Os registros já existem, pulando a inserção.");
						}
					}
					catch (Exception ex)
					{
						await transaction.RollbackAsync();
						logger.LogError(ex, "Erro ao tentar inicializar o banco de dados.");
					}
				}
			}
		}
	}
}
