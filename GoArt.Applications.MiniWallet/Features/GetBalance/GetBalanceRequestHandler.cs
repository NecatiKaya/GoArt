using System.Net;
using GoArt.Applications.MiniWallet.Core.Problem;
using GoArt.Applications.MiniWallet.Domain;
using GoArt.Applications.MiniWallet.Domain.ValueTypes;
using GoArt.Applications.MiniWallet.Repository;
using MediatR;

namespace GoArt.Applications.MiniWallet.Features.GetBalance;

public class GetBalanceRequestHandler : IRequestHandler<GetBalanceRequest, GetBalanceResponse>
{
    private readonly IWalletRepository _walletRepository;

    private readonly ICurrencyConverter _currencyConverter;

    public GetBalanceRequestHandler(IWalletRepository walletRepository, ICurrencyConverter currencyConverter)
    {
        _walletRepository = walletRepository;
        _currencyConverter = currencyConverter;
    }

    public async Task<GetBalanceResponse> Handle(GetBalanceRequest request, CancellationToken cancellationToken)
    {
        Wallet? wallet = await _walletRepository.GetWalletById(request.WalletId);
        if (wallet is null)
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.WALLET_NOT_FOUND, (int)HttpStatusCode.NotFound));
        }

        MoneyAmountWithCurrency balanceAmount = wallet.Balance(request.Currency, _currencyConverter);
        return new GetBalanceResponse(balanceAmount);
    }
}