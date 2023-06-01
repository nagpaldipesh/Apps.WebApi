using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using WebApi.DbEntities;

namespace WebApi.Services.Documents {
    public class PagesDocument : IDocument {
        public IEnumerable<Page> Model { get; }
        public PagesDocument(IEnumerable<Page> model) { Model = model; }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container) {
            container
                .Page(page => {
                    page.Margin(50);
                    page.Content().Background(Colors.Grey.Lighten3);
                    page.Content().Element(ComposeContent);
                });
        }

        void ComposeContent(IContainer container) {
            container.PaddingVertical(40).Column(column => {
                column.Spacing(5);

                column.Item().Element(ComposeTable);
            });
        }
        void ComposeTable(IContainer container) {
            container.Table(table => {
                // step 1
                table.ColumnsDefinition(columns => {
                    columns.RelativeColumn(150);
                    columns.RelativeColumn(150);

                });

                // step 2
                table.Header(header => {
                    header.Cell().Element(CellStyle).Text("Code");
                    header.Cell().Element(CellStyle).Text("Title");

                    static IContainer CellStyle(IContainer container) {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var item in Model) {
                    table.Cell().Element(CellStyle).Text(item.Code);
                    table.Cell().Element(CellStyle).Text(item.Title);

                    static IContainer CellStyle(IContainer container) {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }
    }
}
