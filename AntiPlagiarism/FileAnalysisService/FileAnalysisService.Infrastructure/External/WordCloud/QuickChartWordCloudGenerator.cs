using System.Text;
using FileAnalysisService.UseCases.Analysis.Common;

namespace FileAnalysisService.Infrastructure.External.WordCloud;

public class QuickChartWordCloudGenerator : IWordCloudGenerator
{
    private readonly HttpClient _http;

    public QuickChartWordCloudGenerator(HttpClient http)
    {
        _http = http;
    }

    public async Task<string?> GenerateAsync(Stream content, CancellationToken token)
    {
        content.Position = 0;

        using var reader = new StreamReader(content, Encoding.UTF8, leaveOpen: true);
        var text = await reader.ReadToEndAsync(token);

        if (string.IsNullOrWhiteSpace(text))
            return null;
        
        if (text.Length > 4000)
            text = text[..4000];

        var encoded = Uri.EscapeDataString(text);

        var relative = $"/wordcloud?text={encoded}";

        var res = await _http.GetAsync(relative, token);

        if (!res.IsSuccessStatusCode)
            return null;

        return new Uri(_http.BaseAddress!, relative).ToString();
    }
}