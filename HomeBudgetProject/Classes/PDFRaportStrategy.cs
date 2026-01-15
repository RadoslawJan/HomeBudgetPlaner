using HomeBudgetProject.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace HomeBudgetProject.Classes
{
    public class PDFRaportStrategy : IRaportStrategy
    {
        public void GenerateRaport(HomeBudgetPlanner planner)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content().Column(column =>
                    {
                        column.Item().Text("RAPORT BUDŻETU DOMOWEGO")
                            .FontSize(20)
                            .Bold()
                            .AlignCenter();

                        column.Item().PaddingVertical(10);

                        foreach (var item in planner.budgetItemsList)
                        {
                            RenderItem(column, item, 0);
                        }
                    });
                });
            })
            .GeneratePdf("budget_report.pdf");

            Console.WriteLine("Raport PDF zapisany: budget_report.pdf");
        }

        private void RenderItem(ColumnDescriptor column, BudgetItem item, int indent)
        {
            string prefix = new string(' ', indent * 4);

            if (item is BudgetGroup group)
            {
                column.Item().Text($"{prefix}[GRUPA] {group.Name} | SUMA: {group.GetValue()}")
                    .Bold();

                foreach (var subItem in group.budgetItemList)
                {
                    RenderItem(column, subItem, indent + 1);
                }
            }
            else
            {
                column.Item().Text($"{prefix}{item.Name} -> {item.GetValue()}");
            }
        }
    }
}
