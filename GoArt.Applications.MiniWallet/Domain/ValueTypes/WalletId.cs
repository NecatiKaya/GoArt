using GoArt.Applications.MiniWallet.Core.Problem;
using Newtonsoft.Json.Linq;

namespace GoArt.Applications.MiniWallet.Domain.ValueTypes;

public record WalletId
{
    public Guid Value { get; init; }

    private WalletId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.NOT_SUPPORTED_WALLET_ID));
        }

        Value = value;
    }

    /// <summary>
    /// Creates wallet id
    /// </summary>
    /// <returns></returns>
    public static WalletId Create()
    {
        /*
            There might an id generation business maybe for a business or maybe performance for guid. As of simplicy simple guid is generated
         */
        return new WalletId(Guid.NewGuid());
    }

    public static WalletId Create(Guid guid)
    {
        return new WalletId(guid);
    }

    public static WalletId Create(string guid)
    {
        if (string.IsNullOrWhiteSpace(guid))
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.NOT_SUPPORTED_WALLET_ID));
        }

        if (!Guid.TryParse(guid, out Guid parsedGuid))
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.NOT_SUPPORTED_WALLET_ID));
        }

        return Create(parsedGuid);
    }
}