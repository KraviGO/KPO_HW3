using Markdig;
using NPOI.XWPF.UserModel;
using UglyToad.PdfPig;
using System.Text;

namespace FileAnalysisService.Infrastructure.TextExtraction;

public class TextExtractor : ITextExtractor
{
    public async Task<string> ExtractAsync(string fileName, Stream content, CancellationToken token)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        content.Position = 0;

        return ext switch
        {
            ".txt"  => await ExtractTxt(content),
            ".md"   => await ExtractMarkdown(content),
            ".docx" => ExtractDocx(content),
            ".pdf"  => ExtractPdf(content),
            
            _ => string.Empty
        };
    }

    private async Task<string> ExtractTxt(Stream stream)
    {
        using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
        return await reader.ReadToEndAsync();
    }

    private async Task<string> ExtractMarkdown(Stream stream)
    {
        using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
        var md = await reader.ReadToEndAsync();

        return Markdown.ToPlainText(md);
    }

    private string ExtractDocx(Stream stream)
    {
        try
        {
            var doc = new XWPFDocument(stream);

            return string.Join("\n", doc.Paragraphs.Select(p => p.ParagraphText));
        }
        catch
        {
            return string.Empty;
        }
    }

    private string ExtractPdf(Stream stream)
    {
        try
        {
            var builder = new StringBuilder();
            using var pdf = PdfDocument.Open(stream);

            foreach (var page in pdf.GetPages())
                builder.AppendLine(page.Text);

            return builder.ToString();
        }
        catch
        {
            return string.Empty;
        }
    }
}