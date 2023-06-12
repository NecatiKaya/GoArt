using GoArt.Applications.MiniWallet.Domain;
using GoArt.Applications.MiniWallet.Domain.ValueTypes;
using GoArt.Applications.MiniWallet.Repository;
using MediatR;

namespace GoArt.Applications.MiniWallet.Features.AddWallet;

public class AddWalletRequestHandler : IRequestHandler<AddWalletRequest, AddWalletResponse>
{
    private readonly IWalletRepository _walletRepository;

    private readonly ICurrencyConverter _currencyConverter;

    public AddWalletRequestHandler(IWalletRepository walletRepository, ICurrencyConverter currencyConverter)
    {
        _walletRepository = walletRepository;
        _currencyConverter = currencyConverter;
    }

    public async Task<AddWalletResponse> Handle(AddWalletRequest request, CancellationToken cancellationToken)
    {
        //When wallet instance is created, according to business rules (DDD) it is actullay validated. If object has errors it will not be created and the code will not continue
        Wallet wallet = Wallet.CreateWallet(WalletId.Create().Value, request.WalletName, new MoneyTransactionCollection());

        await _walletRepository.CreateWallet(wallet.Id, wallet.WalletName);

        AddWalletResponse response = new AddWalletResponse(wallet.Id, wallet.WalletName);
        return response;
    }
}