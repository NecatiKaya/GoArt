namespace GoArt.Applications.MiniWallet.Core.Problem;

public class ProblemDetails
{
    public string Type { get; init; }

    public string? Title { get; set; }

    public string? Detail { get; set; }

    public string? Instance { get; set; }

    public ProblemDetails(string type)
    {
        Type = type;
    }
}