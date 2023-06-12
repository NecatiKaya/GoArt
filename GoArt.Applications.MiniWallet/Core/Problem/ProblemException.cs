namespace GoArt.Applications.MiniWallet.Core.Problem;

public class ProblemException : Exception
{
    public Problem Problem { get; init; }

    public string? ErrorCode { get; set; }

    public ProblemException(Problem problem, string? errorCode = null) : base(errorCode)
    {
        Problem = problem;
        ErrorCode = errorCode;
    }
}