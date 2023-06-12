using GoArt.Applications.MiniWallet.Domain;
using GoArt.Applications.MiniWallet.Repository;
using MediatR;

namespace GoArt.Applications.MiniWallet.Features.GetWalletById;

public class GetWalletByIdRequestHandler : IRequestHandler<GetWalletByIdRequest, GetWalletByIdResponse?>
{
    private readonly IWalletRepository _walletRepository;

    public GetWalletByIdRequestHandler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<GetWalletByIdResponse?> Handle(GetWalletByIdRequest request, CancellationToken cancellationToken)
    {
        Wallet? wallet = await _walletRepository.GetWalletById(request.Id);

        if (wallet is not null)
        {
            GetWalletByIdResponse response = new GetWalletByIdResponse(wallet);
            return response;
        }

        return null;
    }
}