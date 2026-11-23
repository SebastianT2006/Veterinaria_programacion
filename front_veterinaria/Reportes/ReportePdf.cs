using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace front_veterinaria.Reportes;

// Genera un PDF con un titulo y una tabla (encabezados + filas de texto)
public static class ReportePdf
{
    public static byte[] Generar(string titulo, string[] columnas, List<string[]> filas)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        return Document.Create(documento =>
        {
            documento.Page(pagina =>
            {
                pagina.Size(PageSizes.A4.Landscape());
                pagina.Margin(30);
                pagina.DefaultTextStyle(estilo => estilo.FontSize(10));
                pagina.Header().Text(titulo).FontSize(18).Bold();

                pagina.Content().PaddingVertical(10).Table(tabla =>
                {
                    tabla.ColumnsDefinition(c =>
                    {
                        foreach (var columna in columnas)
                            c.RelativeColumn();
                    });

                    tabla.Header(encabezado =>
                    {
                        foreach (var columna in columnas)
                            encabezado.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text(columna).Bold();
                    });

                    foreach (var fila in filas)
                        foreach (var valor in fila)
                            tabla.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(4).Text(valor);
                });

                pagina.Footer().AlignCenter().Text(texto =>
                {
                    texto.Span("Página ");
                    texto.CurrentPageNumber();
                });
            });
        }).GeneratePdf();
    }
}
