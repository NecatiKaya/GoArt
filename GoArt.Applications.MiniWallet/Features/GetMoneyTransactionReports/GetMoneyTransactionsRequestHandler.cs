using GoArt.Applications.MiniWallet.Core.Problem;
using System.Net;
using GoArt.Applications.MiniWallet.Domain;
using GoArt.Applications.MiniWallet.Domain.ValueTypes;
using GoArt.Applications.MiniWallet.Features.GetBalance;
using GoArt.Applications.MiniWallet.Repository;
using MediatR;

namespace GoArt.Applications.MiniWallet.Features.GetMoneyTransactionReports;

public class GetMoneyTransactionsRequestHandler : IRequestHandler<GetMoneyTransactionsRequest, GetMoneyTransactionsReponse>
{
    private readonly IWalletRepository _walletRepository;

    private readonly ICurrencyConverter _currencyConverter;

    public GetMoneyTransactionsRequestHandler(IWalletRepository walletRepository, ICurrencyConverter currencyConverter)
    {
        _walletRepository = walletRepository;
        _currencyConverter = currencyConverter;
    }

    public async Task<GetMoneyTransactionsReponse> Handle(GetMoneyTransactionsRequest request, CancellationToken cancellationToken)
    {
        Wallet? wallet = await _walletRepository.GetWalletById(request.WalletId);
        if (wallet is null)
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.WALLET_NOT_FOUND, (int)HttpStatusCode.NotFound));
        }

        GetMoneyTransactionsReponse reponse = new GetMoneyTransactionsReponse(wallet.Transactions().AsReadOnly());
        return reponse;
    }
}