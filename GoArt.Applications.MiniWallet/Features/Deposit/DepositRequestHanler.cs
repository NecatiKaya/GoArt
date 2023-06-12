using System.Net;
using GoArt.Applications.MiniWallet.Core.Problem;
using GoArt.Applications.MiniWallet.Domain;
using GoArt.Applications.MiniWallet.Repository;
using MediatR;

namespace GoArt.Applications.MiniWallet.Features.Deposit;

public class DepositRequestHanler : IRequestHandler<DepositRequest, DepositResponse>
{
    private readonly IWalletRepository _walletRepository;

    public DepositRequestHanler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<DepositResponse> Handle(DepositRequest request, CancellationToken cancellationToken)
    {
        Wallet? wallet = await _walletRepository.GetWalletById(request.WalletToAddDeposit);
        if (wallet is null)
        {
            throw new ProblemException(Problem.Create(MiniWalletErrorCodes.WALLET_NOT_FOUND, (int)HttpStatusCode.NotFound));
        }

        //Do apply business logic whether end user can deposit.
        //If yes, save it to database and object.
        //If not dont add deposit operation to dataase and object
        WalletOperationResponse canDeposit = wallet.CanDeposit(request.Amount.Currency, request.Amount.Amount);
        if (!canDeposit.IsSuccess)
        {
            throw new ProblemException(canDeposit.Problems!.First());
        }

        await _walletRepository.Deposit(request.WalletToAddDeposit, request.Amount);

        wallet.Deposit(request.Amount.Currency, request.Amount.Amount);

        DepositResponse response = new DepositResponse(wallet);
        return response;
    }
}