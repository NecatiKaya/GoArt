using System.Net;
using GoArt.Applications.MiniWallet.Core.Problem;
using GoArt.Applications.MiniWallet.Domain;
using GoArt.Applications.MiniWallet.Repository;
using MediatR;

namespace GoArt.Applications.MiniWallet.Features.Withdraw;

public class WithdrawRequestHandler : IRequestHandler<WithdrawRequest, WithdrawResponse>
{
    private readonly IWalletRepository _walletRepository;

    private readonly ICurrencyConverter _currencyConverter;

    public WithdrawRequestHandler(IWalletRepository walletRepository, ICurrencyConverter currencyConverter)
    {
        _walletRepository = walletRepository;
        _currencyConverter = currencyConverter;
    }

    public async Task<WithdrawResponse> Handle(WithdrawRequest request, CancellationToken cancellationToken)
    {
        Wallet? wallet = await _walletRepository.GetWalletById(request.WalletToWithdraw);
        if (wallet is null)
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.WALLET_NOT_FOUND, (int)HttpStatusCode.NotFound));
        }

        //Do apply business logic whether end user can withdraw.
        //If yes, save it to database and object.
        //If not dont add withdraw operation to dataase and object
        WalletOperationResponse canDeposit = wallet.CanWithdraw(request.Amount.Currency, request.Amount.Amount, _currencyConverter);
        if (!canDeposit.IsSuccess)
        {
            throw new ProblemException(canDeposit.Problems!.First());
        }

        await _walletRepository.Withdraw(request.WalletToWithdraw, request.Amount);

        wallet.Withdraw(request.Amount.Currency, request.Amount.Amount, _currencyConverter);

        WithdrawResponse response = new WithdrawResponse(wallet);
        return response;
    }
}