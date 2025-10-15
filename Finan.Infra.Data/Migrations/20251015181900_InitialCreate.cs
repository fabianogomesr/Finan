using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Finan.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Code = table.Column<string>(type: "varchar(5)", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostCenter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Code = table.Column<string>(type: "varchar(3)", nullable: false),
                    Symbol = table.Column<string>(type: "varchar(5)", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    Nature = table.Column<byte>(type: "tinyint", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UserQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Agency = table.Column<string>(type: "varchar(10)", nullable: false),
                    Number = table.Column<string>(type: "varchar(15)", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classification_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_SubscriptionPlan_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostCenterId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    ClassificationId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccrualPeriodDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Observation = table.Column<string>(type: "varchar(100)", nullable: false),
                    AccountInId = table.Column<int>(type: "int", nullable: false),
                    AccountOutId = table.Column<int>(type: "int", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankTransaction_Classification_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Classification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankTransaction_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankTransaction_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostCenterId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    ClassificationId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LateFee = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPaid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CashFlowDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AccrualPeriodDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Observation = table.Column<string>(type: "varchar(500)", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Classification_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Classification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", nullable: false),
                    Role = table.Column<string>(type: "varchar(100)", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReconciledDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: true),
                    BankTransactionId = table.Column<int>(type: "int", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Reversed = table.Column<bool>(type: "bit", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statement_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statement_BankTransaction_BankTransactionId",
                        column: x => x.BankTransactionId,
                        principalTable: "BankTransaction",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Statement_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Bank",
                columns: new[] { "Id", "Code", "ContractId", "Name" },
                values: new object[,]
                {
                    { 1, "001", 0, "Banco do Brasil S.A." },
                    { 2, "033", 0, "Banco Santander (Brasil) S.A." },
                    { 3, "104", 0, "Caixa Econômica Federal" },
                    { 4, "237", 0, "Banco Bradesco S.A." },
                    { 5, "341", 0, "Itaú Unibanco S.A." },
                    { 6, "260", 0, "Nu Pagamentos S.A." },
                    { 7, "336", 0, "Banco C6 S.A." },
                    { 8, "077", 0, "Banco Inter S.A." }
                });

            migrationBuilder.InsertData(
                table: "CostCenter",
                columns: new[] { "Id", "ContractId", "Description" },
                values: new object[,]
                {
                    { 1, 0, "Casa" },
                    { 2, 0, "Transporte" },
                    { 3, 0, "Saúde" },
                    { 4, 0, "Educação" },
                    { 5, 0, "Lazer" },
                    { 6, 0, "Investimentos" },
                    { 7, 0, "Alimentação" },
                    { 8, 0, "Cartão de Crédito" },
                    { 9, 0, "Doações e Presentes" },
                    { 10, 0, "Trabalho Formal" },
                    { 11, 0, "Trabalho Autônomo" },
                    { 12, 0, "Aluguéis" },
                    { 13, 0, "Outras Receitas" }
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "Code", "ContractId", "Name", "Symbol" },
                values: new object[,]
                {
                    { 1, "BRL", 0, "Real", "R$" },
                    { 2, "USD", 0, "Dólar", "$" }
                });

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "Id", "ContractId", "Description", "Nature" },
                values: new object[,]
                {
                    { 1, 0, "Despesas Fixas", (byte)0 },
                    { 2, 0, "Despesas Variáveis", (byte)0 },
                    { 3, 0, "Receitas de Trabalho", (byte)1 },
                    { 4, 0, "Receitas de Investimentos", (byte)1 },
                    { 5, 0, "Receitas Eventuais ou Extraordinárias", (byte)1 }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlan",
                columns: new[] { "Id", "Name", "UserQuantity", "Value" },
                values: new object[,]
                {
                    { 1, "Gratuito", 1, 0m },
                    { 2, "Profissional", 5, 29.90m },
                    { 3, "Empresarial", 20, 99.90m }
                });

            migrationBuilder.InsertData(
                table: "Classification",
                columns: new[] { "Id", "ContractId", "Description", "GroupId" },
                values: new object[,]
                {
                    { 1, 0, "Moradia", 1 },
                    { 2, 0, "Alimentação", 2 },
                    { 3, 0, "Transporte", 2 },
                    { 4, 0, "Lazer", 2 },
                    { 5, 0, "Viagens", 2 },
                    { 6, 0, "Salário", 3 },
                    { 7, 0, "Gratificações e Bônus", 3 },
                    { 8, 0, "Rendimentos de Poupança", 4 },
                    { 9, 0, "Dividendos", 4 },
                    { 10, 0, "Juros de Investimentos", 4 },
                    { 11, 0, "Aluguel de Imóveis", 4 },
                    { 12, 0, "Presentes", 5 },
                    { 13, 0, "Vendas de Bens", 5 },
                    { 14, 0, "Educação e Cursos", 2 },
                    { 15, 0, "Cartão de Crédito", 2 },
                    { 16, 0, "Serviços de Internet, Gás e Energia", 2 },
                    { 17, 0, "Pagamento de Impostos", 2 },
                    { 18, 0, "Restituição de Impostos", 5 }
                });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "EndDate", "IsActive", "StartDate", "SubscriptionPlanId" },
                values: new object[] { 1, new DateTime(2124, 10, 15, 15, 18, 59, 970, DateTimeKind.Local).AddTicks(3291), true, new DateTime(2025, 10, 15, 15, 18, 59, 970, DateTimeKind.Local).AddTicks(3278), 1 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ContractId", "Email", "Password", "Role", "UserName" },
                values: new object[] { 1, 1, "dev.fabianorocha@gmail.com", "Finan@1234", "Manager", "Finan" });

            migrationBuilder.CreateIndex(
                name: "IX_Account_BankId",
                table: "Account",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransaction_ClassificationId",
                table: "BankTransaction",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransaction_CostCenterId",
                table: "BankTransaction",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransaction_GroupId",
                table: "BankTransaction",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Classification_GroupId",
                table: "Classification",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_SubscriptionPlanId",
                table: "Contract",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Statement_AccountId",
                table: "Statement",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Statement_BankTransactionId",
                table: "Statement",
                column: "BankTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Statement_TransactionId",
                table: "Statement",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ClassificationId",
                table: "Transaction",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CostCenterId",
                table: "Transaction",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CurrencyId",
                table: "Transaction",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_GroupId",
                table: "Transaction",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ContractId",
                table: "User",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statement");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "BankTransaction");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Classification");

            migrationBuilder.DropTable(
                name: "CostCenter");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "SubscriptionPlan");

            migrationBuilder.DropTable(
                name: "Group");
        }
    }
}
