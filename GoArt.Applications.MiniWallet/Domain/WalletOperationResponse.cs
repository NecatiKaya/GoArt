using GoArt.Applications.MiniWallet.Core.Problem;

namespace GoArt.Applications.MiniWallet.Domain;

public class WalletOperationResponse
{
    public Problem[]? Problems { get; set; }

    public bool IsSuccess { get; set; }

    public static WalletOperationResponse Success()
    {
        return new WalletOperationResponse() { IsSuccess = true };
    }

    public static WalletOperationResponse Fail(Problem problem)
    {
        return new WalletOperationResponse()
        {
            Problems = new Problem[] { problem }
        };
    }

    public static WalletOperationResponse Fail(Problem[] problems)
    {
        return new WalletOperationResponse()
        {
            Problems = (Problem[])problems.Clone()
        };
    }
}