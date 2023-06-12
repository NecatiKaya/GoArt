namespace GoArt.Applications.MiniWallet.Core.Problem;

public class Problem : ProblemDetails
{
    public int? Status { get; set; }

    public Guid TraceId { get; set; } = Guid.NewGuid();

    public IEnumerable<ProblemDetails> Problems { get; set; } = new List<ProblemDetails>();

    private Problem(string type) : base(type)
    {

    }

    public static Problem Create(string type, int? status = null)
    {
        Problem problem = new Problem(type)
        {
            Status = status
        };

        return problem;
    }

    public override string ToString()
    {
        return Type;
    }
}